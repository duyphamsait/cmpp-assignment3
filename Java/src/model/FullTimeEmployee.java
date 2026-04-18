package model;

import java.util.Date;

public class FullTimeEmployee extends Employee {
    private int weeklyHrsTarget = 40;
    private String payType = "salary";
    private String employmentType = "permanent";
    
    public FullTimeEmployee(int employeeId, String firstName, String lastName,
                            Date dob, String currentPosition, Department dept) {
        super(employeeId, firstName, lastName, dob, currentPosition, dept);
    }
    
    @Override
    public void reportToManager() {
        System.out.println("[FULL-TIME] " + getFirstName() + " " + getLastName() + 
                          " is reporting to manager. Weekly target: " + weeklyHrsTarget + " hours");
    }
    
    public int getWeeklyHrsTarget() { return weeklyHrsTarget; }
    public String getPayType() { return payType; }
    public String getEmploymentType() { return employmentType; }
}