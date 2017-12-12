using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Helper
{
    public class SaxParser
    {//TODO XML tags for public properties
        private const string OsmFile = @"C:\projects\SWE1\SWE1-CS\deploy\small.osm";
        public static Dictionary<string, List<string>> Cities { get; set; } = new Dictionary<string, List<string>>();
        public static bool IsUpdated { get; set; } = false;

        /// <summary>
        /// Accepts a street-string and returns a List of strings with all cities containing that street according to the provided OSM file. If the Cities Dictionary has been filled, this method will search there directly
        /// </summary>
        /// <param name="street"></param>
        /// <returns></returns>
        public static List<string> GetCities(string street)
        {
            if (IsUpdated)
            {
                return Cities[street];
            }
            var cities = new List<string>();
            using (var fs = File.OpenRead(OsmFile))
            using (var xml = new System.Xml.XmlTextReader(fs))
            {
                while (xml.Read())
                {
                    if (xml.NodeType == System.Xml.XmlNodeType.Element && xml.Name == "osm")
                    {
                        cities = GetCitiesFromNodes(xml, street);
                    }
                }
            }
            return cities;
        }

        private static List<string> GetCitiesFromNodes(System.Xml.XmlTextReader xml, string street)
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

        /// <summary>
        /// Traverses the provided OSM file and creates a replica of the content within the Cities Dictionary
        /// </summary>
        public static void Update()
        {
            using (var fs = File.OpenRead(OsmFile))
            using (var xml = new System.Xml.XmlTextReader(fs))
            {
                while (xml.Read())
                {
                    if (xml.NodeType == System.Xml.XmlNodeType.Element && xml.Name == "osm")
                    {
                        Cities.Clear(); //TODO add thread safety here
                        IsUpdated = false;
                        FillCitiesDictionary(xml);
                        IsUpdated = true;
                    }
                }
            }
        }

        private static void FillCitiesDictionary(System.Xml.XmlTextReader xml)
        {
            var streets = new List<string>();
            var cities = new List<string>();
            using (var osm = xml.ReadSubtree())
            {
                while (osm.Read())
                {
                    if (osm.NodeType == System.Xml.XmlNodeType.Element && (osm.Name == "node"))
                    {
                        var pair = GetCityAndStreet(osm);
                        if (pair.Count == 2)
                        {
                            cities.Add(pair[0]);
                            streets.Add(pair[1].ToLower());
                        }
                    }
                }
            }
            for (var i = 0; i < streets.Count; i++)
            {
                if (!Cities.ContainsKey(streets[i]))
                {
                    Cities.Add(streets[i], new List<string> { cities[i] });
                }
                else
                {
                    if (!Cities[streets[i]].Contains(cities[i]))
                    {
                        Cities[streets[i]].Add(cities[i]); //TODO add thread safety here
                    }
                }
            }
        }
    }
}
