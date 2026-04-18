package exception;

public class EmployeeNotFoundException extends Exception {
    public EmployeeNotFoundException(String message) {
        super(message);
    }
    
    @Override
    public String getMessage() {
        return "Employee Not Found: " + super.getMessage();
    }
}