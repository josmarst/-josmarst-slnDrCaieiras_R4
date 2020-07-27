using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Net;
using atribuicaoAulas.Models;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace atribuicaoAulas
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private PerfilDB_ db = new PerfilDB_();
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
