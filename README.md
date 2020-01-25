# SimpleWMI
💻😊 .NET library for the easy use of WMI features available in the System.Management library.

# Purpose
SimpleWMI tries to overcome the cumbersome nature of using the System.Management library. Select queries are avoided and common WMI functions are made achievable in a single line of code, rather than several.

I implemented this library to solve the problem I faced at work while developing a computer auditing program that involved having an abundance of meaningless code. This code repeated itself constantly and wasn't adaptable to any new WMI classes that I wanted to add in the future.

SimpleWMI solves that problem by making the code much tidier, whilst still performing the required functions for a sophisticated program that needs to retrieve data from WMI classes.

# Table of Contents
- [Installation](#installation)
- [Usage](#usage)
  - [Notable Links](#notable-links-from-msdn)
  - [WMIQuery Functions](#wmiquery-functions)
  - [WMI Constant Classes](#wmi-constant-classes)
- [Credits](#credits)

# Installation
Download the SimpleWMI .dll from the releases tab and include it as a reference in your .NET 4.5+ project. After that, remember to include:
```csharp
using SimpleWMI;
```
Once you've referenced the library, look at the *usage* section below to start using it.

# Usage
All the documentation for the WMI classes themselves (including their specific class names, attributes and respective data types) can be found on the Microsoft Developer Network (MSDN).

## Notable links from MSDN
- [Win32 Provider Classes](https://msdn.microsoft.com/en-us/library/aa394388(v=vs.85).aspx)
  - This page includes the most useful WMI classes used for getting information about your computer and the software that runs on it.
- [CIM WMI Provider Classes](https://msdn.microsoft.com/en-us/library/dn792216(v=vs.85).aspx)
  - This page provides documentation for all of the CIM WMI classes. This is not documented as nicely as the Win32 Provider page, so avoid using it if you can.

## WMIQuery Functions
SimpleWMI currently only uses two functions to query WMI classes. Both are located in the class `WMIQuery`.

### WMIQuery.GetAllObjects(string className)
This function will retrieve all WMI objects in the class that you specify as a parameter and return them as a dynamic list. The dynamic data type has been used here so that a single function can manipulate and return every single WMI class.

Once returned, it can be iterated through to access each object and reference their attributes as demonstrated below:
```csharp
foreach (dynamic item in WMIQuery.GetAllObjects(Win32.VideoController))
{
    //Wrapped in a try-catch because trying to write null values to console causes errors.
    try
    {
        Console.WriteLine(item.Name);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```
On my specific machine, this will output the following:
```
Intel(R) HD Graphics 630
NVIDIA GeForce GTX 1050
```
Since this function returns a *list* of dynamic data types, it can be further queried using LINQ as standard in C#. This means that functions such as `.Where` and `.FirstOrDefault` will work on the returned data.

An example can be seen below:
```csharp
IEnumerable<dynamic> googleProcesses = WMIQuery.GetAllObjects(Win32.Process).Where(s => s.Caption.ToLower().Contains("google"));

foreach (dynamic item in googleProcesses)
{
    Console.WriteLine(item.Caption);
}
```
I have used .Where to only include processes currently running with *google* in the caption.

On my specific machine, this will output the following:
```
GoogleCrashHandler.exe
GoogleCrashHandler64.exe
```

### WMIQuery.GetObject(string className)
This function is a modified version of `WMIQuery.GetAllObjects()` that will only return the first object queried. It should really only be used in circumstances where you know only one object will be returned, such as when getting information about your battery or processor, as most machines will only have one battery or processor. The reason this is used rather than GetAllObjects is purely because it is nicer to access the attributes of the returned dynamic when it isn't a list and is a stand-alone object.

I have demonstrated why it is nicer to use for single objects below:
```csharp
//accessing the attributes using GetAllObjects:
List<dynamic> cpu = WMIQuery.GetAllObjects(Win32.Processor);
Console.WriteLine(cpu[0].Name);

//accessing the attributes using GetObject:
dynamic cpu = WMIQuery.GetObject(Win32.Processor);
Console.WriteLine(cpu.Name);
```
As you can see, it is a very minor difference but it makes dealing with the data just a little nicer.

If you don't wish to use this function, the same functionality can be achieved using `GetAllObjects`.

## WMI Constant Classes
To make it a bit easier to find the right class that you want to use without having to scour MSDN, I have implemented three classes that contain a huge number of constants that represent the hundreds of WMI classes available.

For example, I have used `Win32.Processor` as the attribute in the above code which has a constant value of `"Win32_Processor"` so that the class `Win32_Processor` is queried.

This makes it so much easier to query the right class, rather than having to remember the exact name of the class or having to use the documentation as instead of remembering a string, you can just use a constant in the appropriate class.
```csharp
//using strings instead of a constant from the Win32 class:
dynamic cpu = WMIQuery.GetObject("Win32_Processor");
Console.WriteLine(cpu.Name);

//using a constant from the Win32 class:
dynamic cpu = WMIQuery.GetObject(Win32.Processor);
Console.WriteLine(cpu.Name);
```
This means that when you type `Win32.` while programming, the classes you can query will appear through autocomplete for more efficient programming.
### Example: Win32 Class
This class corresponds to all of the classes (and more!) found in the Win32 Provider Classes documentation found in the Notable Links section.

It is formatted as follows:
```csharp
public class Win32
{
  public const string DeviceChangeEvent = "Win32_DeviceChangeEvent";
  public const string SystemConfigurationChangeEvent = "Win32_SystemConfigurationChangeEvent";
  public const string VolumeChangeEvent = "Win32_VolumeChangeEvent";
  ...
}
```
The attribute name corresponds with the text after `Win32_` in the class name.

The other two constant classes, `CIM` and `Msft` are formatted the same way, only the attribute name corresponds with the text after `CIM_` or `Msft_` respectively.

# Credits
**James Ball** - jhb119@ic.ac.uk
