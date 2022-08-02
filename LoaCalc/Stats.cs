using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoaCalc
{
    public class SettingInfo
    {
        public class CombatStats
        {
            public int critical = 0;
            public int specialization = 0;
            public int domination = 0;
            public int swiftness = 0;
            public int endurance = 0;
            public int expertise = 0;

            public CombatStats Copy() => new CombatStats
            {
                critical = this.critical,
                specialization = this.specialization,
                domination = this.domination,
                swiftness = this.swiftness,
                endurance = this.endurance,
                expertise = this.expertise
            };
        }

        public class Engraving
        {
            public enum NAME { 원한, 예리한_둔기, 저주받은_인형, 질량_증가, 타격의_대가, 기습의_대가, 아드레날린, 돌격대장, 피스메이커 }
            public enum LEV { __1레벨, __2레벨, __3레벨 }


            public NAME name = NAME.원한;
            public LEV lev = LEV.__1레벨;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public static List<LEV> GetLevList() => Enum.GetValues(typeof(LEV)).OfType<LEV>().ToList();
            public Engraving Copy() => new Engraving { name = name, lev = lev };
        }

        public class Card
        {
            public enum NAME { 남겨진_바람의_절벽, 세상을_구하는_빛 }
            public enum SET { __1세트, __2세트, __3세트, __4세트, __5세트, __6세트 }
            public enum AWAKENING { __0각성, __9각성, __12각성, __15각성, __18각성, __30각성 }


            public NAME name = NAME.남겨진_바람의_절벽;
            public SET set = SET.__1세트;
            public AWAKENING awakening = AWAKENING.__0각성;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public static Dictionary<NAME, List<SET>> GetSetList() => new Dictionary<NAME, List<SET>>
            {
                { NAME.남겨진_바람의_절벽, new List<SET> { SET.__6세트 } },
                { NAME.세상을_구하는_빛, new List<SET> { SET.__6세트 } },
            };
            public static Dictionary<NAME, List<AWAKENING>> GetAwakeningList() => new Dictionary<NAME, List<AWAKENING>>
            {
                { NAME.남겨진_바람의_절벽, new List<AWAKENING> { AWAKENING.__0각성, AWAKENING.__12각성 } },
                { NAME.세상을_구하는_빛, new List<AWAKENING> { AWAKENING.__0각성, AWAKENING.__18각성, AWAKENING.__30각성 } },
            };

            public Card Copy() => new Card { name = this.name, set = this.set, awakening = this.awakening };
        }

        public class Gem
        {
            public enum NAME { 멸화, 홍염 }
            public enum LEV { __7레벨, __8레벨, __9레벨, __10레벨 }


            public NAME name = NAME.멸화;
            public LEV lev = LEV.__7레벨;
            public Skill.NAME targetSkill = Skill.NAME.레인_오브_불릿;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public static List<LEV> GetLevList() => Enum.GetValues(typeof(LEV)).OfType<LEV>().ToList();
            public Gem Copy() => new Gem { name = name, lev = lev, targetSkill = targetSkill };
        }

        public class Gear
        {
            public enum NAME { 악몽, 구원, 환각, 사멸, 지배 }
            public enum SET  { __1세트, __2세트, __3세트, __4세트, __5세트, __6세트 }
            public enum LEV { __1레벨, __2레벨, __3레벨 }


            public NAME name = NAME.악몽;
            public SET set = SET.__1세트;
            public LEV setLev = LEV.__1레벨;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public static Dictionary<NAME, List<SET>> GetSetList() => new Dictionary<NAME, List<SET>>
            {
                { NAME.악몽, new List<SET> { SET.__6세트 } },
                { NAME.구원, new List<SET> { SET.__6세트 } },
                { NAME.환각, new List<SET> { SET.__6세트 } },
                { NAME.사멸, new List<SET> { SET.__6세트 } },
                { NAME.지배, new List<SET> { SET.__6세트 } },
            };
            public static Dictionary<NAME, List<LEV>> GetSetLevList() => new Dictionary<NAME, List<LEV>>
            {
                { NAME.악몽, new List<LEV> { LEV.__3레벨 } },
                { NAME.구원, new List<LEV> { LEV.__3레벨 } },
                { NAME.환각, new List<LEV> { LEV.__3레벨 } },
                { NAME.사멸, new List<LEV> { LEV.__3레벨 } },
                { NAME.지배, new List<LEV> { LEV.__3레벨 } },
            };

            public Gear Copy() => new Gear { name = name, set = set, setLev = setLev };
        }

        public class WeaponQual
        {
            public decimal additionalDamage = 0;

            public WeaponQual Copy() => new WeaponQual { additionalDamage = additionalDamage };
        }

        public class Buff
        {
            public enum NAME { 건슬링어_치적_시너지 , 갈망_6세트_버프 }


            public NAME name = NAME.건슬링어_치적_시너지;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public Buff Copy() => new Buff { name = name };
        }
        
        public class Skill
        {
            private static HashSet<NAME> awakeningName = new HashSet<NAME>
            { 
                NAME.황혼의_눈, NAME.황혼의_눈_샷건활용, NAME.황혼의_눈_라이플활용, NAME.대구경_폭발_탄환, NAME.대구경_폭발_탄환_샷건활용, NAME.대구경_폭발_탄환_라이플활용
            };
            private static HashSet<NAME> customName = new HashSet<NAME> { NAME.대재앙_샷건활용 };
            public enum NAME 
            { 
                레인_오브_불릿, 
                샷건_연사, 최후의_만찬, 절멸의_탄환, 마탄의_사수, 
                대재앙, 대재앙_샷건활용, 퍼펙트_샷, 포커스_샷, 타겟_다운,
                황혼의_눈, 황혼의_눈_샷건활용, 황혼의_눈_라이플활용, 대구경_폭발_탄환, 대구경_폭발_탄환_샷건활용, 대구경_폭발_탄환_라이플활용
            };
            
            public enum LEV { __1레벨, __4레벨, __7레벨, __10레벨, __11레벨, __12레벨 };
            public enum TRIPOD
            {
                기습, 광역_사격, 사면초가, 원거리_사격, 날렵한_움직임, 속사, 화염_사격, 빠른_준비,        // 레인 오브 불릿 (기습, 광역_사격, 사면초가, 원거리_사격, 날렵한_움직임, 속사, 화염_사격, 빠른_준비)
                콤보_연사, 강화된_사격, 회피의_달인, 연장_사격, 특수_탄환,                                // 샷건 연사 (기습, 사면초가, 콤보_연사, 강화된_사격, 회피의_달인, 빠른_준비, 연장_사격, 특수_탄환)
                화염탄, 냉기탄, 뜨거운_열기, 강한_폭발, 집행, 더블_샷, 연발_사격,                         // 최후의 만찬 (빠른_준비, 화염탄, 냉기탄, 뜨거운_열기, 강한_폭발, 집행, 더블_샷, 연발_사격)
                재빠른_손놀림, 다가오는_죽음, 강인함, 강화_사격, 뇌진탕, 최후의_일격, 반동_회피,          // 절멸의 탄환 (사면초가, 재빠른_손놀림, 다가오는_죽음, 강인함, 강화_사격, 뇌진탕, 최후의_일격, 반동_회피)
                무한의_마탄, 전방위_사격, 영혼의_일발, 가디언의_숨결, 혹한의_안식처,                      // 마탄의 사수 (무한의_마탄, 원거리_사격, 사면초가, 특수_탄환, 전방위_사격, 영혼의_일발, 가디언의_숨결, 혹한의_안식처)
                원거리_조준, 재빠른_조준, 숨통_끊기, 무방비_표적, 융단_폭격, 영원한_재앙,                 // 대재앙 (강인함, 원거리_조준, 재빠른_조준, 숨통_끊기, 무방비_표적, 뇌진탕, 융단_폭격, 영원한_재앙)
                출혈_효과, 안정된_자세, 근육_경련, 정밀_사격, 완벽한_조준, 마무리_사격, 준비된_사수,      // 퍼펙트 샷 (출혈_효과, 안정된_자세, 근육_경련, 정밀_사격, 완벽한_조준, 마무리_사격,  준비된_사수, 강화된_사격)
                방향_전환, 강화_탄환, 섬광, 더블탭, 빠른_마무리,                                          // 포커스 샷 (방향_전환, 재빠른_조준, 근육_경련, 강화_탄환, 섬광, 더블탭, 빠른_마무리, 최후의_일격)
                숨_참기, 대구경_탄환, 대용량_탄창, 작렬철강탄, 반자동_라이플, 정조준, 천국의_계단,        // 타겟 다운 (재빠른_조준, 숨_참기, 대구경_탄환, 대용량_탄창, 작렬철강탄, 반자동_라이플, 정조준, 천국의_계단)
                선택_불가
            };
            public enum CATEGORY { 일반_스킬, 각성_스킬, 핸드건_스탠스, 샷건_스탠스, 라이플_스탠스 };
            public enum TYPE { 일반, 지점, 콤보, 홀딩, 캐스팅, 차지 };
            public enum ATTACKTYPE { 타대, 백_어택, 헤드_어택 };
            public enum CLASSENGRAVING { 피스메이커_핸드건_스탠스, 피스메이커_샷건_스탠스, 피스메이커_라이플_스탠스, 사냥의_시간_핸드건_스탠스, 사냥의_시간_샷건_스탠스, 사냥의_시간_라이플_스탠스 };
            
            
            public NAME name = NAME.레인_오브_불릿;
            public LEV lev = LEV.__1레벨;
            public TRIPOD tp1 = TRIPOD.기습;
            public TRIPOD tp2 = TRIPOD.원거리_사격;
            public TRIPOD tp3 = TRIPOD.화염_사격;


            public static List<NAME> GetNameList(bool exceptCustom = false, bool exceptAwakening = false)
            {
                List<NAME> nameList = new List<NAME>();

                foreach (var name in Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList())
                {
                    if (exceptCustom && customName.Contains(name)) continue;
                    if (exceptAwakening && awakeningName.Contains(name)) continue;
                    nameList.Add(name);
                }

                return nameList;
            }            
            public static List<LEV> GetLevList() => Enum.GetValues(typeof(LEV)).OfType<LEV>().ToList();
            public static Dictionary<NAME, List<TRIPOD>> GetTripod1List() => new Dictionary<NAME, List<TRIPOD>>
            {
                { NAME.레인_오브_불릿, new List<TRIPOD>{ TRIPOD.기습, TRIPOD.광역_사격, TRIPOD.사면초가 } },
                { NAME.샷건_연사, new List<TRIPOD>{ TRIPOD.기습, TRIPOD.사면초가, TRIPOD.콤보_연사 } },
                { NAME.최후의_만찬, new List<TRIPOD> { TRIPOD.빠른_준비, TRIPOD.화염탄, TRIPOD.냉기탄 } },
                { NAME.절멸의_탄환, new List<TRIPOD> { TRIPOD.사면초가, TRIPOD.재빠른_손놀림, TRIPOD.다가오는_죽음 } },
                { NAME.마탄의_사수, new List<TRIPOD> { TRIPOD.무한의_마탄, TRIPOD.원거리_사격, TRIPOD.사면초가 } },
                { NAME.대재앙, new List<TRIPOD> { TRIPOD.강인함, TRIPOD.원거리_조준, TRIPOD.재빠른_조준 } },
                { NAME.대재앙_샷건활용, new List<TRIPOD> { TRIPOD.강인함, TRIPOD.원거리_조준, TRIPOD.재빠른_조준 } },
                { NAME.퍼펙트_샷, new List<TRIPOD> { TRIPOD.출혈_효과, TRIPOD.안정된_자세, TRIPOD.근육_경련 } },
                { NAME.포커스_샷, new List<TRIPOD> { TRIPOD.방향_전환, TRIPOD.재빠른_조준, TRIPOD.근육_경련 } },
                { NAME.타겟_다운, new List<TRIPOD> { TRIPOD.재빠른_조준, TRIPOD.숨_참기, TRIPOD.대구경_탄환 } },
                { NAME.황혼의_눈, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_샷건활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_라이플활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_샷건활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_라이플활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
            };
            public static Dictionary<NAME, List<TRIPOD>> GetTripod2List() => new Dictionary<NAME, List<TRIPOD>>
            {
                { NAME.레인_오브_불릿, new List<TRIPOD>{ TRIPOD.원거리_사격, TRIPOD.날렵한_움직임, TRIPOD.속사 } },
                { NAME.샷건_연사, new List<TRIPOD>{ TRIPOD.강화된_사격, TRIPOD.회피의_달인, TRIPOD.빠른_준비 } },
                { NAME.최후의_만찬, new List<TRIPOD>{ TRIPOD.뜨거운_열기, TRIPOD.강한_폭발, TRIPOD.집행 } },
                { NAME.절멸의_탄환, new List<TRIPOD>{ TRIPOD.강인함, TRIPOD.강화_사격, TRIPOD.뇌진탕 } },
                { NAME.마탄의_사수, new List<TRIPOD>{ TRIPOD.특수_탄환, TRIPOD.전방위_사격, TRIPOD.영혼의_일발 } },
                { NAME.대재앙, new List<TRIPOD>{ TRIPOD.숨통_끊기, TRIPOD.무방비_표적, TRIPOD.뇌진탕 } },
                { NAME.대재앙_샷건활용, new List<TRIPOD>{ TRIPOD.숨통_끊기, TRIPOD.무방비_표적, TRIPOD.뇌진탕 } },
                { NAME.퍼펙트_샷, new List<TRIPOD>{ TRIPOD.정밀_사격, TRIPOD.완벽한_조준, TRIPOD.마무리_사격 } },
                { NAME.포커스_샷, new List<TRIPOD>{ TRIPOD.강화_탄환, TRIPOD.섬광, TRIPOD.더블탭 } },
                { NAME.타겟_다운, new List<TRIPOD>{ TRIPOD.대용량_탄창, TRIPOD.작렬철강탄, TRIPOD.반자동_라이플 } },
                { NAME.황혼의_눈, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_샷건활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_라이플활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_샷건활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_라이플활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
            };
            public static Dictionary<NAME, List<TRIPOD>> GetTripod3List() => new Dictionary<NAME, List<TRIPOD>>
            {
                { NAME.레인_오브_불릿, new List<TRIPOD>{ TRIPOD.화염_사격, TRIPOD.빠른_준비 } },
                { NAME.샷건_연사, new List<TRIPOD>{ TRIPOD.연장_사격, TRIPOD.특수_탄환 } },
                { NAME.최후의_만찬, new List<TRIPOD>{ TRIPOD.더블_샷, TRIPOD.연발_사격 } },
                { NAME.절멸의_탄환, new List<TRIPOD>{ TRIPOD.최후의_일격, TRIPOD.반동_회피 } },
                { NAME.마탄의_사수, new List<TRIPOD>{ TRIPOD.가디언의_숨결, TRIPOD.혹한의_안식처 } },
                { NAME.대재앙, new List<TRIPOD>{ TRIPOD.융단_폭격, TRIPOD.영원한_재앙 } },
                { NAME.대재앙_샷건활용, new List<TRIPOD>{ TRIPOD.융단_폭격, TRIPOD.영원한_재앙 } },
                { NAME.퍼펙트_샷, new List<TRIPOD>{ TRIPOD.준비된_사수, TRIPOD.강화된_사격 } },
                { NAME.포커스_샷, new List<TRIPOD>{ TRIPOD.빠른_마무리, TRIPOD.최후의_일격 } },
                { NAME.타겟_다운, new List<TRIPOD>{ TRIPOD.정조준, TRIPOD.천국의_계단 } },
                { NAME.황혼의_눈, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_샷건활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_라이플활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_샷건활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_라이플활용, new List<TRIPOD> { TRIPOD.선택_불가 } },
            };
            public Skill Copy() => new Skill { name = name, lev = lev, tp1 = tp1, tp2 = tp2, tp3 = tp3 };
        }
    }
    

    public class ValueM
    {
        public enum Group { None, AttackPowerPer_Engraving, Damage_PeaceMakerRifleStance };

        private Dictionary<Group, decimal> groupValues = new Dictionary<Group, decimal>();
        
        public ValueM() { }
        public ValueM(decimal value, Group group = Group.None)
        {
            Add(value, group);
        }
        public ValueM(ValueM valuem)
        {
            Add(valuem);
        }

        public void Add(decimal value, Group group = Group.None)  // 그룹이 없을 시 -> 기본그룹(값끼리 곱적용)
        {
            if (group == Group.None)
            {
                if (groupValues.ContainsKey(Group.None))
                {
                    var v1 = 1 + (value / 100);
                    var v2 = 1 + (groupValues[Group.None] / 100);
                    groupValues[Group.None] = ((v1 * v2) - 1) * 100;
                }
                else groupValues.Add(Group.None, value);
            }
            else
            {
                if (groupValues.ContainsKey(group))
                {
                    groupValues[group] += value;
                }
                else groupValues.Add(group, value);
            }
        }
        public void Add(ValueM valueM)
        {
            foreach (var group in valueM.groupValues)
                Add(group.Value, group.Key);       
        }
        public ValueM Copy()
        {
            ValueM valueM = new ValueM();

            foreach (var group in groupValues)
                valueM.Add(group.Value, group.Key);

            return valueM;
        }
        public decimal GetValue()
        {
            decimal result = 1;

            foreach (var v in groupValues)
                result *= 1 + (v.Value / 100);

            return (result - 1) * 100;
        }
        public void clear()
        {
            groupValues.Clear();
        }
        public new string ToString()
        {
            string s = "";

            s += " - Value: " + GetValue().ToString() + Environment.NewLine;
            s += " - Info: " + Environment.NewLine;

            foreach (var v in groupValues)
            {
                s += "   -> " + v.Key.ToString() + ": " + v.Value + Environment.NewLine;
            }

            return s;
        }
    }
    public class ValueP
    {
        private decimal Value = 0;

        public ValueP() { }
        public ValueP(decimal value)
        {
            Add(value);
        }
        public ValueP(ValueP valueP)
        {
            Add(valueP);
        }
        public void Add(decimal value)
        {
            Value += value;
        }
        public void Add(ValueP valueP)
        {
            Add(valueP.Value);
        }
        public ValueP Copy() => new ValueP(Value);
        public void clear()
        {
            Value = 0;
        }
        public decimal GetValue() => Value;
        public new string ToString()
        {
            string s = "";

            s += " - Value: " + GetValue().ToString() + Environment.NewLine;

            return s;
        }
    }
    public class Stats
    {
        public ValueP attackSpeed = new ValueP();
        public ValueP moveSpeed = new ValueP();
        public ValueP additionalDamage = new ValueP();
        public ValueP criticalRate = new ValueP();
        public ValueP criticalDamage = new ValueP();
        public ValueM damage = new ValueM();
        public ValueM attackPowerPer = new ValueM();
        public ValueM skillCoolDown = new ValueM();

        public Stats() { }
        public Stats(Stats stats)
        {
            attackSpeed = new ValueP(stats.attackSpeed);
            moveSpeed = new ValueP(stats.moveSpeed);
            additionalDamage = new ValueP(stats.additionalDamage);
            criticalRate = new ValueP(stats.criticalRate);
            criticalDamage = new ValueP(stats.criticalDamage);
            damage = new ValueM(stats.damage);
            attackPowerPer = new ValueM(stats.attackPowerPer);
            skillCoolDown = new ValueM(stats.skillCoolDown);
        }
        public void Add(Stats stat)
        {
            attackSpeed.Add(stat.attackSpeed);
            moveSpeed.Add(stat.moveSpeed);
            additionalDamage.Add(stat.additionalDamage);
            criticalRate.Add(stat.criticalRate);
            criticalDamage.Add(stat.criticalDamage);
            damage.Add(stat.damage);
            attackPowerPer.Add(stat.attackPowerPer);
            skillCoolDown.Add(stat.skillCoolDown);
        }
        public Stats Copy() => new Stats
        {
            attackSpeed = attackSpeed.Copy(),
            moveSpeed = moveSpeed.Copy(),
            additionalDamage = additionalDamage.Copy(),
            criticalRate = criticalRate.Copy(),
            criticalDamage = criticalDamage.Copy(),
            damage = damage.Copy(),
            attackPowerPer = attackPowerPer.Copy(),
            skillCoolDown = skillCoolDown.Copy(),
        };
        public new string ToString()
        {
            string s = "";

            s += "1) AttackSpeed" + Environment.NewLine + attackSpeed.ToString() + Environment.NewLine;
            s += "2) MoveSpeed" + Environment.NewLine + moveSpeed.ToString() + Environment.NewLine;
            s += "3) AdditionalDamage" + Environment.NewLine + additionalDamage.ToString() + Environment.NewLine;
            s += "4) CriticalRate" + Environment.NewLine + criticalRate.ToString() + Environment.NewLine;
            s += "5) CriticalDamage" + Environment.NewLine + criticalDamage.ToString() + Environment.NewLine;
            s += "6) Damage" + Environment.NewLine + damage.ToString() + Environment.NewLine;
            s += "7) AttackPowerPer" + Environment.NewLine + attackPowerPer.ToString() + Environment.NewLine;
            s += "8) CoolDown" + Environment.NewLine + skillCoolDown.ToString() + Environment.NewLine;

            return s;
        }
    }
}