using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Xml.Extensions;
using Skybrud.Integrations.AffaldPlus.Models.Content;

namespace Skybrud.Integrations.AffaldPlus.Responses {

    public class AffaldPlusGetContentResponse : AffaldPlusResponse<AffaldPlusContentResult> {
        
        #region Constructors

        private AffaldPlusGetContentResponse(IHttpResponse response) : base(response) {

            // Validate the response
            ValidateResponse(response);

            // Declare the namespaces used in the response body
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "urn:xmethods-delayed-quotes");

            // Parse the outer XML body
            XElement body = XElement.Parse(response.Body);

            // Parse the inner XML body
            string datapakke = body.GetElementValue("SOAP-ENV:Body/ns1:genXMLSoegeOrdResultResponse/GetDatapakke", nsmgr);

            // The inner XML body may contain invalid XML, so we need to wrap that in CDATA
            datapakke = datapakke.Replace("<AfleveringsStedBody>", "<AfleveringsStedBody><![CDATA[");
            datapakke = datapakke.Replace("</AfleveringsStedBody>", "]]></AfleveringsStedBody>");

            // Parse the content
            Body = new AffaldPlusContentResult(XElement.Parse(datapakke).GetElement("Data/Result"));

        }

        #endregion

        #region Static methods

        public static AffaldPlusGetContentResponse ParseResponse(IHttpResponse response) {
            return response == null ? null : new AffaldPlusGetContentResponse(response);
        }

        #endregion

    }

}