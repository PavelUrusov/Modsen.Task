namespace Store.Application.Common;

public static class Roles
{

    public static readonly Dictionary<string, int> RoleLevels = new()
    {
        { User, 1 },
        { Employee, 2 },
        { Admin, 3 },
        { Owner, 4 }
    };

    public static string User => "user";
    public static string Employee => "employee";
    public static string Admin => "admin";
    public static string Owner => "owner";

}