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
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    internal class APIDoc
    {
        internal static XmlDocument PyAPI = new XmlDocument();
        internal static XmlDocument RbAPI = new XmlDocument();

        internal static void Initialise()
        {
            try
            {
                PyAPI.Load("APIdoc_Py.xml");
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to load Python API Doc", Exp.Message, Exp.StackTrace);
            }
            try
            {
                RbAPI.Load("APIdoc_Rb.xml");
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to load Ruby API Doc", Exp.Message, Exp.StackTrace);
            }
            try
            {
                BuildPyAPITrees();
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to build Python API Doc tree", Exp.Message, Exp.StackTrace);
            }
            try
            {
                BuildRbAPITrees();
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to build Python API Doc tree", Exp.Message, Exp.StackTrace);
            }
        }
        
        internal static void BuildPyAPITrees()
        {
            BuildAPITree(IronUI.UI.ScriptingShellPythonAPITree, PyAPI);
            BuildAPITree(IronUI.UI.PluginEditorPythonAPITree, PyAPI);
        }

        internal static void BuildPluginEditorTrees()
        {
            BuildAPITree(IronUI.PE.PluginEditorPythonAPITree, PyAPI);
            BuildAPITree(IronUI.PE.PluginEditorRubyAPITree, RbAPI);
        }

        internal static void BuildRbAPITrees()
        {
            BuildAPITree(IronUI.UI.ScriptingShellRubyAPITree, RbAPI);
            BuildAPITree(IronUI.UI.PluginEditorRubyAPITree, RbAPI);
        }

        static void BuildAPITree(TreeView Tree, XmlDocument API)
        {
            try
            {
                Tree.BeginUpdate();
                Tree.Nodes.Clear();
                //Build class
                foreach (XmlNode Class in API.SelectNodes("api")[0].SelectNodes("class"))
                {
                    TreeNode ClassNode = new TreeNode();
                    string ClassName = Class.SelectNodes("name")[0].InnerText;
                    ClassNode.Name = ClassName;
                    ClassNode.Text = ClassName;
                    ClassNode.ForeColor = Color.Green;
                    string ClassType = Class.SelectNodes("type")[0].InnerText;
                    ClassNode.ToolTipText = Class.SelectNodes("description")[0].InnerText;

                    TreeNode ConstructorsNode = new TreeNode();
                    ConstructorsNode.Name = "Constructors";
                    ConstructorsNode.Text = "Constructors";
                    ConstructorsNode.ForeColor = Color.Brown;

                    TreeNode PropertiesNode = new TreeNode();
                    PropertiesNode.Name = "Properties";
                    PropertiesNode.Text = "Properties";
                    PropertiesNode.ForeColor = Color.DarkBlue;

                    TreeNode MethodsNode = new TreeNode();
                    MethodsNode.Name = "Methods";
                    MethodsNode.Text = "Methods";
                    MethodsNode.ForeColor = Color.IndianRed;

                    TreeNode MembersNode = new TreeNode();
                    MembersNode.Name = "Members";
                    MembersNode.Text = "Members";

                    if (ClassType.Equals("Non-Static"))
                    {
                        ClassNode.Nodes.Add(ConstructorsNode);
                    }
                    if (ClassType.Equals("Non-Static") || ClassType.Equals("Static"))
                    {
                        ClassNode.Nodes.Add(PropertiesNode);
                        ClassNode.Nodes.Add(MethodsNode);
                    }
                    if (ClassType.Equals("Enum"))
                    {
                        ClassNode.Nodes.Add(MembersNode);
                    }
                    //Build constructors
                    try
                    {
                        foreach (XmlNode Constructor in Class.SelectNodes("constructors")[0].SelectNodes("constructor"))
                        {
                            TreeNode ConstructorNode = new TreeNode();
                            string ConstructorName = Constructor.SelectNodes("name")[0].InnerText;
                            ConstructorNode.Name = "constructors-" + ConstructorName;
                            ConstructorNode.Text = ConstructorName;
                            ConstructorNode.ForeColor = Color.Brown;
                            ConstructorNode.ToolTipText = Constructor.SelectNodes("description")[0].InnerText;
                            ConstructorsNode.Nodes.Add(ConstructorNode);
                        }
                    }
                    catch
                    {

                    }
                    //Build properties
                    try
                    {
                        foreach (XmlNode Property in Class.SelectNodes("properties")[0].SelectNodes("property"))
                        {
                            TreeNode PropertyNode = new TreeNode();
                            string PropertyName = Property.SelectNodes("name")[0].InnerText + " : " + Property.SelectNodes("datatype")[0].InnerText;
                            PropertyNode.Name = "properties-" + PropertyName;
                            PropertyNode.Text = PropertyName;
                            PropertyNode.ForeColor = Color.DarkBlue;
                            PropertyNode.ToolTipText = Property.SelectNodes("description")[0].InnerText;
                            PropertiesNode.Nodes.Add(PropertyNode);
                        }
                    }
                    catch
                    {

                    }
                    //Build methods
                    try
                    {
                        foreach (XmlNode Method in Class.SelectNodes("methods")[0].SelectNodes("method"))
                        {
                            TreeNode MethodNode = new TreeNode();
                            string MethodName = Method.SelectNodes("name")[0].InnerText + " : " + Method.SelectNodes("return")[0].InnerText;
                            MethodNode.Name = "methods-" + MethodName;
                            MethodNode.Text = MethodName;
                            MethodNode.ForeColor = Color.IndianRed;
                            MethodNode.ToolTipText = Method.SelectNodes("description")[0].InnerText;
                            MethodsNode.Nodes.Add(MethodNode);
                        }
                    }
                    catch
                    {

                    }
                    //Build members
                    try
                    {
                        foreach (XmlNode Member in Class.SelectNodes("members")[0].SelectNodes("member"))
                        {
                            TreeNode MemberNode = new TreeNode();
                            string MemberName = Member.SelectNodes("name")[0].InnerText;
                            MemberNode.Name = "members-" + MemberName;
                            MemberNode.Text = MemberName;
                            MemberNode.ForeColor = Color.IndianRed;
                            MemberNode.ToolTipText = Member.SelectNodes("description")[0].InnerText;
                            MembersNode.Nodes.Add(MemberNode);
                        }
                    }
                    catch
                    {

                    }
                    Tree.Nodes.Add(ClassNode);
                    Tree.EndUpdate();
                }
            }
            catch(Exception Exp)
            {
                Tree.EndUpdate();
                throw Exp;
            }
        }
        internal static string GetPyDecription(TreeNode Node)
        {
            try
            {
                return GetDecription(Node, PyAPI);
            }
            catch(Exception Exp)
            {
                IronException.Report("Error reading Python API Doc information", Exp.Message, Exp.StackTrace);
                return "";
            }
        }
        internal static string GetRbDecription(TreeNode Node)
        {
            try
            {
                return GetDecription(Node, RbAPI);
            }
            catch(Exception Exp)
            {
                IronException.Report("Error reading Ruby API Doc information", Exp.Message, Exp.StackTrace);
                return "";
            }
        }
        static string GetDecription(TreeNode Node, XmlDocument API)
        {
            string Desc="";
            if (Node.Level == 0)
            {
                Desc = GetClassDescription(Node, API);
            }
            else if (Node.Level == 1)
            {
                if(Node.Name.Equals("Constructors"))
                {
                    Desc = GetRichText(API.SelectNodes("api")[0].SelectNodes("constructorsdesc")[0].InnerText);
                }
                else if (Node.Name.Equals("Properties"))
                {
                    Desc = GetRichText(API.SelectNodes("api")[0].SelectNodes("propertiesdesc")[0].InnerText);
                }
                else if (Node.Name.Equals("Methods"))
                {
                    Desc = GetRichText(API.SelectNodes("api")[0].SelectNodes("methodsdesc")[0].InnerText);
                }
                else if (Node.Name.Equals("Members"))
                {
                    Desc = GetRichText(API.SelectNodes("api")[0].SelectNodes("membersdesc")[0].InnerText);
                }
            }
            else if (Node.Level == 2)
            {
                XmlNode ClassNode = API.SelectNodes("api")[0].SelectNodes("class")[Node.Parent.Parent.Index];
                string ClassSection = Node.Name.Substring(0, Node.Name.IndexOf("-"));
                XmlNode ObjectNode = ClassNode.SelectNodes(ClassSection)[0].ChildNodes[Node.Index];
                if (ClassSection.Equals("constructors"))
                {
                    return GetRichText(GetConstructorDescription(ObjectNode));
                }
                else if (ClassSection.Equals("properties"))
                {
                    return GetRichText(GetPropertyDescription(ObjectNode));
                }
                else if (ClassSection.Equals("methods"))
                {
                    return GetRichText(GetMethodDescription(ObjectNode));
                }
                else if (ClassSection.Equals("members"))
                {
                    GetMemberDescription(ObjectNode);
                }
                Desc = ObjectNode.SelectNodes("description")[0].InnerText;
                
            }
            return Desc;
        }

        static string GetClassDescription(TreeNode Node, XmlDocument API)
        {
            StringBuilder Desc = new StringBuilder();
            Desc.Append(API.SelectNodes("api")[0].SelectNodes("class")[Node.Index].SelectNodes("description")[0].InnerText.Replace("\n", @" \par"));
            Desc.Append(@" \par \b Type: \b0 ");
            Desc.Append(API.SelectNodes("api")[0].SelectNodes("class")[Node.Index].SelectNodes("type")[0].InnerText);
            return GetRichText(Desc.ToString());
        }
        static string GetConstructorDescription(XmlNode Base)
        {
            StringBuilder Desc = new StringBuilder();
            Desc.Append(Base.SelectNodes("description")[0].InnerText.Replace("[i[br]]", @" \par "));
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            Desc.Append(@" \cf1 \b ");
            Desc.Append(Base.SelectNodes("name")[0].InnerText);
            Desc.Append(@" \b0 \cf0 ");
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            Desc.Append(@" \b Input Parameters: \b0 ");
            Desc.Append(GetParametersDescription(Base));
            return Desc.ToString();
        }
        static string GetPropertyDescription(XmlNode Base)
        {
            StringBuilder Desc = new StringBuilder();
            Desc.Append(Base.SelectNodes("description")[0].InnerText.Replace("[i[br]]", @" \par "));
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            Desc.Append(@" \cf1 \b ");
            Desc.Append(Base.SelectNodes("name")[0].InnerText);
            Desc.Append(@" \b0 \cf0 ");
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            Desc.Append(@" \b DataType: \b0 ");
            Desc.Append(Base.SelectNodes("datatype")[0].InnerText);
            Desc.Append(@" \par ");
            Desc.Append(@" \par");
            Desc.Append(@" \b Type: \b0 ");
            Desc.Append(Base.SelectNodes("type")[0].InnerText);
            Desc.Append(@" \par ");
            Desc.Append(@" \par");
            return Desc.ToString();
        }
        static string GetMethodDescription(XmlNode Base)
        {
            StringBuilder Desc = new StringBuilder();
            Desc.Append(Base.SelectNodes("description")[0].InnerText.Replace("[i[br]]", @" \par "));
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            Desc.Append(@" \cf1 \b ");
            Desc.Append(Base.SelectNodes("name")[0].InnerText);
            Desc.Append(@" \b0 \cf0 ");
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            Desc.Append(@" \b Type: \b0 ");
            Desc.Append(Base.SelectNodes("type")[0].InnerText);
            Desc.Append(@" \par ");
            Desc.Append(@" \par");
            Desc.Append(@" \b Input Parameters: \b0 ");
            Desc.Append(GetParametersDescription(Base));
            Desc.Append(@" \b Return Type: \b0 ");
            Desc.Append(Base.SelectNodes("return")[0].InnerText);
            Desc.Append(@" \par ");
            Desc.Append(@" \par");
            return Desc.ToString();
        }

        static void GetMemberDescription(XmlNode Base)
        {

        }
        
        static string GetParametersDescription(XmlNode Base)
        {
            XmlNodeList ParameterList = Base.SelectNodes("parameters")[0].SelectNodes("parameter");
            if (ParameterList.Count == 0)
            {
                return @" None \par \par";
            }
            StringBuilder Desc = new StringBuilder();
            Desc.Append(@" \par");
            Desc.Append(@" \par");
            foreach (XmlNode Parameter in ParameterList)
            {
                Desc.Append(@"{\pntext\f1\'B7\tab}{\*\pn\pnlvlblt\pnf1\pnindent0{\pntxtb\'B7}}\fi-360\li720\sl240\slmult1");
                Desc.Append(@"\cf1 \b ");
                Desc.Append(Parameter.SelectNodes("name")[0].InnerText);
                Desc.Append(@" \b0 \cf0");
                Desc.Append(@"\par");
                Desc.Append(@"\pard");
                Desc.Append(@" \tab \b DataType: \b0 ");
                Desc.Append(Parameter.SelectNodes("datatype")[0].InnerText);
                Desc.Append(@" \par ");
                Desc.Append(@" \tab ");
                Desc.Append(Parameter.SelectNodes("description")[0].InnerText.Replace("[i[br]]", @" \par \tab "));
                Desc.Append(@" \par ");
                Desc.Append(@" \pard ");
                Desc.Append(@" \par");
            }
            return Desc.ToString();
        }
        static string GetRichText(string PoorText)
        {
            StringBuilder Desc = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;}");
            Desc.Append(PoorText.Replace("[i[br]]", @" \par "));
            Desc.Append(@"\par}");
            return Desc.ToString();
        }
    }
}
