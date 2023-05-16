# Simple Logger
*A colored custom logger that keeps traces of your Debug.Log...*

## Description:
This is just a simple add-on I made for me to have colored logs... I also added a way to get a trace of every log even if you relaunch play mode (every log is stored in a text file named with the creation date).
If you have a lot of text files in your *Logs* folder you can delete them all by pressing a button.

### There is 5 level of log: 
1. Debug (Black)
2. Info (Cyan)
3. Warning (Yellow)
4. Error (Red)
5. Valid (Green)

And that's about it :)

## How does it work ?
1. Click on "Tools → Zartox → Log → Generate new config file"
2. If you want to generate text files with trace, just set "LogToFile" to True

## How to make colorful Debug.Log ?
Just call *Print(string message, LogLevel)*... The default log level is debug.

```
        ZartoxLog.Print("test debug", Enums.LogLevel.Debug); //BLACK

        ZartoxLog.Print("test info", Enums.LogLevel.Info); //CYAN

        ZartoxLog.Print("test warn", Enums.LogLevel.Warning); //YELLOW

        ZartoxLog.Print("test error", Enums.LogLevel.Error); //RED

        ZartoxLog.Print("test valid", Enums.LogLevel.Valid); //GREEN
```

## Menu:
In Tools → Zartox → Log you have:

1. *Delete today's log*: Delete every file in Logs folder made today
2. *Delete every log*: Delete every file in *Logs* folder
3. *Delete config file*: Delete the config file in *Config* folder
4. *Generate new config file*: Generate a new configuration file with every option inside in *Config* folder
5. *Reload config*: Load the config file in the script **(not necessary since it's loaded every time you enter play mode)**




