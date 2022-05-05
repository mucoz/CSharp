//START VS in ADMIN MODE

//MAKE COM VISIBLE = Project -> Properties
//In Properties window, select Signing and Sign the assembly as well

//CREATE REGISTER.BAT
//C:\Windows\Microsoft.NET\Framework\v4 .0.30319\regasm.exe "PATH.dll" / tlb / codebase
//Pause

using System;
using System.Runtime.InteropServices;

[InterfaceType(ComInterfaceType.InterfaceIsDual)]
[Guid("3AD8FDFB-B37A-4526-A72B-9C361465A26C")]

public interface _Visible_Methods
{
    string GetName();
    void SetName(string name);

    int GetAge();

    void SetAge(int age);
}

[ClassInterface(ClassInterfaceType.None)]
[Guid("2401ECAA-DFC3-4852-BA3A-DD6157C1C951")]
[ProgId("Employee")]

public class Employee : _Visible_Methods
{
    private string _name;
    private int _age;
    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public int GetAge()
    {
        return _age;
    }

    public void SetAge(int age)
    {
        _age = age;
    }

}
