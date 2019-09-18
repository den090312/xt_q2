using Common;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI
{
    public class Class1
    {
        void JoinAwardToUser()
        {
            var userGuidJoin = Request.Form["userGuidJoin"];
            var awardGuidJoin = Request.Form["awardGuidJoin"];

            if (userGuidJoin != null & awardGuidJoin != null)
            {
                new DependencyResolver()?.UserAwardBll?.JoinedAwardToUser
                (
                    Guid.Parse(userGuidJoin), Guid.Parse(awardGuidJoin)
                );
            }
        }
    }
}