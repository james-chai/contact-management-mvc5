﻿using System.Web.Mvc;

namespace ContactManagement.MVC5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
            => filters.Add(new HandleErrorAttribute());
    }
}
