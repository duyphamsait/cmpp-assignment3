using System;

namespace EmployeeManagementSystem.Exceptions
{
    public class EmployeeIneligibleException : Exception
    {
        public EmployeeIneligibleException(string message) : base(message)
        {
        }
        
        public override string Message => "Employee Ineligible: " + base.Message;
    }
}