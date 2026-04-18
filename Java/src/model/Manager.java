package model;

import java.util.ArrayList;
import java.util.Date;

public class Manager extends Employee {
    private Department dept;
    private int numEmployeesManaging;
    private String currDeptManaging;
    private ArrayList<Employee> employeesManaging = new ArrayList<>();
    
    public Manager(int employeeId, String firstName, String lastName,
                   Date dob, String currentPosition, Department dept) {
        super(employeeId, firstName, lastName, dob, currentPosition, dept);
        this.dept = dept;
        this.currDeptManaging = dept.toString();
    }
    
    @Override
    public void reportToManager() {
        System.out.println("[MANAGER] " + getFirstName() + " " + getLastName() + 
                          " is reporting to upper management. Managing " + 
                          numEmployeesManaging + " employees in " + currDeptManaging);
    }
    
    public void respondToEmployee(Employee employee) {
        System.out.println("Manager " + getFirstName() + " is responding to " + 
                          employee.getFirstName() + " " + employee.getLastName());
    }
    
    public void addEmployee(Employee employee) {
        employeesManaging.add(employee);
        numEmployeesManaging = employeesManaging.size();
        System.out.println("Employee " + employee.getFirstName() + " added to team of manager " + getFirstName());
    }
    
    public Department getManagedDept() { return dept; }
    public int getNumEmployeesManaging() { return numEmployeesManaging; }
}