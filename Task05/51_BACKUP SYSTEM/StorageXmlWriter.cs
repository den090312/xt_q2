using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _51_BACKUP_SYSTEM
{
    public static class StorageXmlWriter
    {
        public static void ChangeXmlLog(FileSystemEventArgs file)
        {
            // Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<item><name>wrench</name></item>");

            // Add a price element.
            XmlElement newElem = doc.CreateElement("price");
            newElem.InnerText = "10.95";
            doc.DocumentElement.AppendChild(newElem);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            // Save the document to a file and auto-indent the output.
            XmlWriter writer = XmlWriter.Create("data.xml", settings);
            doc.Save(writer);
        }

        private static byte[] GetBitArray(FileSystemEventArgs file) => File.ReadAllBytes(file.FullPath);
    }
}
