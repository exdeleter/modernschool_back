namespace modernschool_back.AuthorizationAttr
{
    public class AdminBL
    {
        public List<Admin> GetAdmins()
        {
            /*Добавить получение админов из БД*/

            List<Admin> adminsList = new List<Admin>();


            for (int i = 0; i < 10; i++)
            {
                if (i > 5)
                {
                    adminsList.Add(new Admin()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Password = "IT",
                        Gender = "Male"
                    });
                }
                else
                {
                    adminsList.Add(new Admin()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Password = "HR",
                        Gender = "Female"
                    });
                }
            }
            return adminsList;
        }
    }
}
