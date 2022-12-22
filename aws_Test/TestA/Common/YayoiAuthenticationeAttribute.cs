using Microsoft.AspNetCore.Authorization;
using System;

namespace Common;

public class YayoiAuthenticationeAttribute : Attribute  //System.Attribute
{
    /*
    private string name { get; set; }
    public double version { get; set; }

    public YayoiAuthorAttribute(string _name)
    {
        name = _name;
        version = 1.0;
    }
    */

    public YayoiAuthenticationeAttribute(bool isAuth=true)
    {
    }
}
