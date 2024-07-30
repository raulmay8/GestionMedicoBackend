using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAttribute : Attribute
{
    public string Permission { get; }

    public PermissionAttribute(string permission)
    {
        Permission = permission;
    }
}
