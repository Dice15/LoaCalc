using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostarkSimulator
{

    /// <summary>
    /// 로스트아크 직업(클래스)를 구현한 클래스. 
    /// 직업을 구현할 때 이 클래스를 상속받는다.
    /// 세팅 정보와 특성, 각인, 보석, 장비, 카드 정보 등이 이 클래스에 구현되어 있다.
    /// </summary>
    public abstract class AbstractLostArkJob
    {

        public Setting setting = new Setting();





        //*********************************************************//
        //               세팅 정보를 담고 있는 클래스              //
        //*********************************************************//
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

            public void SetCombatStats(int critical, int specialization, int swiftness)
            {
                combatStats.critical = critical;
                combatStats.specialization = specialization;
                combatStats.swiftness = swiftness;
            }

            public void SetEngraving(List<(SettingInfo.Engraving.NAME name, SettingInfo.Engraving.LEV lev)?> selectedEngravingList)
            {
                engravingList.Clear();

                foreach (var selectedEngraving in selectedEngravingList)
                {
                    if (selectedEngraving != null)
                    {
                        var newItem = selectedEngraving.GetValueOrDefault();
                        int index = engravingList.FindIndex(item => item.name == newItem.name);

                        if (index == -1) engravingList.Add(new SettingInfo.Engraving { name = newItem.name, lev = newItem.lev });
                        else if (engravingList[index].lev < newItem.lev) engravingList[index].lev = newItem.lev;
                    }
                }
            }

            public void SetCard(List<(SettingInfo.Card.NAME name, SettingInfo.Card.SET set, SettingInfo.Card.AWAKENING awakening)?> selectedCardList)
            {
                cardList.Clear();

                foreach (var selectedCard in selectedCardList)
                {
                    if (selectedCard != null)
                    {
                        var newItem = selectedCard.GetValueOrDefault();
                        int index = cardList.FindIndex(item => item.name == newItem.name);

                        if (index == -1) cardList.Add(new SettingInfo.Card { name = newItem.name, set = newItem.set, awakening = newItem.awakening });
                        else if (cardList[index].awakening < newItem.awakening)
                        {
                            cardList[index].set = newItem.set;
                            cardList[index].awakening = newItem.awakening;
                        }
                    }
                }
            }
           
            public void SetGem(List<(SettingInfo.Gem.NAME name, SettingInfo.Gem.LEV lev, SettingInfo.Skill.NAME targetSkill)?> selectedGemList)
            {
                gemList.Clear();

                foreach (var selectedGem in selectedGemList)
                {
                    if (selectedGem != null)
                    {
                        var newItem = selectedGem.GetValueOrDefault();
                        int index = gemList.FindIndex(item => (item.name == newItem.name && item.targetSkill == newItem.targetSkill));

                        if (index == -1) gemList.Add(new SettingInfo.Gem { name = newItem.name, lev = newItem.lev, targetSkill = newItem.targetSkill });
                        else if (gemList[index].lev < newItem.lev) gemList[index].lev = newItem.lev;
                    }
                }
            }

            public void SetGear(List<(SettingInfo.Gear.NAME name, SettingInfo.Gear.SET set, SettingInfo.Gear.LEV setLev)?> selectedGearList)
            {
                gearList.Clear();

                foreach (var selectedGear in selectedGearList)
                {
                    if (selectedGear != null)
                    {
                        var newItem = selectedGear.GetValueOrDefault();
                        int index = gearList.FindIndex(item => item.name == newItem.name);

                        if (index == -1) gearList.Add(new SettingInfo.Gear { name = newItem.name, set = newItem.set, setLev = newItem.setLev });
                        else if (gearList[index].set < newItem.set)
                        {
                            gearList[index].set = newItem.set;
                            gearList[index].setLev = newItem.setLev;
                        }
                        else if(gearList[index].set == newItem.set && gearList[index].setLev < newItem.setLev)
                        {
                            gearList[index].setLev = newItem.setLev;
                        }
                    }
                }
            }
           
            public void SetWeaponQual(decimal additionalDamage)
            {
                weaponQual.additionalDamage = additionalDamage;
            }

            public void SetBuff(List<SettingInfo.Buff.NAME?> selectedBuffList)
            {
                buffList.Clear();

                foreach (var selectedBuff in selectedBuffList)
                {
                    if (selectedBuff != null)
                    {
                        var newItem = selectedBuff.GetValueOrDefault();
                        int index = buffList.FindIndex(item => item.name == newItem);

                        if (index == -1) buffList.Add(new SettingInfo.Buff { name = newItem });
                    }
                }
            }          

            public void SetSkill(List<(SettingInfo.Skill.NAME name, SettingInfo.Skill.LEV lev, SettingInfo.Skill.TRIPOD tp1, SettingInfo.Skill.TRIPOD tp2, SettingInfo.Skill.TRIPOD tp3)?> selectedSkillList)
            {
                skillList.Clear();

                foreach (var selectedSkill in selectedSkillList)
                {
                    if (selectedSkill != null)
                    {
                        var newItem = selectedSkill.GetValueOrDefault();
                        skillList.Add(new SettingInfo.Skill { name = newItem.name, lev = newItem.lev, tp1 = newItem.tp1, tp2 = newItem.tp2, tp3 = newItem.tp3 });
                    }
                    else skillList.Add(null);
                }
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




        //*********************************************************//
        //               세팅을 바탕으로 스탯을 계산               //
        //*********************************************************//

        public PackedStats GetStats(int hpCondition)
        {
            PackedStats stats = new PackedStats();

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

        private void ApplyBasicStat(PackedStats stats)
        {
            // 기본 공속 100, 기본 이속 100, 기본 치피 200
            stats.AddStats(new Stats { attackSpeed = new ValueP(100), moveSpeed = new ValueP(100), criticalDamage = new ValueP(200) });

            // 백 어택 보너스
            stats.AddStats(new Stats { damage = new ValueM(5), criticalRate = new ValueP(10) }, attackType: SettingInfo.Skill.ATTACKTYPE.백_어택);

            // 헤드 어택 보너스
            stats.AddStats(new Stats { damage = new ValueM(20) }, attackType: SettingInfo.Skill.ATTACKTYPE.헤드_어택);
        }

        private void ApplyingCombatStat(PackedStats stats)
        {
            var combatStats = setting.GetCombatStats();

            // 치명
            stats.AddStats(new Stats { criticalRate = new ValueP(combatStats.critical * 0.035775m) });

            // 특화
            stats.AddPackedStats(Specialization(combatStats));

            // 신속
            stats.AddStats(new Stats { attackSpeed = new ValueP(combatStats.swiftness / 58.21m) });
            stats.AddStats(new Stats { moveSpeed = new ValueP(combatStats.swiftness / 58.21m) });
            stats.AddStats(new Stats { skillCoolDown = new ValueM(-1 * (combatStats.swiftness / 46.58m)) });
        }

        protected abstract PackedStats Specialization(SettingInfo.CombatStats combatStats);

        private void ApplyingEngraving(PackedStats stats, int hpCondition)
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
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 4, 10, 20 }[lev]) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.예리한_둔기)  // 치피 10% / 25% / 50%, 피해 -2%
                {
                    stats.AddStats(new Stats { criticalDamage = new ValueP(new decimal[] { 10, 25, 50 }[lev]) });
                    stats.AddStats(new Stats { damage = new ValueM(-2) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.저주받은_인형)  // 공격력 3% / 8% / 16% (공격력 각인 끼리 합적용)
                {
                    stats.AddStats(new Stats { attackPowerPer = new ValueM(new decimal[] { 3, 8, 16 }[lev], ValueM.Group.AttackPowerPer_Engraving) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.질량_증가)  // 공격력 4% / 10% / 18% (공격력 각인 끼리 합적용), 공속 -10%
                {
                    stats.AddStats(new Stats { attackPowerPer = new ValueM(new decimal[] { 4, 10, 18 }[lev], ValueM.Group.AttackPowerPer_Engraving) });
                    stats.AddStats(new Stats { attackSpeed = new ValueP(-10) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.타격의_대가)  // '타대' 피해 3% / 8% / 16%
                {
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 3, 8, 16 }[lev]) }, attackType: SettingInfo.Skill.ATTACKTYPE.타대);
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.기습의_대가)  // '백 어택' 피해 5% / 12% / 25%
                {
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 5, 12, 25 }[lev]) }, attackType: SettingInfo.Skill.ATTACKTYPE.백_어택);
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.아드레날린)  // 치적 5% / 10% / 15%, 공격력 1.8% / 3.6% / 6% (공격력 각인 끼리 합적용)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(new decimal[] { 5, 10, 15 }[lev]) });
                    stats.AddStats(new Stats { attackPowerPer = new ValueM(new decimal[] { 1.8m, 3.6m, 6m }[lev], ValueM.Group.AttackPowerPer_Engraving) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.속전속결)  // 홀딩 및 캐스팅 스킬 피해 4% / 10% / 20%
                {
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 4, 10, 20 }[lev]) }, typeList: new List<SettingInfo.Skill.TYPE> { SettingInfo.Skill.TYPE.홀딩, SettingInfo.Skill.TYPE.캐스팅 });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.돌격대장)  // 피해 4% / 8.8% / 18%
                {
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 4, 8.8m, 18 }[lev]) });
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.피스메이커)
                {
                    // 핸드건 스탠스 상태에서 공속 8% / 12% / 16%
                    stats.AddStats(new Stats { attackSpeed = new ValueP(new decimal[] { 8, 12, 16 }[lev]) }, classEngraving: SettingInfo.Skill.CLASSENGRAVING.피스메이커_핸드건_스탠스);

                    // 샷건 스탠스 상태에서 치적 10%, 피해 5% / 10% / 15%
                    stats.AddStats(new Stats { criticalRate = new ValueP(10) }, classEngraving: SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스);
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 5, 10, 15 }[lev]) }, classEngraving: SettingInfo.Skill.CLASSENGRAVING.피스메이커_샷건_스탠스);

                    // 라이플 스탠스 상태에서 피해 10%, 약무 이후 '추가로' 10/20/30% 증가 ('추가로' -> 합적용)
                    if (hpCondition <= 50) stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 20, 30, 40 }[lev]) }, classEngraving: SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스);
                    else stats.AddStats(new Stats { damage = new ValueM(10) }, classEngraving: SettingInfo.Skill.CLASSENGRAVING.피스메이커_라이플_스탠스);
                }
                else if (engraving.name == SettingInfo.Engraving.NAME.사냥의_시간)
                {
                    // 핸드건 스킬과 라이플 스킬의 치명타 적중률이 22% / 33% / 45% 증가
                    stats.AddStats(new Stats { criticalRate = new ValueP(new decimal[] { 22, 33, 45 }[lev]) }, classEngravingList: new List<SettingInfo.Skill.CLASSENGRAVING> { SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_핸드건_스탠스, SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_라이플_스탠스 });

                    // 샷건 스킬을 사용할 수 없다 (데미지 100% 감소로 구현)
                    stats.AddStats(new Stats { damage = new ValueM(-100) }, classEngraving: SettingInfo.Skill.CLASSENGRAVING.사냥의_시간_샷건_스탠스);
                }
            }
        }
       
        private void ApplyingCard(PackedStats stats)
        {
            foreach (var card in setting.GetCardList())
            {
                if (card.name == SettingInfo.Card.NAME.남겨진_바람의_절벽)
                {
                    if (card.set == SettingInfo.Card.SET.__6세트)
                    {
                        if (card.awakening == SettingInfo.Card.AWAKENING.__12각성)
                        {
                            stats.AddStats(new Stats { criticalRate = new ValueP(7) });
                        }
                    }
                }
                else if (card.name == SettingInfo.Card.NAME.세상을_구하는_빛)
                {
                    if (card.set == SettingInfo.Card.SET.__6세트)
                    {
                        if (card.awakening == SettingInfo.Card.AWAKENING.__18각성)
                        {
                            stats.AddStats(new Stats { damage = new ValueM(7) });
                        }
                        else if (card.awakening == SettingInfo.Card.AWAKENING.__30각성)
                        {
                            stats.AddStats(new Stats { damage = new ValueM(15) });
                        }
                    }
                }
            }
        }

        private void ApplyingGem(PackedStats stats)
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
                    stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 21, 24, 30, 40 }[lev]) }, name: gem.targetSkill);
                }
                else if (gem.name == SettingInfo.Gem.NAME.홍염)
                {
                    stats.AddStats(new Stats { skillCoolDown = new ValueM(new decimal[] { -14, -16, -18, -20 }[lev]) }, name: gem.targetSkill);
                }
            }
        }
      
        private void ApplyingGear(PackedStats stats)
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
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, 17 }[lev]) });
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, -17 }[lev]) }, category: SettingInfo.Skill.CATEGORY.각성_스킬);

                        // 4세트: 마나중독 발동 시 추피 20%
                        stats.AddStats(new Stats { additionalDamage = new ValueP(new decimal[] { 0, 0, 20 }[lev]) });

                        // 6세트: 마나중독 발동 시 피해 20% '추가 증가'
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, 20 }[lev]) });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.구원)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2+4+6세트: 추피 63%
                        stats.AddStats(new Stats { additionalDamage = new ValueP(new decimal[] { 0, 0, 63 }[lev]) });

                        // 4세트: 공속 10%
                        stats.AddStats(new Stats { attackSpeed = new ValueP(10) });

                        // 6세트: 피해 5%
                        stats.AddStats(new Stats { damage = new ValueM(5) });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.환각)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 4세트: 치적 20%
                        stats.AddStats(new Stats { criticalRate = new ValueP(new decimal[] { 15, 18, 20 }[lev]) });

                        // 2 + 6세트: 피증 17 + 15%, 치적 8%
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 13 + 12, 15 + 14, 17 + 15 }[lev]) });
                        stats.AddStats(new Stats { criticalRate = new ValueP(new decimal[] { 5, 7, 8 }[lev]) });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.사멸)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2세트: '백 어택' 또는 '헤드 어택' 적중 시 치피 +65% (적중 실패 시 치피 +22%)
                        stats.AddStats(new Stats { criticalDamage = new ValueP(new decimal[] { 0, 0, 65 }[lev]) }, attackTypeList: new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택, SettingInfo.Skill.ATTACKTYPE.헤드_어택 });

                        // 4세트: 치적 +22%
                        stats.AddStats(new Stats { criticalRate = new ValueP(new decimal[] { 0, 0, 22 }[lev]) });

                        // 6세트: '백 어택' 또는 '헤드 어택' 적중 시 피해 +26% (적중 실패 시 피해 +9%)
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, 26 }[lev]) }, attackTypeList: new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택, SettingInfo.Skill.ATTACKTYPE.헤드_어택 });
                    }
                }
                else if (gear.name == SettingInfo.Gear.NAME.지배)
                {
                    if (gear.set == SettingInfo.Gear.SET.__6세트)
                    {
                        // 2세트: 각성기 피해 -10%, 각성기 쿨타임 -20%
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, -10 }[lev]) }, category: SettingInfo.Skill.CATEGORY.각성_스킬);
                        stats.AddStats(new Stats { skillCoolDown = new ValueM(-20) }, category: SettingInfo.Skill.CATEGORY.각성_스킬);

                        // 4세트: 내면의 각성 발동 시 쿨탐 -18%, 피증 31% (각성기 제외)
                        stats.AddStats(new Stats { skillCoolDown = new ValueM(-18) });
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, 31 }[lev]) });

                        stats.AddStats(new Stats { skillCoolDown = new ValueM(18) }, category: SettingInfo.Skill.CATEGORY.각성_스킬);
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, -31 }[lev]) }, category: SettingInfo.Skill.CATEGORY.각성_스킬);

                        // 6세트: 강화 발동 시 피증 20%
                        stats.AddStats(new Stats { damage = new ValueM(new decimal[] { 0, 0, 20 }[lev]) });
                    }
                }
            }
        }
     
        private void ApplyingWeaponQual(PackedStats stats)
        {
            stats.AddStats(new Stats { additionalDamage = new ValueP(setting.GetWeaponQual().additionalDamage) });
        }
     
        private void ApplyBuff(PackedStats stats)
        {
            foreach (var buff in setting.GetBuffList())
            {
                if (buff.name == SettingInfo.Buff.NAME.건슬링어_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.갈망_6세트_버프)
                {
                    stats.AddStats(new Stats { attackSpeed = new ValueP(12) });
                    stats.AddStats(new Stats { moveSpeed = new ValueP(12) });
                    stats.AddStats(new Stats { additionalDamage = new ValueP(12) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서폿_낙인)
                {
                    stats.AddStats(new Stats { damage = new ValueM(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서폿_공증)
                {
                    stats.AddStats(new Stats { attackPowerPer = new ValueM(6, ValueM.Group.AttackPowerPer_Supporter) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서폿_2버블)
                {
                    stats.AddStats(new Stats { damage = new ValueM(15) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.데빌헌터_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.아르카나_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.기상술사_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(10) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.배틀마스터_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(18) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.창술사_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(18) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.스트라이커_치적_시너지)
                {
                    stats.AddStats(new Stats { criticalRate = new ValueP(18) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.블래스터_방깍_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.디스트로이어_방깍_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.워로드_방깍_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.서머너_방깍_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.리퍼_방깍_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12 * 0.6m) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.버서커_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.호크아이_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.인파이터_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.소서리스_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.데모닉_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.스카우터_공증_시너지)
                {
                    stats.AddStats(new Stats { attackPowerPer = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.기공사_공증_시너지)
                {
                    stats.AddStats(new Stats { attackPowerPer = new ValueM(6) });
                }
                else if (buff.name == SettingInfo.Buff.NAME.워로드_백헤드_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12) }, attackTypeList: new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택, SettingInfo.Skill.ATTACKTYPE.헤드_어택 });
                }
                else if (buff.name == SettingInfo.Buff.NAME.블레이드_백헤드_피증_시너지)
                {
                    stats.AddStats(new Stats { damage = new ValueM(12) }, attackTypeList: new List<SettingInfo.Skill.ATTACKTYPE> { SettingInfo.Skill.ATTACKTYPE.백_어택, SettingInfo.Skill.ATTACKTYPE.헤드_어택 });
                }
            }
        }
    }
}
