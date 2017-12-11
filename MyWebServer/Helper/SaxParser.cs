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
        public static List<string> GetCities(string street)
        {
            var cities = new List<string>();
            const string file = @"C:\projects\SWE1\SWE1-CS\deploy\Vienna.osm";
            using (var fs = File.OpenRead(file))
            using (var xml = new System.Xml.XmlTextReader(fs))
            {
                while (xml.Read())
                {
                    if (xml.NodeType == System.Xml.XmlNodeType.Element && xml.Name == "osm")
                    {
                        cities = GetCityFromNodes(xml, street);
                    }
                }
            }
            return cities;
        }

        private static List<string> GetCityFromNodes(System.Xml.XmlTextReader xml, string street)
        {
            var cities = new List<string>();
            using (var osm = xml.ReadSubtree())
            {
                while (osm.Read())
                {
                    if (osm.NodeType == System.Xml.XmlNodeType.Element && (osm.Name == "node"))
                    {
                        var pair = GetCityAndStreet(osm);
                        if (pair.Count == 2 && pair[1].ToLower() == street && !cities.Contains(pair[0]))
                        {
                            cities.Add(pair[0]);
                        }
                    }
                }
            }
            return cities;
        }



        private static List<string> GetCityAndStreet(System.Xml.XmlReader node)
        {
            var city = "";
            var street = "";
            var pair = new List<string>();
            using (var element = node.ReadSubtree())
            {
                while (element.Read())
                {
                    if (element.NodeType == System.Xml.XmlNodeType.Element && element.Name == "tag")
                    {
                        string tagType = element.GetAttribute("k");
                        string value = element.GetAttribute("v");
                        switch (tagType)
                        {
                            case "addr:city":
                                city = value;
                                break;
                            case "addr:street":
                                street = value;
                                break;
                        }
                    }
                }
            }
            if (city == "" || street == "") return pair;
            pair.Add(city);
            pair.Add(street);
            return pair;
        }
        //private string GetStreet(System.Xml.XmlReader osm)
        //{
        //    var street = "";
        //    using (var element = osm.ReadSubtree())
        //    {
        //        while (element.Read())
        //        {
        //            if (element.NodeType == System.Xml.XmlNodeType.Element
        //                && element.Name == "tag")
        //            {
        //                string tagType = element.GetAttribute("k");
        //                string value = element.GetAttribute("v");
        //                switch (tagType)
        //                {
        //                    case "addr:city":
        //                        break;
        //                    case "addr:street":
        //                        street = value;
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    return street;
        //}

        //public Dictionary<string, List<string>> Streets;
        //public void Update()
        //{
        //    const string file = @"C:\projects\SWE1\SWE1-CS\deploy\Vienna.osm";
        //    using (var fs = File.OpenRead(file))
        //    using (var xml = new System.Xml.XmlTextReader(fs))
        //    {
        //        while (xml.Read())
        //        {
        //            if (xml.NodeType == System.Xml.XmlNodeType.Element && xml.Name == "osm")
        //            {
        //                ReadNode(xml);
        //            }
        //        }
        //    }
        //}

        //private void ReadAnyOsmElement(System.Xml.XmlReader osm)
        //{
        //    using (var element = osm.ReadSubtree())
        //    {
        //        while (element.Read())
        //        {
        //            if (element.NodeType == System.Xml.XmlNodeType.Element
        //                && element.Name == "tag")
        //            {
        //                //ReadTag(element);
        //                if (GetCity(element) != "" && GetStreet(element) != "")
        //                {
        //                    Console.WriteLine("The Node contains a city and a street");
        //                }
        //            }
        //        }
        //    }
        //}

        //private void ReadTag(System.Xml.XmlReader element)
        //{
        //    string tagType = element.GetAttribute("k");
        //    string value = element.GetAttribute("v");
        //    switch (tagType)
        //    {
        //        case "addr:city":
        //            Console.WriteLine(value);
        //            break;
        //        //case "addr:postcode":
        //        //    a.PostCode = value;
        //        //    break;
        //        case "addr:street":
        //            Console.WriteLine(value);
        //            break;
        //    }
        //}
    }
}
