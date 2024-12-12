using System;
using System.IO;
using System.Windows.Forms;

public class Logger
{
    private readonly string logFilePath;

    public Logger(string filePath = "application.log")
    {
        logFilePath = filePath;
    }

    public void Write(string action, string details, bool isError = false)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {(isError ? "ERROR" : "INFO")} | {action} | {details}";
        try
        {
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не вдалося записати в лог: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
