using modernschool_back.AuthorizationAttr;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web;


namespace modernschool_back.Models
{
    public class BasicAuthenticationAttribute: AuthorizationFilterAttribute
    {
        private const string Realm = "My Realm";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //If the Authorization header is empty or null
            //then return Unauthorized
            if (actionContext.Request.Headers.Authorization == null) 
            {
                actionContext.Response = 
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            /*
            Если запрос был несанкционированным, добавим заголовок WWW-Authenticate
            к ответу, который указывает, что для него требуется базовая аутентификация
            */
                if (actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    actionContext.Response.Headers.Add("WWW-Authenticate",
                        string.Format("Basic realm=\"{0}\"", Realm));
                }
            }
            else
            {
                string authorizationToken = 
                    actionContext.Request.Headers.Authorization.Parameter;  
                string decodedAuthorizationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authorizationToken));
                string[] usernamePasswordArr = decodedAuthorizationToken.Split(':');

                // We will get a separate login and password
                string username = usernamePasswordArr[0];
                string password = usernamePasswordArr[1];

                // check the username and password

                if(UserValidate.Login(username, password))
                {
                    var identity = new GenericIdentity(username);
                    IPrincipal principal = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principal;

                    /*
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    */
                }
                actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);

            }
        }

    }
}
