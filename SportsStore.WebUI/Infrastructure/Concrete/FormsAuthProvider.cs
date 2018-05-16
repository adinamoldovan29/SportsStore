using SportsStore.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string user, string password)
        {
            var result = FormsAuthentication.Authenticate(user, password);
            if (result) {
                FormsAuthentication.SetAuthCookie(user, false);
            }

            return result;
        }
    }
}