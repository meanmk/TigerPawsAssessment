using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;

namespace TigerPaws.Helper
{
    public static class HtmlHelperExtensions
    {
        /// Applies an XSL transformation to an XML document.
        /// 
        public static HtmlString RenderXml(this HtmlHelper helper, string xml, string xsltPath)
        {
            XsltArgumentList args = new XsltArgumentList();
            XslCompiledTransform t = new XslCompiledTransform();
            t.Load(xsltPath);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            using (XmlReader reader = XmlReader.Create(new StringReader(xml), settings))
            {
                StringWriter writer = new StringWriter();
                t.Transform(reader, args, writer);
                HtmlString htmlString = new HtmlString(writer.ToString());
                return htmlString;
            }
        }
    }
}