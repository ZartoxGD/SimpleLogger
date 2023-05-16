using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

using Enums;

public static class ZartoxLog
{

    private static bool logToFile = false;

    static ZartoxLog()
    {
        LoadConfigFile();
    }

    public static void Init()
    {
        Print("Zartox Log Initialised...", LogLevel.Valid);
    }

    public static void Print(string message, LogLevel level=LogLevel.Debug)
    {
        string colorCode = "";
        string formattedMessage = "";

        switch (level)
        {
            case LogLevel.Debug:
                colorCode = "<color=#808080ff>"; // Black
                formattedMessage = $"{colorCode}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage);
                break;
            case LogLevel.Info:
                colorCode = "<color=#00ffffff>"; // White
                formattedMessage = $"{colorCode}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage);
                break;
            case LogLevel.Warning:
                colorCode = "<color=#ffff00ff>"; // Yellow
                formattedMessage = $"{colorCode}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.LogWarning(formattedMessage);
                break;
            case LogLevel.Error:
                colorCode = "<color=#ff0000ff>"; // Red
                formattedMessage = $"{colorCode}[{level.ToString().ToUpper()}] {message}</color>";

                if(logToFile)
                    FileLogger(message, level);

                Debug.LogError(formattedMessage);
                break;
            case LogLevel.Valid:
                colorCode = "<color=#00ff00ff>"; // Green
                formattedMessage = $"{colorCode}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage);
                break;
        }

        if(level != LogLevel.Error && logToFile)
            FileLogger(message, level);
    }

    private static void FileLogger(string message, LogLevel level=LogLevel.Debug)
    {
        string directoryPath = Application.dataPath + "/Logs";

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_ConsoleLogs.txt";
        string filePath = directoryPath + "/" + fileName;

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            writer.WriteLine($"[{timestamp}] [{level.ToString().ToUpper()}] : {message}");
        }
    }

    #region CommandsMenu

    [MenuItem("Tools/Zartox/Log/Delete today's Logs")]
    public static void DeleteTodaysLogs()
    {
        string logsDir = Application.dataPath + "/Logs";
        string[] logFiles = Directory.GetFiles(logsDir);
        int counter = 0;

        foreach (string logFile in logFiles)
        {
            FileInfo fileInfo = new FileInfo(logFile);

            if (fileInfo.CreationTime.Date == DateTime.Today)
            {
                File.Delete(logFile);
                counter++;
            }
        }

        AssetDatabase.Refresh();
        Print($"[Zartox Log] : Deleted today's logs ({counter / 2} files) in /Logs...", LogLevel.Warning);
    }
    
    [MenuItem("Tools/Zartox/Log/Delete every Logs")]
    public static void DeleteEveryLogs()
    {
        string[] filePaths = Directory.GetFiles(Application.dataPath + "/Logs");
        int counter = 0;

        foreach (string filePath in filePaths)
        {
            File.Delete(filePath);
            counter++;
        }

        AssetDatabase.Refresh();
        Print($"[Zartox Log] : Deleted every logs ({counter / 2} files)...", LogLevel.Warning);
    }

    [MenuItem("Tools/Zartox/Log/Delete config file")]
    public static void DeleteConfigFile()
    {
        string filePath = Application.dataPath + "/Config/Zartox_log_config.txt";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            AssetDatabase.Refresh();
            Print("[Zartox Log] : Config file deleted...", LogLevel.Valid);
        }
        else
        {
            Print("[Zartox Log] : There is no config file to delete...", LogLevel.Warning);
        }
            
    }

    [MenuItem("Tools/Zartox/Log/Generate new config file")]
    public static void GenerateConfigFile()
    {
        string configDir = Application.dataPath + "/Config";
        string configFile = configDir + "/Zartox_log_config.txt";

        if (!Directory.Exists(configDir))
            Directory.CreateDirectory(configDir);

        using (StreamWriter sw = File.CreateText(configFile))
        {
            sw.WriteLine("LogToFile=True");
        }

        AssetDatabase.Refresh();
        Print("[Zartox Log] : Generated a fresh config file with defaults values...", LogLevel.Valid);
    }

    [MenuItem("Tools/Zartox/Log/Reload config")]
    public static void LoadConfigFile()
    {
        string configFilePath = Application.dataPath + "/Config/Zartox_log_config.txt";

        if (File.Exists(configFilePath))
        {
            Dictionary<string, string> configValues = new Dictionary<string, string>();
            string[] configFileLines = File.ReadAllLines(configFilePath);

            foreach (string line in configFileLines)
            {
                string[] keyValue = line.Split('=');

                if (keyValue.Length == 2)
                {
                    configValues[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            if (configValues.ContainsKey("LogToFile"))
            {
                //bool.TryParse(configValues["logToFile"], out logToFile);
                logToFile = bool.Parse(configValues["LogToFile"]);
            }

            Print("[Zartox Log] : Configuration reloaded !", LogLevel.Valid);
            Print("[Zartox Log] : Active config: ", LogLevel.Info);
            Print($"[Zartox Log] : - logToFile = {logToFile.ToString()}", LogLevel.Info);
        }
        else
        {
            Print("[Zartox Log] : Config not found... Please generate config file first...", LogLevel.Error);
        }
    }

    #endregion

}
