import model.*;
import controller.MonthlyPaymentStatusTracker;
import exception.*;
import java.util.*;
import java.text.SimpleDateFormat;
import view.ContentView;

/**
 * Main is the Java entry point used to demonstrate all rubric items:
 * inheritance, encapsulation, abstraction, polymorphism, binding,
 * custom exceptions, file persistence, and multithreading.
 */
public class Main {
    public static void main(String[] args) {
        ContentView view = new ContentView();
        view.showHeader("SAIT EMPLOYEE MANAGEMENT SYSTEM");
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");

        try {
            // Test data used to prove that the program can create multiple employee types.
            Date dob1 = sdf.parse("1985-03-15");
            Date dob2 = sdf.parse("1990-07-22");
            Date dob3 = sdf.parse("1995-11-10");
            Date dob4 = sdf.parse("1980-01-05");
            Date dob5 = sdf.parse("1992-09-18");

            // Inheritance + abstraction:
            // Employee is abstract and the concrete objects are FullTimeEmployee,
            // PartTimeEmployee, and Manager.
            Employee emp1 = new FullTimeEmployee(101, "John", "Smith", dob1, "Senior Instructor", Department.STAFF);
            Employee emp2 = new PartTimeEmployee(102, "Sarah", "Johnson", dob2, "Junior Instructor", Department.STAFF, 35.50);
            Employee emp3 = new FullTimeEmployee(103, "Mike", "Williams", dob3, "IT Specialist", Department.IT);
            Employee emp4 = new PartTimeEmployee(104, "Emily", "Brown", dob4, "Admin Assistant", Department.ADMINISTRATION, 28.75);
            Employee manager = new Manager(105, "David", "Wilson", dob5, "Department Manager", Department.MANAGEMENT);

            MonthlyPaymentStatusTracker tracker = new MonthlyPaymentStatusTracker(view);

            tracker.addToTracker(emp1);
            tracker.addToTracker(emp2);
            tracker.addToTracker(emp3);
            tracker.addToTracker(emp4);
            tracker.addToTracker(manager);

            view.showHeader("DEMONSTRATING POLYMORPHISM & DYNAMIC BINDING");
            // Dynamic binding happens here because reportToManager() is overridden,
            // and Java chooses the correct version at runtime based on object type.
            List<Employee> employeeList = Arrays.asList(emp1, emp2, emp3, emp4, manager);
            for (Employee emp : employeeList) {
                emp.reportToManager();
            }

            view.showHeader("DEMONSTRATING STATIC BINDING (OVERLOADED METHODS)");
            // Static binding demonstration through overloaded getPaid methods.
            emp1.getPaid(5000.0, 101);
            emp2.getPaid(20, 35.50, 102);

            view.showHeader("MANAGER FUNCTIONALITY");
            Manager mgr = (Manager) manager;
            mgr.addEmployee(emp1);
            mgr.addEmployee(emp2);
            mgr.respondToEmployee(emp1);
            mgr.reportToManager();

            view.showHeader("TRACKER FUNCTIONALITY");
            tracker.showTracker();
            tracker.showTracker(Department.STAFF);

            view.showHeader("EXCEPTION HANDLING DEMONSTRATION");
            try {
                // Invalid constructor arguments trigger validation exceptions.
                Employee invalidEmp = new FullTimeEmployee(-1, "", "", sdf.parse("2000-01-01"), "Invalid", Department.IT);
                view.showEmployee(invalidEmp);
            } catch (IllegalArgumentException e) {
                view.showError("Caught Exception: " + e.getMessage());
            }

            try {
                tracker.addToTracker(999);
            } catch (EmployeeNotFoundException e) {
                view.showError("Caught Exception: " + e.getMessage());
            }

            view.showHeader("MULTITHREADING DEMONSTRATION");
            // FileHandler implements Runnable, so file I/O is executed in separate threads.
            boolean saved = tracker.saveTracker();
            if (saved) {
                view.showMessage("Data saved successfully using separate thread!");
            }

            view.showHeader("LOADING DATA FROM FILE");
            tracker.loadTracker();

            view.showHeader("PROCESSING PAYMENTS");
            for (Employee emp : tracker.getListOfEmployees()) {
                if (emp instanceof FullTimeEmployee) {
                    emp.getPaid(5200.0, emp.getEmployeeId());
                } else if (emp instanceof PartTimeEmployee) {
                    PartTimeEmployee pte = (PartTimeEmployee) emp;
                    emp.getPaid(pte.getWeeklyHrsTarget(), pte.getWage(), emp.getEmployeeId());
                } else if (emp instanceof Manager) {
                    emp.getPaid(6800.0, emp.getEmployeeId());
                }
            }

            view.showHeader("PAYMENT STATUS");
            for (Employee emp : tracker.getListOfEmployees()) {
                view.showMessage(emp.getFirstName() + " " + emp.getLastName() + " - Paid: " + emp.successfullyPaid(emp.getEmployeeId()));
            }
        } catch (Exception e) {
            view.showError("Unexpected error: " + e.getMessage());
            e.printStackTrace();
        }

        view.showHeader("PROGRAM EXECUTION COMPLETED");
    }
}
