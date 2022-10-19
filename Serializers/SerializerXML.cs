using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Tracer;

namespace Serializers
{
    public class SerializerXML : ISerializer
    {
        public string FileFormat => "xml";

        public string Serialize(TraceResults.TraceResult traceResult)
        {
            using var memoryStream = new MemoryStream();
            var ser = new DataContractSerializer(typeof(TraceResults.TraceResult));

            using (XmlWriter xw = XmlWriter.Create(memoryStream, new()
                   {
                       Encoding = Encoding.UTF8,
                       Indent = true
                   }))
            {
                ser.WriteObject(xw, traceResult);
            }

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}