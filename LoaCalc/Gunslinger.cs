using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoaCalc
{

    /// <summary>
    /// 스탯 클래스
    /// </summary>
    public class CharacterStats
    {
        private Stats notCategorizedStats = new Stats();
        private Dictionary<SettingInfo.Skill.NAME, Stats> categorizedStatsByName = new Dictionary<SettingInfo.Skill.NAME, Stats>();
        private Dictionary<SettingInfo.Skill.CATEGORY, Stats> categorizedStatsByCategory = new Dictionary<SettingInfo.Skill.CATEGORY, Stats>();
        private Dictionary<SettingInfo.Skill.TYPE, Stats> categorizedStatsByType = new Dictionary<SettingInfo.Skill.TYPE, Stats>();
        private Dictionary<SettingInfo.Skill.ATTACKTYPE, Stats> categorizedStatsByAttackType = new Dictionary<SettingInfo.Skill.ATTACKTYPE, Stats>();
        private Dictionary<SettingInfo.Skill.CLASSENGRAVING, Stats> categorizedStatsByClassEngraving = new Dictionary<SettingInfo.Skill.CLASSENGRAVING, Stats>();


        public void AddStat(Stats stats)
        {
            notCategorizedStats.Add(stats.Copy());
        }
        public void AddStat(Stats stats, SettingInfo.Skill.NAME name)
        {
            if (categorizedStatsByName.ContainsKey(name)) categorizedStatsByName[name].Add(stats.Copy());
            else categorizedStatsByName.Add(name, stats.Copy());
        }
        public void AddStat(Stats stats, SettingInfo.Skill.CATEGORY name)
        {
            if (categorizedStatsByCategory.ContainsKey(name)) categorizedStatsByCategory[name].Add(stats.Copy());
            else categorizedStatsByCategory.Add(name, stats.Copy());
        }
        public void AddStat(Stats stats, SettingInfo.Skill.TYPE name)
        {
            if (categorizedStatsByType.ContainsKey(name)) categorizedStatsByType[name].Add(stats.Copy());
            else categorizedStatsByType.Add(name, stats.Copy());
        }
        public void AddStat(Stats stats, SettingInfo.Skill.ATTACKTYPE name)
        {
            if (categorizedStatsByAttackType.ContainsKey(name)) categorizedStatsByAttackType[name].Add(stats.Copy());
            else categorizedStatsByAttackType.Add(name, stats.Copy());
        }
        public void AddStat(Stats stats, SettingInfo.Skill.CLASSENGRAVING name)
        {
            if (categorizedStatsByClassEngraving.ContainsKey(name)) categorizedStatsByClassEngraving[name].Add(stats.Copy());
            else categorizedStatsByClassEngraving.Add(name, stats.Copy());
        }
        public Stats GetSkillStat(CombatSkill.Part part)
        {
            Stats stats = new Stats();

            stats.Add(notCategorizedStats.Copy());

            foreach (var name in part.name)
            {
                if (categorizedStatsByName.ContainsKey(name)) 
                    stats.Add(categorizedStatsByName[name]);
            }
           
            foreach (var category in part.category)
            {
                if (categorizedStatsByCategory.ContainsKey(category))
                    stats.Add(categorizedStatsByCategory[category]);
            }

            foreach (var type in part.type)
            {
                if (categorizedStatsByType.ContainsKey(type))
                    stats.Add(categorizedStatsByType[type]);
            }

            foreach (var attackType in part.attackType)
            {
                if (categorizedStatsByAttackType.ContainsKey(attackType))
                    stats.Add(categorizedStatsByAttackType[attackType]);
            }

            foreach (var classEngraving in part.classEngraving)
            {
                if (categorizedStatsByClassEngraving.ContainsKey(classEngraving))
                    stats.Add(categorizedStatsByClassEngraving[classEngraving]);
            }

            return stats;
        }
        public new string ToString()
        {
            string s = "";

       /*     s += "[분류 기준: 스킬명]" + Environment.NewLine;

            foreach (var criteria in categorizedStats)
            {
                s += "[분류 기준: " + criteria.Key.ToString() + "]" + Environment.NewLine;

                foreach (var criteriaName in criteria.Value)
                {
                    s += "* 분류명: " + criteriaName.Key.ToString() + Environment.NewLine + criteriaName.Value.ToString() + Environment.NewLine;
                }

                s += Environment.NewLine;
            }*/

            return s;
        }
    }



    /// <summary>
    /// 전투 스킬 클래스
    /// </summary>
    public class CombatSkill
    {
        public class Part
        {
            // 스킬 옵션
            public List<SettingInfo.Skill.NAME> name = new List<SettingInfo.Skill.NAME>();                                 // 스킬 이름
            public List<SettingInfo.Skill.CATEGORY> category = new List<SettingInfo.Skill.CATEGORY>();                     // 스킬 분류: 일반 스킬, 각성 스킬 / 핸드건 스탠스, 샷건 스탠스, 라이플 스탠스 / 난무 스킬, 집중 스킬 / 루인 스킬, 스택트 스킬
            public List<SettingInfo.Skill.TYPE> type = new List<SettingInfo.Skill.TYPE>();                                 // 스킬 타입: 일반, 지점, 콤보 / 홀딩, 캐스팅 / 차지
            public List<SettingInfo.Skill.ATTACKTYPE> attackType = new List<SettingInfo.Skill.ATTACKTYPE>();               // 공격 타입: 타대, 백 어택, 헤드 어택
            public List<SettingInfo.Skill.CLASSENGRAVING> classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING>();   // 직업 각인: 피메 핸드건 스탠스 / 피메 샷건 스탠스 / 피메 라이플 스탠스 / 사시 핸드건 스탠스 / 사시 샷건 스탠스 / 사시 라이플 스탠스

            // 스킬 계수
            public decimal constant = 0;
            public decimal ratio = 0;

            // 적용된 스탯
            public Stats appliedStats = new Stats();

            public Part Copy() => new Part
            {
                name = name.ToList(),
                category = category.ToList(),
                type = type.ToList(),
                attackType = attackType.ToList(),
                classEngraving = classEngraving.ToList(),
                constant = constant,
                ratio = ratio,
                appliedStats = appliedStats.Copy()
            };
        }


        public SettingInfo.Skill.NAME name = SettingInfo.Skill.NAME.레인_오브_불릿;
        public decimal cooldownTime = 0;
        public List<Part> partList = new List<Part>();


        public string GetDetailedInfo()
        {
            string s = "[\"" + name + "\"]" + Environment.NewLine + Environment.NewLine;
            s += "쿨타임: " + cooldownTime + Environment.NewLine + Environment.NewLine;

            for (int i = 0; i < partList.Count; i++)
            {
                s += ">> Part" + (i + 1).ToString() + Environment.NewLine;
                s += "- 스킬 이름: " + "{" + partList[i].name.ConvertAll(name => name.ToStr()).Aggregate((x, y) => x + ", " + y) + "}" + Environment.NewLine;
                s += "- 스킬 분류: " + "{" + partList[i].category.ConvertAll(category => category.ToStr()).Aggregate((x, y) => x + ", " + y) + "}" + Environment.NewLine;
                s += "- 스킬 타입: " + "{" + partList[i].type.ConvertAll(type => type.ToStr()).Aggregate((x, y) => x + ", " + y) + "}" + Environment.NewLine;
                s += "- 공격 타입: " + "{" + partList[i].attackType.ConvertAll(attackType => attackType.ToStr()).Aggregate((x, y) => x + ", " + y) + "}" + Environment.NewLine;
                s += "- 직업 각인: " + "{" + partList[i].classEngraving.ConvertAll(classEngraving => classEngraving.ToStr()).Aggregate((x, y) => x + ", " + y) + "}" + Environment.NewLine;
                s += "- 상수: " + partList[i].constant + Environment.NewLine;
                s += "- 계수: " + partList[i].ratio + Environment.NewLine + Environment.NewLine;
                s += partList[i].appliedStats.ToString() + Environment.NewLine + Environment.NewLine;
            }
            s += Environment.NewLine;

            return s;
        }
    }



    /// <summary>
    /// 건슬링어 클래스
    /// </summary>     
    public class Gunslinger
    {
        public class Setting
        {
            private SettingInfo.CombatStats combatStats = new SettingInfo.CombatStats();
            private List<SettingInfo.Engraving> engravingList = new List<SettingInfo.Engraving>();
            private List<SettingInfo.Card> cardList = new List<SettingInfo.Card>();
            private List<SettingInfo.Gem> gemList = new List<SettingInfo.Gem>();
            private List<SettingInfo.Gear> gearList = new List<SettingInfo.Gear>();
            private SettingInfo.WeaponQual weaponQual = new SettingInfo.WeaponQual();
            private List<SettingInfo.Buff> buffList = new List<SettingInfo.Buff>();
            private List<SettingInfo.Skill> skillList = new List<SettingInfo.Skill>();


            public void SetCombatStats(string critical, string specialization, string swiftness)
            {
                combatStats.critical = int.Parse(critical);
                combatStats.specialization = int.Parse(specialization);
                combatStats.swiftness = int.Parse(swiftness);
            }
            public void SetEngraving(SettingInfo.Engraving.NAME name, SettingInfo.Engraving.LEV lev)
            {
                engravingList.Add(new SettingInfo.Engraving { name = name, lev = lev });
            }
            public void SetCard(SettingInfo.Card.NAME name, SettingInfo.Card.SET set, SettingInfo.Card.AWAKENING awakening)
            {
                cardList.Add(new SettingInfo.Card { name = name, set = set, awakening = awakening });
            }
            public void SetGem(SettingInfo.Gem.NAME name, SettingInfo.Gem.LEV lev, SettingInfo.Skill.NAME targetSkill)
            {
                gemList.Add(new SettingInfo.Gem { name = name, lev = lev, targetSkill = targetSkill });
            }
            public void SetGear(SettingInfo.Gear.NAME name, SettingInfo.Gear.SET set, SettingInfo.Gear.LEV setLev)
            {
                gearList.Add(new SettingInfo.Gear { name = name, set = set, setLev = setLev });
            }
            public void SetWeaponQual(string additionalDamage)
            {
                weaponQual.additionalDamage = decimal.Parse(additionalDamage);
            }
            public void SetBuff(SettingInfo.Buff.NAME name)
            {
                buffList.Add(new SettingInfo.Buff { name = name });
            }
            public void SetEmptySkill()
            {
                skillList.Add(null);
            }
            public void SetSkill(SettingInfo.Skill.NAME name, SettingInfo.Skill.LEV lev, SettingInfo.Skill.TRIPOD tp1, SettingInfo.Skill.TRIPOD tp2, SettingInfo.Skill.TRIPOD tp3)
            {
                skillList.Add(new SettingInfo.Skill { name = name, lev = lev, tp1 = tp1, tp2 = tp2, tp3 = tp3 });
            }
            public SettingInfo.CombatStats GetCombatStats() => combatStats.Copy();
            public List<SettingInfo.Engraving> GetEngravingList() => engravingList.ConvertAll(engraving => engraving.Copy());
            public List<SettingInfo.Card> GetCardList() => cardList.ConvertAll(card => card.Copy());
            public List<SettingInfo.Gem> GetGemList() => gemList.ConvertAll(gem => gem.Copy());
            public List<SettingInfo.Gear> GetGearList() => gearList.ConvertAll(gear => gear.Copy());
            public SettingInfo.WeaponQual GetWeaponQual() => weaponQual.Copy();
            public List<SettingInfo.Buff> GetBuffList() => buffList.ConvertAll(buff => buff.Copy());
            public List<SettingInfo.Skill> GetSkillList() => skillList;
            public void Clear()
            {
                combatStats = new SettingInfo.CombatStats();
                engravingList = new List<SettingInfo.Engraving>();
                cardList = new List<SettingInfo.Card>();
                gemList = new List<SettingInfo.Gem>();
                gearList = new List<SettingInfo.Gear>();
                weaponQual = new SettingInfo.WeaponQual();
                buffList = new List<SettingInfo.Buff>();
                skillList = new List<SettingInfo.Skill>();
            }
        }
        
        
        public Setting setting = new Setting();


        //*********************************************************//
        //               세팅을 바탕으로 스탯을 계산               //
        //*********************************************************//
      
        public CharacterStats GetStats(int hpCondition)
        {
            CharacterStats stats = new CharacterStats();

            ApplyBasicStat(stats);
            ApplyingCombatStat(stats);
            ApplyingEngraving(stats, hpCondition);
            ApplyingCard(stats);
            ApplyingGem(stats);
            ApplyingGear(stats);
            ApplyingWeaponQual(stats);
            ApplyBuff(stats);

            return stats;
        }
        private void ApplyBasicStat(CharacterStats stats)
        {
            // 기본 공속 100, 기본 이속 100, 기본 치피 200
            stats.AddStat(new Stats { attackSpeed = new ValueP(100), moveSpeed = new ValueP(100), criticalDamage = new ValueP(200) });

            // 백 어택 보너스
            stats.AddStat(new Stats { damage = new ValueM(5), criticalRate = new ValueP(10) }, SettingInfo.Skill.ATTACKTYPE.백_어택);

            // 헤드 어택 보너스
            stats.AddStat(new Stats { damage = new ValueM(20) }, SettingInfo.Skill.ATTACKTYPE.헤드_어택);
        }
        private void ApplyingCombatStat(CharacterStats stats)
        {
            var combatStats = setting.GetCombatStats();

            // 치명
            stats.AddStat(new Stats { criticalRate = new ValueP(combatStats.critical * 0.035775m) });

            // 특화
            stats.AddStat(new Stats { criticalDamage = new ValueP(combatStats.specialization * 0.03575m * 3) }, SettingInfo.Skill.CATEGORY.핸드건_스탠스);
            stats.AddStat(new Stats { damage = new ValueM(combatStats.specialization * 0.03575m * 0.6m) }, SettingInfo.Skill.CATEGORY.샷건_스탠스);
            stats.AddStat(new Stats { damage = new ValueM(combatStats.specialization * 0.03575m) }, SettingInfo.Skill.CATEGORY.라이플_스탠스);
            stats.AddStat(new Stats { damage = new ValueM(combatStats.specialization * 0.05465m) }, SettingInfo.Skill.CATEGORY.각성_스킬);

            // 신속
            stats.AddStat(new Stats { attackSpeed = new ValueP(combatStats.swiftness / 58.21m) });
            stats.AddStat(new Stats { moveSpeed = new ValueP(combatStats.swiftness / 58.21m) });
            stats.AddStat(new Stats { skillCoolDown = new ValueM(-1 * (combatStats.swiftness / 46.58m)) });
        }
        private void ApplyingEngraving(CharacterStats stats, int hpCondition)
        {
            var lev_sToi = new Dictionary<SettingInfo.Engraving.LEV, int>
            {
                { SettingInfo.Engraving.LEV.__1레벨, 0 }, { SettingInfo.Engraving.LEV.__2레벨, 1 }, { SettingInfo.Engraving.LEV.__3레벨, 2 }
            };

            foreach (var engraving in setting.GetEngravingList())
            {
                int lev = lev_sToi[engraving.lev];
        
                if (engraving.name == SettingInfo.Engraving.NAME.원한)  // 피해 4% / 10% / 20%
                {
                    stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 4, 10, 20 }[lev]) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.예리한_둔기)  // 치피 10% / 25% / 50%, 피해 -2%
                {
                    stats.AddStat(new Stats { criticalDamage = new ValueP(new decimal[] { 10, 25, 50 }[lev]) });
                    stats.AddStat(new Stats { damage = new ValueM(-2) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.저주받은_인형)  // 공격력 3% / 8% / 16% (공격력 각인 끼리 합적용)
                {
                    stats.AddStat(new Stats { attackPowerPer = new ValueM(new decimal[] { 3, 8, 16 }[lev], ValueM.Group.AttackPowerPer_Engraving) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.질량_증가)  // 공격력 4% / 10% / 18% (공격력 각인 끼리 합적용), 공속 -10%
                {
                    stats.AddStat(new Stats { attackPowerPer = new ValueM(new decimal[] { 4, 10, 18 }[lev], ValueM.Group.AttackPowerPer_Engraving) });
                    stats.AddStat(new Stats { attackSpeed = new ValueP(-10) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.타격의_대가)  // '타대' 피해 3% / 8% / 16%
                {
                    stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 3, 8, 16 }[lev]) }, SettingInfo.Skill.ATTACKTYPE.타대);
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.기습의_대가)  // '백 어택' 피해 5% / 12% / 25%
                {
                    stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 5, 12, 25 }[lev]) }, SettingInfo.Skill.ATTACKTYPE.백_어택);
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.아드레날린)  // 치적 5% / 10%/ 15%, 공격력 1.8% / 3.6% / 6% (공격력 각인 끼리 합적용)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(new decimal[] { 5, 10, 15 }[lev]) });
                    stats.AddStat(new Stats { attackPowerPer = new ValueM(new decimal[] { 1.8m, 3.6m, 6m }[lev], ValueM.Group.AttackPowerPer_Engraving) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.돌격대장)  // 피해 4% / 8.8% / 18%
                {
                    stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 4, 8.8m, 18 }[lev]) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.피스메이커)
                {
                    // 핸드건 스탠스 상태에서 공속 8% / 12% / 16%
                    stats.AddStat(new Stats { attackSpeed = new ValueP(new decimal[] { 8, 12, 16 }[lev]) }, SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스);

                    // 샷건 스탠스 상태에서 치적 10%, 피해 5% / 10% / 15%
                    stats.AddStat(new Stats { criticalRate = new ValueP(10) }, SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스);
                    stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 5, 10, 15 }[lev]) }, SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스);

                    // 라이플 스탠스 상태에서 피해 10%, 약무 이후 '추가로' 10/20/30% 증가 ('추가로' -> 합적용)
                    if(hpCondition <= 50)
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 20, 30, 40 }[lev]) }, SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스);
                    else
                        stats.AddStat(new Stats { damage = new ValueM(10) }, SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스);
                }
            }
        }
        private void ApplyingCard(CharacterStats stats)
        {
            foreach (var card in setting.GetCardList())
            {
                if (card.name == SettingInfo.Card.NAME.남겨진_바람의_절벽)
                {
                    if (card.set == SettingInfo.Card.SET.__6세트)
                    {
                        if (card.awakening == SettingInfo.Card.AWAKENING.__12각성)
                        {
                            stats.AddStat(new Stats { criticalRate = new ValueP(7) });
                        }
                    }
                }
                else if (card.name == SettingInfo.Card.NAME.세상을_구하는_빛)
                {
                    if (card.set == SettingInfo.Card.SET.__6세트)
                    {
                        if (card.awakening == SettingInfo.Card.AWAKENING.__18각성)
                        {
                            stats.AddStat(new Stats { damage = new ValueM(7) });
                        }
                        else if (card.awakening == SettingInfo.Card.AWAKENING.__30각성)
                        {
                            stats.AddStat(new Stats { damage = new ValueM(15) });
                        }
                    }
                }
            }
        }
        private void ApplyingGem(CharacterStats stats)
        {
            var lev_sToi = new Dictionary<SettingInfo.Gem.LEV, int>
            {
                { SettingInfo.Gem.LEV.__7레벨, 0 }, { SettingInfo.Gem.LEV.__8레벨, 1 }, { SettingInfo.Gem.LEV.__9레벨, 2 }, { SettingInfo.Gem.LEV.__10레벨, 3 }
            };

            foreach (var gem in setting.GetGemList())
            {
                int lev = lev_sToi[gem.lev];

                if (gem.name == SettingInfo.Gem.NAME.멸화)
                {
                    stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 21, 24, 30, 40 }[lev]) }, gem.targetSkill);
                }
                else if (gem.name == SettingInfo.Gem.NAME.홍염)
                {
                    stats.AddStat(new Stats { skillCoolDown = new ValueM(new decimal[] { -14, -16, -18, -20 }[lev]) }, gem.targetSkill);
                }
            }
        }
        private void ApplyingGear(CharacterStats stats)
        {
            var lev_sToi = new Dictionary<SettingInfo.Gear.LEV, int> { { SettingInfo.Gear.LEV.__1레벨, 0 }, { SettingInfo.Gear.LEV.__2레벨, 1 }, { SettingInfo.Gear.LEV.__3레벨, 2 } };

            foreach (var gear in setting.GetGearList())
            {
                int lev = lev_sToi[gear.setLev];

                if (gear.name == SettingInfo.Gear.NAME.악몽)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2세트: 피해 17% (각성 스킬 제외)
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, 17 }[lev]) });
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, -17 }[lev]) }, SettingInfo.Skill.CATEGORY.각성_스킬);

                        // 4세트: 마나중독 발동 시 추피 20%
                        stats.AddStat(new Stats { additionalDamage = new ValueP(new decimal[] { 0, 0, 20 }[lev]) });

                        // 6세트: 마나중독 발동 시 피해 20% '추가 증가'
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, 20 }[lev]) });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.구원)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2+4+6세트: 추피 63%
                        stats.AddStat(new Stats { additionalDamage = new ValueP(new decimal[] { 0, 0, 63 }[lev]) });

                        // 4세트: 공속 10%
                        stats.AddStat(new Stats { attackSpeed = new ValueP(10) });

                        // 6세트: 피해 5%
                        stats.AddStat(new Stats { damage = new ValueM(5) });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.환각)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 4세트: 치적 20%
                        stats.AddStat(new Stats { criticalRate = new ValueP(new decimal[] { 15, 18, 20 }[lev]) });

                        // 6세트: 피증 15%, 치적 8% '추가 증가'
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 12, 14, 15 }[lev]) });
                        stats.AddStat(new Stats { criticalRate = new ValueP(new decimal[] { 5, 7, 8 }[lev]) });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.사멸)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2세트: '백 어택' 또는 '헤드 어택' 적중 시 치피 +65% (적중 실패 시 치피 +22%)
                        stats.AddStat(new Stats { criticalDamage = new ValueP(new decimal[] { 0, 0, 65 }[lev]) }, SettingInfo.Skill.ATTACKTYPE.백_어택);
                        stats.AddStat(new Stats { criticalDamage = new ValueP(new decimal[] { 0, 0, 65 }[lev]) }, SettingInfo.Skill.ATTACKTYPE.헤드_어택);

                        // 4세트: 치적 +22%
                        stats.AddStat(new Stats { criticalRate = new ValueP(new decimal[] { 0, 0, 22 }[lev]) });

                        // 6세트: '백 어택' 또는 '헤드 어택' 적중 시 피해 +26% (적중 실패 시 피해 +9%)
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, 26 }[lev]) }, SettingInfo.Skill.ATTACKTYPE.백_어택);
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, 26 }[lev]) }, SettingInfo.Skill.ATTACKTYPE.헤드_어택);
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.지배)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2세트: 각성기 피해 -10%, 각성기 쿨타임 -20%
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, -10 }[lev]) }, SettingInfo.Skill.CATEGORY.각성_스킬);
                        stats.AddStat(new Stats { skillCoolDown = new ValueM(-20) }, SettingInfo.Skill.CATEGORY.각성_스킬);

                        // 4세트: 내면의 각성 발동 시 쿨탐 -18%, 피증 31% (각성기 제외)
                        stats.AddStat(new Stats { skillCoolDown = new ValueM(-18) });
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, 31 }[lev]) });

                        stats.AddStat(new Stats { skillCoolDown = new ValueM(18) }, SettingInfo.Skill.CATEGORY.각성_스킬);
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, -31 }[lev]) }, SettingInfo.Skill.CATEGORY.각성_스킬);

                        // 6세트: 강화 발동 시 피증 20%
                        stats.AddStat(new Stats { damage = new ValueM(new decimal[] { 0, 0, 20 }[lev]) });
                    }
                }
            }
        }
        private void ApplyingWeaponQual(CharacterStats stats)
        {
            stats.AddStat(new Stats { additionalDamage = new ValueP(setting.GetWeaponQual().additionalDamage) });
        }
        private void ApplyBuff(CharacterStats stats)
        {
            foreach (var buff in setting.GetBuffList())
            {
                if (buff.name == SettingInfo.Buff.NAME.건슬링어_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.갈망_6세트_버프)
                {
                    stats.AddStat(new Stats { attackSpeed = new ValueP(12) });
                    stats.AddStat(new Stats { moveSpeed = new ValueP(12) });
                    stats.AddStat(new Stats { additionalDamage = new ValueP(12) });
                }
            }
        }



        //***************************************************************************//
        //                                 스킬 구현                                 //
        //                                                                           //
        //  Part Skill 선언 -> Tripod 적용 -> Part Skill Stats 적용 순서로 구현한다  //
        //***************************************************************************//

        public CombatSkill GetSkill(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)
        {
            if (skillSetting.name == SettingInfo.Skill.NAME.레인_오브_불릿) return RainOfBullet(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.샷건_연사) return ShotgunRapidFire(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.퍼펙트_샷) return PerfectShot(skillSetting, hpCondition, characterStats);
            return new CombatSkill();
        }
        private CombatSkill RainOfBullet(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)
        {
            var name = skillSetting.name;
            var cool = 22m;
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 }, 
                { SettingInfo.Skill.LEV.__4레벨, 1 }, 
                { SettingInfo.Skill.LEV.__7레벨, 2 }, 
                { SettingInfo.Skill.LEV.__10레벨, 3 }, 
                { SettingInfo.Skill.LEV.__11레벨, 4 }, 
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬 구현
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { name },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.핸드건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스 },

                constant = new decimal[] { 42.79105545m, 112.9771528m, 145.9607833m, 167.2741259m, 153.8214569m, 146.5868532m }[levIndex],
                ratio = new decimal[] { 18.25147783m, 18.25147783m, 18.25147783m, 18.25147783m, 19.85418719m, 20.8408867m }[levIndex]
            };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.기습:
                        part1.appliedStats.damage.Add(45);
                        break;
                    case SettingInfo.Skill.TRIPOD.사면초가:
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
                    case SettingInfo.Skill.TRIPOD.속사:
                        part1.appliedStats.damage.Add(95);
                        break;
                }
            }
           
            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.화염_사격:
                        part1.appliedStats.damage.Add(95);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return new CombatSkill { name = name, cooldownTime = cool, partList = new List<CombatSkill.Part> { part1 } };
        }
        private CombatSkill ShotgunRapidFire(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)
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
            var shotgunRapidFire = new CombatSkill { name = SettingInfo.Skill.NAME.샷건_연사, cooldownTime = 36m, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.기습:
                        part1.appliedStats.damage.Add(45);
                        break;
                    case SettingInfo.Skill.TRIPOD.사면초가:
                        part1.attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 };
                        part1.appliedStats.damage.Add(28);
                        part1.appliedStats.criticalRate.Add(10);
                        break;
                    case SettingInfo.Skill.TRIPOD.콤보_연사:
                        part1.type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.콤보 };
                        break;
                }
            }
           
            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.강화된_사격:
                        part1.appliedStats.damage.Add(50);
                        break;
                    case SettingInfo.Skill.TRIPOD.빠른_준비:
                        shotgunRapidFire.cooldownTime = 36m - 15m;
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
                    case SettingInfo.Skill.TRIPOD.특수_탄환:    // 피해가 120% 증가한다
                        part1.appliedStats.damage.Add(120);
                        break;
                }
            }
           
            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return shotgunRapidFire;
        }
        private CombatSkill PerfectShot(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)
        {
            var name = skillSetting.name;
            var cool = 30m;
            var levIndex = new Dictionary<SettingInfo.Skill.LEV, int>
            {
                { SettingInfo.Skill.LEV.__1레벨, 0 },
                { SettingInfo.Skill.LEV.__4레벨, 1 },
                { SettingInfo.Skill.LEV.__7레벨, 2 },
                { SettingInfo.Skill.LEV.__10레벨, 3 },
                { SettingInfo.Skill.LEV.__11레벨, 4 },
                { SettingInfo.Skill.LEV.__12레벨, 5 }
            }[skillSetting.lev];

            // 부분 스킬 구현
            var part1 = new CombatSkill.Part
            {
                name = new List<SettingInfo.Skill.NAME> { name },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.95753854m, 113.6370086m, 146.4756603m, 167.4389391m, 153.9864359m, 146.752898m }[levIndex],
                ratio = new decimal[] { 26.18865136m, 26.18865136m, 26.18865136m, 26.18865136m, 28.4895223m, 29.9074162m }[levIndex]
            };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.출혈_효과:
                        break;
                }
            }

            // 트라이포드 2단계
            if (levIndex >= 2)
            {
                switch (skillSetting.tp2)
                {
                    case SettingInfo.Skill.TRIPOD.정밀_사격:
                        part1.appliedStats.criticalRate.Add(80);
                        break;
                    case SettingInfo.Skill.TRIPOD.마무리_사격:
                        if (hpCondition <= 50) part1.appliedStats.damage.Add(96);
                        break;
                }
            }

            // 트라이포드 3단계
            if (levIndex >= 3)
            {
                switch (skillSetting.tp3)
                {
                    case SettingInfo.Skill.TRIPOD.강화된_사격:
                        part1.appliedStats.damage.Add(80);
                        break;
                }
            }
            
            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return new CombatSkill { name = name, cooldownTime = cool, partList = new List<CombatSkill.Part> { part1 } };
        }
    }
}