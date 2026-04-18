using System;

namespace EmployeeManagementSystem.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string message) : base(message)
        {
        }
        
        public override string Message => "Employee Not Found: " + base.Message;
    }
}