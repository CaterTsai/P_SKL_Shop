using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sklLoginData
/// </summary>
public class sklLoginData
{
    public string Account { get; set; }
    public string Pass { get; set; }
    public string AuthType { get; set; }

    public sklLoginData()
    {
        AuthType = "A";
    }
}