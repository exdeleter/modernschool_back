namespace modernschool_back.AuthorizationAttr
{
    public class AdminValidate
    {
        public static bool Login(string adminName, string password)
        {
            AdminBL adminBL = new AdminBL();
            var adminsList = adminBL.GetAdmins();
            return adminsList.Any(admin =>
                admin.Name.Equals(adminName, StringComparison.OrdinalIgnoreCase)
                && admin.Password == password);
        }
    }
}
