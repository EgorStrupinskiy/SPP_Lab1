using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private readonly ConcurrentDictionary<int, ConcurrentStack<MethodTraceResult>> _stacksForMethodsOfThreads;
        private readonly ConcurrentDictionary<int, ThreadTraceResult> _threads;

        public Tracer()
        {
            _stacksForMethodsOfThreads = new ConcurrentDictionary<int, ConcurrentStack<MethodTraceResult>>();
            _threads = new ConcurrentDictionary<int, ThreadTraceResult>();
        }

        public void StartTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;
            var frame = new StackTrace(true).GetFrame(1);
            var className = frame?.GetMethod()?.DeclaringType?.FullName;
            var methodName = frame?.GetMethod()?.Name;
            var stack = _stacksForMethodsOfThreads.GetOrAdd(threadId, new ConcurrentStack<MethodTraceResult>());
            var methodResult = new MethodTraceResult(className, methodName);
            stack.Push(methodResult);

            methodResult.stopwatch.Start();
        }

        public void StopTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;
            var stack = _stacksForMethodsOfThreads.GetOrAdd(threadId, new ConcurrentStack<MethodTraceResult>());
            stack.TryPop(out var method);
            method.stopwatch.Stop();
            MethodTraceResult parent;
            if (stack.TryPeek(out parent))
            {
                parent.AddMethod(method);
            }
            else
            {
                var thread = _threads.GetOrAdd(threadId, new ThreadTraceResult(threadId));
                thread.AddMethod(method);
            }
        }

        public TraceResults.TraceResult GetTraceResult()
        {
            var traceResult = new TraceResults.TraceResult();
            foreach (var thread in _threads.Values)
            {
                traceResult.AddThread(thread);
            }

            return traceResult;
        }
    }
}