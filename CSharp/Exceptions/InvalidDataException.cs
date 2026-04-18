using System;

namespace EmployeeManagementSystem.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message)
        {
        }
        
        public override string Message => "Invalid Data: " + base.Message;
    }
}