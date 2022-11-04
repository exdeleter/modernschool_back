namespace modernschool_back.AuthorizationAttr
{
    public class UserValidate
    {
        //This method is used to check the user credentials
        public static bool Login(string userName, string password)
        {
            UsersBL userBL = new UsersBL();
            var userList = userBL.GetUsers();
            return userList.Any(user =>
                user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }
    }
}
