using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Helper
{
    public class SaxParser
    {
        public Dictionary<string, List<string>> Streets;
        public void Update()
        {
            const string file = @"C:\projects\SWE1\SWE1-CS\deploy\Vienna.osm";
            using (var fs = File.OpenRead(file))
            using (var xml = new System.Xml.XmlTextReader(fs))
            {
                while (xml.Read())
                {
                    if (xml.NodeType == System.Xml.XmlNodeType.Element
                        && xml.Name == "osm")
                    {
                        ReadOsm(xml);
                    }
                }
            }
        }

        private void ReadOsm(System.Xml.XmlTextReader xml)
        {
            using (var osm = xml.ReadSubtree())
            {
                while (osm.Read())
                {
                    if (osm.NodeType == System.Xml.XmlNodeType.Element
                        && (osm.Name == "node" || osm.Name == "way"))
                    {
                        ReadAnyOsmElement(osm);
                    }
                }

            }
        }

        private void ReadAnyOsmElement(System.Xml.XmlReader osm)
        {
            using (var element = osm.ReadSubtree())
            {
                while (element.Read())
                {
                    if (element.NodeType == System.Xml.XmlNodeType.Element
                        && element.Name == "tag")
                    {
                        ReadTag(element);
                    }
                }
            }
        }

        private void ReadTag(System.Xml.XmlReader element)
        {
            string tagType = element.GetAttribute("k");
            string value = element.GetAttribute("v");
            switch (tagType)
            {
                case "addr:city":
                    Console.WriteLine(value);
                    break;
                //case "addr:postcode":
                //    a.PostCode = value;
                //    break;
                case "addr:street":
                    Console.WriteLine(value);
                    break;
            }
        }
    }
}
