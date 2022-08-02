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
                s += "- 스킬 이름: " + "{" + (partList[i].name.Count > 0 ? partList[i].name.ConvertAll(name => name.ToStr()).Aggregate((x, y) => x + ", " + y) : "") + "}" + Environment.NewLine;
                s += "- 스킬 분류: " + "{" + (partList[i].category.Count > 0 ? partList[i].category.ConvertAll(category => category.ToStr()).Aggregate((x, y) => x + ", " + y) : "") + "}" + Environment.NewLine;
                s += "- 스킬 타입: " + "{" + (partList[i].type.Count > 0 ? partList[i].type.ConvertAll(type => type.ToStr()).Aggregate((x, y) => x + ", " + y) + "}" : "") + Environment.NewLine;
                s += "- 공격 타입: " + "{" + (partList[i].attackType.Count > 0 ? partList[i].attackType.ConvertAll(attackType => attackType.ToStr()).Aggregate((x, y) => x + ", " + y) : "") + "}" + Environment.NewLine;
                s += "- 직업 각인: " + "{" + (partList[i].classEngraving.Count > 0 ? partList[i].classEngraving.ConvertAll(classEngraving => classEngraving.ToStr()).Aggregate((x, y) => x + ", " + y) : "") + "}" + Environment.NewLine;
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
                else if (buff.name == SettingInfo.Buff.NAME.서폿_낙인)
                {
                    stats.AddStat(new Stats { damage = new ValueM(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서폿_공증)
                {
                    stats.AddStat(new Stats { attackPowerPer = new ValueM(6, ValueM.Group.AttackPowerPer_Supporter) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서폿_2버블)
                {
                    stats.AddStat(new Stats { damage = new ValueM(15) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.데빌헌터_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.아르카나_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.기상술사_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.배틀마스터_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(18) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.창술사_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(18) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.스트라이커_치적_시너지)
                {
                    stats.AddStat(new Stats { criticalRate = new ValueP(18) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.블래스터_방깍_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.디스트로이어_방깍_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.워로드_방깍_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서머너_방깍_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.리퍼_방깍_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.버서커_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.호크아이_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.인파이터_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.소서리스_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.데모닉_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.스카우터_공증_시너지)
                {
                    stats.AddStat(new Stats { attackPowerPer = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.기공사_공증_시너지)
                {
                    stats.AddStat(new Stats { attackPowerPer = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.워로드_백헤드_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12) }, SettingInfo.Skill.ATTACKTYPE.백_어택);
                    stats.AddStat(new Stats { damage = new ValueM(12) }, SettingInfo.Skill.ATTACKTYPE.헤드_어택);
                }
                else if (buff.name == SettingInfo.Buff.NAME.블레이드_백헤드_피증_시너지)
                {
                    stats.AddStat(new Stats { damage = new ValueM(12) }, SettingInfo.Skill.ATTACKTYPE.백_어택);
                    stats.AddStat(new Stats { damage = new ValueM(12) }, SettingInfo.Skill.ATTACKTYPE.헤드_어택);
                }


                /*
         워로드_백헤드_피증_시너지,
                블레이드_백헤드_피증_시너지,
                 */
            }
        }



        //***************************************************************************//
        //                                 스킬 구현                                 //
        //                                                                           //
        //  Part Skill 선언 -> Tripod 적용 -> Part Skill Stats 적용 순서로 구현한다  //
        //***************************************************************************//

        public CombatSkill GetSkill(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)
        {
            if (skillSetting.name == SettingInfo.Skill.NAME.레인_오브_불릿) return BulletRain(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.샷건_연사) return ShotgunRapidFire(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.최후의_만찬) return LastRequest(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.절멸의_탄환) return DualBuckshot(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.마탄의_사수) return Sharpshooter(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.대재앙 
                || skillSetting.name == SettingInfo.Skill.NAME.대재앙_샷건활용) return Catastrophe(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.퍼펙트_샷) return PerfectShot(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.포커스_샷) return FocusedShot(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.타겟_다운) return TargetDown(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈 
                || skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_샷건활용 
                || skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_라이플활용) return TwilightEye(skillSetting, hpCondition, characterStats);
            else if (skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환
                || skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_샷건활용
                || skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_라이플활용) return HighCaliberHEBullet(skillSetting, hpCondition, characterStats);
            return new CombatSkill();
        }
        private CombatSkill BulletRain(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 레인 오브 불릿
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

            // 부분 스킬 구현
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
            var bulletRain = new CombatSkill { name = SettingInfo.Skill.NAME.레인_오브_불릿, cooldownTime = 22m, partList = new List<CombatSkill.Part> { part1 } };

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
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return bulletRain;
        }
        private CombatSkill ShotgunRapidFire(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 샷건 연사
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
                    case SettingInfo.Skill.TRIPOD.특수_탄환:    // 피해 +120%
                        part1.appliedStats.damage.Add(120);
                        break;
                }
            }
           
            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return shotgunRapidFire;
        }
        private CombatSkill LastRequest(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 최후의 만찬
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
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.최후의_만찬 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.샷건_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.일반 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스 },

                constant = new decimal[] { 42.99251556m, 113.5521002m, 146.4512628m, 167.3048768m, 153.8874387m, 146.6361637m }[levIndex],
                ratio = new decimal[] { 19.51502463m, 19.51527094m, 19.51502463m, 19.51527094m, 21.22980296m, 22.2864532m }[levIndex]
            };

            // 최종 스킬
            var lastRequest = new CombatSkill { name = SettingInfo.Skill.NAME.최후의_만찬, cooldownTime = 36m, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계  
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.빠른_준비:   // 재사용 대기시간 -13초
                        lastRequest.cooldownTime = 36m - 13m;
                        break;
                    case SettingInfo.Skill.TRIPOD.화염탄:
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
                    case SettingInfo.Skill.TRIPOD.뜨거운_열기:
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
                    case SettingInfo.Skill.TRIPOD.더블_샷:    // 기본 피해량의 97.6%인 샷건을 두 방을 쏜다
                        part1.appliedStats.damage.Add(95.2m);
                        break;
                    case SettingInfo.Skill.TRIPOD.연발_사격:    // 피해 +119.6%
                        part1.appliedStats.damage.Add(119.6m);
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return lastRequest;
        }
        private CombatSkill DualBuckshot(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 절멸의 탄환
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
            var dualBuckshot = new CombatSkill { name = SettingInfo.Skill.NAME.절멸의_탄환, cooldownTime = 30m, partList = new List<CombatSkill.Part> { part1, part1, part2 } };

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
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));
            part2.appliedStats.Add(characterStats.GetSkillStat(part2));

            return dualBuckshot;
        }
        private CombatSkill Sharpshooter(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 마탄의 사수
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

            // 부분 스킬: 가디언의 숨결 (참고 사항: 특화의 샷건 방무 미적용)
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

            // 최종 스킬
            var sharpshooter = new CombatSkill { name = SettingInfo.Skill.NAME.마탄의_사수, cooldownTime = 30m, partList = new List<CombatSkill.Part> { part1 } };

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
                    case SettingInfo.Skill.TRIPOD.가디언의_숨결:   // 기본 피해량의 102%에 해댱하는 추가 피해를 준다
                        sharpshooter.partList = new List<CombatSkill.Part> { part1, part2 };
                        break;
                    case SettingInfo.Skill.TRIPOD.혹한의_안식처:
                        break;
                }
            }

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));
            part2.appliedStats.Add(characterStats.GetSkillStat(part2));

            return sharpshooter;
        }
        private CombatSkill Catastrophe(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 대재앙
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
                    skillSetting.name==SettingInfo.Skill.NAME.대재앙_샷건활용 ? SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스 : SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스,
                    SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스
                },

                constant = new decimal[] { 42.99325907m, 113.6458383m, 146.5943969m, 167.6258098m, 154.1585823m, 146.8923798m }[levIndex],
                ratio = new decimal[] { 20.60788177m, 20.60788177m, 20.60788177m, 20.60541872m, 22.41847291m, 23.53423645m }[levIndex] * 1.704m
            };

            // 최종 스킬
            var catastrophe = new CombatSkill { name = SettingInfo.Skill.NAME.대재앙, cooldownTime = 24m, partList = new List<CombatSkill.Part> { part1 } };

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
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));
            part2.appliedStats.Add(characterStats.GetSkillStat(part2));

            return catastrophe;
        }
        private CombatSkill PerfectShot(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 퍼펙트 샷
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
                name = new List<SettingInfo.Skill.NAME> { SettingInfo.Skill.NAME.퍼펙트_샷 },
                category = new List<SettingInfo.Skill.CATEGORY> { SettingInfo.Skill.CATEGORY.라이플_스탠스 },
                type = new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩 },
                attackType = new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.타대 },
                classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 },

                constant = new decimal[] { 42.96599407m, 113.6593763m, 146.5044918m, 167.4718969m, 154.0163431m, 146.7808338m }[levIndex],
                ratio = new decimal[] { 26.18349754m, 26.18349754m, 26.18349754m, 26.18349754m, 28.48399015m, 29.90172414m }[levIndex]
            };

            // 최종 스킬
            var perfectShot = new CombatSkill { name = SettingInfo.Skill.NAME.퍼펙트_샷, cooldownTime = 30m, partList = new List<CombatSkill.Part> { part1 } };

            // 트라이포드 1단계
            if (levIndex >= 1)
            {
                switch (skillSetting.tp1)
                {
                    case SettingInfo.Skill.TRIPOD.출혈_효과:
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
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return perfectShot;
        }
        private CombatSkill FocusedShot(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 포커스 샷
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
            var focusedShot = new CombatSkill { name = SettingInfo.Skill.NAME.포커스_샷, cooldownTime = 27m, partList = new List<CombatSkill.Part> { part1, part1, part3 } };

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
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));
            part2.appliedStats.Add(characterStats.GetSkillStat(part2));
            part3.appliedStats.Add(characterStats.GetSkillStat(part3));

            return focusedShot;
        }
        private CombatSkill TargetDown(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 타겟 다운
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

            // 최종 스킬
            var targetDown = new CombatSkill { name = SettingInfo.Skill.NAME.타겟_다운, cooldownTime = 36m, partList = new List<CombatSkill.Part> { part1, part2, part3 } };

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
                    case SettingInfo.Skill.TRIPOD.작렬철강탄:
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
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));
            part2.appliedStats.Add(characterStats.GetSkillStat(part2));
            part3.appliedStats.Add(characterStats.GetSkillStat(part3));
            part4.appliedStats.Add(characterStats.GetSkillStat(part4));

            return targetDown;
        }
        private CombatSkill TwilightEye(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 황혼의 눈
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
            if (skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_샷건활용) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스 };
            else if (skillSetting.name == SettingInfo.Skill.NAME.황혼의_눈_라이플활용) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스 };

            // 최종 스킬
            var twilightEye = new CombatSkill { name = SettingInfo.Skill.NAME.황혼의_눈, cooldownTime = 300m, partList = new List<CombatSkill.Part> { part1 } };

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return twilightEye;
        }
        private CombatSkill HighCaliberHEBullet(SettingInfo.Skill skillSetting, int hpCondition, CharacterStats characterStats)   // 대구경_폭발_탄환
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
            if (skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_샷건활용) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스 };
            else if (skillSetting.name == SettingInfo.Skill.NAME.대구경_폭발_탄환_라이플활용) part1.classEngraving = new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스 };

            // 최종 스킬
            var highCaliberHEBullet = new CombatSkill { name = SettingInfo.Skill.NAME.대구경_폭발_탄환, cooldownTime = 300m, partList = new List<CombatSkill.Part> { part1 } };

            // 캐릭터 스탯 적용
            part1.appliedStats.Add(characterStats.GetSkillStat(part1));

            return highCaliberHEBullet;
        }

    }
}