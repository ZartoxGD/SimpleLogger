using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

using Enums;

public static class ZartoxLog
{

    //TODO: Rajouter un contexte pour les objets

    private static bool logToFile = false;

    //Custom colors
    private static string debugColor = "#808080ff";
    private static string infoColor = "#00ffffff";
    private static string warningColor = "#ffff00ff";
    private static string errorColor = "#ff0000ff";
    private static string validColor = "#00ff00ff";

    static ZartoxLog()
    {
        LoadConfigFile();
    }

    public static void Init()
    {
        Print("Zartox Log Initialised...", LogLevel.Valid);
    }

    private static string GetColorCode(string color)
    {
        return $"<color={color}>";
    }

    public static void Print(string message, GameObject context, LogLevel level=LogLevel.Debug)
    {
        string formattedMessage = "";

        switch (level)
        {
            case LogLevel.Debug:
                formattedMessage = $"{GetColorCode(debugColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage, context);
                break;
            case LogLevel.Info:
                formattedMessage = $"{GetColorCode(infoColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage, context);
                break;
            case LogLevel.Warning:
                formattedMessage = $"{GetColorCode(warningColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.LogWarning(formattedMessage, context);
                break;
            case LogLevel.Error:
                formattedMessage = $"{GetColorCode(errorColor)}[{level.ToString().ToUpper()}] {message}</color>";

                if(logToFile)
                    FileLogger(message, level);

                Debug.LogError(formattedMessage, context);
                break;
            case LogLevel.Valid:
                formattedMessage = $"{GetColorCode(validColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage, context);
                break;
        }

        if(level != LogLevel.Error && logToFile)
            FileLogger(message, level);
    }

    public static void Print(string message, LogLevel level=LogLevel.Debug)
    {
        string formattedMessage = "";

        switch (level)
        {
            case LogLevel.Debug:
                formattedMessage = $"{GetColorCode(debugColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage);
                break;
            case LogLevel.Info:
                formattedMessage = $"{GetColorCode(infoColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.Log(formattedMessage);
                break;
            case LogLevel.Warning:
                formattedMessage = $"{GetColorCode(warningColor)}[{level.ToString().ToUpper()}] {message}</color>";
                Debug.LogWarning(formattedMessage);
                break;
            case LogLevel.Error:
                formattedMessage = $"{GetColorCode(errorColor)}[{level.ToString().ToUpper()}] {message}</color>";

                if(logToFile)
                    FileLogger(message, level);

                Debug.LogError(formattedMessage);
                break;
            case LogLevel.Valid:
                formattedMessage = $"{GetColorCode(validColor)}[{level.ToString().ToUpper()}] {message}</color>";
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
    
    [MenuItem("Tools/Zartox/Log/Delete every Log")]
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

            //Custom colors:
            sw.WriteLine("DebugColor=#808080ff");
            sw.WriteLine("InfoColor=#00ffffff");
            sw.WriteLine("WarningColor=#ffff00ff");
            sw.WriteLine("ErrorColor=#ff0000ff");
            sw.WriteLine("ValidColor=#00ff00ff");
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
                logToFile = bool.Parse(configValues["LogToFile"]);

            if (configValues.ContainsKey("DebugColor"))
                debugColor = configValues["DebugColor"];
            
            if (configValues.ContainsKey("InfoColor"))
                infoColor = configValues["InfoColor"];
            
            if (configValues.ContainsKey("WarningColor"))
                warningColor = configValues["WarningColor"];
            
            if (configValues.ContainsKey("ErrorColor"))
                errorColor = configValues["ErrorColor"];
            
            if (configValues.ContainsKey("ValidColor"))
                validColor = configValues["ValidColor"];

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
