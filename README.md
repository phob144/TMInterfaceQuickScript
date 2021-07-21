### [Download donadigo's TMInterface](https://donadigo.github.io/tminterface)

**This project is in early stage of development. Please [open an issue](https://github.com/phob144/TMInterfaceQuickScript/issues/new) if you found a major bug or have an idea for a feature :)**

# How to use TMIQS

### Preparing to run the program
You will need to install the [.NET 5 Runtime](https://dotnet.microsoft.com/download/dotnet/5.0) to start TMIQS. Before running the program, make sure to open **config.json** and set **ScriptFolderPath** to the TMInterface script folder.

### Creating and compiling TMIQS script files
Open the TMIQS application folder in command line using `cd <path>` and type in `tmiqs <script name>` to automatically create a new script file. The program will begin listening for any file changes and compiling the scripts into TMInterface commands.

If you want to only start compiling, type in `tmiqs` or start **tmiqs.exe** by itself.

Once you're done scripting, press **CTRL+C** or close the window to stop compiling.

# TMIQS syntax

**NOTE:** Whitespaces can be used anywhere within the script to make it more readable.

### Commands
A command is a comma-separated list of parameters, which contains the **command name**, **delay**, **duration** and **steering strength**. All of them, except **name**, are optional and are defaulted to 0 if not provided.  
Each command must be separated by a new line and will be executed after the previous line has ended execution (+ **delay**).

Syntax: `<name>,<delay>,<duration>,<steerStrength>`

Examples:  
`l,0,2000`  
`r,300`  
`u`

All available commands can be found and modified in **config.json**.

### Command groups
Commands can be grouped by placing semicolons instead of new lines between them.  
Each command in the group will be executed at the same time after the previous line has ended execution (+ **delay**).

Syntax: `<command>;<command>;<command> ...`

Examples:  
`u,0,3000;d,300,20`  
`r,300,10000;d,2000,500;d,4000,1000`
