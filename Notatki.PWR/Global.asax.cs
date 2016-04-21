using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Notatki.PWR.Controllers;
using Notatki.PWR.Models;

namespace Notatki.PWR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapper.Mapper.Initialize(c =>
            {
                c.CreateMap<AddNoteModel, Note>();
                c.CreateMap<EditNoteDto, Note>();
                c.CreateMap<EditViewModel, Note>();
                c.CreateMap<Note, ListNoteItem>();
                c.CreateMap<List<Note>, ListNotesViewModel>().ForMember(d => d.Notes, o => o.MapFrom(s => s));

            });
        }
    }
}
