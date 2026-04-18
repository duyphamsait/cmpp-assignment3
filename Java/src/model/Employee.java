package model;

import java.util.Date;

/**
 * Employee is the abstract base class for all employees in the system.
 * It demonstrates abstraction and encapsulation because subclasses share
 * the same protected behavior while fields remain private.
 */
public abstract class Employee {
    private int employeeId;
    private String firstName;
    private String lastName;
    private Date dob;
    private String currentPosition;
    private Department dept;
    private boolean paid = false;

    public Employee(int employeeId, String firstName, String lastName,
                    Date dob, String currentPosition, Department dept) {
        validateCommonData(employeeId, firstName, lastName, dob, currentPosition, dept);
        this.employeeId = employeeId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dob = dob;
        this.currentPosition = currentPosition;
        this.dept = dept;
    }

    private void validateCommonData(int employeeId, String firstName, String lastName,
                                    Date dob, String currentPosition, Department dept) {
        if (employeeId <= 0) throw new IllegalArgumentException("Employee ID must be positive");
        if (firstName == null || firstName.trim().isEmpty()) throw new IllegalArgumentException("First name cannot be empty");
        if (lastName == null || lastName.trim().isEmpty()) throw new IllegalArgumentException("Last name cannot be empty");
        if (dob == null) throw new IllegalArgumentException("Date of birth cannot be null");
        if (currentPosition == null || currentPosition.trim().isEmpty()) throw new IllegalArgumentException("Current position cannot be empty");
        if (dept == null) throw new IllegalArgumentException("Department cannot be null");
    }

    // Encapsulation: all fields are private and accessed through getters/setters.
    public int getEmployeeId() { return employeeId; }
    public String getFirstName() { return firstName; }
    public String getLastName() { return lastName; }
    public Date getDob() { return dob; }
    public String getCurrentPosition() { return currentPosition; }
    public Department getDept() { return dept; }
    public boolean isPaid() { return paid; }
    public void setPaid(boolean paid) { this.paid = paid; }

    // Dynamic binding target: each subclass provides its own implementation.
    public abstract void reportToManager();

    // Static binding example 1: overloaded method for salaried employees.
    public double getPaid(double amount, int id) {
        if (this.employeeId != id) throw new SecurityException("Employee ID mismatch");
        if (amount <= 0) throw new IllegalArgumentException("Payment amount must be positive");
        this.paid = true;
        System.out.println("Employee " + firstName + " " + lastName + " received payment: $" + amount);
        return amount;
    }

    // Static binding example 2: overloaded method for hourly employees.
    public double getPaid(int weeklyHrsTarget, double wage, int id) {
        if (this.employeeId != id) throw new SecurityException("Employee ID mismatch");
        if (weeklyHrsTarget <= 0 || wage <= 0) throw new IllegalArgumentException("Hours and wage must be positive");
        double amount = weeklyHrsTarget * wage * 4;
        this.paid = true;
        System.out.println("Employee " + firstName + " " + lastName + " received hourly payment: $" + amount);
        return amount;
    }

    public boolean successfullyPaid(int id) {
        if (this.employeeId != id) throw new SecurityException("Employee ID mismatch");
        return this.paid;
    }

    @Override
    public String toString() {
        return String.format("ID: %d | Name: %s %s | DOB: %tF | Position: %s | Dept: %s | Paid: %s",
                employeeId, firstName, lastName, dob, currentPosition, dept, paid);
    }
}
