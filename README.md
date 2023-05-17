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
        //The line above is the same as ZartoxLog.Print("test debug"); Since the default LogLevel is Debug

        ZartoxLog.Print("test info", Enums.LogLevel.Info); //CYAN

        ZartoxLog.Print("test warn", Enums.LogLevel.Warning); //YELLOW

        ZartoxLog.Print("test error", Enums.LogLevel.Error); //RED

        ZartoxLog.Print("test valid", Enums.LogLevel.Valid); //GREEN
```

If you want to set the context of your debug do it this way:

```
        //This way if you click on the log in the Unity console it will point to the game object that called the log
        ZartoxLog.Print("CONTEXT", this.gameObject, Enums.LogLevel.Valid);
```

## How to set custom colors ?
Go in the *config* file and change *DebugColor, InfoColor, WarningColor, ErrorColor and ValidColor* to the hex color code you want. 
Once you chose them: Reload the config with the command *(Tools -> Zartox -> Log -> Reload config)* or launch play mode. 

## Menu:
In Tools → Zartox → Log you have:

1. *Delete today's log*: Delete every file in Logs folder made today
2. *Delete every log*: Delete every file in *Logs* folder
3. *Delete config file*: Delete the config file in *Config* folder
4. *Generate new config file*: Generate a new configuration file with every option inside in *Config* folder
5. *Reload config*: Load the config file in the script **(not necessary since it's loaded every time you enter play mode)**


## Changelog:
---------------------------------------
### 1.0.0:
        - Initial release
---------------------------------------
### 1.0.1: 
        - Added custom colors via the config file
        - Added context for the Unity console
        





