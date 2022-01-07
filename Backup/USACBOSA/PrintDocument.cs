using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Printing;
using System.Drawing;

namespace EasyMa
{
    public class PrintDocument
    {
        // there are other properties you can enter here
        // for instance, page orientation, size, font, etc.

        private string textout;

        public string PrintText
        {
            get { return textout; }
            set { textout = value; }
        }

        // you will also need to add any appropriate class ctor
        // experiment with the PrintDocument class to learn more

        
    }
}