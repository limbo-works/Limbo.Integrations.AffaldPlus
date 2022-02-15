using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Http.Client;
using Skybrud.Essentials.Http.Collections;

namespace Limbo.Integrations.AffaldPlus {

    public class AffaldPlusHttpClient : HttpClient {

        public IHttpResponse GetSuggestions(int municipalityId, string text) {
            return DoSoapPostRequest("genXMLSoegeOrd", new HttpPostData {
                {"Komnr", municipalityId},
                {"ord", text}
            });
        }

        public IHttpResponse GetContent(int municipalityId, int suggestionId) {
            return DoSoapPostRequest("genXMLSoegeOrdResult", new HttpPostData {
                {"Komnr", municipalityId},
                {"id", suggestionId}
            });
        }

        private IHttpResponse DoSoapPostRequest(string methodName, HttpPostData data) {
            
            XNamespace env = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace ns1 = "urn:xmethods-delayed-quotes";

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "urn:xmethods-delayed-quotes");

            XElement method = new XElement(
                ns1 + methodName,
                new XAttribute(XNamespace.Xmlns + "ns1", "urn:xmethods-delayed-quotes")
            );

            foreach (KeyValuePair<string, string> pair in data) {
                method.Add(new XElement(pair.Key, pair.Value));
            }

            XElement envelope = new XElement(
                env + "Envelope",
                new XAttribute(XNamespace.Xmlns + "SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/")
            );

            XElement body = new XElement(env + "Body", method);
            envelope.Add(body);

            XDeclaration declaration = new XDeclaration("1.0", "utf-8", null);

            XDocument doc = new XDocument(envelope);

            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriter(builder)) {
                doc.Save(writer);
            }

            HttpRequest request = new HttpRequest {
                Url = "https://affaldonline.dk/AffaldsGuide/api/server.php",
                Method = HttpMethod.Post,
                Body = String.Concat(declaration, Environment.NewLine, doc.ToString()),
                ContentType = "text/xml"
            };

            return request.GetResponse();

        }

    }

}