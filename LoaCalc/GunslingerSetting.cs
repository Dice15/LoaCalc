using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public static int Preset_ControllerMaxNum = 4;

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

        private List<GunslingerCalculator.Result> calculateResult = new List<GunslingerCalculator.Result>();




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
            calculateResult = Enumerable.Repeat(new GunslingerCalculator.Result(), Skill_ControllerMaxNum).ToList();
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

                engravingName.Items.Add("선택 안 함");
                engravingName.Items.AddRange(engravingNameList.ConvertAll(name => name.ToStr()).ToArray());
                engravingLev.Items.AddRange(engravingLevList.ConvertAll(lev => lev.ToStr()).ToArray());
            }

            // 카드
            for (int i = 0; i < Card_ControllerMaxNum; i++)
            {
                ComboBox cardName = GetControlByName("Card" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox cardSet = GetControlByName("Card" + (i + 1).ToString() + "_Sets") as ComboBox;
                ComboBox cardAwakening = GetControlByName("Card" + (i + 1).ToString() + "_Awakening") as ComboBox;

                cardName.Items.Add("선택 안 함");
                cardName.Items.AddRange(cardNameList.ConvertAll(name => name.ToStr()).ToArray());
            }

            // 보석
            for (int i = 0; i < Gem_ControllerMaxNum; i++)
            {
                ComboBox gemName = GetControlByName("Gem" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gemLev = GetControlByName("Gem" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox gemTargetskill = GetControlByName("Gem" + (i + 1).ToString() + "_TargetSkillName") as ComboBox;

                gemName.Items.Add("선택 안 함");
                gemName.Items.AddRange(gemNameList.ConvertAll(name => name.ToStr()).ToArray());
                gemLev.Items.AddRange(gemLevList.ConvertAll(lev => lev.ToStr()).ToArray());
                gemTargetskill.Items.AddRange(gemTargetSkillList.ConvertAll(targetSkill => targetSkill.ToStr()).ToArray());
            }

            // 장비 세트
            for (int i = 0; i < Gear_ControllerMaxNum; i++)
            {
                ComboBox gearName = GetControlByName("Gear" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gearSet = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonus") as ComboBox;
                ComboBox gearSetLev = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonusLev") as ComboBox;

                gearName.Items.Add("선택 안 함");
                gearName.Items.AddRange(gearNameList.ConvertAll(name => name.ToStr()).ToArray());
            }

            // 버프 (시너지)
            for (int i = 0; i < Buff_ControllerMaxNum; i++)
            {
                ComboBox buffName = GetControlByName("Buff" + (i + 1).ToString() + "_Name") as ComboBox;

                buffName.Items.Add("선택 안 함");
                buffName.Items.AddRange(buffNameList.ConvertAll(name => name.ToStr()).ToArray());
            }

            // 스킬
            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                ComboBox skillName = GetControlByName("Skill" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox skillLev = GetControlByName("Skill" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox skillTp1 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp1") as ComboBox;
                ComboBox skillTp2 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp2") as ComboBox;
                ComboBox skillTp3 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp3") as ComboBox;

                skillName.Items.Add("선택 안 함");
                skillName.Items.AddRange(skillNameList.ConvertAll(name => name.ToStr()).ToArray());
                skillLev.Items.AddRange(skillLevList.ConvertAll(lev => lev.ToStr()).ToArray());
            }

            // 모든 컨트롤 값 초기화
            FormCtrlInitializer();

            // 저장된 설정이 불러오기
            FormCtrlDataLoad();
        }


        /// <summary>
        /// 모든 컨트롤의 값을 초기화 한다.
        /// </summary>
        private void FormCtrlInitializer()
        {
            foreach (Control control in allControls.Values)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    (control as TextBox).Text = "0";
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
            }
        }


        /// <summary>
        /// 저장된 Form Control 데이터 불러오기
        /// </summary>
        private void FormCtrlDataLoad()
        {
            var directoryName = Application.StartupPath;
            var serializedList = File.ReadAllText(directoryName + @"\FormCtrlList.json");
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
            }
        }




        //*********************************************************//
        //                       계산 이벤트                       //
        //*********************************************************//

        /// <summary>
        /// 캐릭터 세팅을 기준으로 스킬 데미지를 계산한다.
        /// </summary>
        private void SkillCalculate()
        {
            var character = new Gunslinger();

            UpdateSetting(character);

            calculateResult = GunslingerCalculator.Calculate(character);

            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                TextBox damageBeforeHalf = GetControlByName("Skill" + (i + 1).ToString() + "_DamageBeforeHalf") as TextBox;
                TextBox damageAfterHalf = GetControlByName("Skill" + (i + 1).ToString() + "_DamageAfterHalf") as TextBox;
                TextBox damageArithmeticAvg = GetControlByName("Skill" + (i + 1).ToString() + "_DamageArithmeticAvg") as TextBox;
                TextBox damageHarmonicAvg = GetControlByName("Skill" + (i + 1).ToString() + "_DamageHarmonicAvg") as TextBox;
                TextBox dpsArithmeticAvg = GetControlByName("Skill" + (i + 1).ToString() + "_DpsArithmeticAvg") as TextBox;
                TextBox dpsHarmonicAvg = GetControlByName("Skill" + (i + 1).ToString() + "_DpsHarmonicAvg") as TextBox;
                TextBox cooldownTime = GetControlByName("Skill" + (i + 1).ToString() + "_CooldownTime") as TextBox;

                damageBeforeHalf.Text = calculateResult[i].Damage_BeforeHalf.ToString();
                damageAfterHalf.Text = calculateResult[i].Damage_AfterHalf.ToString();
                damageArithmeticAvg.Text = calculateResult[i].Damage_ArithmeticMean.ToString();
                damageHarmonicAvg.Text = calculateResult[i].Damage_HarmonicMean.ToString();
                dpsArithmeticAvg.Text = calculateResult[i].Dps_ArithmeticAvg.ToString();
                dpsHarmonicAvg.Text = calculateResult[i].Dps_HarmonicAvg.ToString();
                cooldownTime.Text = calculateResult[i].CooldownTime.ToString();
            }
        }


        /// <summary>
        /// 스킬 상세 정보.
        /// </summary>
        private void SkillDetailed(int controlnum)
        {
            DetailedInfo detailedInfo = new DetailedInfo(calculateResult[controlnum - 1].DetailedInfo_BeforeHalf, calculateResult[controlnum - 1].DetailedInfo_AfterHalf);
            detailedInfo.Show();
        }


        /// <summary>
        /// 캐릭터 세팅을 업데이트 한다.
        /// </summary>
        private void UpdateSetting(Gunslinger character)
        {
            // 세팅값 초기화 
            character.setting.Clear();

            // 전투 특성
            character.setting.SetCombatStats(CombatStat_Crit.Text, CombatStat_Specialization.Text, CombatStat_Swiftness.Text);

            // 각인
            for (int i = 0; i < Engraving_ControllerMaxNum; i++)
            {
                ComboBox engravingName = GetControlByName("Engraving" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox engravingLev = GetControlByName("Engraving" + (i + 1).ToString() + "_Lev") as ComboBox;

                if (engravingName.Text != "선택 안 함")
                {
                    var name = engravingNameList[engravingName.SelectedIndex - 1];
                    var lev = engravingLevList[engravingLev.SelectedIndex];
                    character.setting.SetEngraving(name, lev);
                }
            }

            // 카드
            for (int i = 0; i < Card_ControllerMaxNum; i++)
            {
                ComboBox cardName = GetControlByName("Card" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox cardSet = GetControlByName("Card" + (i + 1).ToString() + "_Sets") as ComboBox;
                ComboBox cardAwakening = GetControlByName("Card" + (i + 1).ToString() + "_Awakening") as ComboBox;

                if (cardName.Text != "선택 안 함")
                {
                    var name = cardNameList[cardName.SelectedIndex - 1];
                    var set = cardSetList[name][cardSet.SelectedIndex];
                    var awakening = cardAwakeningList[name][cardAwakening.SelectedIndex];
                    character.setting.SetCard(name, set, awakening);
                }
            }

            // 보석
            for (int i = 0; i < Gem_ControllerMaxNum; i++)
            {
                ComboBox gemName = GetControlByName("Gem" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gemLev = GetControlByName("Gem" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox gemTargetskill = GetControlByName("Gem" + (i + 1).ToString() + "_TargetSkillName") as ComboBox;

                if (gemName.Text != "선택 안 함")
                {
                    var name = gemNameList[gemName.SelectedIndex - 1];
                    var lev = gemLevList[gemLev.SelectedIndex];
                    var target = gemTargetSkillList[gemTargetskill.SelectedIndex];
                    character.setting.SetGem(name, lev, target);
                }
            }

            // 장비 세트
            for (int i = 0; i < Gear_ControllerMaxNum; i++)
            {
                ComboBox gearName = GetControlByName("Gear" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox gearSet = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonus") as ComboBox;
                ComboBox gearSetLev = GetControlByName("Gear" + (i + 1).ToString() + "_SetsBonusLev") as ComboBox;

                if (gearName.Text != "선택 안 함")
                {
                    var name = gearNameList[gearName.SelectedIndex - 1];
                    var set = gearSetList[name][gearSet.SelectedIndex];
                    var setLev = gearSetLevList[name][gearSetLev.SelectedIndex];
                    character.setting.SetGear(name, set, setLev);
                }
            }

            // 무기 품질
            character.setting.SetWeaponQual(Weapon_AdditionalDamage.Text);

            // 버프 (시너지)
            for (int i = 0; i < Buff_ControllerMaxNum; i++)
            {
                ComboBox buffName = GetControlByName("Buff" + (i + 1).ToString() + "_Name") as ComboBox;

                if (buffName.Text != "선택 안 함")
                {
                    var name = buffNameList[buffName.SelectedIndex - 1];
                    character.setting.SetBuff(name);
                }
            }

            // 스킬
            for (int i = 0; i < Skill_ControllerMaxNum; i++)
            {
                ComboBox skillName = GetControlByName("Skill" + (i + 1).ToString() + "_Name") as ComboBox;
                ComboBox skillLev = GetControlByName("Skill" + (i + 1).ToString() + "_Lev") as ComboBox;
                ComboBox skillTp1 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp1") as ComboBox;
                ComboBox skillTp2 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp2") as ComboBox;
                ComboBox skillTp3 = GetControlByName("Skill" + (i + 1).ToString() + "_Tp3") as ComboBox;

                if (skillName.Text != "선택 안 함")
                {
                    var name = skillNameList[skillName.SelectedIndex - 1];
                    var lev = skillLevList[skillLev.SelectedIndex];
                    var tp1 = skillTp1List[name][skillTp1.SelectedIndex == -1 ? 0 : skillTp1.SelectedIndex];  // 비활성화 된 경우 인덱스의 값은 -1인 데 0으로 바꿔도 상관이 없다
                    var tp2 = skillTp2List[name][skillTp2.SelectedIndex == -1 ? 0 : skillTp2.SelectedIndex];
                    var tp3 = skillTp3List[name][skillTp3.SelectedIndex == -1 ? 0 : skillTp3.SelectedIndex];
                    character.setting.SetSkill(name, lev, tp1, tp2, tp3);
                }
                else character.setting.SetEmptySkill();
            }
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
            return controls.SelectMany(ctrl => GetAllControls(ctrl)).Concat(controls).Where(p => p is TextBox || p is ComboBox);
        }


        /// <summary>
        /// 해당 Name을 가진 컨트롤을 반환한다.
        /// </summary>
        private Control GetControlByName(string name)
        {
            return allControls.ContainsKey(name) ? allControls[name] : null;
        }



        //*********************************************************//
        //                       컨트롤 이벤트                     //
        //*********************************************************//

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

            if (engravingName.SelectedIndex == -1 || engravingName.SelectedIndex == Engraving_Name_PrevSelectedIndex[controlnum]) return;
            else
            {
                Engraving_Name_PrevSelectedIndex[controlnum] = engravingName.SelectedIndex;

                if (engravingName.SelectedItem.ToString() == "선택 안 함")
                {
                    engravingLev.SelectedIndex = -1;
                    engravingLev.Enabled = false;
                }
                else
                {
                    engravingLev.SelectedIndex = 0;
                    engravingLev.Enabled = true;
                }
            }
        }
        private List<int> Engraving_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Engraving_ControllerMaxNum + 1).ToList();


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

            if (cardName.SelectedIndex == -1 || cardName.SelectedIndex == Card_Name_PrevSelectedIndex[controlnum]) return;
            else
            {
                if (cardName.SelectedItem.ToString() == "선택 안 함")
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

                    cardSet.Items.AddRange(cardSetList[cardNameList[cardName.SelectedIndex - 1]].ConvertAll(set => set.ToStr()).ToArray());
                    cardAwakening.Items.AddRange(cardAwakeningList[cardNameList[cardName.SelectedIndex - 1]].ConvertAll(awakening => awakening.ToStr()).ToArray());

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
        /// 보석 이름 선택 시 발생하는 이벤트.
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

            if (gemName.SelectedIndex == -1 || gemName.SelectedIndex == Gem_Name_PrevSelectedIndex[controlnum]) return;
            else
            {
                if (gemName.SelectedItem.ToString() == "선택 안 함")
                {
                    gemLev.SelectedIndex = -1;
                    gemLev.Enabled = false;

                    gemTargerskillname.SelectedIndex = -1;
                    gemTargerskillname.Enabled = false;
                }
                else
                {
                    gemLev.SelectedIndex = 0;
                    gemLev.Enabled = true;

                    gemTargerskillname.SelectedIndex = 0;
                    gemTargerskillname.Enabled = true;
                }

                Gem_Name_PrevSelectedIndex[controlnum] = gemName.SelectedIndex;
            }

        }
        private List<int> Gem_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Gem_ControllerMaxNum + 1).ToList();


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

            if (gearName.SelectedIndex == -1 || gearName.SelectedIndex == Gear_Name_PrevSelectedIndex[controlnum]) return;
            else
            {
                if (gearName.SelectedItem.ToString() == "선택 안 함")
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

                    gearSet.Items.AddRange(gearSetList[gearNameList[gearName.SelectedIndex - 1]].ConvertAll(set => set.ToStr()).ToArray());
                    gearSetlev.Items.AddRange(gearSetLevList[gearNameList[gearName.SelectedIndex - 1]].ConvertAll(setLev => setLev.ToStr()).ToArray());

                    gearSet.SelectedIndex = 0;
                    gearSetlev.SelectedIndex = 0;

                    gearSet.Enabled = true;
                    gearSetlev.Enabled = true;
                }

                Gear_Name_PrevSelectedIndex[controlnum] = gearName.SelectedIndex;
            }
        }
        private List<int> Gear_Name_PrevSelectedIndex = Enumerable.Repeat(-1, Gear_ControllerMaxNum + 1).ToList();


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

            if (buffName.SelectedIndex == -1 || buffName.SelectedIndex == Buff_Name_PrevSelectedIndex[controlnum]) return;
            else
            {
                Buff_Name_PrevSelectedIndex[controlnum] = buffName.SelectedIndex;
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

            if (skillName.SelectedIndex == -1 || skillName.SelectedIndex == Skill_Name_PrevSelectedIndex[controlnum]) return;
            else
            {
                if (skillName.SelectedItem.ToString() == "선택 안 함")
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

            if (skillLev.SelectedIndex == -1 || skillLev.SelectedIndex == Skill_Lev_PrevSelectedIndex[controlnum]) return;
            else
            {
                if (skillLev.SelectedItem.ToString() == SettingInfo.Skill.LEV.__1레벨.ToStr())
                {
                    skillTp1.SelectedIndex = -1;
                    skillTp2.SelectedIndex = -1;
                    skillTp3.SelectedIndex = -1;

                    skillTp1.Enabled = false;
                    skillTp2.Enabled = false;
                    skillTp3.Enabled = false;
                }
                else if (skillLev.SelectedItem.ToString() == SettingInfo.Skill.LEV.__4레벨.ToStr())
                {
                    skillTp1.SelectedIndex = skillTp1.Enabled == false ? 0 : skillTp1.SelectedIndex;
                    skillTp2.SelectedIndex = -1;
                    skillTp3.SelectedIndex = -1;

                    skillTp1.Enabled = true;
                    skillTp2.Enabled = false;
                    skillTp3.Enabled = false;
                }
                else if (skillLev.SelectedItem.ToString() == SettingInfo.Skill.LEV.__7레벨.ToStr())
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
            }
        }
        private List<int> Skill_Lev_PrevSelectedIndex = Enumerable.Repeat(-1, Skill_ControllerMaxNum + 1).ToList();


        /// <summary>
        /// 계산 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void Calculate_Click(object sender, EventArgs e)
        {
            SkillCalculate();
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
                FormCtrlList.Add(new FormCtrl(skillName.GetType(), skillName.Name, skillName.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillLev.GetType(), skillLev.Name, skillLev.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillTp1.GetType(), skillTp1.Name, skillTp1.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillTp2.GetType(), skillTp2.Name, skillTp2.SelectedIndex.ToString()));
                FormCtrlList.Add(new FormCtrl(skillTp3.GetType(), skillTp3.Name, skillTp3.SelectedIndex.ToString()));
            }

            var currDirectory = Application.StartupPath;
            var fileName = "Preset" + controlnum.ToString();
            var serializedList = JsonConvert.SerializeObject(FormCtrlList, Formatting.Indented);

            if (File.Exists(currDirectory + @"\" + fileName))
            {
                var result = MessageBox.Show("기존 프리셋에 덮어쓰겠습니까?", "", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    File.WriteAllText(currDirectory + @"\" + fileName, serializedList);
                    if(File.Exists(currDirectory + @"\" + fileName)) MessageBox.Show("프리셋 저장 완료");
                }
            }
            else
            {
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

            var currDirectory = Application.StartupPath;
            var fileName = "Preset" + controlnum.ToString();

            FormCtrlInitializer();

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
                }

                MessageBox.Show("프리셋 로딩 완료");
            }
            else
            {
                MessageBox.Show("프리셋 로딩 완료");
            }
        }


        /// <summary>
        /// 세팅 리셋 버튼 클릭할 시 발생하는 이벤트.
        /// </summary>
        private void ResetSetting_Click(object sender, EventArgs e)
        {
            FormCtrlInitializer();
            MessageBox.Show("현재 프리셋 리셋 완료");
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

            SkillDetailed(controlnum);
        }


        private void GunslingerSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
