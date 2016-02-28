using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Selenium.Test.UI.utils.tests
{
    class TestsReusableFunctions
    {
        private string debugPrefix = "[MercuriusTests]: ";

        public void DebugLog(string textAction)
        {
            Debug.WriteLine(debugPrefix + textAction);
        }
        public void MenuAdd()
        {

        }

    }
}
