using System;

namespace EmployeeManagementSystem.Models
{
    public class PartTimeEmployee : Employee
    {
        private int weeklyHrsTarget = 20;
        private string payType = "hourly";
        private double wage;
        private string employmentType = "temporary";
        
        public PartTimeEmployee(int employeeId, string firstName, string lastName,
                               DateTime dob, string currentPosition, Department dept, double wage)
            : base(employeeId, firstName, lastName, dob, currentPosition, dept)
        {
            if (wage <= 0) throw new ArgumentException("Wage must be positive");
            this.wage = wage;
        }
        
        // Overrides the ReportToManager method from Employee.
        // Polymorphism and dynamic binding
        public override void ReportToManager()
        {
            Console.WriteLine($"[PART-TIME] {FirstName} {LastName} is reporting to manager. Hourly wage: ${wage}");
        }
        
        // Read-only properties: Encapsulation.
        public double Wage => wage;
        public int WeeklyHrsTarget => weeklyHrsTarget;
        public string PayType => payType;
        public string EmploymentType => employmentType;
    }
}