using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class DiffResultView : UserControl
    {
        public DiffResultView()
        {
            InitializeComponent();
        }

        delegate void ShowDiffResults_d(string SinglePage, string SideBySideSource, string SideBySideDestination);
        internal void ShowDiffResults(string SinglePage, string SideBySideSource, string SideBySideDestination)
        {
            if (ResultsTabs.InvokeRequired)
            {
                ShowDiffResults_d SDR_d = new ShowDiffResults_d(ShowDiffResults);
                ResultsTabs.Invoke(SDR_d, new object[] { SinglePage, SideBySideSource, SideBySideDestination });
            }
            else
            {
                DiffResultRTB.Rtf = SinglePage;
                SourceResultRTB.Rtf = SideBySideSource;
                DestinationResultRTB.Rtf = SideBySideDestination;
            }
        }

        delegate void ClearDiffResults_d();
        internal void ClearDiffResults()
        {
            if (ResultsTabs.InvokeRequired)
            {
                ClearDiffResults_d CDR_d = new ClearDiffResults_d(ClearDiffResults);
                ResultsTabs.Invoke(CDR_d, new object[] { });
            }
            else
            {
                DiffResultRTB.Text = "";
                SourceResultRTB.Text = "";
                DestinationResultRTB.Text = "";
            }
        }
    }
}
