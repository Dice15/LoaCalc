using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LostarkSimulator
{
    public partial class DetailedInfo : Form
    {
        private string BforeHalf = "";
        private string AfterHalf = "";

        public DetailedInfo()
        {
            InitializeComponent();
        }

        public DetailedInfo(string bforeHalf, string afterHalf)
        {
            InitializeComponent();
            BforeHalf = bforeHalf;
            AfterHalf = afterHalf;
        }

        private void DetailedInfo_Load(object sender, EventArgs e)
        {
            DetailedInfoBeforeHalf.Text = BforeHalf;
            DetailedInfoAfterHalf.Text = AfterHalf;
        }
    }
}
