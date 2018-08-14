using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using OnlineTranzaktionOperator.LIB.Modul;

namespace OnlineTranzaktionOperator.LIB
{
    public class ServiceXmlDocument
    {
        public ServiceXmlDocument() { }
        public ServiceXmlDocument(string pathDocument)
        {
            this.pathDocument = pathDocument;
        }
        public string pathDocument { get; set; }
        public XmlDocument GetDocument()
        {
            XmlDocument doc = new XmlDocument();
            if (!string.IsNullOrEmpty(pathDocument))
            {
                FileInfo file = new FileInfo(pathDocument);
                if (file.Exists)
                {
                    try
                    {
                        doc.Load(pathDocument);
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == -2146232000)
                        {
                            XmlElement root = doc.CreateElement("operators");
                            doc.AppendChild(root);
                            doc.Save(pathDocument);
                            return doc;
                        }
                    }
                    doc.Load(pathDocument);

                    return doc;
                }
                else
                {
                    using (Stream stream = file.Create())
                    {
                        XmlElement root = doc.CreateElement("operators");
                        doc.AppendChild(root);
                    }
                    doc.Save(pathDocument);
                    return doc;
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

        }

        public void CreateOperator(Operator oper)
        {
            XmlDocument doc = GetDocument();

            if (ExistsOperator(oper.NameOperator) == false)
            {
                XmlElement xOper = doc.CreateElement("operator");

                XmlElement xName = doc.CreateElement("name");
                xName.InnerText = oper.NameOperator;
                xOper.AppendChild(xName);

                XmlElement xPercent = doc.CreateElement("percent");
                xPercent.InnerText = oper.Percent.ToString();
                xOper.AppendChild(xPercent);

                XmlElement xLogotip = doc.CreateElement("logotip");
                xLogotip.InnerText = oper.Logotip.ToString();
                xOper.AppendChild(xLogotip);

                XmlElement xPrefixes = doc.CreateElement("prefixes");
                foreach (Prefix pref in oper.Prefixes)
                {
                    if (ExistsPrefixOperator(pref.Pref) == false)
                    {
                        XmlElement xPrefix = doc.CreateElement("prefix");
                        xPrefix.InnerText = pref.Pref.ToString();
                        xPrefixes.AppendChild(xPrefix);
                    }
                    else
                    {
                        Console.WriteLine("Prefix "+pref.Pref+" uzhe zanyat!");
                    }
                }
                xOper.AppendChild(xPrefixes);

                doc.DocumentElement.AppendChild(xOper);
                doc.Save(pathDocument);
            }
            else
                throw new Exception("Operator " + oper.NameOperator + " sushestvuet");
           
        }

        private bool ExistsOperator(string name)
        {
            XmlDocument doc = GetDocument();

            XmlNodeList operators = doc.SelectNodes("operators/operator/name");

            foreach (XmlNode item in operators)
            {
                if (item.InnerText.ToUpper() == name.ToUpper())
                    return true;
            }

            return false;
        }

        private bool ExistsPrefixOperator(int pref)
        {


            XmlDocument doc = GetDocument();

            XmlNodeList operators = doc.SelectNodes("operators/operator/prefixes/prefix");

            foreach (XmlNode item in operators)
            {
                if (item.InnerText == pref.ToString())
                    return true;
            }

            return false;
        }
    }
}
