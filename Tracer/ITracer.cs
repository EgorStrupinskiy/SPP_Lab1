namespace Tracer
{
    public interface ITracer
    {
        void startTrace();

        void stopTrace();

        void getResults();
    }
}