using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace LoaCalc
{
    public partial class GunslingerSetting : Form
    {
        public class FormCtrl
        {
            public Type type { get; set; }
            public string name { get; set; }
            public string data { get; set; }

            public FormCtrl(Type _type, string _name, string _data) { type = _type; name = _name; data = _data; }
        }
        private Dictionary<string, Control> allControls;

        public static int CombatStat_ControllerMaxNum = 6;
        public static int Engraving_ControllerMaxNum = 7;
        public static int Card_ControllerMaxNum = 3;
        public static int Gem_ControllerMaxNum = 22;
        public static int Gear_ControllerMaxNum = 3;
        public static int Weapon_ControllerMaxNum = 1;
        public static int Buff_ControllerMaxNum = 7;
        public static int Skill_ControllerMaxNum = 15;
        public static int Preset_ControllerMaxNum = 6;

        private List<SettingInfo.Engraving.NAME> engravingNameList = new List<SettingInfo.Engraving.NAME>();
        private List<SettingInfo.Engraving.LEV> engravingLevList = new List<SettingInfo.Engraving.LEV>();

        private List<SettingInfo.Card.NAME> cardNameList = new List<SettingInfo.Card.NAME>();
        private Dictionary<SettingInfo.Card.NAME, List<SettingInfo.Card.SET>> cardSetList = new Dictionary<SettingInfo.Card.NAME, List<SettingInfo.Card.SET>>();
        private Dictionary<SettingInfo.Card.NAME, List<SettingInfo.Card.AWAKENING>> cardAwakeningList = new Dictionary<SettingInfo.Card.NAME, List<SettingInfo.Card.AWAKENING>>();

        private List<SettingInfo.Gem.NAME> gemNameList = new List<SettingInfo.Gem.NAME>();
        private List<SettingInfo.Gem.LEV> gemLevList = new List<SettingInfo.Gem.LEV>();
        private List<SettingInfo.Skill.NAME> gemTargetSkillList = new List<SettingInfo.Skill.NAME>();

        private List<SettingInfo.Gear.NAME> gearNameList = new List<SettingInfo.Gear.NAME>();
        private Dictionary<SettingInfo.Gear.NAME, List<SettingInfo.Gear.SET>> gearSetList = new Dictionary<SettingInfo.Gear.NAME, List<SettingInfo.Gear.SET>>();
        private Dictionary<SettingInfo.Gear.NAME, List<SettingInfo.Gear.LEV>> gearSetLevList = new Dictionary<SettingInfo.Gear.NAME, List<SettingInfo.Gear.LEV>>();

        private List<SettingInfo.Buff.NAME> buffNameList = new List<SettingInfo.Buff.NAME>();

        private List<SettingInfo.Skill.NAME> skillNameList = new List<SettingInfo.Skill.NAME>();
        private List<SettingInfo.Skill.LEV> skillLevList = new List<SettingInfo.Skill.LEV>();
        private Dictionary<SettingInfo.Skill.NAME, List<SettingInfo.Skill.TRIPOD>> skillTp1List = new Dictionary<SettingInfo.Skill.NAME, List<SettingInfo.Skill.TRIPOD>>();
        private Dictionary<SettingInfo.Skill.NAME, List<SettingInfo.Skill.TRIPOD>> skillTp2List = new Dictionary<SettingInfo.Skill.NAME, List<SettingInfo.Skill.TRIPOD>>();
        private Dictionary<SettingInfo.Skill.NAME, List<SettingInfo.Skill.TRIPOD>> skillTp3List = new Dictionary<SettingInfo.Skill.NAME, List<SettingInfo.Skill.TRIPOD>>();

        private Gunslinger character = new Gunslinger();
        private List<Gunslinger.ResultCalculateCombatSkill> combatSkillDamageList = new List<Gunslinger.ResultCalculateCombatSkill>();
        private (decimal damageBeforeHalf, decimal damageAfterHalf, decimal damageArithmeticMean, decimal damageHarmonicMean, decimal dpsArithmeticMean, decimal dpsHarmonicMean) sumOfSkillDamage = (0, 0, 0, 0, 0, 0);
 



        public GunslingerSetting()
        {
            InitializeComponent();
            Construct();
            Initializer();
        }




        //*********************************************************//
        //                     Form 데이터 관리                    //
        //*********************************************************//

        /// <summary>
        /// 세팅에 사용될 데이터.
        /// </summary>
        private void Construct()
        {
            // 모든 컨트롤은 딕셔너리로 관리한다
            allControls = new Dictionary<string, Control>();
            foreach (var control in GetAllControls(this).ToList())
            {
                allControls.Add(control.Name, control);

                if (control.GetType() == typeof(TextBox)) (control as TextBox).Text = "";
                else if (control.GetType() == typeof(ComboBox)) (control as ComboBox).Items.Clear();
                else if (control.GetType() == typeof(CustomCheckBox)) (control as CustomCheckBox).Check = false;
            }

            // 각인
            engravingNameList = SettingInfo.Engraving.GetNameList();
            engravingLevList = SettingInfo.Engraving.GetLevList();

            // 카드
            cardNameList = SettingInfo.Card.GetNameList();
            cardSetList = SettingInfo.Card.GetSetList();
            cardAwakeningList = SettingInfo.Card.GetAwakeningList();

            // 보석
            gemNameList = SettingInfo.Gem.GetNameList();
            gemLevList = SettingInfo.Gem.GetLevList();
            gemTargetSkillList = SettingInfo.Skill.GetNameList(exceptCustom: true, exceptAwakening: true);

            // 장비 세트
            gearNameList = SettingInfo.Gear.GetNameList();
            gearSetList = SettingInfo.Gear.GetSetList();
            gearSetLevList = SettingInfo.Gear.GetSetLevList();

            // 버프 (시너지)
            buffNameList = SettingInfo.Buff.GetNameList();

            // 스킬
            skillNameList = SettingInfo.Skill.GetNameList();
            skillLevList = SettingInfo.Skill.GetLevList();
            skillTp1List = SettingInfo.Skill.GetTripod1List();
            skillTp2List = SettingInfo.Skill.GetTripod2List();
            skillTp3List = SettingInfo.Skill.GetTripod3List();

            // 계산 결과
            combatSkillDamageList = Enumerable.Repeat(new Gunslinger.ResultCalculateCombatSkill(), Skill_ControllerMaxNum + 1).ToList();
            sumOfSkillDamage = (0, 0, 0, 0, 0, 0);
        }


        /// <summary>
        /// 모든 컨트롤에 데이터를 추가하고, 값을 초기화 한다.
        /// </summary>
        private void Initializer()
        {
            // 각인
            for (int i = 0; i < Engraving_ControllerMaxNum; i++)
            {
                ComboBox engravingName = GetControlByName("Engraving" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox engravingLev = GetControlByName("Engraving" + (i + 1).ToString() + "_Lev") as ComboBox;

                engravingName.Items.AddRange(new List<string> { "선택 안 함" }.Union(engravingNameList.ConvertAll(name => name.ToStr())).ToArray());
                engravingLev.Items.AddRange(new List<string> { "미사용" }.Union(engravingLevList.ConvertAll(lev => lev.ToStr())).ToArray());
            }

            // 카드
            for (int i = 0; i < Card_ControllerMaxNum; i++)
            {
                ComboBox cardName = GetControlByName("Card" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox cardSet = GetControlByName("Card" + (i + 1).ToString() + "_Sets") as ComboBox;
                ComboBox cardAwakening = GetControlByName("Card" + (i + 1).ToString() + "_Awakening") as ComboBox;

                cardName.Items.AddRange(new List<string> { "선택 안 함" }.Union(cardNameList.ConvertAll(name => name.ToStr())).ToArray());
            }

            // 보석
            for (int i = 0; i < Gem_ControllerMaxNum; i++)
            {
                ComboBox gemName = GetControlByName("Gem" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gemLev = GetControlByName("Gem" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox gemTargetskill = GetControlByName("Gem" + (i + 1).ToString() + "_TargetSkillName") as ComboBox;

                gemName.Items.AddRange(new List<string> { "선택 안 함" }.Union(gemNameList.ConvertAll(name => name.ToStr())).ToArray());
                gemLev.Items.AddRange(new List<string> { "미사용" }.Union(gemLevList.ConvertAll(lev => lev.ToStr())).ToArray());
                gemTargetskill.Items.AddRange(new List<string> { "미사용" }.Union(gemTargetSkillList.ConvertAll(targetSkill => targetSkill.ToStr())).ToArray());
            }

            // 장비 세트
            for (int i = 0; i < Gear_ControllerMaxNum; i++)
            {
                ComboBox gearName = GetControlByName("Gear" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gearSet = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonus") as ComboBox;
                ComboBox gearSetLev = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonusLev") as ComboBox;

                gearName.Items.AddRange(new List<string> { "선택 안 함" }.Union(gearNameList.ConvertAll(name => name.ToStr())).ToArray());
            }

            // 버프 (시너지)
            for (int i = 0; i < Buff_ControllerMaxNum; i++)
            {
                ComboBox buffName = GetControlByName("Buff" + (i + 1).ToString() + "_Name") as ComboBox;

                buffName.Items.AddRange(new List<string> { "선택 안 함" }.Union(buffNameList.ConvertAll(name => name.ToStr())).ToArray());
            }

            // 스킬
            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                ComboBox skillName = GetControlByName("Skill" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox skillLev = GetControlByName("Skill" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox skillTp1 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp1") as ComboBox;
                ComboBox skillTp2 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp2") as ComboBox;
                ComboBox skillTp3 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp3") as ComboBox;

                skillName.Items.AddRange(new List<string> { "선택 안 함" }.Union(skillNameList.ConvertAll(name => name.ToStr())).ToArray());
                skillLev.Items.AddRange(skillLevList.ConvertAll(lev => lev.ToStr()).ToArray());
            }

            // 특성별 Dps
            DpsByCombatStatsRatio.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
            DpsByCombatStatsRatio.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "치명", HeaderText = "치명", Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable, AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    HeaderCell = new DataGridViewColumnHeaderCell { Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } } },
                new DataGridViewTextBoxColumn { Name = "특화", HeaderText = "특화", Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable, AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    HeaderCell = new DataGridViewColumnHeaderCell { Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } } },
                new DataGridViewTextBoxColumn { Name = "신속", HeaderText = "신속", Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable, AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    HeaderCell = new DataGridViewColumnHeaderCell { Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } } },
                new DataGridViewTextBoxColumn { Name = "Dps", HeaderText = "Dps", Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable, AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    HeaderCell = new DataGridViewColumnHeaderCell { Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } } },
                new DataGridViewTextBoxColumn { Name = "Dps배율", HeaderText = "Dps배율", Width = 90, SortMode = DataGridViewColumnSortMode.NotSortable, AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    HeaderCell = new DataGridViewColumnHeaderCell { Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } } }
            });
            DpsByCombatStatsRatio.Font = new Font("맑은 고딕", 12, FontStyle.Regular);
            DpsByCombatStatsRatio.RowHeadersVisible = false;
            DpsByCombatStatsRatio.ReadOnly = true;
            DpsByCombatStatsRatio.AllowUserToAddRows = false;
            DpsByCombatStatsRatio.AllowUserToDeleteRows = false;
            DpsByCombatStatsRatio.AllowUserToResizeColumns = false;
            DpsByCombatStatsRatio.AllowUserToResizeRows = false;

            // 모든 컨트롤 값 초기화
            FormCtrlInitializer();
        }


        /// <summary>
        /// 모든 컨트롤의 값을 초기화 한다.
        /// </summary>
        private void FormCtrlInitializer()
        {
            ProcessingMessageCalcOptimalCombatStats.Visible = false;

            foreach (Control control in allControls.Values)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    (control as TextBox).Text = "";
                }
                else if (control.GetType() == typeof(ComboBox))
                {
                    if ((control as ComboBox).Items.Count > 0)
                    {
                        string[] strlist = control.Name.Split('_');

                        if (strlist.Length > 1)
                        {
                            (control as ComboBox).SelectedIndex = strlist[1].CompareTo("Name") == 0 ? 0 : -1;
                        }
                    }
                }
                else if (control.GetType() == typeof(CustomCheckBox))
                {
                    (control as CustomCheckBox).Check = false;
                }
            }
        }




        //*********************************************************//
        //                       계산 이벤트                       //
        //*********************************************************//

        /// <summary>
        /// 캐릭터 세팅을 기준으로 스킬 데미지를 계산한다.
        /// </summary>
        private void CalculateCombatSkillDamage(bool show = true)
        {
            combatSkillDamageList = character.CalculateCombatSkill();

            if (show) ShowCombatSkillDamage();
        }
        private void ShowCombatSkillDamage()
        {
            var minDamage = combatSkillDamageList.Where(combatSkill => combatSkill.damage.harmonicMean != 0).DefaultIfEmpty(new Gunslinger.ResultCalculateCombatSkill()).Min(combatSkill => combatSkill.damage.harmonicMean);
            var minDps = combatSkillDamageList.Where(combatSkill => combatSkill.dps.harmonicMean != 0).DefaultIfEmpty(new Gunslinger.ResultCalculateCombatSkill()).Min(combatSkill => combatSkill.dps.harmonicMean);

            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                ComboBox name = GetControlByName("Skill" + (i + 1).ToString() + "_Name") as ComboBox;
                TextBox damageBeforeHalf = GetControlByName("Skill" + (i + 1).ToString() + "_DamageBeforeHalf") as TextBox;
                TextBox damageAfterHalf = GetControlByName("Skill" + (i + 1).ToString() + "_DamageAfterHalf") as TextBox;
                TextBox damageArithmeticMean = GetControlByName("Skill" + (i + 1).ToString() + "_DamageArithmeticAvg") as TextBox;
                TextBox damageHarmonicMean = GetControlByName("Skill" + (i + 1).ToString() + "_DamageHarmonicAvg") as TextBox;
                TextBox damageMagnification = GetControlByName("Skill" + (i + 1).ToString() + "_DamageMagnification") as TextBox;
                TextBox dpsArithmeticMean = GetControlByName("Skill" + (i + 1).ToString() + "_DpsArithmeticAvg") as TextBox;
                TextBox dpsHarmonicMean = GetControlByName("Skill" + (i + 1).ToString() + "_DpsHarmonicAvg") as TextBox;
                TextBox dpsMagnification = GetControlByName("Skill" + (i + 1).ToString() + "_DpsShare") as TextBox;
                TextBox cooldownTime = GetControlByName("Skill" + (i + 1).ToString() + "_CooldownTime") as TextBox;

                if (name.SelectedIndex <= 0)
                {
                    damageBeforeHalf.Text = damageAfterHalf.Text = damageArithmeticMean.Text = damageHarmonicMean.Text = damageMagnification.Text = dpsArithmeticMean.Text = dpsHarmonicMean.Text = dpsMagnification.Text = cooldownTime.Text = "";
                }
                else
                {
                    damageBeforeHalf.Text = Math.Round(combatSkillDamageList[i].damage.beforeHalf).ToString();
                    damageAfterHalf.Text = Math.Round(combatSkillDamageList[i].damage.afterHalf).ToString();
                    damageArithmeticMean.Text = Math.Round(combatSkillDamageList[i].damage.arithmeticMean).ToString();
                    damageHarmonicMean.Text = Math.Round(combatSkillDamageList[i].damage.harmonicMean).ToString();
                    damageMagnification.Text = Math.Round(minDamage == 0 ? 0 : combatSkillDamageList[i].damage.harmonicMean / minDamage * 100, 2).ToString("F2") + "%";
                    dpsArithmeticMean.Text = Math.Round(combatSkillDamageList[i].dps.arithmeticMean).ToString();
                    dpsHarmonicMean.Text = Math.Round(combatSkillDamageList[i].dps.harmonicMean).ToString();
                    dpsMagnification.Text = Math.Round(minDps == 0 ? 0 : combatSkillDamageList[i].dps.harmonicMean / minDps * 100, 2).ToString("F2") + "%";
                    cooldownTime.Text = Math.Round(combatSkillDamageList[i].cooldown, 2).ToString();
                }
            }
        }
        private void ShowSkillDetailed(int controlnum)
        {
            DetailedInfo detailedInfo = new DetailedInfo(combatSkillDamageList[controlnum - 1].detailedInfo.beforeHalf, combatSkillDamageList[controlnum - 1].detailedInfo.afterHalf);
            detailedInfo.Show();
        }


        /// <summary>
        /// 선택한 스킬들로 데미지 합, 평균 데미지 합, DPS 합을 구한다.
        /// </summary>
        private void CalculateSumOfSkillDamage(bool show = true)
        {
            sumOfSkillDamage = (0, 0, 0, 0, 0, 0);

            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                CustomCheckBox selectedSkill = GetControlByName("Skill" + (i + 1).ToString() + "_IncludeInFinalDps") as CustomCheckBox;

                if (selectedSkill.Check)
                {
                    sumOfSkillDamage.damageBeforeHalf += combatSkillDamageList[i].damage.beforeHalf;
                    sumOfSkillDamage.damageAfterHalf += combatSkillDamageList[i].damage.afterHalf;
                    sumOfSkillDamage.damageArithmeticMean += combatSkillDamageList[i].damage.arithmeticMean;
                    sumOfSkillDamage.damageHarmonicMean += combatSkillDamageList[i].damage.harmonicMean;
                    sumOfSkillDamage.dpsArithmeticMean += combatSkillDamageList[i].dps.arithmeticMean;
                    sumOfSkillDamage.dpsHarmonicMean += combatSkillDamageList[i].dps.harmonicMean;
                }
            }

            if (show) ShowSumOfSkillDamage();
        }
        private void ShowSumOfSkillDamage()
        {
            SkillSum_DamageBeforeHalf.Text = Math.Round(sumOfSkillDamage.damageBeforeHalf).ToString();
            SkillSum_DamageAfterHalf.Text = Math.Round(sumOfSkillDamage.damageAfterHalf).ToString();
            SkillSum_DamageArithmeticAvg.Text = Math.Round(sumOfSkillDamage.damageArithmeticMean).ToString();
            SkillSum_DamageHarmonicAvg.Text = Math.Round(sumOfSkillDamage.damageHarmonicMean).ToString();
            SkillSum_DpsArithmeticAvg.Text = Math.Round(sumOfSkillDamage.dpsArithmeticMean).ToString();
            SkillSum_DpsHarmonicAvg.Text = Math.Round(sumOfSkillDamage.dpsHarmonicMean).ToString();
        }


        /// <summary>
        /// 최적 특성을 구한다.
        /// </summary>
        private void CalculateOptimalCombatStats(int critLower, int critUpper, int specLower, int specUpper, int swiftLower, int swiftUpper)
        {
            var accessoryValueList = new List<int> { 50, 50, 50, 120, 120, 500, 500, 300, 300, 200, 200 };
            var combatStats = new List<int> { 0, 0, 0 };
            var optiamlCombatStats = new List<decimal> { 0, 0, 0, 0 };

            ProcessingMessageCalcOptimalCombatStats.Visible = true;
            Delay(1);

            var count = 0;
            var checkDuplication = new HashSet<string>();
            UpdateAllSetting();
            DpsByCombatStatsRatio.Rows.Clear();

            for (int x1 = 0; x1 < 3; x1++)
            {
                for (int x2 = 0; x2 < 3; x2++)
                {
                    if (x1 == x2) continue;
                    for (int y1 = 0; y1 < 3; y1++)
                    {
                        for (int y2 = 0; y2 < 3; y2++)
                        {
                            if (y1 == y2) continue;
                            combatStats[0] = accessoryValueList[0];
                            combatStats[1] = accessoryValueList[1];
                            combatStats[2] = accessoryValueList[2];
                            combatStats[x1] += accessoryValueList[3];
                            combatStats[x2] += accessoryValueList[4];
                            combatStats[y1] += accessoryValueList[5];
                            combatStats[y2] += accessoryValueList[6];
                            SubCalcOptimalCombatStats(7, accessoryValueList, combatStats, optiamlCombatStats, critLower, critUpper, specLower, specUpper, swiftLower, swiftUpper, ref count, checkDuplication);
                        }
                    }
                }
            }

            DpsByCombatStatsRatio.Sort(DpsByCombatStatsRatio.Columns["Dps"], ListSortDirection.Descending);
            int minDps = int.Parse(DpsByCombatStatsRatio.Rows[DpsByCombatStatsRatio.Rows.Count - 1].Cells[3].Value.ToString());

            for (int i = 0; i < DpsByCombatStatsRatio.Rows.Count; i++)
                DpsByCombatStatsRatio.Rows[i].Cells[4].Value = Math.Round(((decimal)DpsByCombatStatsRatio.Rows[i].Cells[3].Value / (decimal)minDps) * 100m, 2).ToString("F2") + "%";

            ProcessingMessageCalcOptimalCombatStats.Visible = false;
            Delay(1);
        }
        private void SubCalcOptimalCombatStats(int index, List<int> accessoryValueList, List<int> combatStats, List<decimal> optimalCombatStats, int critLower, int critUpper, int specLower, int specUpper, int swiftLower, int swiftUpper, ref int count, HashSet<string> checkDuplication)
        {
            if (index == accessoryValueList.Count)
            {
                for (int i = 0; i < 3; i++)
                {
                    count++;

                    int temp = combatStats[i];
                    combatStats[i] = Decimal.ToInt32(Math.Round(combatStats[i] * 1.1m));

                    if ((critLower <= combatStats[0] && combatStats[0] <= critUpper) && (specLower <= combatStats[1] && combatStats[1] <= specUpper) && (swiftLower <= combatStats[2] && combatStats[2] <= swiftUpper))
                    {         
                        var checkStr = combatStats[0].ToString() + "_" + combatStats[1].ToString() + "_" + combatStats[2].ToString();

                        if (!checkDuplication.Contains(checkStr))
                        {
                            checkDuplication.Add(checkStr);

                            character.setting.SetCombatStats(combatStats[0], combatStats[1], combatStats[2]);
                            CalculateCombatSkillDamage(show: false);
                            CalculateSumOfSkillDamage(show: false);

                            decimal dps = sumOfSkillDamage.dpsHarmonicMean;

                            if (optimalCombatStats[3] < dps)
                            {
                                optimalCombatStats[0] = combatStats[0];
                                optimalCombatStats[1] = combatStats[1];
                                optimalCombatStats[2] = combatStats[2];
                                optimalCombatStats[3] = dps;
                            }

                            DpsByCombatStatsRatio.Rows.Add(combatStats[0], combatStats[1], combatStats[2], Math.Round(dps), 0);
                            ShowProcessing(count);
                            Delay(1);
                        }
                    }

                    combatStats[i] = temp;
                }
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                combatStats[i] += accessoryValueList[index];

                if ((critLower <= combatStats[0] && combatStats[0] <= critUpper) && (specLower <= combatStats[1] && combatStats[1] <= specUpper) && (swiftLower <= combatStats[2] && combatStats[2] <= swiftUpper))
                {
                    SubCalcOptimalCombatStats(index + 1, accessoryValueList, combatStats, optimalCombatStats, critLower, critUpper, specLower, specUpper, swiftLower, swiftUpper, ref count, checkDuplication);
                }
                else
                {
                    count += (int)Math.Pow(3, 10 - index);
                    ShowProcessing(count);
                    Delay(1);
                }

                combatStats[i] -= accessoryValueList[index];
            }
        }
        private void ShowProcessing(decimal count)
        {
            string ProcessingString = "최적 특성비 구하는 중";
            ProcessingMessageCalcOptimalCombatStats.Text = ProcessingString + " " + Math.Round((count / 8748m) * 100m).ToString() + "%";        
        }


        /// <summary>
        /// 캐릭터 세팅을 업데이트 한다.
        /// </summary>
        private void UpdateSetting(bool combatStats = false, bool engraving = false, bool card = false, bool gem = false, bool gear = false, bool weaponQual = false, bool buff = false, bool skill = false)
        {
            if (combatStats)   // 전투 특성
            {
                if (CombatStat_Crit.Text == "") CombatStat_Crit.Text = "0";
                if (CombatStat_Specialization.Text == "") CombatStat_Specialization.Text = "0";
                if (CombatStat_Swiftness.Text == "") CombatStat_Swiftness.Text = "0";
                character.setting.SetCombatStats(int.Parse(CombatStat_Crit.Text), int.Parse(CombatStat_Specialization.Text), int.Parse(CombatStat_Swiftness.Text));
            }

            if (engraving)   // 각인
            {
                var selectedEngravingList = new List<(SettingInfo.Engraving.NAME name, SettingInfo.Engraving.LEV lev)?>();
                for (int i = 0; i < Engraving_ControllerMaxNum; i++)
                {
                    ComboBox engravingName = GetControlByName("Engraving" + (i + 1).ToString() + "_Name") as ComboBox;
                    ComboBox engravingLev = GetControlByName("Engraving" + (i + 1).ToString() + "_Lev") as ComboBox;

                    if (engravingName.SelectedIndex <= 0 || engravingLev.SelectedIndex <= 0) selectedEngravingList.Add(null);
                    else selectedEngravingList.Add((engravingNameList[engravingName.SelectedIndex - 1], engravingLevList[engravingLev.SelectedIndex - 1]));
                }
                character.setting.SetEngraving(selectedEngravingList);
            }

            if (card)   // 카드
            {
                var selectedCardList = new List<(SettingInfo.Card.NAME name, SettingInfo.Card.SET set, SettingInfo.Card.AWAKENING awakening)?>();
                for (int i = 0; i < Card_ControllerMaxNum; i++)
                {
                    ComboBox cardName = GetControlByName("Card" + (i + 1).ToString() + "_Name") as ComboBox;
                    ComboBox cardSet = GetControlByName("Card" + (i + 1).ToString() + "_Sets") as ComboBox;
                    ComboBox cardAwakening = GetControlByName("Card" + (i + 1).ToString() + "_Awakening") as ComboBox;

                    if (cardName.SelectedIndex <= 0 || cardSet.SelectedIndex <= 0 || cardAwakening.SelectedIndex <= 0) selectedCardList.Add(null);
                    else
                    {
                        var name = cardNameList[cardName.SelectedIndex - 1];
                        var set = cardSetList[name][cardSet.SelectedIndex - 1];
                        var awakening = cardAwakeningList[name][cardAwakening.SelectedIndex - 1];
                        selectedCardList.Add((name, set, awakening));
                    }
                }
                character.setting.SetCard(selectedCardList);
            }

            if (gem)   // 보석
            {
                var selectedGemList = new List<(SettingInfo.Gem.NAME name, SettingInfo.Gem.LEV lev, SettingInfo.Skill.NAME targetSkill)?>();
                for (int i = 0; i < Gem_ControllerMaxNum; i++)
                {
                    ComboBox gemName = GetControlByName("Gem" + (i + 1).ToString() + "_Name") as ComboBox;
                    ComboBox gemLev = GetControlByName("Gem" + (i + 1).ToString() + "_Lev") as ComboBox;
                    ComboBox gemTargetskill = GetControlByName("Gem" + (i + 1).ToString() + "_TargetSkillName") as ComboBox;

                    if (gemName.SelectedIndex <= 0 || gemLev.SelectedIndex <= 0 || gemTargetskill.SelectedIndex <= 0) selectedGemList.Add(null);
                    else
                    {
                        var name = gemNameList[gemName.SelectedIndex - 1];
                        var lev = gemLevList[gemLev.SelectedIndex - 1];
                        var target = gemTargetSkillList[gemTargetskill.SelectedIndex - 1];
                        selectedGemList.Add((name, lev, target));
                    }
                }
                character.setting.SetGem(selectedGemList);
            }

            if (gear)   // 장비 세트
            {
                var selectedGearList = new List<(SettingInfo.Gear.NAME name, SettingInfo.Gear.SET set, SettingInfo.Gear.LEV setLev)?>();
                for (int i = 0; i < Gear_ControllerMaxNum; i++)
                {
                    ComboBox gearName = GetControlByName("Gear" + (i + 1).ToString() + "_Name") as ComboBox;
                    ComboBox gearSet = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonus") as ComboBox;
                    ComboBox gearSetLev = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonusLev") as ComboBox;

                    if (gearName.SelectedIndex <= 0 || gearSet.SelectedIndex <= 0 || gearSetLev.SelectedIndex <= 0) selectedGearList.Add(null);
                    else
                    {
                        var name = gearNameList[gearName.SelectedIndex - 1];
                        var set = gearSetList[name][gearSet.SelectedIndex - 1];
                        var setLev = gearSetLevList[name][gearSetLev.SelectedIndex - 1];
                        selectedGearList.Add((name, set, setLev));
                    }
                }
                character.setting.SetGear(selectedGearList);
            }

            if (weaponQual)   // 무기 품질
            {
                if (Weapon_AdditionalDamage.Text == "") Weapon_AdditionalDamage.Text = "0";
                character.setting.SetWeaponQual(decimal.Parse(Weapon_AdditionalDamage.Text));
            }

            if (buff)   // 버프 (시너지)
            {
                var selectedBuffList = new List<SettingInfo.Buff.NAME?>();
                for (int i = 0; i < Buff_ControllerMaxNum; i++)
                {
                    ComboBox buffName = GetControlByName("Buff" + (i + 1).ToString() + "_Name") as ComboBox;

                    if (buffName.SelectedIndex <= 0) selectedBuffList.Add(null);
                    else selectedBuffList.Add(buffNameList[buffName.SelectedIndex - 1]);
                }
                character.setting.SetBuff(selectedBuffList);
            }

            if (skill)   // 스킬
            {
                var selectedSkillList = new List<(SettingInfo.Skill.NAME name, SettingInfo.Skill.LEV lev, SettingInfo.Skill.TRIPOD tp1, SettingInfo.Skill.TRIPOD tp2, SettingInfo.Skill.TRIPOD tp3)?>();
                for (int i = 0; i < Skill_ControllerMaxNum; i++)
                {
                    ComboBox skillName = GetControlByName("Skill" + (i + 1).ToString() + "_Name") as ComboBox;
                    ComboBox skillLev = GetControlByName("Skill" + (i + 1).ToString() + "_Lev") as ComboBox;
                    ComboBox skillTp1 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp1") as ComboBox;
                    ComboBox skillTp2 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp2") as ComboBox;
                    ComboBox skillTp3 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp3") as ComboBox;

                    if (skillName.SelectedIndex <= 0 || skillLev.SelectedIndex < 0) selectedSkillList.Add(null);
                    else
                    {
                        var name = skillNameList[skillName.SelectedIndex - 1];
                        var lev = skillLevList[skillLev.SelectedIndex];
                        var tp1 = skillTp1List[name][skillTp1.SelectedIndex == -1 ? 0 : skillTp1.SelectedIndex];
                        var tp2 = skillTp2List[name][skillTp2.SelectedIndex == -1 ? 0 : skillTp2.SelectedIndex];
                        var tp3 = skillTp3List[name][skillTp3.SelectedIndex == -1 ? 0 : skillTp3.SelectedIndex];
                        selectedSkillList.Add((name, lev, tp1, tp2, tp3));
                    }
                }
                character.setting.SetSkill(selectedSkillList);
            }
        }
        private void UpdateAllSetting()
        {
            UpdateSetting(combatStats: true, engraving: true, card: true, gem: true, gear: true, weaponQual: true, buff: true, skill: true);
        }




        //*********************************************************//
        //                        컨트롤 제어                      //
        //*********************************************************//

        /// <summary>
        /// 모든 컨트롤을 반환한다.
        /// </summary>
        private IEnumerable<Control> GetAllControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl)).Concat(controls).Where(p => p is TextBox || p is ComboBox || p is CustomCheckBox);
        }


        /// <summary>
        /// 해당 Name을 가진 컨트롤을 반환한다.
        /// </summary>
        private Control GetControlByName(string name)
        {
            return allControls.ContainsKey(name) ? allControls[name] : null;
        }


        /// <summary>
        ///  Delay 매서드.
        /// </summary>
        private static DateTime Delay(int ms)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime AfterWards = ThisMoment.Add(duration);
            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents(); ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }


        //*********************************************************//
        //                       컨트롤 이벤트                     //
        //*********************************************************//


        /// <summary>
        /// 전투 특성 값을 바꿨을 때 발생하는 이벤트.
        /// </summary>
        private void CombatStat_TextChanged(object sender, EventArgs e)
        {
            TextBox combatStat = sender as TextBox;

            if (!CombatStat_PrevText.ContainsKey(combatStat.Name)) CombatStat_PrevText.Add(combatStat.Name, "0");

            if (new Regex(@"^[0-9]*$").IsMatch(combatStat.Text) && combatStat.Text.Length < 5)
            {
                CombatStat_PrevText[combatStat.Name] = combatStat.Text;
            }
            else
            {
                combatStat.Text = CombatStat_PrevText[combatStat.Name];
                combatStat.Select(combatStat.Text.Length, 0);
            }

            UpdateSetting(combatStats: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }
        private Dictionary<string, string> CombatStat_PrevText = new Dictionary<string, string>();


        /// <summary>
        /// 최적 전투 특성을 구하는 버튼을 눌렀을 때 발생하는 이벤트.
        /// </summary>
        private void CalcOptimalCombatStats_Click(object sender, EventArgs e)
        {
            bool existSelectedSkill = false;
            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                CustomCheckBox selectedSkill = GetControlByName("Skill" + (i + 1).ToString() + "_IncludeInFinalDps") as CustomCheckBox;

                if (selectedSkill.Check)
                {
                    existSelectedSkill = true;
                    break;
                }
            }

            if (existSelectedSkill)
            {
                MessageBox_ReqAutoCombatStats reqAutoStats = new MessageBox_ReqAutoCombatStats();
                reqAutoStats.ShowDialog();

                var condition = reqAutoStats.messageBoxResult;

                if (condition.dialogResult == DialogResult.Yes)
                {
                    CalculateOptimalCombatStats(condition.critLower, condition.critUpper, condition.specLower, condition.specUpper, condition.swiftLower, condition.swiftUpper);
                }
            }
            else
            {
                MessageBox.Show("\'합계\'에 포함되는 스킬이 없습니다" + Environment.NewLine + "\'합계\'에 포함할 스킬을 체크해주세요");
            }
        }


        /// <summary>
        /// 각인 선택 시 발생하는 이벤트.
        /// </summary>
        private void Engraving_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Engraving_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = (i + 1);
                    break;
                }
            }

            ComboBox engravingName = sender as ComboBox;
            ComboBox engravingLev = GetControlByName("Engraving" + controlnum.ToString() + "_Lev") as ComboBox;

            if (!(engravingName.SelectedIndex == -1 || engravingName.SelectedIndex == Engraving_Name_PrevSelectedIndex[controlnum]))
            {
                if (engravingName.SelectedIndex == 0)
                {
                    engravingLev.SelectedIndex = -1;
                    engravingLev.Enabled = false;
                }
                else
                {
                    engravingLev.SelectedIndex = 0;
                    engravingLev.Enabled = true;
                }

                Engraving_Name_PrevSelectedIndex[controlnum] = engravingName.SelectedIndex;
            }
        }
        private List<int> Engraving_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Engraving_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 각인 레벨 선택 시 발생하는 이벤트.
        /// </summary>
        private void Engraving_Lev_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSetting(engraving: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 카드 선택 시 발생하는 이벤트.
        /// </summary>
        private void Card_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Card_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox cardName = GetControlByName("Card" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox cardSet = GetControlByName("Card" + controlnum.ToString() + "_Sets") as ComboBox;
            ComboBox cardAwakening = GetControlByName("Card" + controlnum.ToString() + "_Awakening") as ComboBox;

            if (!(cardName.SelectedIndex == -1 || cardName.SelectedIndex == Card_Name_PrevSelectedIndex[controlnum]))
            {
                if (cardName.SelectedIndex==0)
                {
                    cardSet.Items.Clear();
                    cardAwakening.Items.Clear();

                    cardSet.SelectedIndex = -1;
                    cardAwakening.SelectedIndex = -1;

                    cardSet.Enabled = false;
                    cardAwakening.Enabled = false;
                }
                else
                {
                    cardSet.Items.Clear();
                    cardAwakening.Items.Clear();

                    cardSet.Items.AddRange(new List<string> { "미사용" }.Union(cardSetList[cardNameList[cardName.SelectedIndex - 1]].ConvertAll(set => set.ToStr())).ToArray());
                    cardAwakening.Items.AddRange(new List<string> { "미사용" }.Union(cardAwakeningList[cardNameList[cardName.SelectedIndex - 1]].ConvertAll(awakening => awakening.ToStr())).ToArray());

                    cardSet.SelectedIndex = 0;
                    cardAwakening.SelectedIndex = 0;

                    cardSet.Enabled = true;
                    cardAwakening.Enabled = true;
                }

                Card_Name_PrevSelectedIndex[controlnum] = cardName.SelectedIndex;


            }
        }
        private List<int> Card_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Card_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 카드 세트 개수 선택 시 발생하는 이벤트.
        /// </summary>
        private void Card_Sets_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSetting(card: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 카드 세트 각성 선택 시 발생하는 이벤트.
        /// </summary>
        private void Card_Awakening_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSetting(card: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 보석 종류 선택 시 발생하는 이벤트.
        /// </summary>
        private void Gem_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 0;

            for (int i = 0; i < Gem_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox gemName = GetControlByName("Gem" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox gemLev = GetControlByName("Gem" + controlnum.ToString() + "_Lev") as ComboBox;
            ComboBox gemTargerskillname = GetControlByName("Gem" + controlnum.ToString() + "_TargetSkillName") as ComboBox;

            if (!(gemName.SelectedIndex == -1 || gemName.SelectedIndex == Gem_Name_PrevSelectedIndex[controlnum]))
            {
                if (gemName.SelectedIndex == 0)
                {
                    gemLev.SelectedIndex = -1;
                    gemTargerskillname.SelectedIndex = -1;

                    gemLev.Enabled = false;
                    gemTargerskillname.Enabled = false;
                }
                else
                {
                    gemLev.SelectedIndex = 0;
                    gemTargerskillname.SelectedIndex = -1;

                    gemLev.Enabled = true;
                    gemTargerskillname.Enabled = false;
                }

                Gem_Name_PrevSelectedIndex[controlnum] = gemName.SelectedIndex;
            }

        }
        private List<int> Gem_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Gem_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 보석 레벨 선택 시 발생하는 이벤트.
        /// </summary>
        private void Gem_Lev_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 0;

            for (int i = 0; i < Gem_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox gemName = GetControlByName("Gem" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox gemLev = GetControlByName("Gem" + controlnum.ToString() + "_Lev") as ComboBox;
            ComboBox gemTargerskillname = GetControlByName("Gem" + controlnum.ToString() + "_TargetSkillName") as ComboBox;

            if (!(gemLev.SelectedIndex == -1 || gemLev.SelectedIndex == Gem_Lev_PrevSelectedIndex[controlnum]))
            {
                if (gemLev.SelectedIndex == 0)
                {
                    gemTargerskillname.SelectedIndex = -1;
                    gemTargerskillname.Enabled = false;
                }
                else
                {
                    gemTargerskillname.SelectedIndex = 0;
                    gemTargerskillname.Enabled = true;
                }

                Gem_Lev_PrevSelectedIndex[controlnum] = gemLev.SelectedIndex;
            }
        }
        private List<int> Gem_Lev_PrevSelectedIndex = Enumerable.Repeat(-1, Gem_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 보석 적용 스킬 선택 시 발생하는 이벤트.
        /// </summary>
        private void Gem_TargetSkillName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSetting(gem: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 장비 세트 이름 선택 시 발생하는 이벤트.
        /// </summary>
        private void Gear_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Gear_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox gearName = GetControlByName("Gear" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox gearSet = GetControlByName("Gear" + controlnum.ToString() + "_SetsBonus") as ComboBox;
            ComboBox gearSetlev = GetControlByName("Gear" + controlnum.ToString() + "_SetsBonusLev") as ComboBox;

            if (!(gearName.SelectedIndex == -1 || gearName.SelectedIndex == Gear_Name_PrevSelectedIndex[controlnum]))
            {
                if (gearName.SelectedIndex == 0)
                {
                    gearSet.Items.Clear();
                    gearSetlev.Items.Clear();

                    gearSet.SelectedIndex = -1;
                    gearSetlev.SelectedIndex = -1;

                    gearSet.Enabled = false;
                    gearSetlev.Enabled = false;
                }
                else
                {
                    gearSet.Items.Clear();
                    gearSetlev.Items.Clear();

                    gearSet.Items.AddRange(new List<string> { "미사용" }.Union(gearSetList[gearNameList[gearName.SelectedIndex - 1]].ConvertAll(set => set.ToStr())).ToArray());
                    gearSetlev.Items.AddRange(new List<string> { "미사용" }.Union(gearSetLevList[gearNameList[gearName.SelectedIndex - 1]].ConvertAll(setLev => setLev.ToStr())).ToArray());

                    gearSet.SelectedIndex = 0;
                    gearSetlev.SelectedIndex = -1;

                    gearSet.Enabled = true;
                    gearSetlev.Enabled = false;
                }

                Gear_Name_PrevSelectedIndex[controlnum] = gearName.SelectedIndex;
            }
        }
        private List<int> Gear_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Gear_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 장비 세트 개수 선택 시 발생하는 이벤트.
        /// </summary>
        private void Gear_SetsBonus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Gear_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox gearName = GetControlByName("Gear" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox gearSet = GetControlByName("Gear" + controlnum.ToString() + "_SetsBonus") as ComboBox;
            ComboBox gearSetlev = GetControlByName("Gear" + controlnum.ToString() + "_SetsBonusLev") as ComboBox;

            if (!(gearSet.SelectedIndex == -1 || gearSet.SelectedIndex == Gear_Set_PrevSelectedIndex[controlnum]))
            {
                if (gearSet.SelectedIndex == 0)
                {
                    gearSetlev.SelectedIndex = -1;
                    gearSetlev.Enabled = false;
                }
                else
                {
                    gearSetlev.SelectedIndex = 0;
                    gearSetlev.Enabled = true;
                }
            }
        }
        private List<int> Gear_Set_PrevSelectedIndex = Enumerable.Repeat(-1, Gear_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 장비 세트 레벨 선택 시 발생하는 이벤트.
        /// </summary>
        private void Gear_SetsBonusLev_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSetting(gear: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 무기 추가 피해 값을 바꿨을 때 발생하는 이벤트.
        /// </summary>
        private void Weapon_AdditionalDamage_TextChanged(object sender, EventArgs e)
        {
            TextBox weaponAD = sender as TextBox;

            if (new Regex(@"^[0-9]*$").IsMatch(weaponAD.Text) && weaponAD.Text.Length < 3)
            {
                Weapon_AdditionalDamage_PrevText = weaponAD.Text;
            }
            else
            {
                weaponAD.Text = Weapon_AdditionalDamage_PrevText;
                weaponAD.Select(weaponAD.Text.Length, 0);
            }

            UpdateSetting(weaponQual: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }
        private string Weapon_AdditionalDamage_PrevText = "0";


        /// <summary>
        /// 버프 이름 선택 시 발생하는 이벤트.
        /// </summary>
        private void Buff_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Buff_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox buffName = GetControlByName("Buff" + controlnum.ToString() + "_Name") as ComboBox;

            if (!(buffName.SelectedIndex == -1 || buffName.SelectedIndex == Buff_Name_PrevSelectedIndex[controlnum]))
            {
                Buff_Name_PrevSelectedIndex[controlnum] = buffName.SelectedIndex;

                UpdateSetting(buff: true);
                CalculateCombatSkillDamage();
                CalculateSumOfSkillDamage();
            }
        }
        private List<int> Buff_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Buff_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 스킬 이름 선택 시 발생하는 이벤트.
        /// </summary>
        private void Skill_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox skillName = GetControlByName("Skill" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox skillLev = GetControlByName("Skill" + controlnum.ToString() + "_Lev") as ComboBox;
            ComboBox skillTp1 = GetControlByName("Skill" + controlnum.ToString() + "_Tp1") as ComboBox;
            ComboBox skillTp2 = GetControlByName("Skill" + controlnum.ToString() + "_Tp2") as ComboBox;
            ComboBox skillTp3 = GetControlByName("Skill" + controlnum.ToString() + "_Tp3") as ComboBox;

            if (!(skillName.SelectedIndex == -1 || skillName.SelectedIndex == Skill_Name_PrevSelectedIndex[controlnum]))
            {
                if (skillName.SelectedIndex == 0)
                {
                    skillTp1.Items.Clear();
                    skillTp2.Items.Clear();
                    skillTp3.Items.Clear();

                    skillLev.SelectedIndex = -1;
                    skillTp1.SelectedIndex = -1;
                    skillTp2.SelectedIndex = -1;
                    skillTp3.SelectedIndex = -1;

                    skillLev.Enabled = false;
                    skillTp1.Enabled = false;
                    skillTp2.Enabled = false;
                    skillTp3.Enabled = false;
                }
                else
                {
                    skillTp1.Items.Clear();
                    skillTp2.Items.Clear();
                    skillTp3.Items.Clear();

                    skillTp1.Items.AddRange(skillTp1List[skillNameList[skillName.SelectedIndex - 1]].ConvertAll(tp1 => tp1.ToStr()).ToArray());
                    skillTp2.Items.AddRange(skillTp2List[skillNameList[skillName.SelectedIndex - 1]].ConvertAll(tp2 => tp2.ToStr()).ToArray());
                    skillTp3.Items.AddRange(skillTp3List[skillNameList[skillName.SelectedIndex - 1]].ConvertAll(tp3 => tp3.ToStr()).ToArray());

                    skillLev.SelectedIndex = 0;
                    skillTp1.SelectedIndex = -1;
                    skillTp2.SelectedIndex = -1;
                    skillTp3.SelectedIndex = -1;

                    skillLev.Enabled = true;
                }

                Skill_Name_PrevSelectedIndex[controlnum] = skillName.SelectedIndex;

                UpdateSetting(skill: true);
                CalculateCombatSkillDamage();
                CalculateSumOfSkillDamage();
            }
        }
        private List<int> Skill_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Skill_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 스킬 레벨 선택 시 발생하는 이벤트.
        /// </summary>
        private void Skill_Lev_SelectedIndexChanged(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as ComboBox).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            ComboBox skillName = GetControlByName("Skill" + controlnum.ToString() + "_Name") as ComboBox;
            ComboBox skillLev = GetControlByName("Skill" + controlnum.ToString() + "_Lev") as ComboBox;
            ComboBox skillTp1 = GetControlByName("Skill" + controlnum.ToString() + "_Tp1") as ComboBox;
            ComboBox skillTp2 = GetControlByName("Skill" + controlnum.ToString() + "_Tp2") as ComboBox;
            ComboBox skillTp3 = GetControlByName("Skill" + controlnum.ToString() + "_Tp3") as ComboBox;

            if (!(skillLev.SelectedIndex == -1 || skillLev.SelectedIndex == Skill_Lev_PrevSelectedIndex[controlnum]))
            {
                if (skillLev.SelectedIndex == 0)
                {
                    skillTp1.SelectedIndex = -1;
                    skillTp2.SelectedIndex = -1;
                    skillTp3.SelectedIndex = -1;

                    skillTp1.Enabled = false;
                    skillTp2.Enabled = false;
                    skillTp3.Enabled = false;
                }
                else if (skillLev.SelectedIndex == 1)
                {
                    skillTp1.SelectedIndex = skillTp1.Enabled == false ? 0 : skillTp1.SelectedIndex;
                    skillTp2.SelectedIndex = -1;
                    skillTp3.SelectedIndex = -1;

                    skillTp1.Enabled = true;
                    skillTp2.Enabled = false;
                    skillTp3.Enabled = false;
                }
                else if (skillLev.SelectedIndex == 2)
                {
                    skillTp1.SelectedIndex = skillTp1.Enabled == false ? 0 : skillTp1.SelectedIndex;
                    skillTp2.SelectedIndex = skillTp2.Enabled == false ? 0 : skillTp2.SelectedIndex;
                    skillTp3.SelectedIndex = -1;

                    skillTp1.Enabled = true;
                    skillTp2.Enabled = true;
                    skillTp3.Enabled = false;
                }
                else
                {
                    skillTp1.SelectedIndex = skillTp1.Enabled == false ? 0 : skillTp1.SelectedIndex;
                    skillTp2.SelectedIndex = skillTp2.Enabled == false ? 0 : skillTp2.SelectedIndex;
                    skillTp3.SelectedIndex = skillTp3.Enabled == false ? 0 : skillTp3.SelectedIndex;

                    skillTp1.Enabled = true;
                    skillTp2.Enabled = true;
                    skillTp3.Enabled = true;
                }

                Skill_Lev_PrevSelectedIndex[controlnum] = skillLev.SelectedIndex;

                UpdateSetting(skill: true);
                CalculateCombatSkillDamage();
                CalculateSumOfSkillDamage();
            }
        }
        private List<int> Skill_Lev_PrevSelectedIndex = Enumerable.Repeat(-1, Skill_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 스킬 트라이포드 선택 시 발생하는 이벤트.
        /// </summary>
        private void Skill_Tp3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSetting(skill: true);
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 계산 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void Calculate_Click(object sender, EventArgs e)
        {
            UpdateAllSetting();
            CalculateCombatSkillDamage();
            CalculateSumOfSkillDamage();
        }


        /// <summary>
        /// 세팅 저장 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void Preset_Save_Click(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Preset_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as Button).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            List<FormCtrl> FormCtrlList = new List<FormCtrl>();

            // 전투 특성
            FormCtrlList.Add(new FormCtrl(CombatStat_Crit.GetType(), CombatStat_Crit.Name, CombatStat_Crit.Text));
            FormCtrlList.Add(new FormCtrl(CombatStat_Specialization.GetType(), CombatStat_Specialization.Name, CombatStat_Specialization.Text));
            FormCtrlList.Add(new FormCtrl(CombatStat_Swiftness.GetType(), CombatStat_Swiftness.Name, CombatStat_Swiftness.Text));

            // 각인
            for (int i = 0; i < Engraving_ControllerMaxNum; i++)
            {
                ComboBox engravingName = GetControlByName("Engraving" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox engravingLev = GetControlByName("Engraving" + (i + 1).ToString() + "_Lev") as ComboBox;
                FormCtrlList.Add(new FormCtrl(engravingName.GetType(), engravingName.Name, engravingName.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(engravingLev.GetType(), engravingLev.Name, engravingLev.SelectedIndex.ToString()));
            }

            // 카드
            for (int i = 0; i < Card_ControllerMaxNum; i++)
            {
                ComboBox cardName = GetControlByName("Card" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox cardSet = GetControlByName("Card" + (i + 1).ToString() + "_Sets") as ComboBox;
                ComboBox cardAwakening = GetControlByName("Card" + (i + 1).ToString() + "_Awakening") as ComboBox;
                FormCtrlList.Add(new FormCtrl(cardName.GetType(), cardName.Name, cardName.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(cardSet.GetType(), cardSet.Name, cardSet.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(cardAwakening.GetType(), cardAwakening.Name, cardAwakening.SelectedIndex.ToString()));
            }

            // 보석
            for (int i = 0; i < Gem_ControllerMaxNum; i++)
            {
                ComboBox gemName = GetControlByName("Gem" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gemLev = GetControlByName("Gem" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox gemTargetskill = GetControlByName("Gem" + (i + 1).ToString() + "_TargetSkillName") as ComboBox;
                FormCtrlList.Add(new FormCtrl(gemName.GetType(), gemName.Name, gemName.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(gemLev.GetType(), gemLev.Name, gemLev.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(gemTargetskill.GetType(), gemTargetskill.Name, gemTargetskill.SelectedIndex.ToString()));
            }

            // 장비 세트
            for (int i = 0; i < Gear_ControllerMaxNum; i++)
            {
                ComboBox gearName = GetControlByName("Gear" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gearSet = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonus") as ComboBox;
                ComboBox gearSetLev = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonusLev") as ComboBox;
                FormCtrlList.Add(new FormCtrl(gearName.GetType(), gearName.Name, gearName.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(gearSet.GetType(), gearSet.Name, gearSet.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(gearSetLev.GetType(), gearSetLev.Name, gearSetLev.SelectedIndex.ToString()));
            }

            // 무기 품질
            FormCtrlList.Add(new FormCtrl(Weapon_AdditionalDamage.GetType(), Weapon_AdditionalDamage.Name, Weapon_AdditionalDamage.Text));

            // 버프 (시너지)
            for (int i = 0; i < Buff_ControllerMaxNum; i++)
            {
                ComboBox buffName = GetControlByName("Buff" + (i + 1).ToString() + "_Name") as ComboBox;
                FormCtrlList.Add(new FormCtrl(buffName.GetType(), buffName.Name, buffName.SelectedIndex.ToString()));
            }

            // 스킬
            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                ComboBox skillName = GetControlByName("Skill" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox skillLev = GetControlByName("Skill" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox skillTp1 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp1") as ComboBox;
                ComboBox skillTp2 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp2") as ComboBox;
                ComboBox skillTp3 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp3") as ComboBox;
                CustomCheckBox selectedSkill = GetControlByName("Skill" + (i + 1).ToString() + "_IncludeInFinalDps") as CustomCheckBox;
                FormCtrlList.Add(new FormCtrl(skillName.GetType(), skillName.Name, skillName.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillLev.GetType(), skillLev.Name, skillLev.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillTp1.GetType(), skillTp1.Name, skillTp1.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillTp2.GetType(), skillTp2.Name, skillTp2.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillTp3.GetType(), skillTp3.Name, skillTp3.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(selectedSkill.GetType(), selectedSkill.Name, selectedSkill.Check.ToString()));
            }

            var result = MessageBox.Show(controlnum.ToString() + "번 프리셋에 저장하시겠습니까?", "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var currDirectory = Application.StartupPath;
                var fileName = "Preset" + controlnum.ToString();
                var serializedList = JsonConvert.SerializeObject(FormCtrlList, Formatting.Indented);

                File.WriteAllText(currDirectory + @"\" + fileName, serializedList);
                if (File.Exists(currDirectory + @"\" + fileName)) MessageBox.Show("프리셋 저장 완료");
            }
        }


        /// <summary>
        /// 세팅 불러오기 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void Preset_Load_Click(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Preset_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as Button).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = i + 1;
                    break;
                }
            }

            var result = MessageBox.Show(controlnum.ToString() + "번 프리셋을 불러오시겠습니까?", "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                FormCtrlInitializer();

                var currDirectory = Application.StartupPath;
                var fileName = "Preset" + controlnum.ToString();

                if (File.Exists(currDirectory + @"\" + fileName))
                {
                    var serializedList = File.ReadAllText(currDirectory + @"\" + fileName);
                    var deserializedList = JsonConvert.DeserializeObject<List<FormCtrl>>(serializedList);

                    foreach (var formCtrl in deserializedList)
                    {
                        var control = GetControlByName(formCtrl.name);
                        if (control == null) continue;

                        if (formCtrl.type == typeof(TextBox))
                        {
                            TextBox textBox = control as TextBox;
                            textBox.Text = formCtrl.data;
                        }
                        else if (formCtrl.type == typeof(ComboBox))
                        {
                            ComboBox comboBox = control as ComboBox;
                            comboBox.SelectedIndex = int.Parse(formCtrl.data);
                        }
                        else if (control.GetType() == typeof(CustomCheckBox))
                        {
                            CustomCheckBox customCheckBox = control as CustomCheckBox;
                            customCheckBox.Check = bool.Parse(formCtrl.data);
                        }
                    }
                }

                UpdateAllSetting();
                CalculateCombatSkillDamage();
                CalculateSumOfSkillDamage();
                MessageBox.Show("프리셋 로딩 완료");
            }
        }


        /// <summary>
        /// 세팅 리셋 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void ResetSetting_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "새 세팅을 만드시겠습니까?" + Environment.NewLine + "(기존에 불러온 프리셋에는 영향을 주지 않습니다)",
                "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                FormCtrlInitializer();
            }
        }


        /// <summary>
        /// 상세 정보 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void Skill_SkillData_Click(object sender, EventArgs e)
        {
            int controlnum = 1;

            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                string num = (i + 1).ToString();
                string id = (sender as Button).Name.Split('_')[0];

                if (num.Length == id.Count(c => ('0' <= c && c <= '9')) && id.Contains(num))
                {
                    controlnum = (i + 1);
                    break;
                }
            }

            ShowSkillDetailed(controlnum);
        }


        /// <summary>
        /// 총 합계에 포함할 스킬이 변경 되었을 때 발생하는 이벤트.
        /// </summary>
        private void Skill_IncludeInFinalDps_Click(object sender, EventArgs e)
        {
            CalculateSumOfSkillDamage();
        }


        private void GunslingerSetting_Load(object sender, EventArgs e)
        {
        }






        private void DataGridView_CombatSkill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            (sender as DataGridView).BeginEdit(true);
            ComboBox combo = (sender as DataGridView).EditingControl as ComboBox;
            combo.DroppedDown = true;
        }
    }
}
