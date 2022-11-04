namespace modernschool_back.AuthorizationAttr
{
    public class UsersBL
    {
        public List<User> GetUsers()
        {
            /*Добавить получение юзеров из БД "юзер" */

            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "MaleUser",
                Password = "123456"
            });
            userList.Add(new User()
            {
                ID = 101,
                UserName = "FemaleUser",
                Password = "abcdef"
            });
            return userList;
        }
    }
}
