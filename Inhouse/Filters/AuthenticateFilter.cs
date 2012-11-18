using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inhouse.Filters
{
    public class AuthenticateFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        #region IAuthorizationFilter Members
        public AuthenticateFilter()
            : base()
        {
            // Order = 1;

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["login"] != null && (bool)httpContext.Session["login"] == true)
            {
                return true;
            }
            return false;

            //if (httpContext.Request.IsAuthenticated)
            //{
            //    return true;
            //}
            //return false;
        }
        //public void OnAuthorization(AuthorizationContext filterContext)
        //{
        //  var context = filterContext.HttpContext;
        //  if (context.Session["user"] != null &&  Convert.ToBoolean(context.Session["user"]) == true)
        //  {
        //    context.User.Identity.AuthenticationType = "Forms";
        //    context.User.Identity.Name = "fg";
        //    Thread.CurrentPrincipal = context.User ;
        //  }
        //}
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    var context = filterContext.HttpContext;
        //    var userMan = new ManagerFactory().GetUserManager();
        //    if (context.User != null && context.User.Identity.IsAuthenticated)
        //    {
        //        var email = context.User.Identity.Name;
        //        var user = userMan.GetByUserName(email);

        //        if (user == null)
        //        {                   
        //            FormsAuthentication.SignOut();
        //        }
        //        else
        //        {
        //            AuthenticateAs(context, user);
        //            return;
        //        }
        //    }


        //}

        //private void AuthenticateAs(HttpContextBase context, User user)
        //{
        //    Thread.CurrentPrincipal = context.User = user;
        //}

        #endregion
    }
}