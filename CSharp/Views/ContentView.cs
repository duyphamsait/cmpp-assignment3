using System;
using System.Collections.Generic;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Views
{
    public class ContentView
    {
        public void ShowHeader(string title)
        {
            Console.WriteLine($"\n=== {title} ===");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowEmployee(Employee employee)
        {
            Console.WriteLine(employee);
        }

        public void ShowEmployees(IEnumerable<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                ShowEmployee(employee);
            }
        }

        public void ShowEmployeesByDepartment(IEnumerable<Employee> employees, Department department)
        {
            foreach (Employee employee in employees)
            {
                if (employee.Dept == department)
                {
                    ShowEmployee(employee);
                }
            }
        }
    }
}
