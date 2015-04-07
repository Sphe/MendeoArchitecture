using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IRL.Dtos;

namespace IRL.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult AccessDenied(string urlReached)
        {
            var dto = new AccessDeniedDto();
            dto.UrlReached = urlReached;

            return View(dto);
        }
    }
}
