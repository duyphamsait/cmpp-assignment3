using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem.Utils
{
    // FileHandler demonstrates multithreading because Run() is executed by a separate Thread.
    public class FileHandler
    {
        private string filename;
        private List<Employee> employees;
        private bool isReading;
        private ContentView view;

        public FileHandler(string filename, List<Employee> employees, bool isReading, ContentView view)
        {
            this.filename = filename;
            this.employees = employees;
            this.isReading = isReading;
            this.view = view;
        }

        public void Run()
        {
            if (isReading)
                ReadFromFile();
            else
                WriteToFile();
        }

        // Reads employee data from the file.
        // Demonstrates exception handling using try-catch blocks.
        private void ReadFromFile()
        {
            view.ShowMessage($"[THREAD-{Thread.CurrentThread.ManagedThreadId}] Reading from file: {filename}");
            try
            {
                if (File.Exists(filename))
                {
                    string[] lines = File.ReadAllLines(filename);
                    foreach (string line in lines)
                    {
                        view.ShowMessage($"Read: {line}");
                    }
                    view.ShowMessage("[THREAD] File reading completed successfully");
                }
                else
                {
                    view.ShowMessage("[THREAD] File does not exist yet");
                }
            }
            catch (IOException e)
            {
                // Handles file-related errors during writing.
                view.ShowError($"IO Error while reading: {e.Message}");
            }
        }

        private void WriteToFile()
        {
            view.ShowMessage($"[THREAD-{Thread.CurrentThread.ManagedThreadId}] Writing to file: {filename}");
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (Employee emp in employees)
                    {
                        writer.WriteLine(emp.ToString());
                    }
                }
                view.ShowMessage($"[THREAD] File writing completed successfully. {employees.Count} records saved.");
            }
            catch (IOException e)
            {
                view.ShowError($"IO Error while writing: {e.Message}");
            }
        }
    }
}
