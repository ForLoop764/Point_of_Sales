using Point_of_Sales.MVVM.Models;

namespace Point_of_Sales.MVVM.Services;

public static class UserService
{
    private static readonly List<LoginModels> _users = new()
    {
        // Default admin account — always available
        new LoginModels { ID = 1, Username = "admin", Password = "admin123" }
    };

    private static int _nextId = 2;

    /// <summary>
    /// Register a new user. Returns true if successful, false if username already exists.
    /// </summary>
    public static bool Register(RegisterModel model)
    {
        bool exists = _users.Any(u =>
            u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase));

        if (exists) return false;

        _users.Add(new LoginModels
        {
            ID = _nextId++,
            Username = model.Username.Trim(),
            Password = model.Password
        });

        return true;
    }

    /// <summary>
    /// Validate login credentials. Returns the user if found, null otherwise.
    /// </summary>
    public static LoginModels? ValidateLogin(string username, string password)
    {
        return _users.FirstOrDefault(u =>
            u.Username.Equals(username.Trim(), StringComparison.OrdinalIgnoreCase) &&
            u.Password == password);
    }

    /// <summary>
    /// Check if a username is already taken.
    /// </summary>
    public static bool UsernameExists(string username)
    {
        return _users.Any(u =>
            u.Username.Equals(username.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}