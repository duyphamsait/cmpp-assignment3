package exception;

public class EmployeeIneligibleException extends Exception {
    public EmployeeIneligibleException(String message) {
        super(message);
    }
    
    @Override
    public String getMessage() {
        return "Employee Ineligible: " + super.getMessage();
    }
}