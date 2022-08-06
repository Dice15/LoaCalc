using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaCalc
{

    /// <summary>
    /// 캐릭터 스팩 세팅을 관리하는 클래스.
    /// </summary>
    public class SettingInfo
    {

        /// <summary>
        /// 전투 특성 시스템.
        /// </summary>
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


        /// <summary>
        /// 각인 시스템
        /// </summary>
        public class Engraving
        {
            public enum NAME { 원한, 예리한_둔기, 저주받은_인형, 질량_증가, 타격의_대가, 기습의_대가, 아드레날린, 속전속결, 돌격대장, 피스메이커, 사냥의_시간 }
            public enum LEV { __1레벨, __2레벨, __3레벨 }


            public NAME name = NAME.원한;
            public LEV lev = LEV.__1레벨;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public static List<LEV> GetLevList() => Enum.GetValues(typeof(LEV)).OfType<LEV>().ToList();
            public Engraving Copy() => new Engraving { name = name, lev = lev };
        }


        /// <summary>
        /// 카드 시스템.
        /// </summary>
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


        /// <summary>
        /// 보석 시스템.
        /// </summary>
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


        /// <summary>
        /// 장비 세트 시스템.
        /// </summary>
        public class Gear
        {
            public enum NAME { 악몽, 구원, 환각, 사멸, 지배 }
            public enum SET { __1세트, __2세트, __3세트, __4세트, __5세트, __6세트 }
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



        /// <summary>
        /// 무기 품질 시스템.
        /// </summary>
        public class WeaponQual
        {
            public decimal additionalDamage = 0;

            public WeaponQual Copy() => new WeaponQual { additionalDamage = additionalDamage };
        }



        /// <summary>
        /// 클래스별 버프 및 시너지.
        /// </summary>
        public class Buff
        {
            public enum NAME
            {
                건슬링어_치적_시너지,
                갈망_6세트_버프,
                서폿_낙인,
                서폿_공증,
                서폿_2버블,
                데빌헌터_치적_시너지,
                아르카나_치적_시너지,
                기상술사_치적_시너지,
                배틀마스터_치적_시너지,
                창술사_치적_시너지,
                스트라이커_치적_시너지,
                블래스터_방깍_시너지,
                디스트로이어_방깍_시너지,
                워로드_방깍_시너지,
                서머너_방깍_시너지,
                리퍼_방깍_시너지,
                버서커_피증_시너지,
                호크아이_피증_시너지,
                인파이터_피증_시너지,
                소서리스_피증_시너지,
                데모닉_피증_시너지,
                스카우터_공증_시너지,
                기공사_공증_시너지,
                워로드_백헤드_피증_시너지,
                블레이드_백헤드_피증_시너지,
            }


            public NAME name = NAME.건슬링어_치적_시너지;


            public static List<NAME> GetNameList() => Enum.GetValues(typeof(NAME)).OfType<NAME>().ToList();
            public Buff Copy() => new Buff { name = name };
        }



        /// <summary>
        /// 스킬 설정.
        /// </summary>
        public class Skill
        {
            private static HashSet<NAME> awakeningName = new HashSet<NAME>
            {
                NAME.황혼의_눈, NAME.황혼의_눈_샷건, NAME.황혼의_눈_라이플, NAME.대구경_폭발_탄환, NAME.대구경_폭발_탄환_샷건, NAME.대구경_폭발_탄환_라이플
            };
            private static HashSet<NAME> customName = new HashSet<NAME> { NAME.대재앙_샷건 };
            public enum NAME
            {
                AT02_유탄, 메테오_스트림, 이퀄리브리엄, 레인_오브_불릿,
                심판의_시간, 샷건_연사, 최후의_만찬, 절멸의_탄환, 마탄의_사수,
                스파이럴_플레임, 대재앙, 대재앙_샷건, 퍼펙트_샷, 포커스_샷, 타겟_다운,
                황혼의_눈, 황혼의_눈_샷건, 황혼의_눈_라이플, 대구경_폭발_탄환, 대구경_폭발_탄환_샷건, 대구경_폭발_탄환_라이플
            };

            public enum LEV { __1레벨, __4레벨, __7레벨, __10레벨, __11레벨, __12레벨 };
            public enum TRIPOD
            {
                원거리_투척, 넓은_폭발, 마력_조절, 강화_수류탄, 세열_수류탄, 빙결_수류탄, 불꽃놀이, 내부_발화,    // AT02 유탄 (원거리_투척, 넓은_폭발, 마력_조절, 강화_수류탄, 세열_수류탄, 빙결_수류탄, 불꽃놀이, 내부_발화)
                근육경련, 약점포착, 비열한_공격, 꿰둟는_폭발, 폭격_지원, 혜성_낙화,                               // 메테오 스트림 (마력_조절, 근육경련, 약점포착, 넓은_폭발, 비열한_공격, 꿰둟는_폭발, 폭격_지원, 혜성_낙화)
                고속_사격, 급소_노출, 회피의_달인, 섬멸_사격, 적_소탕, 원거리_사격, 화상_효과, 급소_사격,         // 이퀄리브리엄 (고속_사격, 급소_노출, 회피의_달인, 섬멸_사격, 적_소탕, 원거리_사격, 화상_효과, 급소_사격)
                기습, 광역_사격, 사면초가, 날렵한_움직임, 속사, 화염_사격, 빠른_준비,                             // 레인 오브 불릿 (기습, 광역_사격, 사면초가, 원거리_사격, 날렵한_움직임, 속사, 화염_사격, 빠른_준비)
                징역_선고, 최종_판결, 증거_인멸, 강화_파편, 확산_파편,                                            // 심판의 시간 (빠른_준비, 마력_조절, 회피의_달인, 징역_선고, 최종_판결, 증거_인멸, 강화_파편, 확산_파편)
                콤보_연사, 강화된_사격, 연장_사격, 특수_탄환,                                                     // 샷건 연사 (기습, 사면초가, 콤보_연사, 강화된_사격, 회피의_달인, 빠른_준비, 연장_사격, 특수_탄환)
                화염탄, 냉기탄, 뜨거운_열기, 강한_폭발, 집행, 더블_샷, 연발_사격,                                 // 최후의 만찬 (빠른_준비, 화염탄, 냉기탄, 뜨거운_열기, 강한_폭발, 집행, 더블_샷, 연발_사격)
                재빠른_손놀림, 다가오는_죽음, 강인함, 강화_사격, 뇌진탕, 최후의_일격, 반동_회피,                  // 절멸의 탄환 (사면초가, 재빠른_손놀림, 다가오는_죽음, 강인함, 강화_사격, 뇌진탕, 최후의_일격, 반동_회피)
                무한의_마탄, 전방위_사격, 영혼의_일발, 가디언의_숨결, 혹한의_안식처,                              // 마탄의 사수 (무한의_마탄, 원거리_사격, 사면초가, 특수_탄환, 전방위_사격, 영혼의_일발, 가디언의_숨결, 혹한의_안식처)
                맹렬한_불꽃, 재빠른_조준, 성장_탄환, 고속_탄환, 마무리_사격, 총열_강화, 후폭풍,                   // 스파이럴 플레임 (냉기탄, 맹렬한_불꽃, 재빠른_조준, 성장_탄환, 고속_탄환, 마무리_사격, 총열_강화, 후폭풍)
                원거리_조준, 숨통_끊기, 무방비_표적, 융단_폭격, 영원한_재앙,                                      // 대재앙 (강인함, 원거리_조준, 재빠른_조준, 숨통_끊기, 무방비_표적, 뇌진탕, 융단_폭격, 영원한_재앙)
                출혈_효과, 안정된_자세, 근육_경련, 정밀_사격, 완벽한_조준, 준비된_사수,                           // 퍼펙트 샷 (출혈_효과, 안정된_자세, 근육_경련, 정밀_사격, 완벽한_조준, 마무리_사격,  준비된_사수, 강화된_사격)
                방향_전환, 강화_탄환, 섬광, 더블탭, 빠른_마무리,                                                  // 포커스 샷 (방향_전환, 재빠른_조준, 근육_경련, 강화_탄환, 섬광, 더블탭, 빠른_마무리, 최후의_일격)
                숨_참기, 대구경_탄환, 대용량_탄창, 작렬철강탄, 반자동_라이플, 정조준, 천국의_계단,                // 타겟 다운 (재빠른_조준, 숨_참기, 대구경_탄환, 대용량_탄창, 작렬철강탄, 반자동_라이플, 정조준, 천국의_계단)
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
                { NAME.AT02_유탄, new List<TRIPOD>{ TRIPOD.원거리_투척, TRIPOD.넓은_폭발, TRIPOD.마력_조절 } },
                { NAME.메테오_스트림, new List<TRIPOD>{ TRIPOD.마력_조절, TRIPOD.근육경련, TRIPOD.약점포착 } },
                { NAME.이퀄리브리엄, new List<TRIPOD>{ TRIPOD.고속_사격, TRIPOD.급소_노출, TRIPOD.회피의_달인 } },
                { NAME.레인_오브_불릿, new List<TRIPOD>{ TRIPOD.기습, TRIPOD.광역_사격, TRIPOD.사면초가 } },
                { NAME.심판의_시간, new List<TRIPOD>{ TRIPOD.빠른_준비, TRIPOD.마력_조절, TRIPOD.회피의_달인 } },
                { NAME.샷건_연사, new List<TRIPOD>{ TRIPOD.기습, TRIPOD.사면초가, TRIPOD.콤보_연사 } },
                { NAME.최후의_만찬, new List<TRIPOD> { TRIPOD.빠른_준비, TRIPOD.화염탄, TRIPOD.냉기탄 } },
                { NAME.절멸의_탄환, new List<TRIPOD> { TRIPOD.사면초가, TRIPOD.재빠른_손놀림, TRIPOD.다가오는_죽음 } },
                { NAME.마탄의_사수, new List<TRIPOD> { TRIPOD.무한의_마탄, TRIPOD.원거리_사격, TRIPOD.사면초가 } },
                { NAME.스파이럴_플레임, new List<TRIPOD>{ TRIPOD.냉기탄, TRIPOD.맹렬한_불꽃, TRIPOD.재빠른_조준 } },
                { NAME.대재앙, new List<TRIPOD> { TRIPOD.강인함, TRIPOD.원거리_조준, TRIPOD.재빠른_조준 } },
                { NAME.대재앙_샷건, new List<TRIPOD> { TRIPOD.강인함, TRIPOD.원거리_조준, TRIPOD.재빠른_조준 } },
                { NAME.퍼펙트_샷, new List<TRIPOD> { TRIPOD.출혈_효과, TRIPOD.안정된_자세, TRIPOD.근육_경련 } },
                { NAME.포커스_샷, new List<TRIPOD> { TRIPOD.방향_전환, TRIPOD.재빠른_조준, TRIPOD.근육_경련 } },
                { NAME.타겟_다운, new List<TRIPOD> { TRIPOD.재빠른_조준, TRIPOD.숨_참기, TRIPOD.대구경_탄환 } },
                { NAME.황혼의_눈, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_샷건, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_라이플, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_샷건, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_라이플, new List<TRIPOD> { TRIPOD.선택_불가 } },
            };
            public static Dictionary<NAME, List<TRIPOD>> GetTripod2List() => new Dictionary<NAME, List<TRIPOD>>
            {
                { NAME.AT02_유탄, new List<TRIPOD>{ TRIPOD.강화_수류탄, TRIPOD.세열_수류탄, TRIPOD.빙결_수류탄 } },
                { NAME.메테오_스트림, new List<TRIPOD>{ TRIPOD.넓은_폭발, TRIPOD.비열한_공격, TRIPOD.꿰둟는_폭발 } },
                { NAME.이퀄리브리엄, new List<TRIPOD>{ TRIPOD.섬멸_사격, TRIPOD.적_소탕, TRIPOD.원거리_사격 } },
                { NAME.레인_오브_불릿, new List<TRIPOD>{ TRIPOD.원거리_사격, TRIPOD.날렵한_움직임, TRIPOD.속사 } },
                { NAME.심판의_시간, new List<TRIPOD>{ TRIPOD.징역_선고, TRIPOD.최종_판결, TRIPOD.증거_인멸 } },
                { NAME.샷건_연사, new List<TRIPOD>{ TRIPOD.강화된_사격, TRIPOD.회피의_달인, TRIPOD.빠른_준비 } },
                { NAME.최후의_만찬, new List<TRIPOD>{ TRIPOD.뜨거운_열기, TRIPOD.강한_폭발, TRIPOD.집행 } },
                { NAME.절멸의_탄환, new List<TRIPOD>{ TRIPOD.강인함, TRIPOD.강화_사격, TRIPOD.뇌진탕 } },
                { NAME.마탄의_사수, new List<TRIPOD>{ TRIPOD.특수_탄환, TRIPOD.전방위_사격, TRIPOD.영혼의_일발 } },
                { NAME.스파이럴_플레임, new List<TRIPOD>{ TRIPOD.성장_탄환, TRIPOD.고속_탄환, TRIPOD.마무리_사격 } },
                { NAME.대재앙, new List<TRIPOD>{ TRIPOD.숨통_끊기, TRIPOD.무방비_표적, TRIPOD.뇌진탕 } },
                { NAME.대재앙_샷건, new List<TRIPOD>{ TRIPOD.숨통_끊기, TRIPOD.무방비_표적, TRIPOD.뇌진탕 } },
                { NAME.퍼펙트_샷, new List<TRIPOD>{ TRIPOD.정밀_사격, TRIPOD.완벽한_조준, TRIPOD.마무리_사격 } },
                { NAME.포커스_샷, new List<TRIPOD>{ TRIPOD.강화_탄환, TRIPOD.섬광, TRIPOD.더블탭 } },
                { NAME.타겟_다운, new List<TRIPOD>{ TRIPOD.대용량_탄창, TRIPOD.작렬철강탄, TRIPOD.반자동_라이플 } },
                { NAME.황혼의_눈, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_샷건, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_라이플, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_샷건, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_라이플, new List<TRIPOD> { TRIPOD.선택_불가 } },
            };
            public static Dictionary<NAME, List<TRIPOD>> GetTripod3List() => new Dictionary<NAME, List<TRIPOD>>
            {
                { NAME.AT02_유탄, new List<TRIPOD>{ TRIPOD.불꽃놀이, TRIPOD.내부_발화 } },
                { NAME.메테오_스트림, new List<TRIPOD>{ TRIPOD.폭격_지원, TRIPOD.혜성_낙화 } },
                { NAME.이퀄리브리엄, new List<TRIPOD>{ TRIPOD.화상_효과, TRIPOD.급소_사격 } },
                { NAME.레인_오브_불릿, new List<TRIPOD>{ TRIPOD.화염_사격, TRIPOD.빠른_준비 } },
                { NAME.심판의_시간, new List<TRIPOD>{ TRIPOD.강화_파편, TRIPOD.확산_파편 } },
                { NAME.샷건_연사, new List<TRIPOD>{ TRIPOD.연장_사격, TRIPOD.특수_탄환 } },
                { NAME.최후의_만찬, new List<TRIPOD>{ TRIPOD.더블_샷, TRIPOD.연발_사격 } },
                { NAME.절멸의_탄환, new List<TRIPOD>{ TRIPOD.최후의_일격, TRIPOD.반동_회피 } },
                { NAME.마탄의_사수, new List<TRIPOD>{ TRIPOD.가디언의_숨결, TRIPOD.혹한의_안식처 } },
                { NAME.스파이럴_플레임, new List<TRIPOD>{ TRIPOD.총열_강화, TRIPOD.후폭풍 } },
                { NAME.대재앙, new List<TRIPOD>{ TRIPOD.융단_폭격, TRIPOD.영원한_재앙 } },
                { NAME.대재앙_샷건, new List<TRIPOD>{ TRIPOD.융단_폭격, TRIPOD.영원한_재앙 } },
                { NAME.퍼펙트_샷, new List<TRIPOD>{ TRIPOD.준비된_사수, TRIPOD.강화된_사격 } },
                { NAME.포커스_샷, new List<TRIPOD>{ TRIPOD.빠른_마무리, TRIPOD.최후의_일격 } },
                { NAME.타겟_다운, new List<TRIPOD>{ TRIPOD.정조준, TRIPOD.천국의_계단 } },
                { NAME.황혼의_눈, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_샷건, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.황혼의_눈_라이플, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_샷건, new List<TRIPOD> { TRIPOD.선택_불가 } },
                { NAME.대구경_폭발_탄환_라이플, new List<TRIPOD> { TRIPOD.선택_불가 } },
            };
            public Skill Copy() => new Skill { name = name, lev = lev, tp1 = tp1, tp2 = tp2, tp3 = tp3 };
        }
    }
}
