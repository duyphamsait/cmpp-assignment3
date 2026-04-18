using System;
using System.Security;

namespace EmployeeManagementSystem.Models
{
    // Employee is the abstract parent class for every employee type.
    public abstract class Employee
    {
        private int employeeId;
        private string firstName;
        private string lastName;
        private DateTime dob;
        private string currentPosition;
        private Department dept;
        private bool paid = false;

        protected Employee(int employeeId, string firstName, string lastName,
                          DateTime dob, string currentPosition, Department dept)
        {
            ValidateCommonData(employeeId, firstName, lastName, dob, currentPosition, dept);

            this.employeeId = employeeId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dob = dob;
            this.currentPosition = currentPosition;
            this.dept = dept;
        }

        private void ValidateCommonData(int employeeId, string firstName, string lastName,
                                        DateTime dob, string currentPosition, Department dept)
        {
            if (employeeId <= 0) throw new ArgumentException("Employee ID must be positive");
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be empty");
            if (string.IsNullOrWhiteSpace(currentPosition)) throw new ArgumentException("Current position cannot be empty");
            if (!Enum.IsDefined(typeof(Department), dept)) throw new ArgumentException("Department is invalid");
        }

        // Encapsulation: properties expose values safely while fields remain private.
        public int EmployeeId => employeeId;
        public string FirstName => firstName;
        public string LastName => lastName;
        public DateTime Dob => dob;
        public string CurrentPosition => currentPosition;
        public Department Dept => dept;
        public bool Paid { get => paid; set => paid = value; }

        // Dynamic binding target: subclasses override this method.
        public abstract void ReportToManager();

        // Static binding example 1 for salaried employees.
        public virtual double GetPaid(double amount, int id)
        {
            if (this.employeeId != id) throw new SecurityException("Employee ID mismatch");
            if (amount <= 0) throw new ArgumentException("Payment amount must be positive");
            this.paid = true;
            Console.WriteLine($"Employee {firstName} {lastName} received payment: ${amount}");
            return amount;
        }

        // Static binding example 2 for hourly employees.
        public virtual double GetPaid(int weeklyHrsTarget, double wage, int id)
        {
            if (this.employeeId != id) throw new SecurityException("Employee ID mismatch");
            if (weeklyHrsTarget <= 0 || wage <= 0) throw new ArgumentException("Hours and wage must be positive");
            double amount = weeklyHrsTarget * wage * 4;
            this.paid = true;
            Console.WriteLine($"Employee {firstName} {lastName} received hourly payment: ${amount}");
            return amount;
        }

        public bool SuccessfullyPaid(int id)
        {
            if (this.employeeId != id) throw new SecurityException("Employee ID mismatch");
            return this.paid;
        }

        public override string ToString()
        {
            return $"ID: {employeeId} | Name: {firstName} {lastName} | DOB: {dob:yyyy-MM-dd} | Position: {currentPosition} | Dept: {dept} | Paid: {paid}";
        }
    }
}
