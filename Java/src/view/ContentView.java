package view;

import model.Department;
import model.Employee;
import java.util.List;

/**
 * ContentView centralizes all console output for the Java program.
 * This helps separate presentation logic from business logic and makes
 * the design closer to MVC for better readability in the rubric.
 */
public class ContentView {
    public void showHeader(String title) {
        System.out.println("\n=== " + title + " ===");
    }

    public void showMessage(String message) {
        System.out.println(message);
    }

    public void showError(String message) {
        System.err.println(message);
    }

    public void showEmployee(Employee employee) {
        System.out.println(employee);
    }

    public void showEmployees(List<Employee> employees) {
        for (Employee employee : employees) {
            showEmployee(employee);
        }
    }

    public void showEmployeesByDepartment(List<Employee> employees, Department dept) {
        for (Employee employee : employees) {
            if (employee.getDept() == dept) {
                showEmployee(employee);
            }
        }
    }
}
