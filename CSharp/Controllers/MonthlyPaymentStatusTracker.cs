using System;
using System.Collections.Generic;
using System.Threading;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Utils;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem.Controllers
{
    /// Controller class that stores employees, filters them, and coordinates file I/O.
    public class MonthlyPaymentStatusTracker
    {
        private List<Employee> listOfEmployees;
        private ContentView view;

        public MonthlyPaymentStatusTracker(ContentView view)
        {
            this.listOfEmployees = new List<Employee>();
            this.view = view;
        }

        // Adds an employee object to the tracker.
        // Demonstrates method overloading because 
        public void AddToTracker(Employee employee)
        {
            if (employee == null) throw new ArgumentException("Employee cannot be null");
            listOfEmployees.Add(employee);
            view.ShowMessage($"Added: {employee.FirstName} {employee.LastName}");
        }

        public void AddToTracker(int employeeId)
        {
            view.ShowMessage($"Searching for employee with ID: {employeeId}");
            throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found");
        }

        public void ShowTracker()
        {
            view.ShowHeader("ALL EMPLOYEES");
            view.ShowEmployees(listOfEmployees);
        }

        /// Overloaded method that displays only employees from a selected department
        /// Demonstrates static binding through method overloading
        public void ShowTracker(Department dept)
        {
            view.ShowHeader($"EMPLOYEES IN DEPARTMENT: {dept}");
            view.ShowEmployeesByDepartment(listOfEmployees, dept);
        }

        public bool SaveTracker()
        {
            if (listOfEmployees.Count == 0)
            {
                view.ShowError("No employees to save");
                return false;
            }

            // FileHandler performs the actual file writing task
            FileHandler fileHandler = new FileHandler("employees_data.txt", listOfEmployees, false, view);

            // A separate thread is created to run the file-writing operation.
            Thread writerThread = new Thread(fileHandler.Run);
            writerThread.Start();
            
            // Waits for the thread to finish before continuing.
            writerThread.Join();
            return true;
        }

        /// Loads employee data from a file.
        /// Demonstrates multithreading because file reading is performed in a separate thread.
        public bool LoadTracker()
        {
            FileHandler fileHandler = new FileHandler("employees_data.txt", listOfEmployees, true, view);
            
            // A separate thread is created to run the file-reading operation.
            Thread readerThread = new Thread(fileHandler.Run);
            readerThread.Start();

            // Waits for the thread to finish before continuing.
            readerThread.Join();
            return true;
        }

        // Read-only property demonstrates encapsulation.
        public List<Employee> ListOfEmployees => listOfEmployees;
    }
}
