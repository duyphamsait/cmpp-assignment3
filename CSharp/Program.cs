using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the view object used to display output to the console.

            ContentView view = new ContentView();
            view.ShowHeader("SAIT EMPLOYEE MANAGEMENT SYSTEM (C#)");

            try
            {
                // Test data used to show all required employee categories.
                DateTime dob1 = new DateTime(1985, 3, 15);
                DateTime dob2 = new DateTime(1990, 7, 22);
                DateTime dob3 = new DateTime(1995, 11, 10);
                DateTime dob4 = new DateTime(1980, 1, 5);
                DateTime dob5 = new DateTime(1992, 9, 18);

                // Abstraction + inheritance:
                // Employee is abstract and these subclasses provide concrete behavior.
                Employee emp1 = new FullTimeEmployee(101, "John", "Smith", dob1, "Senior Instructor", Department.STAFF);
                Employee emp2 = new PartTimeEmployee(102, "Sarah", "Johnson", dob2, "Junior Instructor", Department.STAFF, 35.50);
                Employee emp3 = new FullTimeEmployee(103, "Mike", "Williams", dob3, "IT Specialist", Department.IT);
                Employee emp4 = new PartTimeEmployee(104, "Emily", "Brown", dob4, "Admin Assistant", Department.ADMINISTRATION, 28.75);
                Employee manager = new Manager(105, "David", "Wilson", dob5, "Department Manager", Department.MANAGEMENT);

                MonthlyPaymentStatusTracker tracker = new MonthlyPaymentStatusTracker(view);

                tracker.AddToTracker(emp1);
                tracker.AddToTracker(emp2);
                tracker.AddToTracker(emp3);
                tracker.AddToTracker(emp4);
                tracker.AddToTracker(manager);

                view.ShowHeader("DEMONSTRATING POLYMORPHISM & DYNAMIC BINDING");
                // Dynamic binding: overridden ReportToManager() is resolved at runtime.
                foreach (Employee emp in new[] { emp1, emp2, emp3, emp4, manager })
                {
                    emp.ReportToManager();
                }

                view.ShowHeader("DEMONSTRATING STATIC BINDING (OVERLOADED METHODS)");
                // Static binding via overloaded GetPaid methods.
                emp1.GetPaid(5000.0, 101);
                emp2.GetPaid(20, 35.50, 102);

                view.ShowHeader("MANAGER FUNCTIONALITY");
                Manager mgr = (Manager)manager;
                mgr.AddEmployee(emp1);
                mgr.AddEmployee(emp2);
                mgr.RespondToEmployee(emp1);
                mgr.ReportToManager();

                view.ShowHeader("TRACKER FUNCTIONALITY");
                tracker.ShowTracker();
                tracker.ShowTracker(Department.STAFF);

                view.ShowHeader("EXCEPTION HANDLING DEMONSTRATION");
                try
                {
                    // This object uses invalid data and should trigger an exception
                    Employee invalidEmp = new FullTimeEmployee(-1, "", "", DateTime.Now, "Invalid", Department.IT);
                    view.ShowEmployee(invalidEmp);
                }
                catch (ArgumentException e)
                {
                    // Catches invalid input data errors
                    view.ShowError($"Caught Exception: {e.Message}");
                }

                try
                {
                    tracker.AddToTracker(999);
                }
                catch (EmployeeNotFoundException e)
                {
                    view.ShowError($"Caught Exception: {e.Message}");
                }

                view.ShowHeader("MULTITHREADING DEMONSTRATION");
                // FileHandler is executed on separate threads for read/write operations.
                bool saved = tracker.SaveTracker();
                if (saved)
                {
                    view.ShowMessage("Data saved successfully using separate thread!");
                }

                view.ShowHeader("LOADING DATA FROM FILE");
                tracker.LoadTracker();

                view.ShowHeader("PROCESSING PAYMENTS");
                foreach (Employee emp in tracker.ListOfEmployees)
                {
                    if (emp is FullTimeEmployee)
                        emp.GetPaid(5200.0, emp.EmployeeId);
                    else if (emp is PartTimeEmployee pte)
                        emp.GetPaid(pte.WeeklyHrsTarget, pte.Wage, emp.EmployeeId);
                    else if (emp is Manager)
                        emp.GetPaid(6800.0, emp.EmployeeId);
                }

                view.ShowHeader("PAYMENT STATUS");
                foreach (Employee emp in tracker.ListOfEmployees)
                {
                    view.ShowMessage($"{emp.FirstName} {emp.LastName} - Paid: {emp.SuccessfullyPaid(emp.EmployeeId)}");
                }
            }
            catch (Exception e)
            {
                view.ShowError($"Unexpected error: {e.Message}");
            }

            view.ShowHeader("PROGRAM EXECUTION COMPLETED");
        }
    }
}
