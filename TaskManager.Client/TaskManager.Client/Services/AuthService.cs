namespace TaskManager.Client.Services
{
    public class AuthService
    {
        public bool IsAuthenticated { get; private set; } = false;
        public string UserName { get; private set; }

        public void Login(string name)
        {
            UserName = name;
            IsAuthenticated = true;
        }

        public void Logout()
        {
            UserName = null;
            IsAuthenticated = false;
        }
    }
}
