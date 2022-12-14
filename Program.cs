using System.Threading;
using Serializers;
using lab1;
using Tracer;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tracer = new Tracer.Tracer();

            TestClass testClass = new(tracer);
            testClass.Method1();
            testClass.Method1();
            var t = new Thread(() =>
            {
                testClass.Method1();
            });
            t.Start();
            t.Join();

            TraceResults.TraceResult traceResult = tracer.GetTraceResult();

            ISerializer serializer = new SerializerXML();
            string str = serializer.Serialize(traceResult);
            new WriterToConsole().Write(str);
            new WriterToFile().Write(str, serializer.FileFormat);

            serializer = new SerializerJSON();
            str = serializer.Serialize(traceResult);
            new WriterToConsole().Write(str);
            new WriterToFile().Write(str, serializer.FileFormat);
        }
    }


    public class TestClass
    {
        private Tracer.Tracer _tracer;

        public TestClass(Tracer.Tracer tracer)
        {
            this._tracer = tracer; 
        }

        public void Method1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Method2();
            Method2();
            Method3();
            _tracer.StopTrace();
        }

        private void Method2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }

        private void Method3()
        {
            _tracer.StartTrace();
            Thread.Sleep(300);
            _tracer.StopTrace();
        }
    }
}