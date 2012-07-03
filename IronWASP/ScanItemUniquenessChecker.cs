//
// Copyright 2011-2012 Lavakumar Kuppan
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

namespace IronWASP
{
    internal class ScanItemUniquenessChecker
    {
        internal List<string> UniqueQueryParameters = new List<string>();
        internal List<string> NonUniqueQueryParameters = new List<string>();
        internal List<string> UniqueBodyParameters = new List<string>();
        internal List<string> NonUniqueBodyParameters = new List<string>();
        internal List<string> UniqueUrlParameters = new List<string>();
        internal List<string> NonUniqueUrlParameters = new List<string>();

        bool PromptUser = true;

        internal ScanItemUniquenessChecker(bool ShouldPrompt)
        {
            if (ShouldPrompt)
                PromptUser = AskUser.ForBool("User Assistance in Scanning", "Do you want the Scanner to prompt you for help when dealing with items that look like duplicates.<i<br>>Selecting '<i<b>>No<i</b>>' will not prompt for assistance.<i<br>>If you understand the application then you can improve the Scan efficiency by selecting '<i<b>>Yes<i</b>>' and providing assistance.");
            else
                PromptUser = false;
        }

        internal bool IsUniqueToScan(Request Req, List<Request> ScannedRequests, bool IgnoreUrl )
        {
            int DuplicateNamesMatchCounter = 0;
            foreach (Request RR in ScannedRequests)
            {
                if (!Req.Method.Equals(RR.Method)) continue;
                if (Req.HasBody != RR.HasBody) continue;
                if (Req.UrlPathParts.Count != RR.UrlPathParts.Count) continue;
                if (Req.Query.Count != RR.Query.Count) continue;
                if (Req.HasBody)
                {
                    if (Req.Body.Count != RR.Body.Count) continue;
                }
                if (Req.URLPath.Equals(RR.URLPath))
                {
                    List<string> ReqQueryNames = Req.Query.GetNames();
                    List<string> RRQueryNames = RR.Query.GetNames();
                    if (AreListValuesSame(ReqQueryNames, RRQueryNames))
                    {
                        if (PromptUser)
                        {
                            List<string> MismatchedParameters = GetMismatchedQueryParameterNames(Req, RR);
                            if (IsAnyQueryParameterUnique(MismatchedParameters))
                                return true;
                            else if (AreAllQueryParametersNonUnique(MismatchedParameters))
                            {
                                if (!Req.HasBody) return false;
                            }
                            else
                            {
                                StringBuilder Message = new StringBuilder();
                                Message.Append("<i<b>><i<cg>>Request A:<i</cg>><i</b>><i<br>>");
                                Message.Append("  "); Message.Append(RR.Method); Message.Append("  "); Message.Append(RR.UrlPath); Message.Append("?");
                                foreach (string Name in RR.Query.GetNames())
                                {
                                    foreach (string Value in RR.Query.GetAll(Name))
                                    {
                                        if (MismatchedParameters.Contains(Name)) Message.Append("<i<hlo>>");
                                        Message.Append(Name); Message.Append("="); Message.Append(Value);
                                        if (MismatchedParameters.Contains(Name)) Message.Append("<i</hlo>>");
                                        Message.Append("&");
                                    }
                                }
                                if (Message.ToString().EndsWith("&")) Message.Remove(Message.Length - 1, 1);
                                Message.Append("<i<br>>");
                                Message.Append("<i<b>><i<cg>>Request B:<i</cg>><i</b>><i<br>>");
                                Message.Append("  "); Message.Append(Req.Method); Message.Append("  "); Message.Append(Req.UrlPath); Message.Append("?");
                                foreach (string Name in Req.Query.GetNames())
                                {
                                    foreach (string Value in Req.Query.GetAll(Name))
                                    {
                                        if (MismatchedParameters.Contains(Name)) Message.Append("<i<hlo>>");
                                        Message.Append(Name); Message.Append("="); Message.Append(Value);
                                        if (MismatchedParameters.Contains(Name)) Message.Append("<i</hlo>>");
                                        Message.Append("&");
                                    }
                                }
                                if (Message.ToString().EndsWith("&")) Message.Remove(Message.Length - 1, 1);
                                Message.Append("<i<br>>");
                                Message.Append("<i<br>>");
                                Message.Append(@"Request A has been sent for scanning already. Request B is being considered for scanning.<i<br>>
Request A & Request B have the same Query parameter names but some parameters have different values.<i<br>>
If Request B can be considered a duplicate of A and not scanned then just hit 'Submit'.<i<br>>
If the values of some the mis-matched parameters makes Request B unique then select those Query Paramters from the provided list and then hit 'Submit'");
                                List<int> AskUserResponse = AskUser.ForList("Duplicate Scan Item Check", Message.ToString(), "Scan Request B", "Don't scan Request B", "Scan Request B and set these parameters as unique", MismatchedParameters);
                                List<string> UniqueParams = new List<string>();
                                for (int i = 2; i < AskUserResponse.Count; i++)
                                {
                                    UniqueParams.Add(MismatchedParameters[AskUserResponse[i]]);
                                }

                                if (UniqueParams.Count > 0 || AskUserResponse[0] == 1)
                                {
                                    UniqueQueryParameters.AddRange(UniqueParams);
                                    return true;
                                }
                                else
                                {
                                    NonUniqueQueryParameters.AddRange(MismatchedParameters);
                                    if (!Req.HasBody) return false;
                                }
                            }
                        }
                        else
                        {
                            if (!Req.HasBody)
                            {
                                DuplicateNamesMatchCounter++;
                                if (DuplicateNamesMatchCounter > 20)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                    if (Req.HasBody)
                    {
                        List<string> ReqBodyNames = Req.Body.GetNames();
                        List<string> RRBodyNames = RR.Body.GetNames();
                        if (AreListValuesSame(ReqBodyNames, RRBodyNames))
                        {
                            if (PromptUser)
                            {
                                List<string> MismatchedParameters = GetMismatchedBodyParameterNames(Req, RR);
                                if (IsAnyBodyParameterUnique(MismatchedParameters))
                                    return true;
                                else if (AreAllBodyParametersNonUnique(MismatchedParameters))
                                {
                                    return false;
                                }
                                else
                                {
                                    StringBuilder Message = new StringBuilder();
                                    Message.Append("<i<b>><i<cg>>Request A:<i</cg>><i</b>><i<br>>");
                                    Message.Append("  "); Message.Append(RR.Method); Message.Append("  "); Message.Append(RR.Url); Message.Append("<i<br>><i<br>>"); Message.Append("  ");
                                    foreach (string Name in RR.Body.GetNames())
                                    {
                                        foreach (string Value in RR.Body.GetAll(Name))
                                        {
                                            if (MismatchedParameters.Contains(Name)) Message.Append("<i<hlo>>");
                                            Message.Append(Name); Message.Append("="); Message.Append(Value);
                                            if (MismatchedParameters.Contains(Name)) Message.Append("<i</hlo>>");
                                            Message.Append("&");
                                        }
                                    }
                                    if (Message.ToString().EndsWith("&")) Message.Remove(Message.Length - 1, 1);
                                    Message.Append("<i<br>>");
                                    Message.Append("<i<br>>");
                                    Message.Append("<i<b>><i<cg>>Request B:<i</cg>><i</b>><i<br>>");
                                    Message.Append("  "); Message.Append(Req.Method); Message.Append("  "); Message.Append(Req.Url); Message.Append("<i<br>><i<br>>"); Message.Append("  ");
                                    foreach (string Name in Req.Body.GetNames())
                                    {
                                        foreach (string Value in Req.Body.GetAll(Name))
                                        {
                                            if (MismatchedParameters.Contains(Name)) Message.Append("<i<hlo>>");
                                            Message.Append(Name); Message.Append("="); Message.Append(Value);
                                            if (MismatchedParameters.Contains(Name)) Message.Append("<i</hlo>>");
                                            Message.Append("&");
                                        }
                                    }
                                    if (Message.ToString().EndsWith("&")) Message.Remove(Message.Length - 1, 1);
                                    Message.Append("<i<br>>");
                                    Message.Append("<i<br>>");
                                    Message.Append(@"Request A has been sent for scanning already. Request B is being considered for scanning.<i<br>>
Request A & Request B have the same Body parameter names but some parameters have different values.<i<br>>
If Request B can be considered a duplicate of A and not scanned then just hit 'Submit'.<i<br>>
If the values of some the mis-matched parameters makes Request B unique then select those Body Paramters from the provided list and then hit 'Submit'");
                                    List<int> AskUserResponse = AskUser.ForList("Duplicate Scan Item Check", Message.ToString(), "Scan Request B", "Don't scan Request B", "Scan Request B and set these parameters as unique", MismatchedParameters);
                                    List<string> UniqueParams = new List<string>();
                                    for (int i = 2; i < AskUserResponse.Count; i++)
                                    {
                                        UniqueParams.Add(MismatchedParameters[AskUserResponse[i]]);
                                    }

                                    if (UniqueParams.Count > 0 || AskUserResponse[0] == 1)
                                    {
                                        UniqueBodyParameters.AddRange(UniqueParams);
                                        return true;
                                    }
                                    else
                                    {
                                        NonUniqueBodyParameters.AddRange(MismatchedParameters);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                DuplicateNamesMatchCounter++;
                                if (DuplicateNamesMatchCounter > 20)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    if (!PromptUser) return true;
                    if (IgnoreUrl) return false;
                    if (!(Req.File.Equals("") && RR.File.Equals(""))) continue;
                    List<string> PathPartMatch = new List<string>();
                    List<string> ReqUrlPathParts = new List<string>(Req.UrlPathParts);
                    List<string> RRUrlPathParts = new List<string>(RR.UrlPathParts);
                    List<string> MatchUrlPathParts = new List<string>();
                    List<string> MatchedParts = new List<string>();
                    List<string> ReqAMismatchedParts = new List<string>();
                    List<string> ReqBMismatchedParts = new List<string>();

                    bool LastMatch = true;
                    for (int i = 0; i < ReqUrlPathParts.Count; i++)
                    {
                        if (LastMatch)
                        {
                            if (ReqUrlPathParts[i].Equals(RRUrlPathParts[i]))
                            {
                                MatchUrlPathParts.Add(ReqUrlPathParts[i]);
                                MatchedParts.Add(ReqUrlPathParts[i]);
                            }
                            else
                            {
                                LastMatch = false;
                                MatchUrlPathParts.Add("*");
                                ReqAMismatchedParts.Add(RRUrlPathParts[i]);
                                ReqBMismatchedParts.Add(ReqUrlPathParts[i]);
                            }
                        }
                        else
                        {
                            MatchUrlPathParts.Add("*");
                            ReqAMismatchedParts.Add(RRUrlPathParts[i]);
                            ReqBMismatchedParts.Add(ReqUrlPathParts[i]);
                        }
                    }
                    if (MatchedParts.Count == 0) continue;
                    List<string> MatchUrlPathSignatures = UniqueUrlSignatureGenerator(MatchedParts, ReqBMismatchedParts);
                    string MatchUrlPathSignature = "/" + String.Join("/", MatchUrlPathParts.ToArray()) + "/";
                    if (IsUrlPathSignatureUnique(MatchUrlPathSignatures)) return true;
                    if (IsUrlPathSignatureNonUnique(MatchUrlPathSignatures)) return false;

                    StringBuilder Message = new StringBuilder();
                    Message.Append("<i<b>><i<cg>>Request A:<i</cg>><i</b>><i<br>>");
                    Message.Append("  "); Message.Append(RR.Method); Message.Append("  ");
                    foreach (string Part in MatchedParts)
                    {
                        Message.Append("/"); Message.Append(Part);
                    }
                    foreach (string Part in ReqAMismatchedParts)
                    {
                        Message.Append("/"); Message.Append("<i<hlo>>"); Message.Append(Part); Message.Append("<i</hlo>>");
                    }
                    if (RR.Url.EndsWith("/")) Message.Append("/");

                    Message.Append("<i<br>>");
                    Message.Append("<i<br>>");
                    Message.Append("<i<b>><i<cg>>Request B:<i</cg>><i</b>><i<br>>");
                    Message.Append("  "); Message.Append(Req.Method); Message.Append("  ");
                    foreach (string Part in MatchedParts)
                    {
                        Message.Append("/"); Message.Append(Part);
                    }
                    foreach (string Part in ReqBMismatchedParts)
                    {
                        Message.Append("/"); Message.Append("<i<hlo>>"); Message.Append(Part); Message.Append("<i</hlo>>");
                    }
                    if (Req.Url.EndsWith("/")) Message.Append("/");

                    Message.Append("<i<br>>");
                    Message.Append("<i<br>>");
                    Message.Append(@"Request A has been sent for scanning already. Request B is being considered for scanning.<i<br>>
Request A & Request B have the same starting URL but some sections of the URL differ, this could probably be a case of URL re-writing.<i<br>>
If Request B can be considered a duplicate of A and not scanned then just hit 'Submit'.<i<br>>
If some sections of the URL path make Request B unique then select those sections from the provided list and then hit 'Submit'");
                    List<int> AskUserResponse = AskUser.ForList("Duplicate Scan Item Check", Message.ToString(), "Scan Request B", "Don't scan Request B", "Scan Request B and set these parameters as unique", ReqBMismatchedParts);
                    List<int> UniquePathPartPositions = new List<int>();
                    for (int i = 2; i < AskUserResponse.Count; i++)
                    {
                        UniquePathPartPositions.Add(AskUserResponse[i]);
                    }

                    if (UniquePathPartPositions.Count == 0 && AskUserResponse[1] == 1)
                    {
                        NonUniqueUrlParameters.Add(MatchUrlPathSignature);
                        return false;
                    }
                    else
                    {
                        if(UniquePathPartPositions.Count > 0)
                        {
                            List<string> NewSignatureList = new List<string>(MatchedParts);
                            for (int ii = 0; ii < ReqBMismatchedParts.Count; ii++)
                            {
                                if (UniquePathPartPositions.Contains(ii))
                                    NewSignatureList.Add(ReqBMismatchedParts[ii]);
                                else
                                    NewSignatureList.Add("*");
                            }
                            NonUniqueUrlParameters.Add("/" + string.Join("/", NewSignatureList.ToArray()) + "/");
                        }
                        return true;
                    }
                }
            }
            return true;
        }

        bool AreListValuesSame(List<string> Left, List<string> Right)
        {
            if (Left.Count != Right.Count) return false;
            foreach (string Name in Left)
            {
                if (!Right.Contains(Name)) return false;
            }
            return true;
        }

        bool IsAnyQueryParameterUnique(List<string> Pars)
        {
            foreach (string Name in Pars)
            {
                if (UniqueQueryParameters.Contains(Name)) return true;
            }
            return false;
        }

        bool AreAllQueryParametersNonUnique(List<string> Pars)
        {
            foreach (string Name in Pars)
            {
                if (!NonUniqueQueryParameters.Contains(Name)) return false;
            }
            return true;
        }

        bool IsAnyBodyParameterUnique(List<string> Pars)
        {
            foreach (string Name in Pars)
            {
                if (UniqueBodyParameters.Contains(Name)) return true;
            }
            return false;
        }

        bool AreAllBodyParametersNonUnique(List<string> Pars)
        {
            foreach (string Name in Pars)
            {
                if (!NonUniqueBodyParameters.Contains(Name)) return false;
            }
            return true;
        }

        bool IsUrlPathSignatureUnique(string UrlPathSignature)
        {
            if (UniqueUrlParameters.Contains(UrlPathSignature))
                return true;
            else
                return false;
        }

        bool IsUrlPathSignatureUnique(List<string> UrlPathSignatures)
        {
            foreach (string Sign in UrlPathSignatures)
            {
                if (UniqueUrlParameters.Contains(Sign))
                    return true;
            }
            return false;
        }

        bool IsUrlPathSignatureNonUnique(List<string> UrlPathSignatures)
        {
            foreach (string Sign in UrlPathSignatures)
            {
                if (NonUniqueUrlParameters.Contains(Sign))
                    return true;
            }
            return false;
        }

        List<string> GetMismatchedQueryParameterNames(Request ReqOne, Request ReqTwo)
        {
            List<string> ParamNames = new List<string>();
            foreach (string Name in ReqOne.Query.GetNames())
            {
                if (!AreListValuesSame(ReqOne.Query.GetAll(Name), ReqTwo.Query.GetAll(Name)))
                {
                    ParamNames.Add(Name);
                }
            }
            return ParamNames;
        }

        List<string> GetMismatchedBodyParameterNames(Request ReqOne, Request ReqTwo)
        {
            List<string> ParamNames = new List<string>();
            foreach (string Name in ReqOne.Body.GetNames())
            {
                if (!AreListValuesSame(ReqOne.Body.GetAll(Name), ReqTwo.Body.GetAll(Name)))
                {
                    ParamNames.Add(Name);
                }
            }
            return ParamNames;
        }

        List<string> UniqueUrlSignatureGenerator(List<string> MatchingUrlPathParts, List<string> MismatchedUrlPathParts)
        {
            List<string> Signatures = new List<string>();
            StringBuilder UrlStartB = new StringBuilder("/");
            foreach (string Path in MatchingUrlPathParts)
            {
                UrlStartB.Append(Path);
                UrlStartB.Append("/");
            }
            string UrlStart = UrlStartB.ToString();
            int[] Pointer = new int[MismatchedUrlPathParts.Count];
            int i = 0;
            while (i < Pointer.Length)
            {
                Pointer[i] = 0;
                i++;
            }
            Signatures.Add(MakeUrlSignatureFromPointer(UrlStart, MismatchedUrlPathParts, Pointer));
            while (GetSum(Pointer) < Pointer.Length)
            {
                Pointer[Pointer.Length - 1]++;
                for (int j = Pointer.Length - 1; j > 0; j--)
                {
                    if (Pointer[j] == 2)
                    {
                        Pointer[j - 1]++;
                        Pointer[j] = 0;
                    }
                }
                Signatures.Add(MakeUrlSignatureFromPointer(UrlStart, MismatchedUrlPathParts, Pointer));
            }
            return Signatures;
        }

        string MakeUrlSignatureFromPointer(string UrlStart, List<string> MismatchedUrlPathParts, int[] Pointer)
        {
            StringBuilder Result = new StringBuilder(UrlStart);
            if (!UrlStart.EndsWith("/")) Result.Append("/");
            for (int i = 0; i < Pointer.Length; i++)
            {
                if (Pointer[i] == 0)
                    Result.Append(MismatchedUrlPathParts[i]);
                else
                    Result.Append("*");
                Result.Append("/");
            }
            return Result.ToString();
        }

        int GetSum(int[] IntArr)
        {
            int Sum = 0;
            foreach(int i in IntArr)
            {
                Sum = Sum + i;
            }
            return Sum;
        }
    }
}
