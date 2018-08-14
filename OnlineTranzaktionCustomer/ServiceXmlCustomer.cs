using OnlineTranzaktionCustomer.Modul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineTranzaktionCustomer
{
    public class Service
    {
        public Service() { }
        public Service(string pathDocument)
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
                            XmlElement root = doc.CreateElement("user");
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
                        XmlElement root = doc.CreateElement("user");
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

        public void CreateUser(User user)
        {
            string guidUser= Guid.NewGuid().ToString();
            pathDocument = pathDocument + @"/" + guidUser + ".xml";

            XmlDocument doc = GetDocument();

            XmlElement xUser = doc.CreateElement("user");

            XmlElement xUserID = doc.CreateElement("userID");
            xUserID.InnerText = guidUser;
            xUser.AppendChild(xUserID);

            XmlElement xEmail = doc.CreateElement("Email");
            xEmail.InnerText = user.email;
            xUser.AppendChild(xEmail);

            XmlElement xLogin = doc.CreateElement("Login");
            xLogin.InnerText = user.Login;
            xUser.AppendChild(xLogin);

            XmlElement xPassword = doc.CreateElement("Password");
            xPassword.InnerText = user.Password;
            xUser.AppendChild(xPassword);

            doc.DocumentElement.AppendChild(xUser);
            doc.Save(pathDocument);
        }
    }
}
