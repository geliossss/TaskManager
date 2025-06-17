namespace TaskManager.Client.Services
{
   public class AuthService
{
    public bool IsAuthenticated { get; private set; }
    public int UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

    public void LoginUser(int userId, string name, string login, string password)
    {
        IsAuthenticated = true;
        UserId = userId;
        UserName = name;
        Login = login;
        Password = password;
    }

    public void Logout()
    {
        IsAuthenticated = false;
        UserId = 0;
        UserName = string.Empty;
        Login = string.Empty;
        Password = string.Empty;
    }
}

}
