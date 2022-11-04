using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using modernschool_back.AuthorizationAttr;
using modernschool_back.Models;
using System.Net;
using System.Web.Http;

namespace modernschool_back.Controllers
{
    public class AdminController : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage GetAdmins()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var admList = new AdminBL().GetAdmins();
            switch (username.ToLower())
            {
                case "maleuser":
                    return Request.CreateResponse(HttpStatusCode.OK,
                        admList.Where(e => e.Gender.ToLower() == "male").ToList());
                case "femaleuser":
                    return Request.CreateResponse(HttpStatusCode.OK,
                        admList.Where(e => e.Gender.ToLower() == "female").ToList());
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
        }
    }
}
