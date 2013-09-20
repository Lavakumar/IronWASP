using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace IronWASP
{
    public partial class ReportGenerationWizard : Form
    {
        bool Loaded = false;
        Thread ReportingThread;

        List<string> IncludedHosts = new List<string>();
        List<string> IncludedTypes = new List<string>();
        List<string> IncludedTitles = new List<string>();

        Dictionary<int, FindingInfoList> FindingsId = new Dictionary<int, FindingInfoList>();

        Dictionary<string, string> GenerateReportBtnTexts = new Dictionary<string, string>() { { "Running", "Stop" }, { "Stopped", "Generate Report for the Items selected below" } };

        int ReportedFindingsCount = 0;
        int ReportedFindingsCounter = 0;

        string HtmlReport = "";
        string RtfReport = "";

        public ReportGenerationWizard()
        {
            InitializeComponent();
        }

        private void ReportGenerationWizard_Load(object sender, EventArgs e)
        {            
            List<string> Values = GetValues(FindingsTree.Nodes[0].Nodes[0], 3, true);
            GetValues(FindingsTree.Nodes[0].Nodes[1], 2, true, Values);
            GetValues(FindingsTree.Nodes[0].Nodes[2], 2, true, Values);
            Values.Sort();
            IncludedHosts = new List<string>(Values);
            foreach (string Value in Values)
            {
                HostsGrid.Rows.Add(new object[]{true, Value});
            }

            IncludedTypes = new List<string>(new string[] { "Vulnerabilities", "TestLeads", "Information" });
            foreach (string Value in new string[] { "Vulnerabilities", "TestLeads", "Information" })
            {
                FindingTypesGrid.Rows.Add(new object[] { true, Value });
            }

            Values = GetValues(FindingsTree.Nodes[0].Nodes[0], 4, true);
            GetValues(FindingsTree.Nodes[0].Nodes[1], 3, true, Values);
            GetValues(FindingsTree.Nodes[0].Nodes[2], 3, true, Values);
            Values.Sort();
            IncludedTitles = new List<string>(Values);
            foreach (string Value in Values)
            {
                FindingTitlesGrid.Rows.Add(new object[] { true, Value });
            }

            FindingsTree.Nodes[0].Expand();
            FindingsTree.Nodes[0].Nodes[0].Expand();
            FindingsTree.Nodes[0].Nodes[1].Expand();
            FindingsTree.Nodes[0].Nodes[2].Expand();

            Loaded = true;

            FindingsTree.Nodes[0].Text = string.Format("All ({0})", CountSelectedNodes(FindingsTree.Nodes[0]));

        }

        private void FindingsTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                e.Node.ForeColor = Color.Black;
            }
            else
            {
                e.Node.ForeColor = Color.Silver;
                e.Node.Collapse();
            }

            UpdateFilter(e.Node);

            if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            {
                ApplyCheck(e.Node);
                FindingsTree.Nodes[0].Text = string.Format("All ({0})", CountSelectedNodes(FindingsTree.Nodes[0]));
            }
        }

        void ApplyCheck(TreeNode Node)
        {
            if (!Loaded) return;
            foreach (TreeNode CNode in Node.Nodes)
            {
                if (Node.Checked)
                {
                    if (FilteredOut(CNode))
                    {
                        continue;
                    }
                    else
                    {
                        CNode.Checked = true;
                        UpdateFilter(CNode);
                    }
                }
                else
                {
                    CNode.Checked = false;
                    CNode.Collapse();
                }
                if(CNode.Nodes.Count > 0) ApplyCheck(CNode);
            }
        }

        void UpdateFilter(TreeNode Node)
        {
            if (!Node.Checked) return;

            bool NeedsUpdate = false;

            switch (Node.Level)
            {
                case (1):
                    if (!IncludedTypes.Contains(Node.Name))
                    {
                        IncludedTypes.Add(Node.Name);
                        NeedsUpdate = true;
                    }
                    break;
                case (2):
                    if (Node.Parent.Index > 0)
                    {
                        if (!IncludedHosts.Contains(Node.Name))
                        {
                            IncludedHosts.Add(Node.Name);
                            NeedsUpdate = true;
                        }
                    }
                    break;
                case (3):
                    if (Node.Parent.Parent.Index == 0)
                    {
                        if (!IncludedHosts.Contains(Node.Name))
                        {
                            IncludedHosts.Add(Node.Name);
                            NeedsUpdate = true;
                        }
                    }
                    else
                    {
                        if (!IncludedTitles.Contains(Node.Name))
                        {
                            IncludedTitles.Add(Node.Name);
                            NeedsUpdate = true;
                        }
                    }
                    break;
                case (4):
                    if (Node.Parent.Parent.Parent.Index == 0)
                    {
                        if (!IncludedTitles.Contains(Node.Name))
                        {
                            IncludedTitles.Add(Node.Name);
                            NeedsUpdate = true;
                        }
                    }
                    else
                    {
                        if (!IncludedTitles.Contains(Node.Parent.Name))
                        {
                            IncludedTitles.Add(Node.Parent.Name);
                            NeedsUpdate = true;
                        }
                    }
                    break;
                case(5):
                    if (!IncludedTitles.Contains(Node.Parent.Name))
                    {
                        IncludedTitles.Add(Node.Parent.Name);
                        NeedsUpdate = true;
                    }
                    break;
            }
            
            if (!NeedsUpdate) return;

            foreach (DataGridViewRow Row in HostsGrid.Rows)
            {
                if (!((bool)Row.Cells[0].Value))
                {
                    if (IncludedHosts.Contains(Row.Cells[1].Value.ToString())) Row.Cells[0].Value = true;
                }
            }
            foreach (DataGridViewRow Row in FindingTypesGrid.Rows)
            {
                if (!((bool)Row.Cells[0].Value))
                {
                    if (IncludedTypes.Contains(Row.Cells[1].Value.ToString())) Row.Cells[0].Value = true;
                }
            }
            foreach (DataGridViewRow Row in FindingTitlesGrid.Rows)
            {
                if (!((bool)Row.Cells[0].Value))
                {
                    if (IncludedTitles.Contains(Row.Cells[1].Value.ToString())) Row.Cells[0].Value = true;
                }
            }
        }

        bool FilteredOut(TreeNode Node)
        {
            switch(Node.Level)
            {
                case (1):
                    if (IncludedTypes.Contains(Node.Name))
                        return false;
                    else
                        return true;
                case (2):
                    if (Node.Parent.Index > 0)
                    {
                        if (IncludedHosts.Contains(Node.Name))
                            return false;
                        else
                            return true;
                    }
                    break;
                case(3):
                    if (Node.Parent.Parent.Index == 0)
                    {
                        if (IncludedHosts.Contains(Node.Name))
                            return false;
                        else
                            return true;
                    }
                    else
                    {
                        if (IncludedTitles.Contains(Node.Name))
                            return false;
                        else
                            return true;
                    }
                case (4):
                    if (Node.Parent.Parent.Parent.Index == 0)
                    {
                        if (IncludedTitles.Contains(Node.Name))
                            return false;
                        else
                            return true;
                    }
                    break;
            }
            return false;
        }

        List<string> GetValues(TreeNode Node, int Level, bool Name)
        {
            List<string> Values = new List<string>();
            GetValues(Node, Level, Name, Values);
            return Values;
        }

        List<string> GetValues(TreeNode Node, int Level, bool Name, List<string> Values)
        {
            if (Node.Level == Level)
            {
                if (Name)
                {
                    if (!Values.Contains(Node.Name)) Values.Add(Node.Name);
                }
                else
                {
                    if (!Values.Contains(Node.Text)) Values.Add(Node.Text);
                }
            }
            else if (Node.Level + 1== Level)
            {
                if (Node.Nodes.Count > 0)
                {
                    foreach (TreeNode CNode in Node.Nodes)
                    {
                        if (Name)
                        {
                            if (!Values.Contains(CNode.Name)) Values.Add(CNode.Name);
                        }
                        else
                        {
                            if (!Values.Contains(CNode.Text)) Values.Add(CNode.Text);
                        }
                    }
                }
            }
            else if (Node.Level < Level)
            {
                if (Node.Nodes.Count > 0)
                {
                    foreach (TreeNode CNode in Node.Nodes)
                    {
                        GetValues(CNode, Level, Name, Values);
                    }
                }
            }
            return Values;
        }

        private void HostsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= HostsGrid.Rows.Count) return;

            if ((bool)HostsGrid.Rows[e.RowIndex].Cells[0].Value)
            {
                HostsGrid.Rows[e.RowIndex].Cells[0].Value = false;
                IncludedHosts.Remove(HostsGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            else
            {
                HostsGrid.Rows[e.RowIndex].Cells[0].Value = true;
                IncludedHosts.Add(HostsGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            ApplyNewFilter();
            FindingsTree.Nodes[0].Text = string.Format("All ({0})", CountSelectedNodes(FindingsTree.Nodes[0]));
        }

        private void FindingTypesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= FindingTypesGrid.Rows.Count) return;

            if ((bool)FindingTypesGrid.Rows[e.RowIndex].Cells[0].Value)
            {
                FindingTypesGrid.Rows[e.RowIndex].Cells[0].Value = false;
                IncludedTypes.Remove(FindingTypesGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            else
            {
                FindingTypesGrid.Rows[e.RowIndex].Cells[0].Value = true;
                IncludedTypes.Add(FindingTypesGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            ApplyNewFilter();
            FindingsTree.Nodes[0].Text = string.Format("All ({0})", CountSelectedNodes(FindingsTree.Nodes[0]));
        }

        private void FindingTitlesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= FindingTitlesGrid.Rows.Count) return;

            if ((bool)FindingTitlesGrid.Rows[e.RowIndex].Cells[0].Value)
            {
                FindingTitlesGrid.Rows[e.RowIndex].Cells[0].Value = false;
                IncludedTitles.Remove(FindingTitlesGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            else
            {
                FindingTitlesGrid.Rows[e.RowIndex].Cells[0].Value = true;
                IncludedTitles.Add(FindingTitlesGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            ApplyNewFilter();
            FindingsTree.Nodes[0].Text = string.Format("All ({0})", CountSelectedNodes(FindingsTree.Nodes[0]));
        }

        void ApplyNewFilter()
        {
            if (!Loaded) return;
            
            ApplyFilter(FindingsTree.Nodes[0], 1, true, IncludedTypes);
            
            ApplyFilter(FindingsTree.Nodes[0].Nodes[0], 3, true, IncludedHosts);
            ApplyFilter(FindingsTree.Nodes[0].Nodes[1], 2, true, IncludedHosts);
            ApplyFilter(FindingsTree.Nodes[0].Nodes[2], 2, true, IncludedHosts);

            ApplyFilter(FindingsTree.Nodes[0].Nodes[0], 4, true, IncludedTitles);
            ApplyFilter(FindingsTree.Nodes[0].Nodes[1], 3, true, IncludedTitles);
            ApplyFilter(FindingsTree.Nodes[0].Nodes[2], 3, true, IncludedTitles);
        }

        void ApplyFilter(TreeNode Node, int Level, bool Name, List<string> Values)
        {
            if (Node.Level == Level)
            {
                if (Name)
                {
                    if (Values.Contains(Node.Name))
                    {
                        if (!Node.Checked && (Node.Parent == null || Node.Parent.Checked))
                        {
                            Node.Checked = true;
                            ApplyCheck(Node);
                        }
                    }
                    else
                    {
                        if (Node.Checked)
                        {
                            Node.Checked = false;
                            ApplyCheck(Node);
                        }
                    }
                }
                else
                {
                    if (Values.Contains(Node.Text))
                    {
                        if (!Node.Checked && (Node.Parent == null || Node.Parent.Checked))
                        {
                            Node.Checked = true;
                            ApplyCheck(Node);
                        }
                    }
                    else
                    {
                        if (Node.Checked)
                        {
                            Node.Checked = false;
                            ApplyCheck(Node);
                        }
                    }
                }
            }
            else if (Node.Level + 1 == Level)
            {
                if (Node.Nodes.Count > 0)
                {
                    foreach (TreeNode CNode in Node.Nodes)
                    {
                        if (Name)
                        {
                            if (Values.Contains(CNode.Name))
                            {
                                if (!CNode.Checked && (CNode.Parent == null || CNode.Parent.Checked))
                                {
                                    CNode.Checked = true;
                                    ApplyCheck(CNode);
                                }
                            }
                            else
                            {
                                if (CNode.Checked)
                                {
                                    CNode.Checked = false;
                                    ApplyCheck(CNode);
                                }
                            }
                        }
                        else
                        {
                            if (Values.Contains(CNode.Text))
                            {
                                if (!CNode.Checked && (CNode.Parent == null || CNode.Parent.Checked))
                                {
                                    CNode.Checked = true;
                                    ApplyCheck(CNode);
                                }
                            }
                            else
                            {
                                if (CNode.Checked)
                                {
                                    CNode.Checked = false;
                                    ApplyCheck(CNode);
                                }
                            }
                        }
                    }
                }
            }
            else if (Node.Level < Level)
            {
                if (Node.Nodes.Count > 0)
                {
                    foreach (TreeNode CNode in Node.Nodes)
                    {
                        ApplyFilter(CNode, Level, Name, Values);
                    }
                }
            }
        }

        int CountSelectedNodes(TreeNode Node)
        {
            int Count = 0;
            if (Node.Checked && ((Node.Level == 4 && (Node.Parent.Parent.Parent.Index == 1 || Node.Parent.Parent.Parent.Index == 2)) || (Node.Level == 5))) Count++;
            foreach (TreeNode CNode in Node.Nodes)
            {
                Count = Count + CountSelectedNodes(CNode);
            }
            return Count;
        }

        Dictionary<int, FindingInfoList> GetFindingIds()
        {
            Dictionary<int, FindingInfoList> FindingIds = new Dictionary<int, FindingInfoList>();
            GetFindingIds(FindingsTree.Nodes[0], FindingIds);
            return FindingIds;
        }

        void GetFindingIds(TreeNode Node, Dictionary<int, FindingInfoList> FindingIds)
        {
            if (Node.Checked && ((Node.Level == 4 && (Node.Parent.Parent.Parent.Index == 1 || Node.Parent.Parent.Parent.Index == 2)) || (Node.Level == 5)))
            {
                try
                {
                    int ID = Int32.Parse(Node.Name.Replace("+", "").Replace("-", "").Replace(" ", ""));
                    
                    string Host = Node.Parent.Parent.Name;
                    string Title = Node.Parent.Name;
                    string Severity = "";
                    string Confidence = "";
                    string Type = "";

                    switch (Node.Level)
                    {
                        case(4):
                            switch (Node.Parent.Parent.Parent.Index)
                            {
                                case(1):
                                    Type = "Info";
                                    break;
                                case(2):
                                    Type = "Leads";
                                    break;
                            }
                            Severity = Node.Parent.Parent.Parent.Name;
                            break;
                        case (5):
                            Type = "Vuln";
                            if (Node.Name.Contains("+++"))
                            {
                                Confidence = "High";
                            }
                            else if (Node.Name.Contains("++-"))
                            {
                                Confidence = "Medium";
                            }
                            else
                            {
                                Confidence = "Low";
                            }
                            break;
                    }

                    FindingIds[ID] = new FindingInfoList(Type, Host, Severity, Confidence, Title);
                }
                catch { }
            }
            foreach (TreeNode CNode in Node.Nodes)
            {
                GetFindingIds(CNode, FindingIds);
            }
        }

        private void GenerateReportBtn_Click(object sender, EventArgs e)
        {
            GenerateReportBtn.Enabled = false;
            
            LinksPanel.Visible = false;

            if (GenerateReportBtn.Text == GenerateReportBtnTexts["Running"])
            {
                try
                {
                    ReportingThread.Abort();
                }
                catch
                { }
                ResetUiStatus();
            }
            else
            {
                FindingsId = GetFindingIds();
                if (FindingsId.Count == 0)
                {
                    MessageBox.Show("No findings are available for reporting. Make sure there are some findings in this report or change the filter.");
                }
                else
                {
                    FindingsTree.Enabled = false;
                    HostsGrid.Enabled = false;
                    FindingTypesGrid.Enabled = false;
                    FindingTitlesGrid.Enabled = false;

                    ReportingPB.Minimum = 0;
                    ReportingPB.Maximum = FindingsId.Count;
                    ReportingPB.Step = 1;
                    ReportingPB.Visible = true;
                    try
                    {
                        ReportingThread.Abort();
                    }
                    catch { }
                    ReportingThread = new Thread(CreateReport);
                    ReportingThread.Start();
                    GenerateReportBtn.Text = GenerateReportBtnTexts["Running"];
                }
            }

            GenerateReportBtn.Enabled = true;
        }

        delegate void StepProgressBar_d();
        void StepProgressBar()
        {
            if (ReportingPB.InvokeRequired)
            {
                StepProgressBar_d CALL_d = new StepProgressBar_d(StepProgressBar);
                ReportingPB.Invoke(CALL_d, new object[] { });
            }
            else
            {
                ReportingPB.PerformStep();
            }
        }

        delegate void ShowHideLinkPanel_d(bool Show);
        void ShowHideLinkPanel(bool Show)
        {
            if (LinksPanel.InvokeRequired)
            {
                ShowHideLinkPanel_d CALL_d = new ShowHideLinkPanel_d(ShowHideLinkPanel);
                ReportingPB.Invoke(CALL_d, new object[] { Show });
            }
            else
            {
                LinksPanel.Visible = Show;
            }
        }

        delegate void ResetUiStatus_d();
        void ResetUiStatus()
        {
            if (LinksPanel.InvokeRequired)
            {
                ResetUiStatus_d CALL_d = new ResetUiStatus_d(ResetUiStatus);
                ReportingPB.Invoke(CALL_d, new object[] { });
            }
            else
            {
                ReportingPB.Visible = false;
                LinksPanel.Visible = false;
                GenerateReportBtn.Text = GenerateReportBtnTexts["Stopped"];
                FindingsTree.Enabled = true;
                HostsGrid.Enabled = true;
                FindingTypesGrid.Enabled = true;
                FindingTitlesGrid.Enabled = true;
            }
        }

        void CreateReport()
        {
            try
            {
                DoCreateReport();
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp)
            {
                ResetUiStatus();
                IronException.Report("Error Generating Report", Exp);
                MessageBox.Show("An error occured when generating this report. Error details are logged under the 'Exceptions' part of the project tree");
            }
        }

        void DoCreateReport()
        {
            ReportedFindingsCount = 0;
            ReportedFindingsCounter = 0;

            StringBuilder HB = new StringBuilder();
            StringBuilder HSB = new StringBuilder();
            StringBuilder HCB = new StringBuilder();
            StringBuilder RB = new StringBuilder();
            
            string HtmlStart = @"
<html>
<head>
<title>IronWASP Security Analysis Report</title>
<style>
body
{
    font-family: Palatino, ""Palatino Linotype"", ""Palatino LT STD"", ""Book Antiqua"", Georgia, serif;
    margin: 0px;
    padding: 0px;
    background-color: #555;
    overflow: auto;
}
#report_header
{
    background-color: #75D1FF;
    margin-bottom: 10px;
}
#report_title
{
    background-color: #555;
    padding-top: 5px;
    padding-bottom: 5px;
    font-size: xx-large;
    color: #FFF;
    text-align: center;
}
#report_sub_title
{
    padding-top: 5px;
    padding-bottom: 5px;
    color: #555;
    text-align: center;
}
#report_sub_title a
{
    color: #555;
    font-weight: bold;
}
#report_footer
{
    color: #FFF;
    text-align: center;
    font-size: small;
    padding-bottom: 10px;
}
.report
{    
     width: 1000px;
     margin-left: 250px;
     color: #FFF;
}
#overview
{
    color: #333;
     background-color: #FFF;
     border-style: outset;
     border-color: #555;
     padding: 20px;
     margin: 10px;
     overflow: auto;
}
#index
{
    color: #333;
    background-color: #FFF;
    border-style: outset;
    border-color: #555;
    padding: 20px;
    margin: 10px;
    overflow: auto;
}
#index a
{
    text-decoration: none;
}
#index ul
{
    list-style-type: square;
}
#index ul li
{
    padding-top: 15px;
    padding-bottom: 3px;
}
#index ol li
{
    padding: 3px;
}
.index_finding_url
{
    color: #333;
}
.section_title
{
    color: #555;
    border-color: #75D1FF;;
    border-style: solid;
    border-width: thick;
    background-color: #75D1FF;;
    padding: 10px;
    margin: 10px;
    margin-top: 20px;
    font-size: x-large;
    font-weight: bold;
    overflow: auto;
}
.finding
{
    color: #333;
     background-color: #FFF;
     border-style: outset;
     border-color: #555;
     padding: 20px;
     margin: 10px;
     overflow: auto;
}
.finding table
{
    padding: 5px;
    margin: 5px;
    margin-left: 20px;
}
.finding td
{
    margin-left: 10px;
}

.trigger
{
    color: #333;
    border-style: ridge;
    border-color: #AAA;
    margin: 10px;
    padding: 10px;
    overflow: auto;
}
.trigger_desc
{
    margin-top: 20px;
    text-align: center;
    color: #50D;
}
#quick_nav
{
    position: fixed;
    width: 230px;
    overflow: auto;
}

#quick_nav a
{
    color: #000;
    text-decoration: none;
}

#quick_nav a:hover
{
    color: #75D1FF;
}
.quick_nav_link
{
    width: 220px;
    background-color: #FFF;
    padding: 2px;
    padding-left: 5px; 
    margin: 2px;
    margin-top: 5px;
}
.host
{
    color: #555;
    border-color: #75D1FF;;
    border-style: solid;
    border-width: thick;
    background-color: #75D1FF;
    padding: 10px;
    margin: 10px;
    margin-top: 20px;
    font-size: x-large;
    font-weight: bold;
    overflow: auto;
}
.high_finding_title
{
    background-color: red;
    color: #FFF;
    font-size: large;
    font-weight: bold;
    padding: 10px;
    margin-bottom: 10px;
    overflow: auto;
}
.medium_finding_title
{
    background-color: orange;
    color: #000;
    font-size: large;
    font-weight: bold;
    padding: 10px;
    margin-bottom: 10px;
    overflow: auto;
}
.low_finding_title
{
    background-color: yellow;
    color: #000;
    font-size: large;
    font-weight: bold;
    padding: 10px;
    margin-bottom: 10px;
    overflow: auto;
}
.info_finding_title
{
    background-color: blue;
    color: #FFF;
    font-size: large;
    font-weight: bold;
    padding: 10px;
    margin-bottom: 10px;
    overflow: auto;
}
.lead_finding_title
{
    background-color: green;
    color: #FFF;
    font-size: large;
    font-weight: bold;
    padding: 10px;
    margin-bottom: 10px;
    overflow: auto;
}
.affected_url
{
    color: #0077FF;
    text-decoration: underline;
    overflow: auto;
}
.reason_title
{
    background-color: #3C3;
    padding: 3px;
    margin-top: 10px;
    margin-bottom: 5px;
    font-weight: bold;
}
.finding_nav_links
{
    color: #75D1FF;
}
.finding_nav_links a
{
    font-weight: bold;
    color: #75D1FF;
    text-decoration: none;
    padding-left: 10px;
    padding-right: 10px;
    padding-top: 1px;
    padding-bottom: 1px;
    margin: 10px;
    margin-left: 0px;
    border-color: #75D1FF;
    border-style: solid;
    border-width: 1px;
}
.finding_nav_links a:hover
{
    color: #FFF;
    text-decoration: none;
    background-color: #75D1FF;
}
.host_nav_links
{
    color: #75D1FF;
}
.host_nav_links a
{
    font-weight: bold;
    color: #75D1FF;
    text-decoration: none;
    padding-left: 10px;
    padding-right: 10px;
    padding-top: 1px;
    padding-bottom: 1px;
    margin: 10px;
    border-color: #75D1FF;
    border-style: solid;
    border-width: 1px;

}
.host_nav_links a:hover
{
    color: #FFF;
    text-decoration: none;
    background-color: #75D1FF;
}

.desc
{
    margin-top: 15px;
    margin-bottom: 15px;
    overflow: auto;
}
.desc_title
{
    font-weight: bold;
    padding-bottom: 10px;
    text-decoration: underline;
}
.reason
{
    margin-top: 15px;
    margin-bottom: 15px;
    overflow: auto;
}
.reason_section_title
{
    font-weight: bold;
    padding-bottom: 10px;
    text-decoration: underline;
}
.fpa_check
{
    margin-top: 10px;
    margin-bottom: 10px;
    overflow: auto;
}
.fpa_check_title
{
    font-weight: bold;
    padding-bottom: 5px;
    text-decoration: underline;
    color: #3C3;
}
th
{
    background-color: #75D1FF;
    color: #555;
    padding-top: 10px;
    padding-bottom: 10px;
    text-align: center;
}
.t_high
{
    color:  #FF0000;
    border-style: none;
    font-weight: bold;
    text-align: center;
}
.t_med
{
    color:  #FF9900;
    border-style: none;
    font-weight: bold;
    text-align: center;
}
.t_low
{
    color:  #B8B800;
    border-style: none;
    font-weight: bold;
    text-align: center;
}
.t_info
{
    color: #0099FF;
    border-style: none;
    font-weight: bold;
    text-align: center;
}
.t_lead
{
    color: #339933;
    border-style: none;
    font-weight: bold;
    text-align: center;
}
.t_total
{
    color: #000;
    border-style: none;
    font-weight: bold;
    text-align: center;
}
.t_high_conf
{
    background-color: #000;
    color: #FFF;
    margin-bottom: 20px;
    text-align: center;
}
.t_med_conf
{
    background-color: #444;
    color: #AAA;
    margin-bottom: 20px;
    text-align: center;
}
.t_low_conf
{
    background-color: #999;
    color: #333;
    margin-bottom: 20px;
    text-align: center;
            
}
.t_host
{
    width: 400px;
    color: #0077FF;
    text-decoration: underline;
    text-align: left;
    overflow: auto;
}

.legend
{
    font-size: small;
}
.legend td
{
    font-size: small;
}

.icr { color: red; }
.icb { color: #0077FF; }
.icy { color: #B8B800; }
.ico { color: orange; }
.icg { color: #3C3; }

.hlr { background-color: red; }
.hlb { background-color: #0077FF; }
.hly { background-color: #B8B800; }
.hlo { background-color: orange; }
.hlg { background-color: #3C3; }
    
.ihh {font-weight: bold; color: #0077FF; text-decoration: underline;}
.fname {font-weight: bold; }
</style>
</head>
<body>
<div id='report_header'>
<div id='report_title'>IronWASP Security Analysis Report</div>
<div id='report_sub_title'>Report based on the analysis performed by the open source web security software, <a href='https://ironwasp.org'>IronWASP</a></div>
</div>
";
            HCB.AppendLine(@"
<span id='quick_nav'>
    <a href='#overview'><div class='quick_nav_link'>Overview</div></a>
    <a href='#index'><div class='quick_nav_link'>Index</div></a>
");

            RB.AppendLine(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            RB.AppendLine(Tools.RtfSafe("<i<br>><i<br>>This report contains the details of all the findings and is meant for easy Word document creation. If you are looking for a report to use as reference then use the HTML format, it has far better visual presentation and information arrangement.<i<br>><i<br>>"));

            Dictionary<string, long> DictForRanking = new Dictionary<string, long>();
            foreach (string Host in IncludedHosts)
            {
                DictForRanking[Host] = 0;

                foreach (int i in FindingsId.Keys)
                {
                    if (FindingsId[i].Host == Host)
                    {
                        if (FindingsId[i].Type == "Vuln")
                        {
                            if (FindingsId[i].Severity == "High")
                            {
                                DictForRanking[Host] = DictForRanking[Host] + 100000000;
                            }
                            else if (FindingsId[i].Severity == "Medium")
                            {
                                DictForRanking[Host] = DictForRanking[Host] + 1000000;
                            }
                            else
                            {
                                DictForRanking[Host] = DictForRanking[Host] + 10000;
                            }
                        }
                        else if (FindingsId[i].Type == "Info")
                        {
                            DictForRanking[Host] = DictForRanking[Host] + 100;
                        }
                        else if (FindingsId[i].Type == "Leads")
                        {
                            DictForRanking[Host] = DictForRanking[Host] + 1;
                        }
                    }
                }
            }

            long[] TempScoreHolder = (new List<long>(DictForRanking.Values)).ToArray();
            Array.Sort(TempScoreHolder);
            Array.Reverse(TempScoreHolder);

            List<string> HostByRank = new List<string>();
            foreach (long Score in TempScoreHolder)
            {
                foreach (string Host in DictForRanking.Keys)
                {
                    if (DictForRanking[Host] == Score)
                    {
                        if (!HostByRank.Contains(Host)) HostByRank.Add(Host);
                    }
                }
            }


            Dictionary<string, FindingStatsHolder> FindingStatsForSummary = new Dictionary<string, FindingStatsHolder>();
            Dictionary<string, int> HostStatsForSummary = new Dictionary<string, int>();
            //Type, Host, Severity, Confidence, Title
            int HostCount = 0;
            foreach (string Host in HostByRank)
            {
                FindingStatsForSummary[Host] = new FindingStatsHolder();

                List<int> High = new List<int>();
                List<int> Medium = new List<int>();
                List<int> Low = new List<int>();
                List<int> Leads = new List<int>();
                List<int> Infos = new List<int>();

                foreach (int i in FindingsId.Keys)
                {
                    if (FindingsId[i].Host == Host)
                    {
                        if (FindingsId[i].Type == "Vuln")
                        {
                            if (FindingsId[i].Severity == "High")
                            {
                                High.Add(i);
                            }
                            else if (FindingsId[i].Severity == "Medium")
                            {
                                Medium.Add(i);
                            }
                            else
                            {
                                Low.Add(i);
                            }
                        }
                        else if (FindingsId[i].Type == "Info")
                        {
                            Infos.Add(i);
                        }
                        else if(FindingsId[i].Type == "Leads")
                        {
                            Leads.Add(i);
                        }
                    }
                }

                if ((High.Count + Medium.Count + Low.Count + Infos.Count + Leads.Count) > 0)
                {
                    HostCount++;
                    HostStatsForSummary[Host] = HostCount;
                    HCB.AppendLine(string.Format("<a href='#host{0}'><div class='quick_nav_link'>{1}</div></a>", HostCount, Tools.HtmlEncode(Host)));
                    
                    HB.AppendLine(string.Format("<div class='host' id='host{0}'>{1}</div>", HostCount, Tools.HtmlEncode(Host)));
                    HB.Append("<div class='host_nav_links'>");
                    if (HostCount > 1)
                    {
                        HB.Append(string.Format("<a href='#host{0}'>&lt;&lt;&lt;</a>", HostCount - 1));
                    }
                    if (IncludedHosts.Count > HostCount)
                    {
                        HB.Append(string.Format("<a href='#host{0}'>&gt;&gt;&gt;</a>", HostCount + 1));
                    }
                    HB.Append("</div>");

                    RB.AppendLine(Tools.RtfSafe(string.Format("<i<br>><i<h>>{0}<i</h>><i<br>><i<br>><i<br>>", Host)));

                    AddFindings(High, HB, RB, FindingStatsForSummary[Host]);
                    AddFindings(Medium, HB, RB, FindingStatsForSummary[Host]);
                    AddFindings(Low, HB, RB, FindingStatsForSummary[Host]);
                    AddFindings(Infos, HB, RB, FindingStatsForSummary[Host]);
                    AddFindings(Leads, HB, RB, FindingStatsForSummary[Host]);
                }
            }
            HCB.AppendLine("</span>");
            HB.AppendLine("</div>");
            HB.AppendLine(string.Format(@"
<div id='report_footer'>
    This report was generated by IronWASP's Reporting Engine on {0} at {1}
</div>
", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString()));
            HB.AppendLine("</body></html>");

            StringBuilder FHB = new StringBuilder();
            FHB.AppendLine(HtmlStart);
            FHB.AppendLine(HCB.ToString());
            FHB.AppendLine(GetOverviewAndIndex(FindingStatsForSummary, HostByRank));
            
            
            FHB.AppendLine(HB.ToString());

            RB.AppendLine("}");

            HtmlReport = FHB.ToString().Replace("lava@ironwasp.org", "lava<span class='inv'></span>@<span class='inv'></span>ironwasp<span class='inv'></span>.<span class='inv'></span>org");
            RtfReport = RB.ToString();

            ResetUiStatus();
            ShowHideLinkPanel(true);
        }

        void AddFindings(List<int> Ids, StringBuilder HB, StringBuilder RB, FindingStatsHolder Stat)
        {
            Dictionary<string, List<int>> ByTitleDict = new Dictionary<string, List<int>>();

            foreach (int i in Ids)
            {
                if (ByTitleDict.ContainsKey(FindingsId[i].Title))
                {
                    ByTitleDict[FindingsId[i].Title].Add(i);
                }
                else
                {
                    ByTitleDict[FindingsId[i].Title] = new List<int>(){i};
                }
            }
            foreach (string Title in ByTitleDict.Keys)
            {
                string Hash = "";
                foreach (int Id in ByTitleDict[Title])
                {
                    try
                    {
                        Hash = AddFinding(Id, HB, RB, Hash, Stat);
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception Exp)
                    {
                        IronException.Report(string.Format("Error adding Finding ID {0} to the report", Id), Exp);
                    }
                }
            }
        }

        string AddFinding(int Id, StringBuilder HB, StringBuilder RB, string LastHash, FindingStatsHolder Stat)
        {
            ReportedFindingsCount++;

            Finding F = IronDB.GetPluginResultFromDB(Id);
            string FindingHash = GetFindingHash(F);
            if (FindingHash == LastHash)
            {
                StepProgressBar();
                return FindingHash;
            }
            ReportedFindingsCounter++;

            HB.AppendLine(string.Format("<div class='finding' id='finding{0}'>", ReportedFindingsCounter));

            string AffectedUrl = "";
            if (F.FinderType == "ActivePlugin")
            {
                if (F.BaseRequest != null)
                {
                    AffectedUrl = F.BaseRequest.Url;
                }
            }
            else
            {
                if (F.Triggers.Count > 0)
                {
                    Trigger T = F.Triggers.GetTrigger(0);
                    if (T.Request != null)
                    {
                        AffectedUrl =  T.Request.Url;
                    }
                }
            }

            //Title
            if (F.Type == FindingType.Vulnerability)
            {
                if (F.Severity == FindingSeverity.High)
                {
                    HB.Append("<div class='high_finding_title'>"); HB.Append(Tools.HtmlEncode(F.Title)); HB.AppendLine("</div>");
                    RB.Append(Tools.RtfSafe(string.Format("<i<cr>><i<b>>{0}<i</b>><i</cr>><i<br>><i<br>>", F.Title)));

                    if (F.Confidence == FindingConfidence.High)
                    {
                        Stat.HighSevHighConf = Stat.HighSevHighConf + 1;
                        if (!Stat.HighSevHighConfTitles.ContainsKey(F.Title)) Stat.HighSevHighConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.HighSevHighConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                    else if (F.Confidence == FindingConfidence.Medium)
                    {
                        Stat.HighSevMedConf = Stat.HighSevMedConf + 1;
                        if (!Stat.HighSevMedConfTitles.ContainsKey(F.Title)) Stat.HighSevMedConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.HighSevMedConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                    else
                    {
                        Stat.HighSevLowConf = Stat.HighSevLowConf + 1;
                        if (!Stat.HighSevLowConfTitles.ContainsKey(F.Title)) Stat.HighSevLowConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.HighSevLowConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                }
                else if (F.Severity == FindingSeverity.Medium)
                {
                    HB.Append("<div class='medium_finding_title'>"); HB.Append(Tools.HtmlEncode(F.Title)); HB.AppendLine("</div>");
                    RB.Append(Tools.RtfSafe(string.Format("<i<co>><i<b>>{0}<i</b>><i</co>><i<br>>", F.Title)));

                    if (F.Confidence == FindingConfidence.High)
                    {
                        Stat.MedSevHighConf = Stat.MedSevHighConf + 1;
                        if (!Stat.MedSevHighConfTitles.ContainsKey(F.Title)) Stat.MedSevHighConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.MedSevHighConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                    else if (F.Confidence == FindingConfidence.Medium)
                    {
                        Stat.MedSevMedConf = Stat.MedSevMedConf + 1;
                        if (!Stat.MedSevMedConfTitles.ContainsKey(F.Title)) Stat.MedSevMedConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.MedSevMedConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                    else
                    {
                        Stat.MedSevLowConf = Stat.MedSevLowConf + 1;
                        if (!Stat.MedSevLowConfTitles.ContainsKey(F.Title)) Stat.MedSevLowConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.MedSevLowConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                }
                else
                {
                    HB.Append("<div class='low_finding_title'>"); HB.Append(Tools.HtmlEncode(F.Title)); HB.AppendLine("</div>");
                    RB.Append(Tools.RtfSafe(string.Format("<i<cy>><i<b>>{0}<i</b>><i</cy>><i<br>>", F.Title)));

                    if (F.Confidence == FindingConfidence.High)
                    {
                        Stat.LowSevHighConf = Stat.LowSevHighConf + 1;
                        if (!Stat.LowSevHighConfTitles.ContainsKey(F.Title)) Stat.LowSevHighConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.LowSevHighConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                    else if (F.Confidence == FindingConfidence.Medium)
                    {
                        Stat.LowSevMedConf = Stat.LowSevMedConf + 1;
                        if (!Stat.LowSevMedConfTitles.ContainsKey(F.Title)) Stat.LowSevMedConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.LowSevMedConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                    else
                    {
                        Stat.LowSevLowConf = Stat.LowSevLowConf + 1;
                        if (!Stat.LowSevLowConfTitles.ContainsKey(F.Title)) Stat.LowSevLowConfTitles[F.Title] = new Dictionary<int, string>();
                        Stat.LowSevLowConfTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
                    }
                }
            }
            else if (F.Type == FindingType.Information)
            {
                HB.Append("<div class='info_finding_title'>"); HB.Append(Tools.HtmlEncode(F.Title)); HB.AppendLine("</div>");
                RB.Append(Tools.RtfSafe(string.Format("<i<cb>><i<b>>{0}<i</b>><i</cb>><i<br>>", F.Title)));

                Stat.Info = Stat.Info + 1;
                if (!Stat.InfoTitles.ContainsKey(F.Title)) Stat.InfoTitles[F.Title] = new Dictionary<int, string>();
                Stat.InfoTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
            }
            else
            {
                HB.Append("<div class='lead_finding_title'>"); HB.Append(Tools.HtmlEncode(F.Title)); HB.AppendLine("</div>");
                RB.Append(Tools.RtfSafe(string.Format("<i<cg>><i<b>>{0}<i</b>><i</cg>><i<br>>", F.Title)));

                Stat.Leads = Stat.Leads + 1;
                if (!Stat.LeadsTitles.ContainsKey(F.Title)) Stat.LeadsTitles[F.Title] = new Dictionary<int, string>();
                Stat.LeadsTitles[F.Title].Add(ReportedFindingsCounter, AffectedUrl);
            }

            //Navigation Links
            HB.Append("<div class='finding_nav_links'>");
            if (ReportedFindingsCount > 1)
            {
                HB.Append(string.Format("<a href='#finding{0}'>&lt;&lt;&lt;</a>", ReportedFindingsCounter - 1));
            }
            if (ReportedFindingsCount < FindingsId.Count)
            {
                HB.Append(string.Format("<a href='#finding{0}'>&gt;&gt;&gt;</a>", ReportedFindingsCounter + 1));
            }
            HB.AppendLine("</div>");
            
            //Content
            if (F.Type == FindingType.Vulnerability)
            {
                HB.AppendLine("<table cellpadding='2' cellspacing='2'>");
                
                HB.Append("<tr>");
                HB.Append("<div class='type'><td><span class='fname'>Type:</td><td></span>Vulnerability</td></div>");
                RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Type: <i</b>><i</cb>>Vulnerability<i<br>>"));
                HB.AppendLine("</tr>");

                HB.Append("<tr>");
                HB.Append("<div class='severity'><td><span class='fname'>Severity:</span></td>");
                if (F.Severity == FindingSeverity.High)
                {
                    HB.Append("<td><span class='icr'>High");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Severity: <i</b>><i</cb>><i<cr>>High<i</cr>><i<br>>"));
                }
                else if (F.Severity == FindingSeverity.Medium)
                {
                    HB.Append("<td><span class='ico'>Medium");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Severity: <i</b>><i</cb>><i<co>>Medium<i</co>><i<br>>"));
                }
                else
                {
                    HB.Append("<td><span class='icy'>Low");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Severity: <i</b>><i</cb>><i<cy>>Low<i</cy>><i<br>>"));
                }
                HB.Append("</span></td></div>");
                HB.AppendLine("</tr>");

                HB.Append("<tr>");
                HB.Append("<div class='confidence'><td><span class='fname'>Confidence:</span></td>");
                if (F.Confidence == FindingConfidence.High)
                {
                    HB.Append("<td>High</td>");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Confidence: <i</b>><i</cb>>High<i<br>>"));
                }
                else if (F.Confidence == FindingConfidence.Medium)
                {
                    HB.Append("<td>Medium</td>");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Confidence: <i</b>><i</cb>>Medium<i<br>>"));
                }
                else
                {
                    HB.Append("<td>Low</td>");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Confidence: <i</b>><i</cb>>Low<i<br>>"));
                }
                HB.AppendLine("</div>");
                HB.AppendLine("</tr>");
            }
            else if (F.Type == FindingType.Information)
            {
                HB.AppendLine("<table cellpadding='2' cellspacing='2'>");
                HB.AppendLine("<tr><div class='type'><td><span class='fname'>Type:</span></td><td><span class='icb'>Information</span></td></div></tr>");
                RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Type: <i</b>><i</cb>>Information<i<br>>"));
            }
            else
            {
                HB.AppendLine("<table cellpadding='2' cellspacing='2'>");
                HB.AppendLine("<tr><div class='type'><td><span class='fname'>Type:</span></td><td><span class='icg'>Test Lead</span></td></div></tr>");
                RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Type: <i</b>><i</cb>>Test lead<i<br>>"));
            }

            if (F.FinderType.Length > 0)
            {
                if (F.FinderType == "ActivePlugin")
                {
                    HB.AppendLine("<tr><div class='type'><td><span class='fname'>Found By:</span></td><td>Active Scanning</td></div></tr>");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Found By: <i</b>><i</cb>>Active Scanning<i<br>>"));
                }
                else if (F.FinderType == "PassivePlugin")
                {
                    HB.AppendLine("<tr><div class='type'><td><span class='fname'>Found By:</span></td><td>Passive Analysis</td></div></tr>");
                    RB.Append(Tools.RtfSafe("<i<cb>><i<b>>Found By: <i</b>><i</cb>>Passive Analysis<i<br>>"));
                }
                else
                {
                    HB.AppendLine(string.Format("<tr><div class='type'><td><span class='fname'>Found By:</span></td><td>{0}</td></div></tr>", Tools.HtmlEncode(F.FinderName)));
                    RB.Append(Tools.RtfSafe(string.Format("<i<cb>><i<b>>Found By: <i</b>><i</cb>>{0}<i<br>>", F.FinderName)));
                }
            }
            HB.AppendLine("</table>");
            HB.AppendLine("<br>");
            HB.AppendLine("<table cellpadding='2' cellspacing='2'>");
            HB.AppendLine(string.Format("<tr><div class='affected_host'><td><span class='fname'>Affected Site:</span></td><td>{0}</td></div></tr>", Tools.HtmlEncode(F.AffectedHost)));
            if (F.FinderType == "ActivePlugin")
            {
                if (F.BaseRequest != null)
                {
                    HB.AppendLine(string.Format("<tr><div class='affected_url'><td><span class='fname'>Affected Url:</span></td><td><span class='affected_url'>{0}</span></td></div></tr>", Tools.HtmlEncode(F.BaseRequest.Url)));
                }
                HB.AppendLine(string.Format("<tr><div class='affected_parameter'><td><span class='fname'>Affected Parameter:</span></td><td>{0}</td></div></tr>", Tools.HtmlEncode(F.AffectedParameter)));
                RB.Append(Tools.RtfSafe(string.Format("<i<cb>><i<b>>Affected Parameter: <i</b>><i</cb>>{0}<i<br>>", F.AffectedParameter)));
                HB.AppendLine(string.Format("<tr><div class='affected_section'><td><span class='fname'>Parameter Location:</span></td><td>{0}</td></div></tr>", Tools.HtmlEncode(F.AffectedSection)));
                RB.Append(Tools.RtfSafe(string.Format("<i<cb>><i<b>>Affected Section: <i</b>><i</cb>>{0}<i<br>>", F.AffectedSection)));
            }
            else
            {
                if (F.Triggers.Count > 0)
                {
                    Trigger T = F.Triggers.GetTrigger(0);
                    if (T.Request != null)
                    {
                        HB.AppendLine(string.Format("<tr><div class='affected_url'><td><span class='fname'>Affected Url:</span></td><td><span class='affected_url'>{0}</span></td></div></tr>", Tools.HtmlEncode(T.Request.Url)));
                    }
                }
            }

            HB.AppendLine("</table>");

            HB.Append("<div class='desc'><div class='desc_title'>Description:</div>");
            HB.Append(Tools.ConvertForHtmlReport(F.Summary));
            RB.Append(Tools.RtfSafe(string.Format("<i<cb>><i<b>>Summary: <i</b>><i</cb>><i<br>>{0}<i<br>><i<br>><i<br>>", F.Summary)));
            HB.AppendLine("</div>");

            if (F.FinderType == "ActivePlugin")
            {
                if (F.Reasons.Count > 0)
                {
                    HB.AppendLine("<div class='reason_section_title'>Reasons:</div>");
                    RB.Append(Tools.RtfSafe("<i<hh>>Reasons: <i</hh>><i<br>><i<br>>"));
                }
                if (F.Reasons.Count == 1)
                {
                    HB.AppendLine("IronWASP has reported this issue because of the following reason:<br>");
                    RB.Append(Tools.RtfSafe("IronWASP has reported this issue because of the following reason:<i<br>><i<br>>"));
                }
                else if (F.Reasons.Count > 1)
                {
                    HB.AppendLine("IronWASP has reported this issue because of the following reasons:<br>");
                    RB.Append(Tools.RtfSafe("IronWASP has reported this issue because of the following reasons:<i<br>><i<br>>"));
                }
                List<int> TriggerIdsAssociatedWithReasons = new List<int>();
                for (int i = 0; i < F.Reasons.Count; i++)
                {
                    if (F.Reasons.Count > 1)
                    {
                        HB.AppendLine(string.Format("<div class='reason'><div class='reason_title'>Reason {0}:</div>", i + 1));
                        RB.Append(Tools.RtfSafe(string.Format("<i<cg>><i<b>>Reason {0}: <i</b>><i</cg>><i<br>><i<br>>", i + 1)));
                    }
                    else
                    {
                        HB.AppendLine("<div class='reason'><div class='reason_title'>Reason:</div>");
                        RB.Append(Tools.RtfSafe("<i<cg>><i<b>>Reason: <i</b>><i</cg>><i<br>><i<br>>"));
                    }
                    HB.AppendLine(Tools.ConvertForHtmlReport(F.Reasons[i].Reason));
                    if (F.Reasons[i].FalsePositiveCheck.Length > 0)
                    {
                        HB.AppendLine("<div class='fpa_check'><div class='fpa_check_title'>False Positive Check Assistance:</div>");
                        HB.AppendLine(Tools.ConvertForHtmlReport(F.Reasons[i].FalsePositiveCheck));
                        HB.AppendLine("</div>");

                        RB.AppendLine(Tools.RtfSafe("<i<cg>><i<b>>False Positive Check Assistance:<i</b>><i</cg>><i<br>><i<br>>"));
                        RB.AppendLine(Tools.RtfSafe(F.Reasons[i].FalsePositiveCheck));
                    }
                    RB.Append(Tools.RtfSafe(F.Reasons[i].Reason));
                    RB.AppendLine("<i<br>>");

                    if (F.Reasons[i].TriggerIds.Count > 0)
                    {
                        HB.AppendLine("<div class='trigger_desc'>The relevant parts of the requests/responses pairs associated with the check explained in this reason section are available below.</div>");
                    }
                    foreach(int Tid in F.Reasons[i].TriggerIds)
                    {
                        if (!TriggerIdsAssociatedWithReasons.Contains(Tid)) TriggerIdsAssociatedWithReasons.Add(Tid);

                        Trigger T = F.Triggers.GetTrigger(Tid - 1);
                        HB.AppendLine("<div class='trigger'>");
                        
                        string THL = Finding.GetTriggerHighlighting(T, F.FinderType, false, false);
                        HB.AppendLine(Tools.ConvertForHtmlReport(THL));
                        RB.AppendLine(Tools.RtfSafe(THL));
                        RB.AppendLine(Tools.RtfSafe("<i<br>><i<br>>"));

                        HB.AppendLine("</div>");
                    }
                    HB.AppendLine("</div>");
                }
                if (F.Triggers.Count > TriggerIdsAssociatedWithReasons.Count)
                {
                    HB.AppendLine("<div class='trigger_desc'>The relevant parts of the requests/responses pairs associated with the check that discovered this issue are available below.</div>");
                    for (int Tid=1; Tid <= F.Triggers.Count; Tid++)
                    {
                        if (!TriggerIdsAssociatedWithReasons.Contains(Tid))
                        {
                            Trigger T = F.Triggers.GetTrigger(Tid - 1);
                            HB.AppendLine("<div class='trigger'>");

                            string THL = Finding.GetTriggerHighlighting(T, F.FinderType, false, false);
                            HB.AppendLine(Tools.ConvertForHtmlReport(THL));
                            RB.AppendLine(Tools.RtfSafe(THL));
                            RB.AppendLine(Tools.RtfSafe("<i<br>><i<br>>"));

                            HB.AppendLine("</div>");
                        }
                    }
                }
            }
            else
            {
                foreach (Trigger T in F.Triggers.GetTriggers())
                {
                    HB.AppendLine("<div class='trigger'>");

                    string THL = Finding.GetTriggerHighlighting(T, F.FinderType, false, false);
                    HB.AppendLine(Tools.ConvertForHtmlReport(THL));
                    RB.AppendLine(Tools.RtfSafe(THL));
                    RB.AppendLine(Tools.RtfSafe("<i<br>><i<br>>"));
                    
                    HB.AppendLine("</div>");
                }
            }
            HB.AppendLine("</div>");
            StepProgressBar();
            return FindingHash;
        }

        string GetFindingHash(Finding  F)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(F.Title);
            SB.Append(F.Summary);
            SB.Append(F.Type);
            SB.Append(F.FinderName);
            SB.Append(F.FinderType);
            SB.Append(F.Severity.ToString());
            SB.Append(F.Confidence.ToString());
            SB.Append(F.AffectedHost);
            SB.Append(F.AffectedParameter);
            SB.Append(F.AffectedSection);
            foreach (FindingReason Reason in F.Reasons)
            {
                SB.Append(Reason.Reason);
                SB.Append(Reason.FalsePositiveCheck);
            }
            foreach (Trigger T in F.Triggers.GetTriggers())
            {
                SB.Append(T.RawRequestTriggerDescription);
                SB.Append(T.ResponseTriggerDescription);
                SB.Append(T.RequestTrigger);
                SB.Append(T.ResponseTrigger);
            }

            return Tools.MD5(SB.ToString());
        }

        string GetOverviewAndIndex(Dictionary<string, FindingStatsHolder> FindingStatsForSummary, List<string> HostByRank)
        {
            StringBuilder SB = new StringBuilder();
            SB.AppendLine(@"
<div class='report'>
<div class='section_title'>Overview</div>
<div id='overview'>
This report contains the list of security findings discovered by IronWASP. The current section of the report gives a brief overview of the number of different findings, the numbers are categorized by the hosts they were discovered on.
<br>
The index section contains the names of all the findings. The sections after that show details of every individual finding.
<br><br>
The table below shows the number of findings discovered in each host. The findings are seperated based on their type and severity.<br>
<br><br>
<div class='legend'>
<div class='fname'>Legend:</div>
<br>
<table cellpadding='0' cellspacing='0'>
<tr><td align='right'><span class='icr'><b>High</b></span></td> <td>&nbsp;&nbsp;&nbsp;&nbsp;High Severity Vulnerability</td></tr>
<tr><td align='right'><span class='ico'><b>Medium</b></span></td> <td>&nbsp;&nbsp;&nbsp;&nbsp;Medium Severity Vulnerability</td></tr>
<tr><td align='right'><span class='icy'><b>Low</b></span></td> <td>&nbsp;&nbsp;&nbsp;&nbsp;Low Severity Vulnerability</td></tr>
<tr><td align='right'><span class='icb'><b>Info</b></span></td> <td>&nbsp;&nbsp;&nbsp;&nbsp;Information Findings</td></tr>
<tr><td align='right'><span class='icg'><b>Test Leads</b></span></td> <td>&nbsp;&nbsp;&nbsp;&nbsp;Things of interest for manual testing</td></tr>
</table>
<br>
The High, Medium and Low severity vulnerability numbers are also split based on the confidence with which IronWASP has reported them.<br>
<br>
<span class='t_high_conf'>&nbsp;&nbsp;0&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp; High Confidence &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class='t_med_conf'>&nbsp;&nbsp;0&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp; Medium Confidence &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class='t_low_conf'>&nbsp;&nbsp;0&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp; Low Confidence &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
<br>
");

            SB.AppendLine(@"<table cellpadding='0' cellspacing='0'>
<tr>
    <th width='100' >High</th>
    <th width='20'> </th>
    <th width='100'>Medium</th>
    <th width='20'> </th>
    <th width='100'>Low</th>
    <th width='100'>Info</th>
    <th width='100'>Test leads</th>
    <th width='20'> </th>
    <th width='30'>Total</th>
    <th width='30'> </th>
    <th width='400'>Hosts</th>
</tr>
<tr>
    <td height='20px'></td>
</tr>
");
            foreach (string Host in HostByRank)
            {
                SB.AppendLine(string.Format(@"<tr>
            <td>
                <div class='t_high'>{0}</div>
            </td>
            <td></td>
            <td>
                <div class='t_med'>{1}</div>
            </td>
            <td></td>
            <td>
                <div class='t_low'>{2}</div>
            </td>
            <td>
                <div class='t_info'>{3}</div>
            </td>
            <td><div class='t_lead'>{4}</div></td>
            <td></td>
            <td><div class='t_total'>{5}</div></td>
            <td></td>            
            <td><div class='t_host'>{6}</div></td>
        </tr>", FindingStatsForSummary[Host].High, FindingStatsForSummary[Host].Medium, FindingStatsForSummary[Host].Low, FindingStatsForSummary[Host].Info, FindingStatsForSummary[Host].Leads, FindingStatsForSummary[Host].All, Tools.HtmlEncode(Host)));
                
                SB.AppendLine(string.Format(@"<tr>
            <td>
                <table cellpadding='0' cellspacing='0'>
                    <tr>
                        <td width='30'><div class='t_high_conf'>{0}</div></td>
                        <td width='30'><div class='t_med_conf'>{1}</div></td>
                        <td width='30'><div class='t_low_conf'>{2}</div></td>
                    </tr>
                </table>
            </td>
            <td></td>", FindingStatsForSummary[Host].HighSevHighConf, FindingStatsForSummary[Host].HighSevMedConf, FindingStatsForSummary[Host].HighSevLowConf));
                SB.AppendLine(string.Format(@"<td>
                <table cellpadding='0' cellspacing='0'>
                    <tr>
                        <td width='30'><div class='t_high_conf'>{0}</div></td>
                        <td width='30'><div class='t_med_conf'>{1}</div></td>
                        <td width='30'><div class='t_low_conf'>{2}</div></td>
                    </tr>
                </table>
            </td>
            <td></td>", FindingStatsForSummary[Host].MedSevHighConf, FindingStatsForSummary[Host].MedSevMedConf, FindingStatsForSummary[Host].MedSevLowConf));
                SB.AppendLine(string.Format(@"<td>
                <table cellpadding='0' cellspacing='0'>
                    <tr>
                        <td width='30'><div class='t_high_conf'>{0}</div></td>
                        <td width='30'><div class='t_med_conf'>{1}</div></td>
                        <td width='30'><div class='t_low_conf'>{2}</div></td>
                    </tr>
                </table>
            </td>
        </tr>", FindingStatsForSummary[Host].LowSevHighConf, FindingStatsForSummary[Host].LowSevMedConf, FindingStatsForSummary[Host].LowSevLowConf));
                //FindingStatsForSummary[Host].High
            }
            SB.AppendLine("</table>");
            SB.AppendLine("</div>");

            SB.AppendLine(@"<div class='section_title'>Index</div>
<div id='index'>
The titles of all the findings are listed below categorized by the host they were discovered on. All items in the list below are links to relevant sections in the report.
<br>
");
            for (int i=0; i < HostByRank.Count; i++)
            {
                string Host = HostByRank[i];
                SB.AppendLine(string.Format("<a href='#host{0}'><div class='host'>{1}</div></a>",i + 1, Tools.HtmlEncode(Host)));
                
                FindingStatsHolder Stats = FindingStatsForSummary[Host];
                SB.AppendLine("<ul>");

                SB.AppendLine(GetIndexFor(Stats.HighSevHighConfTitles, "High"));
                SB.AppendLine(GetIndexFor(Stats.HighSevMedConfTitles, "High"));
                SB.AppendLine(GetIndexFor(Stats.HighSevLowConfTitles, "High"));

                SB.AppendLine(GetIndexFor(Stats.MedSevHighConfTitles, "Med"));
                SB.AppendLine(GetIndexFor(Stats.MedSevMedConfTitles, "Med"));
                SB.AppendLine(GetIndexFor(Stats.MedSevLowConfTitles, "Med"));

                SB.AppendLine(GetIndexFor(Stats.LowSevHighConfTitles, "Low"));
                SB.AppendLine(GetIndexFor(Stats.LowSevMedConfTitles, "Low"));
                SB.AppendLine(GetIndexFor(Stats.LowSevLowConfTitles, "Low"));

                SB.AppendLine(GetIndexFor(Stats.InfoTitles, "Info"));

                SB.AppendLine(GetIndexFor(Stats.LeadsTitles, "Leads"));
                
                SB.AppendLine("</ul>");
            }
            SB.AppendLine("</div>");
            return SB.ToString();
        }

        string GetIndexFor(Dictionary<string, Dictionary<int, string>> Titles, string Type)
        {
            StringBuilder SB = new StringBuilder();
            foreach (string Title in Titles.Keys)
            {
                if (Titles[Title].Count == 0) continue;
                List<int> KeysList = new List<int>(Titles[Title].Keys);
                KeysList.Sort();
                int FirstFindingId = KeysList[0];

                switch (Type)
                {
                    case ("High"):
                        SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='icr'>{1}</span></a></li>", FirstFindingId, Tools.HtmlEncode(Title)));
                        break;
                    case ("Med"):
                        SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='ico'>{1}</span></a></li>", FirstFindingId, Tools.HtmlEncode(Title)));
                        break;
                    case ("Low"):
                        SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='icy'>{1}</span></a></li>", FirstFindingId, Tools.HtmlEncode(Title)));
                        break;
                    case ("Info"):
                        SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='icb'>{1}</span></a></li>", FirstFindingId, Tools.HtmlEncode(Title)));
                        break;
                    case ("Leads"):
                        SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='icg'>{1}</span></a></li>", FirstFindingId, Tools.HtmlEncode(Title)));
                        break;
                }
                SB.AppendLine("<ol>");
                foreach (int Id in Titles[Title].Keys)
                {
                    switch (Type)
                    {
                        case("High"):
                            SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='index_finding_url'>{1}</span></a></li>", Id, Tools.HtmlEncode(Titles[Title][Id])));
                            break;
                        case("Med"):
                            SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='index_finding_url'>{1}</span></a></li>", Id, Tools.HtmlEncode(Titles[Title][Id])));
                            break;
                        case("Low"):
                            SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='index_finding_url'>{1}</span></a></li>", Id, Tools.HtmlEncode(Titles[Title][Id])));
                            break;
                        case("Info"):
                            SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='index_finding_url'>{1}</span></a></li>", Id, Tools.HtmlEncode(Titles[Title][Id])));
                            break;
                        case("Leads"):
                            SB.AppendLine(string.Format("<li><a href='#finding{0}'><span class='index_finding_url'>{1}</span></a></li>", Id, Tools.HtmlEncode(Titles[Title][Id])));
                            break;
                    }
                }
                SB.AppendLine("</ol>");
            }
            return SB.ToString();
        }

        private void SaveAsHtmlLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveReport(HtmlReport, ".html");
        }

        private void SaveAsRtfLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveReport(RtfReport, ".rtf");
        }

        void SaveReport(string ReportContent, string Ext)
        {
            SaveReportDialog.DefaultExt = Ext;
            SaveReportDialog.FileName = "report" + Ext;

            while (SaveReportDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo Info = new FileInfo(SaveReportDialog.FileName);
                
                if (!SaveReportDialog.FileName.EndsWith(Ext))
                {
                    MessageBox.Show(string.Format("Report should end with {0} extension", Ext));
                }
                else
                {
                    try
                    {
                        StreamWriter Writer = new StreamWriter(Info.FullName);
                        Writer.Write(ReportContent);
                        Writer.Close();
                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(string.Format("Unable to save file: {0}", new object[] { Exp.Message }));
                    }
                    break;
                }
            }
        }

        private void ReportGenerationWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ReportingThread.Abort();
            }
            catch { }
        }
    }

    internal class FindingInfoList
    {
        internal string Type = "";
        internal string Host = "";
        internal string Severity = "";
        internal string Confidence = "";
        internal string Title = "";
        //Type, Host, Severity, Confidence, Title

        internal FindingInfoList()
        {

        }

        internal FindingInfoList(string Type, string Host, string Severity, string Confidence, string Title)
        {
            this.Type = Type;
            this.Host = Host;
            this.Severity = Severity;
            this.Confidence = Confidence;
            this.Title = Title;
        }
    }
    internal class FindingStatsHolder
    {
        internal int HighSevHighConf = 0;
        internal int HighSevMedConf = 0;
        internal int HighSevLowConf = 0;

        internal int MedSevHighConf = 0;
        internal int MedSevMedConf = 0;
        internal int MedSevLowConf = 0;
        
        internal int LowSevHighConf = 0;
        internal int LowSevMedConf = 0;
        internal int LowSevLowConf = 0;

        internal int Info = 0;
        internal int Leads = 0;


        internal Dictionary<string, Dictionary<int, string>> HighSevHighConfTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> HighSevMedConfTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> HighSevLowConfTitles = new Dictionary<string, Dictionary<int, string>>();

        internal Dictionary<string, Dictionary<int, string>> MedSevHighConfTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> MedSevMedConfTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> MedSevLowConfTitles = new Dictionary<string, Dictionary<int, string>>();

        internal Dictionary<string, Dictionary<int, string>> LowSevHighConfTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> LowSevMedConfTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> LowSevLowConfTitles = new Dictionary<string, Dictionary<int, string>>();

        internal Dictionary<string, Dictionary<int, string>> InfoTitles = new Dictionary<string, Dictionary<int, string>>();
        internal Dictionary<string, Dictionary<int, string>> LeadsTitles = new Dictionary<string, Dictionary<int, string>>();

        internal int High
        {
            get
            {
                return HighSevHighConf + HighSevMedConf + HighSevLowConf;
            }
        }
        internal int Medium
        {
            get
            {
                return MedSevHighConf + MedSevMedConf + MedSevLowConf;
            }
        }
        internal int Low
        {
            get
            {
                return LowSevHighConf + LowSevMedConf + LowSevLowConf;
            }
        }
        internal int All
        {
            get
            {
                return High + Medium + Low + Info + Leads;
            }
        }
    }
}
