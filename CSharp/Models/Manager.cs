using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Models
{
    public class Manager : Employee
    {
        private Department dept;
        private int numEmployeesManaging;
        private string currDeptManaging;
        
        // Collection of employees supervised by this manager.
        // Demonstrates aggregation/association in UML.
        private List<Employee> employeesManaging = new List<Employee>();
        
        // Calls the Employee constructor using base().
        // Demonstrates constructor chaining and inheritance.
        public Manager(int employeeId, string firstName, string lastName,
                      DateTime dob, string currentPosition, Department dept)
            : base(employeeId, firstName, lastName, dob, currentPosition, dept)
        {
            this.dept = dept;
            this.currDeptManaging = dept.ToString();
        }
        
        // Overrides the Employee ReportToManager method.
        // Demonstrates polymorphism and dynamic binding
        public override void ReportToManager()
        {
            Console.WriteLine($"[MANAGER] {FirstName} {LastName} is reporting to upper management. Managing {numEmployeesManaging} employees in {currDeptManaging}");
        }
        
        public void RespondToEmployee(Employee employee)
        {
            Console.WriteLine($"Manager {FirstName} is responding to {employee.FirstName} {employee.LastName}");
        }
        
        public void AddEmployee(Employee employee)
        {
            employeesManaging.Add(employee);
            numEmployeesManaging = employeesManaging.Count;
            Console.WriteLine($"Employee {employee.FirstName} added to team of manager {FirstName}");
        }
        
        // Read-only properties demonstrate encapsulation
        public Department ManagedDept => dept;
        public int NumEmployeesManaging => numEmployeesManaging;
    }
}