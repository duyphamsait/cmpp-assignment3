package utils;

import model.Employee;
import view.ContentView;
import java.io.*;
import java.util.ArrayList;

/**
 * FileHandler demonstrates parallel programming because it implements Runnable.
 * A separate thread is used for file reading or writing.
 */
public class FileHandler implements Runnable {
    private String filename;
    private ArrayList<Employee> employees;
    private boolean isReading;
    private ContentView view;

    public FileHandler(String filename, ArrayList<Employee> employees, boolean isReading, ContentView view) {
        this.filename = filename;
        this.employees = employees;
        this.isReading = isReading;
        this.view = view;
    }

    @Override
    public void run() {
        if (isReading) {
            readFromFile();
        } else {
            writeToFile();
        }
    }

    private void readFromFile() {
        view.showMessage("[THREAD-" + Thread.currentThread().getId() + "] Reading from file: " + filename);
        try (BufferedReader reader = new BufferedReader(new FileReader(filename))) {
            String line;
            while ((line = reader.readLine()) != null) {
                view.showMessage("Read: " + line);
            }
            view.showMessage("[THREAD] File reading completed successfully");
        } catch (FileNotFoundException e) {
            view.showError("File not found: " + e.getMessage());
        } catch (IOException e) {
            view.showError("IO Error while reading: " + e.getMessage());
        }
    }

    private void writeToFile() {
        view.showMessage("[THREAD-" + Thread.currentThread().getId() + "] Writing to file: " + filename);
        try (BufferedWriter writer = new BufferedWriter(new FileWriter(filename))) {
            for (Employee emp : employees) {
                writer.write(emp.toString());
                writer.newLine();
            }
            view.showMessage("[THREAD] File writing completed successfully. " + employees.size() + " records saved.");
        } catch (IOException e) {
            view.showError("IO Error while writing: " + e.getMessage());
        }
    }
}
