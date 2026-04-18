package model;

import java.util.Date;

public class PartTimeEmployee extends Employee {
    private int weeklyHrsTarget = 20;
    private String payType = "hourly";
    private double wage;
    private String employmentType = "temporary";
    
    public PartTimeEmployee(int employeeId, String firstName, String lastName,
                            Date dob, String currentPosition, Department dept, double wage) {
        super(employeeId, firstName, lastName, dob, currentPosition, dept);
        if (wage <= 0) throw new IllegalArgumentException("Wage must be positive");
        this.wage = wage;
    }
    
    @Override
    public void reportToManager() {
        System.out.println("[PART-TIME] " + getFirstName() + " " + getLastName() + 
                          " is reporting to manager. Hourly wage: $" + wage);
    }
    
    public double getWage() { return wage; }
    public int getWeeklyHrsTarget() { return weeklyHrsTarget; }
    public String getPayType() { return payType; }
    public String getEmploymentType() { return employmentType; }
}