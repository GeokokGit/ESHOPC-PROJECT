namespace JewelryShop.Services
{
    public class AdminSession
    {
        public bool IsLoggedIn { get; private set; }

        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        public bool Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                IsLoggedIn = true;
                return true;
            }

            return false;
        }

        public void Logout()
        {
            IsLoggedIn = false;
        }
    }
}
