//
// Copyright 2011-2013 Lavakumar Kuppan
//
// This file is part of IronWASP
//
// IronWASP is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3 of the License.
//
// IronWASP is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with IronWASP.  If not, see http://www.gnu.org/licenses/.
//

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
        Thread Thread;
        ManualResetEvent MRE = new ManualResetEvent(false);
        bool STAMode = false;

        static Dictionary<int, IronThread> StartedThreads = new Dictionary<int, IronThread>();

        private IronThread(Thread T)
        {
            this.Thread = T;
        }
        
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

        public static int RunSTAThread(ThreadStart Method)
        {
            IronThread IT = new IronThread(Method);
            IT.STAMode = true;
            return IT.StartThread();
        }

        public static int RunSTAThread(ParameterizedThreadStart Method, object Parameter)
        {
            IronThread IT = new IronThread(Method, Parameter);
            IT.STAMode = true;
            return IT.StartThread();
        }

        int StartThread()
        {
            Thread T = new Thread(this.Run);
            if (this.STAMode)
            {
                T.SetApartmentState(ApartmentState.STA);
            }
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
                        StartedThreads[T.ManagedThreadId].Thread.Abort();
                    }
                    catch { }
                }
                StartedThreads[T.ManagedThreadId] = new IronThread(T);
            }
        }

        public static string GetStatus(int ThreadId)
        {
            lock (StartedThreads)
            {
                if (StartedThreads.ContainsKey(ThreadId))
                {
                    return StartedThreads[ThreadId].Thread.ThreadState.ToString();
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
                        StartedThreads[ThreadId].Thread.Abort();
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
                        StartedThreads[ID].Thread.Abort();
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

        public static void Wait()
        {
            int ThreadId = Thread.CurrentThread.ManagedThreadId;
            ManualResetEvent MRE = null;
            lock (StartedThreads)
            {
                if (StartedThreads.ContainsKey(ThreadId))
                {
                    MRE = StartedThreads[ThreadId].MRE;
                }
            }
            if (MRE != null)
            {
                MRE.Reset();
                MRE.WaitOne();
            }
            else
            {
                throw new Exception("Wait can only be called from an running IronThread");
            }
        }

        public static void Resume(int ThreadId)
        {
            lock (StartedThreads)
            {
                if (StartedThreads.ContainsKey(ThreadId))
                {
                    StartedThreads[ThreadId].MRE.Set();
                }
            }
        }
    }
}
