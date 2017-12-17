using System.IO;
using System.Text;
using BIF.SWE1.Interfaces;
using BIF.SWE1.UnitTests;
using NUnit.Framework;

namespace MyTests
{
    [TestFixture]
    public class MyTests : AbstractTestFixture<IMyTests>
    {
        [Test]
        public void HelloWorld()
        {
            var ueb = CreateInstance();
            ueb.HelloWorld();
        }

        #region ToLowerPlugin

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

        [Test]
        public void tolower_handle_big_input()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = new string('*', 1000);

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
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
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "I contain lots and lots of spaces";

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
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
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "ÜÜÜ ÄÄÄ ÖÖÖ";

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
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
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "Это русская строка";

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
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
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var textToTest = "<script> I am a cross site script </script>";

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: $"text={textToTest}"));
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
            var ueb = CreateInstance();
            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.txt";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
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
            var ueb = CreateInstance();
            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.jpg";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
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
            var ueb = CreateInstance();
            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.png";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
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
            var ueb = CreateInstance();
            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.pdf";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
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
            var ueb = CreateInstance();
            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = @"C:\projects\SWE1\SWE1-CS\deploy\static\test.html";
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.Headers["content-type"], Is.Not.Null);
            Assert.That(resp.Headers["content-type"], Is.EqualTo("text/html"));
        }
        #endregion
    }
}
