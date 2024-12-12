using MadebymeWF.Models;

namespace MadebymeWF
{
    public static class UserSession
    {
        public static User CurrentUser { get; private set; } = null;

        public static void StartSession(User user)
        {
            CurrentUser = user;
        }

        public static void EndSession()
        {
            CurrentUser = null;
        }

        public static bool IsUserLoggedIn()
        {
            return CurrentUser != null;
        }
    }
}
