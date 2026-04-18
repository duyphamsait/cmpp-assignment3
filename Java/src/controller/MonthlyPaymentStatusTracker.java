package controller;

import model.Employee;
import model.Department;
import exception.EmployeeNotFoundException;
import utils.FileHandler;
import view.ContentView;
import java.util.ArrayList;

/**
 * MonthlyPaymentStatusTracker acts like the controller.
 * It coordinates employee storage, filtering, and file persistence.
 */
public class MonthlyPaymentStatusTracker {
    private ArrayList<Employee> listOfEmployees;
    private ContentView view;

    public MonthlyPaymentStatusTracker(ContentView view) {
        this.listOfEmployees = new ArrayList<>();
        this.view = view;
    }

    public void addToTracker(Employee employee) {
        if (employee == null) throw new IllegalArgumentException("Employee cannot be null");
        listOfEmployees.add(employee);
        view.showMessage("Added: " + employee.getFirstName() + " " + employee.getLastName());
    }

    public void addToTracker(int employeeId) throws EmployeeNotFoundException {
        view.showMessage("Searching for employee with ID: " + employeeId);
        throw new EmployeeNotFoundException("Employee with ID " + employeeId + " not found");
    }

    public void showTracker() {
        view.showHeader("ALL EMPLOYEES");
        view.showEmployees(listOfEmployees);
    }

    public void showTracker(Department dept) {
        view.showHeader("EMPLOYEES IN DEPARTMENT: " + dept);
        view.showEmployeesByDepartment(listOfEmployees, dept);
    }

    public boolean saveTracker() {
        if (listOfEmployees.isEmpty()) {
            view.showError("No employees to save");
            return false;
        }

        FileHandler fileHandler = new FileHandler("employees_data.txt", listOfEmployees, false, view);
        Thread writerThread = new Thread(fileHandler);
        writerThread.start();
        try {
            writerThread.join();
            return true;
        } catch (InterruptedException e) {
            view.showError("Thread interrupted: " + e.getMessage());
            Thread.currentThread().interrupt();
            return false;
        }
    }

    public boolean loadTracker() {
        FileHandler fileHandler = new FileHandler("employees_data.txt", listOfEmployees, true, view);
        Thread readerThread = new Thread(fileHandler);
        readerThread.start();
        try {
            readerThread.join();
            return true;
        } catch (InterruptedException e) {
            view.showError("Thread interrupted: " + e.getMessage());
            Thread.currentThread().interrupt();
            return false;
        }
    }

    public ArrayList<Employee> getListOfEmployees() {
        return listOfEmployees;
    }
}
