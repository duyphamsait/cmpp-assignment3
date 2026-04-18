using System;

namespace EmployeeManagementSystem.Models
{
    public class FullTimeEmployee : Employee
    {
        private int weeklyHrsTarget = 40;
        private string payType = "salary";
        private string employmentType = "permanent";
        
        public FullTimeEmployee(int employeeId, string firstName, string lastName,
                               DateTime dob, string currentPosition, Department dept)
            : base(employeeId, firstName, lastName, dob, currentPosition, dept)
        {
        }
        
        public override void ReportToManager()
        {
            Console.WriteLine($"[FULL-TIME] {FirstName} {LastName} is reporting to manager. Weekly target: {weeklyHrsTarget} hours");
        }
        
        public int WeeklyHrsTarget => weeklyHrsTarget;
        public string PayType => payType;
        public string EmploymentType => employmentType;
    }
}