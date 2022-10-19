namespace Tracer
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResults.TraceResult GetTraceResult();
    }
}