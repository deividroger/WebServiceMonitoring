using Log.Model;
using Log.Repository;
using System;
using System.IO;
using System.Web.Services.Protocols;
using System.Xml;

namespace LogService.Configuration
{
    public class LogWebServiceCall : SoapExtension
    {

        private Stream oldStream;
        private Stream newStream;

        public bool generatedLog { get; set; }

        private static XmlDocument xmlRequest;

        public static XmlDocument XmlRequest
        {
            get { return xmlRequest; }
        }

        private static XmlDocument xmlResponse;

        public static XmlDocument XmlResponse
        {
            get { return xmlResponse; }
        }

        private XmlDocument GetSoapEnvelope(Stream stream)
        {

            XmlDocument xml = new XmlDocument();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            xml.LoadXml(reader.ReadToEnd());
            stream.Position = 0;
            return xml;
        }

        public override object GetInitializer(Type serviceType)
        {
            return serviceType;

        }

        public override object GetInitializer(LogicalMethodInfo methodInfo,
            SoapExtensionAttribute attribute)
        {

            return attribute;
        }

        public override void Initialize(object initializer)
        {

        }

        public override void ProcessMessage(System.Web.Services.Protocols.SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:

                    xmlRequest = GetSoapEnvelope(newStream);

                    CopyStream(newStream, oldStream);

                    RecordLog(message.Action);

                    break;
                case SoapMessageStage.BeforeDeserialize:

                    CopyStream(oldStream, newStream);
                    xmlResponse = GetSoapEnvelope(newStream);

                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
            }

        }

        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        public void RecordLog(string _webMethod)
        {
            if (!generatedLog)
            {


                new LoggerDataBase().RecordLogWS(new LogDto()
                {
                    Action = _webMethod,
                    InValue = xmlRequest.InnerXml,
                    OutValue = xmlResponse.InnerXml
                });

                generatedLog = true;
            }
        }

        private void CopyStream(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);

            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();
        }

        
    }
}
