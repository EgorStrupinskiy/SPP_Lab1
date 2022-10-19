using System.Collections.Generic;
using System.Diagnostics;

namespace Tracer
{
    public class TraceResults
    {
        public readonly struct TraceResult
        {
            private readonly List<ThreadTraceResult> _threadTraceResults;

            public IReadOnlyList<ThreadTraceResult> ThreadTraceResults { get => _threadTraceResults; }


            public TraceResult()
            {
                _threadTraceResults = new List<ThreadTraceResult>();
            }

            public void AddThread(ThreadTraceResult threadTraceResult)
            {
                _threadTraceResults.Add(threadTraceResult);
            }
        }
    }
    
    public struct ThreadTraceResult
    {
        private int Id { get;}


        private long _time
        {
            get
            {
                long res = 0;
                foreach (var methodResult in MethodTraceResults)
                {
                    res = res + methodResult.Time;
                }
                return res;
            }
        }

        public long Time { get => _time; }


        private List<MethodTraceResult> _methodTraceResults;

        public IReadOnlyList<MethodTraceResult> MethodTraceResults { get => _methodTraceResults; }

        public ThreadTraceResult(int Id)
        {
            _methodTraceResults = new List<MethodTraceResult>();
            this.Id = Id;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            _methodTraceResults.Add(methodTraceResult);
        }
    }
    
    
    public struct MethodTraceResult
    {
        public Stopwatch stopwatch;

        private long _time
        {
            get
            {
                return stopwatch.ElapsedMilliseconds;
            }
        }

        public long Time { get => _time; }

        private string _name;
        public string Name { get => _name; }

        private string _className;
        public string ClassName { get => _className; }

        private List<MethodTraceResult> _methodTraceResults;

        public IReadOnlyList<MethodTraceResult> MethodTraceResults { get => _methodTraceResults; }

        public MethodTraceResult(string ClassName, string Name)
        {
            _methodTraceResults = new List<MethodTraceResult>();
            stopwatch = new Stopwatch();
            _name = Name;
            _className = ClassName;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            _methodTraceResults.Add(methodTraceResult);
        }
    }
}