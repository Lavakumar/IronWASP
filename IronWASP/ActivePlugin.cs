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
    public class ActivePlugin : Plugin
    {
        internal static List<ActivePlugin> Collection = new List<ActivePlugin>();

        internal Scanner Scnr;

        internal Request BaseRequest;
        internal Response BaseResponse;
        internal int ConfidenceLevel = 0;

        internal List<string> RequestTriggers = new List<string>();
        internal List<string> ResponseTriggers = new List<string>();
        internal List<string> RequestTriggerDescs = new List<string>();
        internal List<string> ResponseTriggerDescs = new List<string>();
        internal List<Request> TriggerRequests = new List<Request>();
        internal List<Response> TriggerResponses = new List<Response>();

        internal List<FindingReason> Reasons = new List<FindingReason>();

        internal int AvgBaseRoundTrip = -1;

        public virtual void Check(Scanner Scan)
        {

        }

        public virtual ActivePlugin GetInstance()
        {
            return new ActivePlugin();
        }

        public static void Add(ActivePlugin AP)
        {
            if ((AP.Name.Length > 0) && !(AP.Name.Equals("All") || AP.Name.Equals("None")))
            {
                if (!List().Contains(AP.Name))
                {
                    if (AP.FileName != "Internal")
                    {
                        AP.FileName = PluginEngine.FileName;
                    }
                    Collection.Add(AP);
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (ActivePlugin AP in Collection)
            {
                Names.Add(AP.Name);
            }
            return Names;
        }

        public static ActivePlugin Get(string Name)
        {
            foreach (ActivePlugin AP in Collection)
            {
                if (AP.Name.Equals(Name))
                {
                    ActivePlugin NewInstance = AP.GetInstance();
                    NewInstance.FileName = AP.FileName;
                    return NewInstance;
                }
            }
            return null;
        }

        internal static void Remove(string Name)
        {
            int PluginIndex = 0;
            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i].Name.Equals(Name))
                {
                    PluginIndex = i;
                    break;
                }
            }
            Collection.RemoveAt(PluginIndex);
        }

        public delegate string PayloadGenerator(int TimeValueInMilliSeconds, object OtherInfo);

        public TimeBasedCheckResults DoTimeDelayBasedCheck(PayloadGenerator PayloadGen, object OtherInfo)
        {
            if (this.AvgBaseRoundTrip < 0)
            {
                this.Scnr.Trace("<i<b>>Sending two requests to find out the normal roundtrip time to be used as baseline<i</b>>");
                //Let's first check the time taken for non-delay payloads
                string NonDelayPayload = PayloadGen(0, OtherInfo);

                Response BaseRes1 = this.Scnr.Inject(NonDelayPayload);
                Response BaseRes2 = this.Scnr.Inject(NonDelayPayload);

                this.AvgBaseRoundTrip = (BaseRes1.RoundTrip + BaseRes2.RoundTrip) / 2;
                this.Scnr.Trace(string.Format("<i<b>>The roundtrip values of the two requests were {0}ms and {1}ms. Their average {2}ms is being used as baseline.<i</b>>", BaseRes1.RoundTrip, BaseRes2.RoundTrip, this.AvgBaseRoundTrip));
            }

            return DoTimeDelayBasedCheck(PayloadGen, OtherInfo, this.AvgBaseRoundTrip);
        }
        
        public TimeBasedCheckResults DoTimeDelayBasedCheck(PayloadGenerator PayloadGen, object OtherInfo, int AverageBaseRoundTrip)
        {
            TimeBasedCheckResults TimeCheckResults = new TimeBasedCheckResults();
            TimeCheckResults.Success = false;

            int DelayTime = 5000;
            if (AverageBaseRoundTrip * 2 > DelayTime)
            {
                DelayTime = AverageBaseRoundTrip * 2;
            }

            string DelayPayload = PayloadGen(DelayTime, OtherInfo);
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", DelayPayload));
            Response DelayRes = this.Scnr.Inject(DelayPayload);

            if (DelayRes.RoundTrip < DelayTime)
            {
                this.Scnr.ResponseTrace(string.Format(" ==> Response roundtrip is {0}ms which is less than the {1}ms delay induced in the payload.", DelayRes.RoundTrip, DelayTime));
                return TimeCheckResults;
            }
            else
            {
                this.Scnr.ResponseTrace(string.Format(" ==> Response roundtrip is {0}ms which is more than the {1}ms delay induced in the payload.", DelayRes.RoundTrip, DelayTime));
                this.Scnr.Trace("<i<b>>Doing further checks to determine if this delay was valid or a false positive<i</b>>");
            }

            List<int> SmallDelays = new List<int>();
            List<int> BigDelays = new List<int>();
            int BiggestDelay = 0;
            Request DelayRequest=null;
            Response DelayResponse=null;

            int SmallDelayTime = 1000;
            string SmallDelayPayload = PayloadGen(SmallDelayTime, OtherInfo);
            foreach (int i in new int[] { 0, 1, 2 })
            {
                Thread.Sleep(2000);//To help the server recover from big delay
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", SmallDelayPayload));
                SmallDelays.Add(this.Scnr.Inject(SmallDelayPayload).RoundTrip);
                if (SmallDelays[i] < SmallDelayTime)
                {
                    this.Scnr.ResponseTrace(string.Format(" ==> Response roundtrip is {0}ms which is less than the {1}ms delay induced in the payload.", SmallDelays[i], SmallDelayTime));
                    this.Scnr.Trace("<i<b>>Concluding that the earlier delays were False Positives<i</b>>");
                    return TimeCheckResults;
                }
                else
                {
                    this.Scnr.ResponseTrace(string.Format(" ==> Response roundtrip is {0}ms which is more than the {1}ms delay induced in the payload.", SmallDelays[i], SmallDelayTime));
                }

                Thread.Sleep(1000);//To help the server recover from small delay
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", DelayPayload));
                Response Res = this.Scnr.Inject(DelayPayload);
                if (Res.RoundTrip >= BiggestDelay)
                {
                    BiggestDelay = Res.RoundTrip;
                    DelayRequest = this.Scnr.InjectedRequest.GetClone();
                    DelayResponse = Res;
                }
                BigDelays.Add(Res.RoundTrip);
                if (BigDelays[i] < DelayTime)
                {
                    this.Scnr.ResponseTrace(string.Format(" ==> Response roundtrip is {0}ms which is less than the {1}ms delay induced in the payload.", BigDelays[i], DelayTime));
                    this.Scnr.Trace("<i<b>>Concluding that the earlier delays were False Positives<i</b>>");
                    return TimeCheckResults;
                }
                else
                {
                    this.Scnr.ResponseTrace(string.Format(" ==> Response roundtrip is {0}ms which is more than the {1}ms delay induced in the payload.", BigDelays[i], DelayTime));
                }
            }

            //Analyze small delays to confirm if this is vulnerable
            int SmallDelayTimeDiffSuccessCount = 0;
            foreach (int i in new int[] { 0, 1, 2 })
            {
                if ((BigDelays[i] - DelayTime) >= (SmallDelays[i] - SmallDelayTime))
                {
                    SmallDelayTimeDiffSuccessCount++;
                }
            }
            if (SmallDelayTimeDiffSuccessCount == 3)
            {
                this.Scnr.Trace(string.Format("<i<cr>>The 3 responses for the payload that induced a delay of {0}ms consistently took atleast {1}ms less than the 3 responses for the payload that induced a delay of {2}ms.<i</cr>>", SmallDelayTime, DelayTime - SmallDelayTime ,DelayTime));
                this.Scnr.Trace("<i<cr>>This strongly indicates that the payloads are causing the delays and they are not false positives.<i</cr>>");
                TimeCheckResults.DelayPayload = DelayPayload;
                TimeCheckResults.DelayInduced = DelayTime;
                TimeCheckResults.DelayObserved = BiggestDelay;
                TimeCheckResults.DelayRequest = DelayRequest;
                TimeCheckResults.DelayResponse = DelayResponse;
                TimeCheckResults.Confidence = FindingConfidence.High;
                TimeCheckResults.AverageBaseTime = this.AvgBaseRoundTrip;
                TimeCheckResults.Success = true;
                return TimeCheckResults;
                //Time delay successful high confidence
            }

            SmallDelayTimeDiffSuccessCount = 0;
            foreach (int i in new int[] { 0, 1, 2 })
            {
                if ((BigDelays[i] - (DelayTime - 1000)) >= (SmallDelays[i] - SmallDelayTime))
                {
                    SmallDelayTimeDiffSuccessCount++;
                }
            }
            if (SmallDelayTimeDiffSuccessCount == 3)
            {
                this.Scnr.Trace(string.Format("<i<cr>>The 3 responses where a delay of {0}ms was induced consistently took atleast {1}ms less than the 3 responses where {2}ms delay was induced.<i</cr>>", SmallDelayTime, DelayTime - 1000 - SmallDelayTime, DelayTime));
                this.Scnr.Trace("<i<cr>>This strongly indicates that the payloads are causing the delays and they are not false positives.<i</cr>>");
                TimeCheckResults.DelayPayload = DelayPayload;
                TimeCheckResults.DelayInduced = DelayTime;
                TimeCheckResults.DelayObserved = BiggestDelay;
                TimeCheckResults.DelayRequest = DelayRequest;
                TimeCheckResults.DelayResponse = DelayResponse;
                TimeCheckResults.Confidence = FindingConfidence.Medium;
                TimeCheckResults.AverageBaseTime = this.AvgBaseRoundTrip;
                TimeCheckResults.Success = true;
                return TimeCheckResults;
                //Time delay successful medium confidence
            }
            
            SmallDelayTimeDiffSuccessCount = 0;
            foreach (int i in new int[] { 0, 1, 2 })
            {
                if ((BigDelays[i] - DelayTime / 2) >= (SmallDelays[i] - SmallDelayTime))
                {
                    SmallDelayTimeDiffSuccessCount++;
                }
            }
            if (SmallDelayTimeDiffSuccessCount == 3)
            {
                this.Scnr.Trace(string.Format("<i<cr>>The 3 responses where a delay of {0}ms was induced consistently took atleast {1}ms less than the 3 responses where {2}ms delay was induced.<i</cr>>", SmallDelayTime, (DelayTime/2)-SmallDelayTime, DelayTime));
                this.Scnr.Trace("<i<cr>>This strongly indicates that the payloads are causing the delays and they are not false positives.<i</cr>>");
                TimeCheckResults.DelayPayload = DelayPayload;
                TimeCheckResults.DelayInduced = DelayTime;
                TimeCheckResults.DelayObserved = BiggestDelay;
                TimeCheckResults.DelayRequest = DelayRequest;
                TimeCheckResults.DelayResponse = DelayResponse;
                TimeCheckResults.Confidence = FindingConfidence.Low;
                TimeCheckResults.AverageBaseTime = this.AvgBaseRoundTrip;
                TimeCheckResults.Success = true;
                return TimeCheckResults;
                //Time delay successful low confidence
            }
            this.Scnr.Trace("<i<b>>There were no consistent delays based on the injected payloads, so concluding that the earlier delays were false positives.<i</b>>");
            return TimeCheckResults;
        }

        public string GetFindingOpeningDesc(string FindingName)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(FindingName);
            SB.Append(" was identified in the ");
            if (this.Scnr.InjectedSection == "URL")
            {
                SB.Append(string.Format("<i<co>><i<b>>{0}<i</b>><i</co>>th position of the URL path section of the scanned request.", this.Scnr.InjectedUrlPathPosition));
            }
            else
            {
                SB.Append(string.Format("<i<co>><i<b>>{0}<i</b>><i</co>> parameter of the <i<co>><i<b>>{1}<i</b>><i</co>> section of the scanned request.", this.Scnr.InjectedParameter, this.Scnr.InjectedSection));
            }
            return SB.ToString();
        }
    }

    public class TimeBasedCheckResults
    {
        public bool Success = false;
        public FindingConfidence Confidence = FindingConfidence.Low;
        public int AverageBaseTime = 0;
        public int DelayInduced = 0;
        public int DelayObserved = 0;
        public string DelayPayload = "";
        public Request DelayRequest;
        public Response DelayResponse;
    }
}
