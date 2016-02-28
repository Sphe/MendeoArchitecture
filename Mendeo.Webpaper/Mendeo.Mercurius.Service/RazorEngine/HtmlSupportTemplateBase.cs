using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Templating;

namespace Mendeo.Mercurius.Service.RazorEngine
{
    public class HtmlSupportTemplateBase<T> : TemplateBase<T>
    {
        public HtmlSupportTemplateBase()
        {
            Html = new RiaHtmlHelper();
        }

        public RiaHtmlHelper Html { get; set; }
    }
}
