using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoaCalc
{

    /// <summary>
    /// 건슬링어 직업을 구현한 클래스. 
    /// AbstractLostArkJob클래스를 상속 받아서 구현한다.
    /// 세팅 정보 및 스펙 적용은 상속 받은 클래스에 구현이 되어있으므로 스킬만 구현을 하면 된다.
    /// </summary>     
    public class Gunslinger : AbstractLostArkJob
    {

        //***************************************************************************//
        //                              건슬링어 특화                                //
        //***************************************************************************//
      
        protected override PackedStats Specialization(SettingInfo.CombatStats combatStats)
        {
            PackedStats stats = new PackedStats();

            stats.AddStats(new Stats { criticalDamage = new ValueP(combatStats.specialization * 0.03575m * 3) }, category: SettingInfo.Skill.CATEGORY.핸드건_스탠스);
            stats.AddStats(new Stats { damage = new ValueM(combatStats.specialization * 0.03575m * 0.6m) }, category: SettingInfo.Skill.CATEGORY.샷건_스탠스);
            stats.AddStats(new Stats { damage = new ValueM(combatStats.specialization * 0.03575m) }, category: SettingInfo.Skill.CATEGORY.라이플_스탠스);
            stats.AddStats(new Stats { damage = new ValueM(combatStats.specialization * 0.05465m) }, category: SettingInfo.Skill.CATEGORY.각성_스킬);

            return stats;
        }





        //***************************************************************************//
        //                            건슬링어 스킬 구현                             //
        //***************************************************************************//

        public CombatSkill GetCombatSkill(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)
        {
            if (skillSetting.name == SettingInfo.Skill.NAME.AT02_유탄) return AT02Grenade(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.메테오_스트림) return MeteorStream(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.이퀄리브리엄) return Equilibrium(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.레인_오브_불릿) return BulletRain(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.심판의_시간) return HourOfJudgment(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.샷건_연사) return ShotgunRapidFire(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.최후의_만찬) return LastRequest(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.절멸의_탄환) return DualBuckshot(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.마탄의_사수) return Sharpshooter(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.스파이럴_플레임) return SpiralFlame(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.대재앙 
                || skillSetting.name == SettingInfo.Skill.NAME.대재앙_샷건) return Catastrophe(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.퍼펙트_샷) return PerfectShot(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.포커스_샷) return FocusedShot(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.타겟_다운) return TargetDown(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈 
                || skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_샷건 
                || skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_라이플) return TwilightEye(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환
                || skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_샷건
                || skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_라이플) return HighCaliberHEBullet(skillSetting, hpCondition, characterStats);
            return new CombatSkill();
        }
        
        private CombatSkill AT02Grenade(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // AT02 유탄
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 수류탄
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.AT02_유탄 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.지점 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = new decimal[] { 42.92538222m, 113.6441774m, 146.6874572m, 167.6869248m, 154.4179543m, 147.1053683m }[levIndex],
                ratio = new decimal[] { 3.23817734m, 3.23817734m, 3.23817734m, 3.23817734m, 3.522906404m, 3.698029557m }[levIndex]
            };

            // 부분 스킬: 내부 발화 (참고: 원래 마탄의 '가디언의 숨결'처럼 특화 미적용이였으나 2021-9-29일 패치로 특화가 적용되도록 바뀜)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.AT02_유탄 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = new decimal[] { 42.92538222m, 113.6441774m, 146.6874572m, 167.6869248m, 154.4179543m, 147.1053683m }[levIndex],
                ratio = new decimal[] { 3.23817734m, 3.23817734m, 3.23817734m, 3.23817734m, 3.522906404m, 3.698029557m }[levIndex]
            };

            // 최종 스킬
            var AT02Grenade = new CombatSkill { name = SettingInfo.Skill.NAME.AT02_유탄, cooldownTime = 6, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.원거리_투척:
                        break;
                    case SettingInfo.Skill.TRIPOD.넓은_폭발:
                        break;
                    case SettingInfo.Skill.TRIPOD.마력_조절:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.강화_수류탄:   // 피해 +80%
                        part1.appliedStats.damage.Add(80);
                        part2.appliedStats.damage.Add(80);
                        break;
                    case SettingInfo.Skill.TRIPOD.세열_수류탄:   // 피해 -10%, 쿨타임 +6초
                        part1.appliedStats.damage.Add(-10);
                        part2.appliedStats.damage.Add(-10);
                        AT02Grenade.cooldownTime = 6 + 6;
                        break;
                    case SettingInfo.Skill.TRIPOD.빙결_수류탄:   // 피해 -67%, 쿨타임 +9초
                        part1.appliedStats.damage.Add(-67);
                        part2.appliedStats.damage.Add(-67);
                        AT02Grenade.cooldownTime = 6 + 9;
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.불꽃놀이:   // 피해 +50%
                        part1.appliedStats.damage.Add(50);
                        break;
                    case SettingInfo.Skill.TRIPOD.내부_발화:   // 피해량의 145%에 해당하는 추가피해
                        part2.ratio *= 1.45m;
                        AT02Grenade.partList = new List<CombatSkill.Part> { part1, part2 };
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));

            return AT02Grenade;
        }
        
        private CombatSkill MeteorStream(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 메테오 스트림
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 탄환 낙하
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.메테오_스트림 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.지점 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = new decimal[] { 42.87143386m, 113.2896443m, 146.1013338m, 166.9729529m, 153.5711701m, 146.3730559m }[levIndex],
                ratio = new decimal[] { 10.6364532m, 10.6364532m, 10.6364532m, 10.6364532m, 11.57118227m, 12.14704433m }[levIndex]
            };

            // 최종 스킬
            var meteorStream = new CombatSkill { name = SettingInfo.Skill.NAME.메테오_스트림, cooldownTime = 20, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.마력_조절:
                        break;
                    case SettingInfo.Skill.TRIPOD.근육경련:
                        break;
                    case SettingInfo.Skill.TRIPOD.약점포착:   // 피해 +45%
                        part1.appliedStats.damage.Add(45);
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.넓은_폭발:
                        break;
                    case SettingInfo.Skill.TRIPOD.비열한_공격:
                        break;
                    case SettingInfo.Skill.TRIPOD.빙결_수류탄:   // 방어력 무시 70%
                        part1.appliedStats.damage.Add(70 * 0.6m);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.폭격_지원:   // 피해 +119.6%
                        part1.appliedStats.damage.Add(119.6m);
                        break;
                    case SettingInfo.Skill.TRIPOD.혜성_낙화:   // 피해 +95.2%
                        part1.appliedStats.damage.Add(95.2m);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));

            return meteorStream;
        }
       
        private CombatSkill Equilibrium(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 이퀄리브리엄
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 핸드건 연사
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.이퀄리브리엄 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = new decimal[] { 42.77444601m, 113.0937551m, 145.9971751m, 166.8673443m, 153.4586162m, 146.1846934m }[levIndex],
                ratio = new decimal[] { 10.63719212m, 10.63719212m, 10.63719212m, 10.63719212m, 11.57315271m, 12.14901478m }[levIndex]
            };

            // 부분 스킬: 화상 효과 (정보: 특화 효과를 받는다, 보석 효과를 받지 않는다, 스킬 레벨에 영향을 받지 않고 트포레벨만 영향을 받는다)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = 37.33333333m,
                ratio = 0.642857143m
            };

            // 최종 스킬
            var equilibrium = new CombatSkill { name = SettingInfo.Skill.NAME.이퀄리브리엄, cooldownTime = 16, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.고속_사격:
                        break;
                    case SettingInfo.Skill.TRIPOD.급소_노출:   // 시너지 & 버프 세팅에서 관리함
                        break;
                    case SettingInfo.Skill.TRIPOD.회피의_달인:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.섬멸_사격:   // 피해 +95.2% (화상 효과에는 적용되지 않는다)
                        part1.appliedStats.damage.Add(95.2m);
                        break;
                    case SettingInfo.Skill.TRIPOD.적_소탕:
                        break;
                    case SettingInfo.Skill.TRIPOD.원거리_사격:   // 치명타 피해 +160% (화상 효과에는 적용되지 않는다)
                        part1.appliedStats.criticalDamage.Add(160);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.화상_효과:   // 5초간 화상 효과를 부여한다. 최대 7회까지 중첩이 된다 (2트포 섬멸 사격은 7중첩, 다른 트포는 4중첩이다)
                        if (skillSetting.tp2 == SettingInfo.Skill.TRIPOD.섬멸_사격) part2.ratio *= 7 * 5;
                        else part2.ratio *= 4 * 5;
                        equilibrium.partList = new List<CombatSkill.Part> { part1, part2 };
                        break;
                    case SettingInfo.Skill.TRIPOD.급소_사격:   // 치적 +95%
                        part1.appliedStats.criticalRate.Add(95);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));

            return equilibrium;
        }
       
        private CombatSkill BulletRain(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 레인 오브 불릿
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 }, 
                { SettingInfo.Skill.LEV.__4레벨, 1 }, 
                { SettingInfo.Skill.LEV.__7레벨, 2 }, 
                { SettingInfo.Skill.LEV.__10레벨, 3 }, 
                { SettingInfo.Skill.LEV.__11레벨, 4 }, 
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 전방 난사
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.레인_오브_불릿 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = new decimal[] { 42.79105545m, 112.9771528m, 145.9607833m, 167.2741259m, 153.8214569m, 146.5868532m }[levIndex],
                ratio = new decimal[] { 18.25147783m, 18.25147783m, 18.25147783m, 18.25147783m, 19.85418719m, 20.8408867m }[levIndex]
            };

            // 최종 스킬
            var bulletRain = new CombatSkill { name = SettingInfo.Skill.NAME.레인_오브_불릿, cooldownTime = 22, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.기습:   // 피해 +45%
                        part1.appliedStats.damage.Add(45);
                        break;
                    case SettingInfo.Skill.TRIPOD.광역_사격:
                        break;
                    case SettingInfo.Skill.TRIPOD.사면초가:   // 백어택 -> 타대, 피해 +28%, 치적 +10%
                        part1.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part1.appliedStats.damage.Add(28);
                        part1.appliedStats.criticalRate.Add(10);
                        break;
                }
            }
          
            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.원거리_사격:
                        break;
                    case SettingInfo.Skill.TRIPOD.날렵한_움직임:
                        break;
                    case SettingInfo.Skill.TRIPOD.속사:   // 피해 +95%
                        part1.appliedStats.damage.Add(95);
                        break;
                }
            }
           
            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.화염_사격:   // 피해 +95%
                        part1.appliedStats.damage.Add(95);
                        break;
                    case SettingInfo.Skill.TRIPOD.빠른_준비:
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));

            return bulletRain;
        }
       
        private CombatSkill HourOfJudgment(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 심판의 시간
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 폭발 공격
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.심판의_시간 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 43.00996027m, 114.0764625m, 147.0370797m, 167.8420083m, 154.2778513m, 146.951656m }[levIndex],
                ratio = new decimal[] { 4.278078818m, 4.277832512m, 4.277832512m, 4.277832512m, 4.653940887m, 4.885960591m }[levIndex] * 3
            };

            // 부분 스킬: 파편 공격
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.심판의_시간 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 43.00253281m, 112.1805204m, 145.8346765m, 166.4011052m, 152.980525m, 145.7016129m }[levIndex],
                ratio = new decimal[] { 1.069704433m, 1.069704433m, 1.069704433m, 1.069704433m, 1.163546798m, 1.221674877m }[levIndex] * 3
            };

            // 최종 스킬
            var hourOfJudgment = new CombatSkill { name = SettingInfo.Skill.NAME.심판의_시간, cooldownTime = 30, partList = new List<CombatSkill.Part> { part1, part2 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.빠른_준비:   // 재사용 대기시간 -11초
                        hourOfJudgment.cooldownTime = 30 - 11;
                        break;
                    case SettingInfo.Skill.TRIPOD.마력_조절:
                        break;
                    case SettingInfo.Skill.TRIPOD.회피의_달인:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.징역_선고:   // 탄환이 한 개로 줄지만, 탄환 피해가 460% 증가한다
                        part1.ratio /= 3m;
                        part2.ratio /= 3m;
                        part1.appliedStats.damage.Add(460m);
                        break;
                    case SettingInfo.Skill.TRIPOD.최종_판결:   // 탄환 피해 +65%
                        part1.appliedStats.damage.Add(65);
                        break;
                    case SettingInfo.Skill.TRIPOD.증거_인멸:   // 탄환이 5개로 늘어난다
                        part1.ratio *= (5m / 3m);
                        part2.ratio *= (5m / 3m);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.강화_파편:    // 피해 +80%
                        part1.appliedStats.damage.Add(80);
                        part2.appliedStats.damage.Add(80);
                        break;
                    case SettingInfo.Skill.TRIPOD.확산_파편:
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));

            return hourOfJudgment;
        }
     
        private CombatSkill ShotgunRapidFire(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 샷건 연사
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 1+2+3타
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.샷건_연사 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 43.00022626m, 113.6016118m, 139.3479757m, 167.4934816m, 154.0891578m, 146.7742149m }[levIndex],
                ratio = new decimal[] { 22.86034483m, 22.86059113m, 22.89950739m, 22.86059113m, 24.86871921m, 26.10812808m }[levIndex]
            };    
           
            // 최종 스킬
            var shotgunRapidFire = new CombatSkill { name = SettingInfo.Skill.NAME.샷건_연사, cooldownTime = 36, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.기습:   // 피해 +45%
                        part1.appliedStats.damage.Add(45);
                        break;
                    case SettingInfo.Skill.TRIPOD.사면초가:    // 백어택 -> 타대, 피해 +28%, 치적 +10%
                        part1.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part1.appliedStats.damage.Add(28);
                        part1.appliedStats.criticalRate.Add(10);
                        break;
                    case SettingInfo.Skill.TRIPOD.콤보_연사:   // 스킬 타입이 콤보로 변경된다
                        part1.type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.콤보 };
                        break;
                }
            }
           
            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.강화된_사격:   // 피해 +50%
                        part1.appliedStats.damage.Add(50);
                        break;
                    case SettingInfo.Skill.TRIPOD.회피의_달인:
                        break;
                    case SettingInfo.Skill.TRIPOD.빠른_준비:   // 재사용 대기시간 -15초
                        shotgunRapidFire.cooldownTime = 36 - 15;
                        break;
                }
            }
           
            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.연장_사격:    // 마지막에 콤보 공격이 추가 되어 94.8%의 추가 피해를 준다
                        part1.appliedStats.damage.Add(94.8m);
                        break;
                    case SettingInfo.Skill.TRIPOD.특수_탄환:    // 피해 +120%
                        part1.appliedStats.damage.Add(120);
                        break;
                }
            }
           
            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));

            return shotgunRapidFire;
        }
     
        private CombatSkill LastRequest(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 최후의 만찬
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 폭발 탄환
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.최후의_만찬 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 42.99251556m, 113.5521002m, 146.4512628m, 167.3048768m, 153.8874387m, 146.6361637m }[levIndex],
                ratio = new decimal[] { 19.51502463m, 19.51527094m, 19.51502463m, 19.51527094m, 21.22980296m, 22.2864532m }[levIndex]
            };

            // 부분 스킬: 화염탄 (정보: 특화 적용, 보적 미적용, 트포-강한폭발 미적용, 스킬 레벨과 상관없이 트포렙에만 영향을 받는다)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = 143.5680918m,
                ratio = 4.165270936m * 3
            };

            // 부분 스킬: 뜨거운열기 (정보: 특화 적용, 보적 적용, 스킬 레벨과 트포렙 두 개의 영향을 받는다)
            var part3 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.최후의_만찬 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 42.70537499m, 112.74219m, 146.0523825m, 167.8321237m, 154.6741443m, 147.3915047m }[levIndex],
                ratio = new decimal[] { 2.341625616m, 2.341625616m, 2.341625616m, 2.341625616m, 2.54729064m, 2.673152709m }[levIndex] * 5
            };

            // 최종 스킬
            var lastRequest = new CombatSkill { name = SettingInfo.Skill.NAME.최후의_만찬, cooldownTime = 36, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.빠른_준비:   // 재사용 대기시간 -13초
                        lastRequest.cooldownTime = 36 - 13;
                        break;
                    case SettingInfo.Skill.TRIPOD.화염탄:   // 3초간 화상
                        lastRequest.partList = new List<CombatSkill.Part> { part1, part2 };
                        break;
                    case SettingInfo.Skill.TRIPOD.냉기탄:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.뜨거운_열기:   // 5초간 장판딜
                        if (skillSetting.tp1 == SettingInfo.Skill.TRIPOD.화염탄)
                            lastRequest.partList = new List<CombatSkill.Part> { part1, part2, part3 };                       
                        else
                            lastRequest.partList = new List<CombatSkill.Part> { part1, part3 };                        
                        break;
                    case SettingInfo.Skill.TRIPOD.강한_폭발:   // 피해 +45%
                        part1.appliedStats.damage.Add(45);
                        break;
                    case SettingInfo.Skill.TRIPOD.집행:
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.더블_샷:    // 기본 피해량의 97.6%인 샷건을 두 방을 쏜다. 화염탄 화상은 2중첩, 뜨거운 열기는 두 개가 생긴다
                        part1.appliedStats.damage.Add(95.2m);
                        if (skillSetting.tp1 == SettingInfo.Skill.TRIPOD.화염탄) part2.ratio *= 2;
                        if (skillSetting.tp2 == SettingInfo.Skill.TRIPOD.뜨거운_열기) part3.ratio *= 2;
                        break;
                    case SettingInfo.Skill.TRIPOD.연발_사격:    // 피해 +119.6%
                        part1.appliedStats.damage.Add(119.6m);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));
            part3.appliedStats.Add(characterStats.GetSkillStats(part3));

            return lastRequest;
        }
      
        private CombatSkill DualBuckshot(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 절멸의 탄환
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 1,2타
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.절멸의_탄환 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.콤보 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 43.0192881m, 113.7459143m, 146.7030638m, 167.7023095m, 154.2875066m, 146.9793043m }[levIndex],
                ratio = new decimal[] { 6.857389163m, 6.857389163m, 6.857389163m, 6.857389163m, 7.460098522m, 7.831034483m }[levIndex]
            };

            // 부분 스킬: 3타
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.절멸의_탄환 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.콤보 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 42.94945939m, 113.6082474m, 146.4948454m, 167.568519m, 154.1042447m, 146.8608455m }[levIndex],
                ratio = new decimal[] { 13.7137931m, 13.7137931m, 13.7137931m, 13.7137931m, 14.91847291m, 15.66108374m }[levIndex]
            };

            // 최종 스킬
            var dualBuckshot = new CombatSkill { name = SettingInfo.Skill.NAME.절멸의_탄환, cooldownTime = 30, partList = new List<CombatSkill.Part> { part1, part1, part2 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.사면초가:   // 백어택 -> 타대, 피해 +28%, 치적 +10%
                        part1.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part1.appliedStats.damage.Add(28);
                        part1.appliedStats.criticalRate.Add(10);

                        part2.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part2.appliedStats.damage.Add(28);
                        part2.appliedStats.criticalRate.Add(10);
                        break;
                    case SettingInfo.Skill.TRIPOD.재빠른_손놀림:
                        break;
                    case SettingInfo.Skill.TRIPOD.다가오는_죽음:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.강인함:
                        break;
                    case SettingInfo.Skill.TRIPOD.강화_사격:   // 피해 +60%
                        part1.appliedStats.damage.Add(60);
                        part2.appliedStats.damage.Add(60);
                        break;
                    case SettingInfo.Skill.TRIPOD.뇌진탕:
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.최후의_일격:   // 마지막 공격의 피해 +290.5%
                        part2.appliedStats.damage.Add(290.5m);
                        break;
                    case SettingInfo.Skill.TRIPOD.반동_회피:   // 피해 +190%
                        part1.appliedStats.damage.Add(190);
                        part2.appliedStats.damage.Add(190);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));

            return dualBuckshot;
        }
     
        private CombatSkill Sharpshooter(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 마탄의 사수
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 7회 사격 (참고 사항: 7번 째 타격이 전체딜의 약 28%를 차지)
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.마탄의_사수 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 43.03187251m, 113.9521912m, 147m, 168.2788845m, 154.7694581m, 147.480511m }[levIndex],
                ratio = new decimal[] { 35.85714286m, 35.85714286m, 35.85714286m, 35.85714286m, 39.00640394m, 40.94778325m }[levIndex]
            };

            // 부분 스킬: 가디언의 숨결 (특화의 샷건 방무 미적용)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.마탄의_사수 },
                category = new List<SettingInfo.Skill.CATEGORY> { },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 43.03187251m, 113.9521912m, 147m, 168.2788845m, 154.7694581m, 147.480511m }[levIndex] * 1.02m,
                ratio = new decimal[] { 35.85714286m, 35.85714286m, 35.85714286m, 35.85714286m, 39.00640394m, 40.94778325m }[levIndex] * 1.02m
            };

            // 부분 스킬: 가디언의 숨결 화상 (특화 적용, 보석 미적용, 트포-특탄 및 사면초가의 영향을 받지 않는다, 트포렙에 영향을 받는다)
            var part3 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = 145.6502242m,
                ratio = 0.164778325m * 7 * 5
            };

            // 최종 스킬
            var sharpshooter = new CombatSkill { name = SettingInfo.Skill.NAME.마탄의_사수, cooldownTime = 30, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.무한의_마탄:
                        break;
                    case SettingInfo.Skill.TRIPOD.원거리_사격:
                        break;
                    case SettingInfo.Skill.TRIPOD.사면초가:   // 백어택 -> 타대, 피해 +28%, 치적 +10%
                        part1.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part1.appliedStats.damage.Add(28);
                        part1.appliedStats.criticalRate.Add(10);

                        part2.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part2.appliedStats.damage.Add(28);
                        part2.appliedStats.criticalRate.Add(10);
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.특수_탄환:   // 피해 +80%
                        part1.appliedStats.damage.Add(80);
                        part2.appliedStats.damage.Add(80);
                        break;
                    case SettingInfo.Skill.TRIPOD.전방위_사격:
                        break;
                    case SettingInfo.Skill.TRIPOD.영혼의_일발:   // 피해 +72.8% (7번 째 타격의 피해가 260% 증가하는 데, 7번 째 타격의 딜 비중은 28%이다)
                        part1.appliedStats.damage.Add(72.8m);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.가디언의_숨결:   // 화상 상태를 부여하며, 화상 7회 중첩 시 '기본 피해량'의 102%에 해당하는 추가 피해를 준다
                        sharpshooter.partList = new List<CombatSkill.Part> { part1, part2, part3 };
                        break;
                    case SettingInfo.Skill.TRIPOD.혹한의_안식처:
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));
            part3.appliedStats.Add(characterStats.GetSkillStats(part3));

            return sharpshooter;
        }
      
        private CombatSkill SpiralFlame(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 스파이럴 플레임
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 불꽃 탄환
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.스파이럴_플레임 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.91676763m, 113.5634238m, 146.4580809m, 167.424582m, 153.9625794m, 146.7152026m }[levIndex],
                ratio = new decimal[] { 16.26403941m, 16.26403941m, 16.26403941m, 16.26403941m, 17.69261084m, 18.57339901m }[levIndex]
            };

            // 부분 스킬: 불길 (후폭풍 트포 선택 시 후폭풍으로 변경)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.스파이럴_플레임 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.78343023m, 112.8597384m, 146.0537791m, 167.4454942m, 153.9368632m, 146.6847048m }[levIndex],
                ratio = new decimal[] { 1.355665025m, 1.355665025m, 1.355665025m, 1.355665025m, 1.474630542m, 1.547536946m }[levIndex] * 3
            };

            // 부분 스킬: 맹렬한 불꽃 (참고: 마탄의 '가디언의 숨결'처럼 특화 미적용)
            var part3 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.스파이럴_플레임 },
                category = new List<SettingInfo.Skill.CATEGORY> { },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.89009498m, 113.4226594m, 146.3772049m, 167.4287653m, 153.9574354m, 146.7091038m }[levIndex],
                ratio = new decimal[] { 20.33103448m, 20.33103448m, 20.33103448m, 20.33103448m, 22.11650246m, 23.21600985m }[levIndex] * 0.45m   // 기본피해량(탄두+불길) * 0.45
            };

            // 최종 스킬
            var spiralFlame = new CombatSkill { name = SettingInfo.Skill.NAME.스파이럴_플레임, cooldownTime = 27, partList = new List<CombatSkill.Part> { part1, part2 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.냉기탄:   // 불길 효과가 사라진다
                        spiralFlame.partList = new List<CombatSkill.Part> { part1 };
                        break;
                    case SettingInfo.Skill.TRIPOD.맹렬한_불꽃:   // '기본 피해량'의 45%에 해당하는 추가 피해를 준다 (이때 기본 피해량은 탄두+불길이다)
                        spiralFlame.partList = new List<CombatSkill.Part> { part1, part2, part3 };
                        break;
                    case SettingInfo.Skill.TRIPOD.재빠른_조준:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.성장_탄환:   // 피해 +80% (불길, 맹렬한 불꽃에는 적용되지 않는다)
                        part1.appliedStats.damage.Add(80);
                        break;
                    case SettingInfo.Skill.TRIPOD.고속_탄환:
                        break;
                    case SettingInfo.Skill.TRIPOD.마무리_사격:   // 약무 탄두 치적 +100%
                        if (hpCondition <= 50)
                        {
                            part1.appliedStats.criticalRate.Add(100);
                        }
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.총열_강화:   // 불길 피해 +350%
                        part2.appliedStats.damage.Add(350);
                        break;
                    case SettingInfo.Skill.TRIPOD.후폭풍:   // 탄두 피해 +64%
                        part1.appliedStats.damage.Add(64);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));
            part3.appliedStats.Add(characterStats.GetSkillStats(part3));

            return spiralFlame;
        }
       
        private CombatSkill Catastrophe(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 대재앙
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.대재앙 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.99325907m, 113.6458383m, 146.5943969m, 167.6258098m, 154.1585823m, 146.8923798m }[levIndex],
                ratio = new decimal[] { 20.60788177m, 20.60788177m, 20.60788177m, 20.60541872m, 22.41847291m, 23.53423645m }[levIndex]
            };

            // 부분 스킬: 영원한 재앙 (샷건 활용 조건이 붙었다면 틱뎀은 샷건 스탠스에서 들어간다)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.대재앙 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING>
                {
                    skillSetting.name==SettingInfo.Skill.NAME.대재앙_샷건 ? SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스 : SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스,
                    SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스
                },

                constant = new decimal[] { 42.99325907m, 113.6458383m, 146.5943969m, 167.6258098m, 154.1585823m, 146.8923798m }[levIndex],
                ratio = new decimal[] { 20.60788177m, 20.60788177m, 20.60788177m, 20.60541872m, 22.41847291m, 23.53423645m }[levIndex] * 1.704m
            };

            // 최종 스킬
            var catastrophe = new CombatSkill { name = SettingInfo.Skill.NAME.대재앙, cooldownTime = 24, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.강인함:
                        break;
                    case SettingInfo.Skill.TRIPOD.원거리_조준:
                        break;
                    case SettingInfo.Skill.TRIPOD.재빠른_조준:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.숨통_끊기:   // 피해 +40%, 약무 피해 20%
                        part1.appliedStats.damage.Add(40);
                        part2.appliedStats.damage.Add(40);
                        if (hpCondition <= 50)
                        {
                            part1.appliedStats.damage.Add(20);
                            part2.appliedStats.damage.Add(20);
                        }
                        break;
                    case SettingInfo.Skill.TRIPOD.무방비_표적:
                        break;
                    case SettingInfo.Skill.TRIPOD.뇌진탕:
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.융단_폭격:   // 두 번 공격 추가
                        catastrophe.partList = new List<CombatSkill.Part> { part1, part1 };
                        break;
                    case SettingInfo.Skill.TRIPOD.영원한_재앙:   // 장판 데미지 적용
                        catastrophe.partList = new List<CombatSkill.Part> { part1, part2 };
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));

            return catastrophe;
        }
      
        private CombatSkill PerfectShot(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 퍼펙트 샷
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 대구경 탄환
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.퍼펙트_샷 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.96599407m, 113.6593763m, 146.5044918m, 167.4718969m, 154.0163431m, 146.7808338m }[levIndex],
                ratio = new decimal[] { 26.18349754m, 26.18349754m, 26.18349754m, 26.18349754m, 28.48399015m, 29.90172414m }[levIndex]
            };

            // 부분 스킬: 출혈 효과 (특화 적용, 보석 미적용, 트포-정밀 사격/마무리사격/강화된 사격 미적용, 스킬 레벨 및 트포렙에 영향을 받는다)
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 43.63392107m, 114.5294266m, 148.6280437m, 169.4081102m, 155.7320367m, 148.6239341m }[levIndex],
                ratio = new decimal[] { 2.933497537m, 2.933743842m, 2.933497537m, 2.933743842m, 3.19137931m, 3.350738916m }[levIndex] * 5
            };

            // 최종 스킬
            var perfectShot = new CombatSkill { name = SettingInfo.Skill.NAME.퍼펙트_샷, cooldownTime = 30, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.출혈_효과:   // 5초간 출혈 상태를 부여한다
                        perfectShot.partList = new List<CombatSkill.Part> { part1, part2 };
                        break;
                    case SettingInfo.Skill.TRIPOD.안정된_자세:
                        break;
                    case SettingInfo.Skill.TRIPOD.근육_경련:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.정밀_사격:   // 치적 +80%
                        part1.appliedStats.criticalRate.Add(80);
                        break;
                    case SettingInfo.Skill.TRIPOD.완벽한_조준:   // 캐스팅 스킬로 변경
                        part1.type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.캐스팅 };
                        break;
                    case SettingInfo.Skill.TRIPOD.마무리_사격:   // 약무 피해 +96%
                        if (hpCondition <= 50) part1.appliedStats.damage.Add(96);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.준비된_사수:
                        break;
                    case SettingInfo.Skill.TRIPOD.강화된_사격:   // 피해 +80%
                        part1.appliedStats.damage.Add(80);
                        break;
                }
            }
            
            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));

            return perfectShot;
        }
      
        private CombatSkill FocusedShot(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 포커스 샷
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 1, 2타
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.포커스_샷 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.81998771m, 113.3551778m, 146.1976927m, 167.1226705m, 153.7579618m, 146.4604441m }[levIndex],
                ratio = new decimal[] { 7.216256158m, 7.216256158m, 7.216256158m, 7.216256158m, 7.85m, 8.241133005m }[levIndex]
            };

            // 부분 스킬: 더블탭 특수탄
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.포커스_샷 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.81998771m, 113.3551778m, 146.1976927m, 167.1226705m, 153.7579618m, 146.4604441m }[levIndex],
                ratio = new decimal[] { 7.216256158m, 7.216256158m, 7.216256158m, 7.216256158m, 7.85m, 8.241133005m }[levIndex] * 0.1m
            };

            // 부분 스킬: 마무리 사격
            var part3 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.포커스_샷 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.95856372m, 113.6323299m, 146.4748447m, 167.5383985m, 154.074016m, 146.8266659m }[levIndex],
                ratio = new decimal[] { 14.43251232m, 14.43251232m, 14.43251232m, 14.43251232m, 15.70024631m, 16.4820197m }[levIndex]
            };

            // 최종 스킬
            var focusedShot = new CombatSkill { name = SettingInfo.Skill.NAME.포커스_샷, cooldownTime = 27, partList = new List<CombatSkill.Part> { part1, part1, part3 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.방향_전환:
                        break;
                    case SettingInfo.Skill.TRIPOD.재빠른_조준:
                        break;
                    case SettingInfo.Skill.TRIPOD.근육_경련:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.강화_탄환:   // 약무 피해 +95%
                        if (hpCondition <= 50)
                        {
                            part1.appliedStats.damage.Add(95);
                            part3.appliedStats.damage.Add(95);
                        }
                        break;
                    case SettingInfo.Skill.TRIPOD.섬광:               
                        break;
                    case SettingInfo.Skill.TRIPOD.더블탭:   // 기본 피해량의 10%에 해당하는 추가탄, 마무리 사격의 피해 +110%
                        focusedShot.partList = new List<CombatSkill.Part> { part1, part1, part2, part3 };
                        part3.appliedStats.damage.Add(110);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.빠른_마무리:   // 즉시 마무리 사격을 한다,  마무리 사격의 피해 +200%
                        if (skillSetting.tp2 == SettingInfo.Skill.TRIPOD.더블탭) focusedShot.partList = new List<CombatSkill.Part> { part2, part3 };
                        else focusedShot.partList = new List<CombatSkill.Part> { part3 };
                        part3.appliedStats.damage.Add(200);
                        break;
                    case SettingInfo.Skill.TRIPOD.최후의_일격:   // 마무리 사격의 피해 +280%
                        part3.appliedStats.damage.Add(280);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));
            part3.appliedStats.Add(characterStats.GetSkillStats(part3));

            return focusedShot;
        }
      
        private CombatSkill TargetDown(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 타겟 다운
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬: 1타
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.타겟_다운 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.지점 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.94680004m, 113.4740755m, 146.3572993m, 167.2911549m, 153.8635855m, 146.646514m }[levIndex],
                ratio = new decimal[] { 11.13004926m, 11.13029557m, 11.13029557m, 11.13029557m, 12.10812808m, 12.71083744m }[levIndex]
            };

            // 부분 스킬: 2타
            var part2 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.타겟_다운 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.지점 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.94680004m, 113.4740755m, 146.3572993m, 167.2911549m, 153.8635855m, 146.646514m }[levIndex],
                ratio = new decimal[] { 11.13004926m, 11.13029557m, 11.13029557m, 11.13029557m, 12.10812808m, 12.71083744m }[levIndex]
            };

            // 부분 스킬: 3타
            var part3 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.타겟_다운 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.지점 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.94680004m, 113.4740755m, 146.3572993m, 167.2911549m, 153.8635855m, 146.646514m }[levIndex],
                ratio = new decimal[] { 11.13004926m, 11.13029557m, 11.13029557m, 11.13029557m, 12.10812808m, 12.71083744m }[levIndex]
            };

            // 부분 스킬: 4타
            var part4 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.타겟_다운 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.지점 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.94680004m, 113.4740755m, 146.3572993m, 167.2911549m, 153.8635855m, 146.646514m }[levIndex],
                ratio = new decimal[] { 11.13004926m, 11.13029557m, 11.13029557m, 11.13029557m, 12.10812808m, 12.71083744m }[levIndex]
            };

            // 부분 스킬: 작렬철강탄 (특화 미적용, 보석 적용, 트포-정조준 미적용 but 천계 적용, 트포 레벨에 영향을 받음)
            var part5 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.타겟_다운 },
                category = new List<SettingInfo.Skill.CATEGORY> { },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.94680004m, 113.4740755m, 146.3572993m, 167.2911549m, 153.8635855m, 146.646514m }[levIndex],
                ratio = new decimal[] { 11.13004926m, 11.13029557m, 11.13029557m, 11.13029557m, 12.10812808m, 12.71083744m }[levIndex] * 0.45m
            };

            // 부분 스킬: 작렬철강탄 출혈 (특화 적용, 보석 미적용, 트포-정조준/천계 미적용, 스킬 레벨과 트포 레벨 모두에 영향을 받음)
            var part6 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 83.3431085m, 214.3108504m, 285.7478006m, 321.4662757m, 297.0731707m, 281.7994859m }[levIndex],
                ratio = new decimal[] { 0.083990148m, 0.083990148m, 0.083990148m, 0.083990148m, 0.0908867m, 0.095812808m }[levIndex] * 3 * 4
            };

            // 최종 스킬
            var targetDown = new CombatSkill { name = SettingInfo.Skill.NAME.타겟_다운, cooldownTime = 36, partList = new List<CombatSkill.Part> { part1, part2, part3 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.재빠른_조준:
                        break;
                    case SettingInfo.Skill.TRIPOD.숨_참기:
                        break;
                    case SettingInfo.Skill.TRIPOD.대구경_탄환:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.대용량_탄창:   // 사격 횟수가 4회로 증가하고, 마지막 사격의 피해 +150%
                        targetDown.partList = new List<CombatSkill.Part> { part1, part2, part3, part4 };
                        part4.appliedStats.damage.Add(150);
                        break;
                    case SettingInfo.Skill.TRIPOD.작렬철강탄:   // 출혈 상태를 부여하며, 출혈 3회 중첩 시 '기본 피해량'의 45%에 해당하는 추가 피해를 준다
                        targetDown.partList = new List<CombatSkill.Part> { part1, part2, part3, part5, part6 };
                        break;
                    case SettingInfo.Skill.TRIPOD.반자동_라이플:   // 피해 +50%
                        part1.appliedStats.damage.Add(50);
                        part2.appliedStats.damage.Add(50);
                        part3.appliedStats.damage.Add(50);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.정조준:   // 피해 +120%
                        part1.appliedStats.damage.Add(120);
                        part2.appliedStats.damage.Add(120);
                        part3.appliedStats.damage.Add(120);
                        part4.appliedStats.damage.Add(120);
                        break;
                    case SettingInfo.Skill.TRIPOD.천국의_계단:   // 치적 +35%, 약무 치피 +0% / +197% / +394% / +591%
                        part1.appliedStats.criticalRate.Add(35);
                        part2.appliedStats.criticalRate.Add(35);
                        part3.appliedStats.criticalRate.Add(35);
                        part4.appliedStats.criticalRate.Add(35);
                        part5.appliedStats.criticalRate.Add(35);
                        if (hpCondition <= 50)
                        {
                            part2.appliedStats.criticalDamage.Add(197);
                            part3.appliedStats.criticalDamage.Add(394);
                            part4.appliedStats.criticalDamage.Add(591);
                        }
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));
            part2.appliedStats.Add(characterStats.GetSkillStats(part2));
            part3.appliedStats.Add(characterStats.GetSkillStats(part3));
            part4.appliedStats.Add(characterStats.GetSkillStats(part4));
            part5.appliedStats.Add(characterStats.GetSkillStats(part5));
            part6.appliedStats.Add(characterStats.GetSkillStats(part6));


            return targetDown;
        }
      
        private CombatSkill TwilightEye(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 황혼의 눈
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.황혼의_눈 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.각성_스킬 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { },

                constant = new decimal[] { 146.7780056m }[levIndex],
                ratio = new decimal[] { 268.0169951m }[levIndex]
            };

            // 사용자 정의 조건
            if (skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_샷건) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스 };
            else if (skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_라이플) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스 };

            // 최종 스킬
            var twilightEye = new CombatSkill { name = SettingInfo.Skill.NAME.황혼의_눈, cooldownTime = 300, partList = new List<CombatSkill.Part> { part1 } };

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));

            return twilightEye;
        }
   
        private CombatSkill HighCaliberHEBullet(SettingInfo.Skill skillSetting, int hpCondition, PackedStats characterStats)   // 대구경_폭발_탄환
        {
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.대구경_폭발_탄환 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.각성_스킬 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { },

                constant = new decimal[] { 146.7630437m }[levIndex],
                ratio = new decimal[] { 223.6189655m }[levIndex]
            };

            // 사용자 정의 조건
            if (skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_샷건) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스 };
            else if (skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_라이플) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스 };

            // 최종 스킬
            var highCaliberHEBullet = new CombatSkill { name = SettingInfo.Skill.NAME.대구경_폭발_탄환, cooldownTime = 300, partList = new List<CombatSkill.Part> { part1 } };

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStats(part1));

            return highCaliberHEBullet;
        }





        //***************************************************************************//
        //                             스킬 데미지 계산                              //
        //***************************************************************************//

        // 전투 특성 계산: 전체 업뎃 -> 특성만 따로 반복 업뎃으로 구함

        public class ResultCalculateCombatSkill
        {
            public (string beforeHalf, string afterHalf) detailedInfo = ("", "");
            public (decimal beforeHalf, decimal afterHalf, decimal arithmeticMean, decimal harmonicMean) damage = (0, 0, 0, 0);
            public (decimal beforeHalf, decimal afterHalf, decimal arithmeticMean, decimal harmonicMean) dps = (0, 0, 0, 0);
            public decimal cooldown = 0;
        }

        public List<ResultCalculateCombatSkill> CalculateCombatSkill()
        {
            // 캐릭터 스탯 불러오기
            var characterStats = new Dictionary<string, PackedStats> { { "Hp100", GetStats(100) }, { "Hp50", GetStats(50) } };

            // 캐릭터 스탯을 기반으로 스킬 스탯 및 데미지 계산
            var resultList = new List<ResultCalculateCombatSkill>();

            foreach (var skillSetting in setting.GetSkillList())
            {
                var result = new ResultCalculateCombatSkill();

                if (skillSetting != null)
                {
                    CombatSkill combatSkill;

                    // 약무 전
                    combatSkill = GetCombatSkill(skillSetting, 100, characterStats["Hp100"]);
                    result.detailedInfo.beforeHalf = combatSkill.GetDetailedInfo();
                    result.damage.beforeHalf = Calculator.CalculateDamage(combatSkill);
                    result.cooldown = Calculator.CalculateCooldown(combatSkill);

                    // 약무 후
                    combatSkill = GetCombatSkill(skillSetting, 50, characterStats["Hp50"]);
                    result.detailedInfo.afterHalf = combatSkill.GetDetailedInfo();
                    result.damage.afterHalf = Calculator.CalculateDamage(combatSkill);

                    // 평균 데미지 및 DPS
                    result.damage.arithmeticMean = Calculator.ArithmeticMean(new List<decimal> { result.damage.beforeHalf, result.damage.afterHalf });
                    result.damage.harmonicMean = Calculator.HarmonicMean(new List<decimal> { result.damage.beforeHalf, result.damage.afterHalf });
                    result.dps.arithmeticMean = result.damage.arithmeticMean / result.cooldown;
                    result.dps.harmonicMean = result.damage.harmonicMean / result.cooldown;
                }

                resultList.Add(result);
            }

            return resultList;
        }
    }



}