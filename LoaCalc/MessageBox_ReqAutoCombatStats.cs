using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace LoaCalc
{
    public partial class MessageBox_ReqAutoCombatStats : Form
    {
        private List<int> numList = new List<int>
        {
            50,55,170,187,250,275,350,370,385,407,450,470,495,517,550,570,605,627,650,670,715,737,750,770,825,847,850,870,935,950,957,970,
            1045,1050,1067,1070,1150,1155,1170,1177,1250,1265,1270,1287,1350,1370,1375,1397,1470,1485,1507,1550,1617,1670,1705,1837
        };

        private (DialogResult dialogResult, int critLower, int critUpper, int specLower, int specUpper, int swiftLower, int swiftUpper) result = (DialogResult.No, 50, 1837, 50, 1837, 50, 1837);

        public (DialogResult dialogResult, int critLower, int critUpper, int specLower, int specUpper, int swiftLower, int swiftUpper) messageBoxResult
        {
            get { return result; }
        }




        public MessageBox_ReqAutoCombatStats()
        {
            InitializeComponent();
        }

        private void MessageBox_ReqAutoCombatStats_Load(object sender, EventArgs e)
        {
            Crit_Lower.Items.AddRange(numList.ConvertAll(num => num.ToString()).ToArray());
            Crit_Upper.Items.AddRange(numList.ConvertAll(num => num.ToString()).ToArray());
            Spec_Lower.Items.AddRange(numList.ConvertAll(num => num.ToString()).ToArray());
            Spec_Upper.Items.AddRange(numList.ConvertAll(num => num.ToString()).ToArray());
            Swift_Lower.Items.AddRange(numList.ConvertAll(num => num.ToString()).ToArray());
            Swift_Upper.Items.AddRange(numList.ConvertAll(num => num.ToString()).ToArray());
            Crit_Lower.SelectedIndex = Spec_Lower.SelectedIndex = Swift_Lower.SelectedIndex = 0;
            Crit_Upper.SelectedIndex = Spec_Upper.SelectedIndex = Swift_Upper.SelectedIndex = Crit_Lower.Items.Count - 1;
        }




        /// <summary>
        /// 확인 버튼 눌렀을 때 발생하는 이벤트.
        /// </summary>
        private void Yes_Click(object sender, EventArgs e)
        {
            bool existError = false;

            result = (DialogResult.Yes, 50, 1837, 50, 1837, 50, 1837);
            
            if (Crit_Lower.SelectedIndex <= Crit_Upper.SelectedIndex)
            {
                result.critLower = numList[Crit_Lower.SelectedIndex];
                result.critUpper = numList[Crit_Upper.SelectedIndex];
            }
            else
            {
                existError = true;
                Crit_Lower.SelectedIndex = Crit_Upper.SelectedIndex = 0;
            }
                       
            if (Spec_Lower.SelectedIndex <= Spec_Upper.SelectedIndex)
            {
                result.specLower = numList[Spec_Lower.SelectedIndex];
                result.specUpper = numList[Spec_Upper.SelectedIndex];
            }
            else
            {
                existError = true;
                Spec_Lower.SelectedIndex = Spec_Upper.SelectedIndex = 0;
            }
            
            if (Swift_Lower.SelectedIndex <= Swift_Upper.SelectedIndex)
            {
                result.swiftLower = numList[Swift_Lower.SelectedIndex];
                result.swiftUpper = numList[Swift_Upper.SelectedIndex];
            }
            else
            {
                existError = true;
                Swift_Lower.SelectedIndex = Swift_Upper.SelectedIndex = 0;
            }

            if (existError)
            {
                MessageBox.Show("오류!! 하한값이 상한보다 큽니다");
            }
            else this.Hide();
        }


        /// <summary>
        /// 취소 버튼 눌렀을 때 발생하는 이벤트.
        /// </summary>
        private void No_Click(object sender, EventArgs e)
        {
            result = (DialogResult.No, 50, 1837, 50, 1837, 50, 1837);
            this.Hide();
        }


        /// <summary>
        /// 폼 닫기 버튼을 눌렀을 때 발생하는 이벤트
        /// </summary>
        private void MessageBox_ReqAutoCombatStats_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
