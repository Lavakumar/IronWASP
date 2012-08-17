using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IronWASP
{
    public class IronThread
    {
        object Parameter;
        bool Parameterized = false;
        ThreadStart Method;
        ParameterizedThreadStart MethodWithParameters;

        static Dictionary<int, Thread> StartedThreads = new Dictionary<int, Thread>();

        private IronThread(ThreadStart Method)
        {
            this.Method = Method;
            this.Parameterized = false;
        }

        private IronThread(ParameterizedThreadStart MethodWithParameters, object Parameter)
        {
            this.MethodWithParameters = MethodWithParameters;
            this.Parameterized = true;
            this.Parameter = Parameter;
        }

        //IronRuby must use IronThread.Run(lambda{method_name}) for parameter less methods
        //IronRuby must use IronThread.Run(lambda{method_name('parameter')}) for methods with parameter
        public static int Run(ThreadStart Method)
        {
            IronThread IT = new IronThread(Method);
            return IT.StartThread();
        }

        public static int Run(ParameterizedThreadStart Method, object Parameter)
        {
            IronThread IT = new IronThread(Method, Parameter);
            return IT.StartThread();
        }

        int StartThread()
        {
            Thread T = new Thread(this.Run);
            T.Start();
            AddThread(T);
            return T.ManagedThreadId;
        }

        void Run()
        {
            try
            {
                if (Parameterized)
                {
                    MethodWithParameters(Parameter);
                }
                else
                {
                    Method();
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp)
            {
                IronException.Report("Exception in Backgroud Thread", Exp);
            }
        }

        static void AddThread(Thread T)
        {
            lock (StartedThreads)
            {
                if (StartedThreads.ContainsKey(T.ManagedThreadId))
                {
                    try
                    {
                        StartedThreads[T.ManagedThreadId].Abort();
                    }
                    catch { }
                }
                StartedThreads[T.ManagedThreadId] = T;
            }
        }

        public static string GetStatus(int ThreadId)
        {
            lock (StartedThreads)
            {
                if (StartedThreads.ContainsKey(ThreadId))
                {
                    return StartedThreads[ThreadId].ThreadState.ToString();
                }
                else
                {
                    return "Invalid thread id";
                }
            }
        }

        public static void Stop(int ThreadId)
        {
            lock (StartedThreads)
            {
                if (StartedThreads.ContainsKey(ThreadId))
                {
                    try
                    {
                        StartedThreads[ThreadId].Abort();
                    }
                    catch { }
                }
            }
        }

        internal static void StopAll()
        {
            lock (StartedThreads)
            {
                foreach(int ID in StartedThreads.Keys)
                {
                    try
                    {
                        StartedThreads[ID].Abort();
                    }
                    catch { }
                }
                StartedThreads.Clear();
            }
        }

        public static void Sleep(int MilliSeconds)
        {
            Thread.Sleep(MilliSeconds);
        }
    }
}
