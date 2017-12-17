using System;
using System.IO;
using System.Text;
using BIF.SWE1.Interfaces;
using MyWebServer;
using NUnit.Framework;

namespace MyTests
{
    [TestFixture]
    public class MyTests
    {
        #region ToLowerPlugin

        [Test]
        public void tolower_handle_big_input()
        {
            var plugin = GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = new string('*', 1000);

            var req = GetRequest(GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain(textToTest.ToLower()));
        }

        [Test]
        public void tolower_handle_spaces()
        {
            var plugin = GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "I contain lots and lots of spaces";

            var req = GetRequest(GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain(textToTest.ToLower()));
        }

        [Test]
        public void tolower_handle_umlauts()
        {
            var plugin = GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "ÜÜÜ ÄÄÄ ÖÖÖ";

            var req = GetRequest(GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain(textToTest.ToLower()));
        }

        [Test]
        public void tolower_handle_cyrillic()
        {
            var plugin = GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "Это русская строка";

            var req = GetRequest(GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain(textToTest.ToLower()));
        }

        [Test]
        public void tolower_handle_html()
        {
            var plugin = GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "<script> I am a cross site script </script>";

            var req = GetRequest(GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain(textToTest.ToLower()));
        }
        #endregion

        #region StaticFilePlugin

        [Test]
        public void static_handle_txt()
        {
            var obj = GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.txt";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.Headers["content-type"], Is.Not.Null);
            Assert.That(resp.Headers["content-type"], Is.EqualTo("text/plain"));
        }

        [Test]
        public void static_handle_jpg()
        {
            var obj = GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.jpg";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.Headers["content-type"], Is.Not.Null);
            Assert.That(resp.Headers["content-type"], Is.EqualTo("image/jpeg"));
        }


        [Test]
        public void static_handle_png()
        {
            var obj = GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.png";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.Headers["content-type"], Is.Not.Null);
            Assert.That(resp.Headers["content-type"], Is.EqualTo("image/png"));
        }

        [Test]
        public void static_handle_pdf()
        {
            var obj = GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.pdf";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.Headers["content-type"], Is.Not.Null);
            Assert.That(resp.Headers["content-type"], Is.EqualTo("application/pdf"));
        }

        [Test]
        public void static_handle_html()
        {
            var obj = GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.html";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.Headers["content-type"], Is.Not.Null);
            Assert.That(resp.Headers["content-type"], Is.EqualTo("text/html"));
        }
        #endregion

        #region TemperaturePlugin

        [Test]
        public void temp_rest_handle_invalid()
        {
            var plugin = GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = "/GetTemperature/yolo";
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/xml"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void temp_rest_handle_future()
        {
            var plugin = GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = GetTemperatureRestUrl(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1));
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/xml"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void temp_handle_invalid_search()
        {
            var plugin = GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = "/temp?search=hallo";
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/html"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void temp_handle_search()
        {
            var plugin = GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = "/temp?search=01.01.2017";
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/html"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void temp_handle_page()
        {
            var plugin = GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = "/temp?page=05";
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = GetRequest(GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/html"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        #endregion

        #region Helper

        public static Stream GetValidRequestStream(string url, string method = "GET", string host = "localhost", string[][] header = null, string body = null)
        {
            byte[] bodyBytes = null;
            if (body != null)
            {
                bodyBytes = Encoding.UTF8.GetBytes(body);
            }

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.ASCII);

            sw.WriteLine("{0} {1} HTTP/1.1", method, url);
            sw.WriteLine("Host: {0}", host);
            sw.WriteLine("Connection: keep-alive");
            sw.WriteLine("Accept: text/html,application/xhtml+xml");
            sw.WriteLine("User-Agent: Unit-Test-Agent/1.0 (The OS)");
            sw.WriteLine("Accept-Encoding: gzip,deflate,sdch");
            sw.WriteLine("Accept-Language: de-AT,de;q=0.8,en-US;q=0.6,en;q=0.4");
            if (bodyBytes != null)
            {
                sw.WriteLine(string.Format("Content-Length: {0}", bodyBytes.Length));
                sw.WriteLine("Content-Type: application/x-www-form-urlencoded");
            }
            if (header != null)
            {
                foreach (var h in header)
                {
                    sw.WriteLine(string.Format("{0}: {1}", h[0], h[1]));
                }
            }
            sw.WriteLine();

            if (bodyBytes != null)
            {
                sw.Flush();
                ms.Write(bodyBytes, 0, bodyBytes.Length);
            }

            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static Stream GetInvalidRequestStream()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.ASCII);

            sw.WriteLine("GET");
            sw.WriteLine();
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static Stream GetEmptyRequestStream()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.ASCII);

            sw.WriteLine();
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private static StringBuilder GetBody(IResponse resp)
        {
            StringBuilder body = new StringBuilder();
            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(ms);
                while (!sr.EndOfStream)
                {
                    body.AppendLine(sr.ReadLine());
                }
            }
            return body;
        }

        public void HelloWorld()
        {
            // I'm fine
        }

        public IRequest GetRequest(System.IO.Stream network)
        {
            return new Request(network);
        }

        public IPlugin GetToLowerPlugin()
        {
            return new ToLowerPlugin();
        }

        public string GetToLowerUrl()
        {
            return "/tolower";
        }

        public IPlugin GetStaticFilePlugin()
        {
            return new StaticFilePlugin();
        }

        public IPlugin GetTemperaturePlugin()
        {
            return new TempPlugin();
        }

        public string GetTemperatureRestUrl(DateTime from, DateTime until)
        {
            return $"/GetTemperature/{from.Year}/{from.Month}/{from.Day}";
        }

        public string GetTemperatureUrl(DateTime from, DateTime until)
        {
            return "/temp/" + from.ToString("yyyyMMdd") + "to" + until.ToString("yyyyMMdd");
        }

        #endregion
    }
}
