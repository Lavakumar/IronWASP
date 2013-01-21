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
using System.Net.Sockets;
using System.Threading;

namespace IronWASP
{
    public class PortScanner
    {
        List<int> OpenPorts = new List<int>();
        
        string Target = "";
        List<int> PortsToScan = new List<int>();

        int ConnectTimeOut = 1000;

        public PortScanner(string Target, string Ports)
        {
            this.Target = Target;
            this.PortsToScan = this.ParsePortsToScanString(Ports);
            this.ConnectTimeOut = 1000;
        }
        
        public PortScanner(string Target, string Ports, int ConnectTimeOut)
        {
            this.Target = Target;
            this.PortsToScan = this.ParsePortsToScanString(Ports);
            this.ConnectTimeOut = ConnectTimeOut;
        }

        public List<int> Scan()
        {
            this.DoScan();
            return new List<int>(this.OpenPorts);
        }

        void DoScan()
        {
            int i = 0;
            while (i < PortsToScan.Count)
            {
                int j = 0;
                List<Thread> ScanThreads = new List<Thread>();
                while (j < 50 && i < PortsToScan.Count)
                {
                    Thread T = new Thread(ScanPort);
                    T.Start(PortsToScan[i]);
                    ScanThreads.Add(T);
                    i++;
                    j++;
                }
                Thread.Sleep(this.ConnectTimeOut);
                foreach (Thread T in ScanThreads)
                {
                    try
                    {
                        T.Abort();
                    }
                    catch { }
                }
            }
        }

        void ScanPort(object PortNumObj)
        {
            int PortNum = (int)PortNumObj;
            try
            {
                TcpClient TC = new TcpClient();
                try
                {
                    TC.Connect(Target, PortNum);
                    lock (OpenPorts)
                    {
                        if(!OpenPorts.Contains(PortNum))
                            OpenPorts.Add(PortNum);
                    }
                    TC.GetStream().Close();
                    TC.Close();
                }
                catch
                {}
                TC.Close();
            }
            catch
            {}
        }

        public List<int> ParsePortsToScanString(string Ports)
        {
            List<int> PortNumbers = new List<int>();
            List<string> StringPortNumbers = new List<string>();
            string[] PortNumStrings = Ports.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string PortNumString in PortNumStrings)
            {
                if (PortNumString.Contains("-"))
                {
                    string[] StartAndEnd = PortNumString.Split(new char[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (StartAndEnd.Length == 2)
                    {
                        try
                        {
                            int Start = Int32.Parse(StartAndEnd[0]);
                            int End = Int32.Parse(StartAndEnd[1]);
                            if (Start < End && Start > 0 && End < 65536)
                            {
                                for (int i = Start; i <= End; i++)
                                {
                                    PortNumbers.Add(i);
                                }
                            }
                            else
                            {
                                throw new Exception("Invalid Port Numbers");
                            }
                        }
                        catch
                        {
                            throw new Exception("Invalid Port Numbers");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid Port Numbers");
                    }
                }
                else
                {
                    try
                    {
                        int PortNum = Int32.Parse(PortNumString);
                        if(!PortNumbers.Contains(PortNum))
                            PortNumbers.Add(PortNum);
                    }
                    catch { throw new Exception("Invalid Port Numbers"); }
                }
            }
            if (PortNumbers.Count == 0)
            {
                throw new Exception("Invalid Port Numbers");
            }
            return PortNumbers;
        }
    }
}
