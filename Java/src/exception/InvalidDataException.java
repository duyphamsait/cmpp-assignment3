package exception;

public class InvalidDataException extends Exception {
    public InvalidDataException(String message) {
        super(message);
    }
    
    @Override
    public String getMessage() {
        return "Invalid Data: " + super.getMessage();
    }
}