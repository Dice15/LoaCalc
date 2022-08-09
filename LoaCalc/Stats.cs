using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoaCalc
{

    /// <summary>
    /// 캐릭터 스탯.
    /// </summary>
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


    /// <summary>
    /// 적용된 스탯을 분류하여 저장.
    /// </summary>
    public class PackedStats
    {
        private Stats notCategorizedStats = new Stats();
        private Dictionary<SettingInfo.Skill.NAME, Stats> categorizedStatsByName = new Dictionary<SettingInfo.Skill.NAME, Stats>();
        private Dictionary<SettingInfo.Skill.CATEGORY, Stats> categorizedStatsByCategory = new Dictionary<SettingInfo.Skill.CATEGORY, Stats>();
        private Dictionary<SettingInfo.Skill.TYPE, Stats> categorizedStatsByType = new Dictionary<SettingInfo.Skill.TYPE, Stats>();
        private Dictionary<SettingInfo.Skill.ATTACKTYPE, Stats> categorizedStatsByAttackType = new Dictionary<SettingInfo.Skill.ATTACKTYPE, Stats>();
        private Dictionary<SettingInfo.Skill.CLASSENGRAVING, Stats> categorizedStatsByClassEngraving = new Dictionary<SettingInfo.Skill.CLASSENGRAVING, Stats>();


        public void AddPackedStats(PackedStats packedStats)
        {
            AddStats(packedStats.notCategorizedStats);
            foreach (var item in packedStats.categorizedStatsByName) AddStats(item.Value, name: item.Key);
            foreach (var item in packedStats.categorizedStatsByCategory) AddStats(item.Value, category: item.Key);
            foreach (var item in packedStats.categorizedStatsByType) AddStats(item.Value, type: item.Key);
            foreach (var item in packedStats.categorizedStatsByAttackType) AddStats(item.Value, attackType: item.Key);
            foreach (var item in packedStats.categorizedStatsByClassEngraving) AddStats(item.Value, classEngraving: item.Key);
        }     
      
        public void AddStats(Stats stats,
            SettingInfo.Skill.NAME? name = null,
            List<SettingInfo.Skill.NAME> nameList = null,
            SettingInfo.Skill.CATEGORY? category = null,
            List<SettingInfo.Skill.CATEGORY> categoryList = null,
            SettingInfo.Skill.TYPE? type = null,
            List<SettingInfo.Skill.TYPE> typeList = null,
            SettingInfo.Skill.ATTACKTYPE? attackType = null,
            List<SettingInfo.Skill.ATTACKTYPE> attackTypeList = null,
            SettingInfo.Skill.CLASSENGRAVING? classEngraving = null,
            List<SettingInfo.Skill.CLASSENGRAVING> classEngravingList = null)
        {
            if (name == null && nameList == null && category == null && categoryList == null && type == null && typeList == null && attackType == null && attackTypeList == null && classEngraving == null && classEngravingList == null)
            {
                notCategorizedStats.Add(stats.Copy());
            }
            else
            {
                if (name != null)
                {
                    var _name = name.GetValueOrDefault();
                    if (categorizedStatsByName.ContainsKey(_name)) categorizedStatsByName[_name].Add(stats.Copy());
                    else categorizedStatsByName.Add(_name, stats.Copy());
                }
                else if (nameList != null)
                {
                    foreach (var _name in nameList)
                    {
                        if (categorizedStatsByName.ContainsKey(_name)) categorizedStatsByName[_name].Add(stats.Copy());
                        else categorizedStatsByName.Add(_name, stats.Copy());
                    }
                }


                if (category != null)
                {
                    var _category = category.GetValueOrDefault();
                    if (categorizedStatsByCategory.ContainsKey(_category)) categorizedStatsByCategory[_category].Add(stats.Copy());
                    else categorizedStatsByCategory.Add(_category, stats.Copy());
                }
                else if (categoryList != null)
                {
                    foreach (var _category in categoryList)
                    {
                        if (categorizedStatsByCategory.ContainsKey(_category)) categorizedStatsByCategory[_category].Add(stats.Copy());
                        else categorizedStatsByCategory.Add(_category, stats.Copy());
                    }
                }


                if (type != null)
                {
                    var _type = type.GetValueOrDefault();
                    if (categorizedStatsByType.ContainsKey(_type)) categorizedStatsByType[_type].Add(stats.Copy());
                    else categorizedStatsByType.Add(_type, stats.Copy());
                }
                else if (typeList != null)
                {
                    foreach (var _type in typeList)
                    {
                        if (categorizedStatsByType.ContainsKey(_type)) categorizedStatsByType[_type].Add(stats.Copy());
                        else categorizedStatsByType.Add(_type, stats.Copy());
                    }
                }


                if (attackType != null)
                {
                    var _attackType = attackType.GetValueOrDefault();
                    if (categorizedStatsByAttackType.ContainsKey(_attackType)) categorizedStatsByAttackType[_attackType].Add(stats.Copy());
                    else categorizedStatsByAttackType.Add(_attackType, stats.Copy());
                }
                else if (attackTypeList != null)
                {
                    foreach (var _attackType in attackTypeList)
                    {
                        if (categorizedStatsByAttackType.ContainsKey(_attackType)) categorizedStatsByAttackType[_attackType].Add(stats.Copy());
                        else categorizedStatsByAttackType.Add(_attackType, stats.Copy());
                    }
                }


                if (classEngraving != null)
                {
                    var _classEngraving = classEngraving.GetValueOrDefault();
                    if (categorizedStatsByClassEngraving.ContainsKey(_classEngraving)) categorizedStatsByClassEngraving[_classEngraving].Add(stats.Copy());
                    else categorizedStatsByClassEngraving.Add(_classEngraving, stats.Copy());
                }
                else if (classEngravingList != null)
                {
                    foreach (var _classEngraving in classEngravingList)
                    {
                        if (categorizedStatsByClassEngraving.ContainsKey(_classEngraving)) categorizedStatsByClassEngraving[_classEngraving].Add(stats.Copy());
                        else categorizedStatsByClassEngraving.Add(_classEngraving, stats.Copy());
                    }
                }
            }
        }
        
        public Stats GetSkillStats(CombatSkill.Part partSkill)
        {
            Stats stats = new Stats();

            stats.Add(notCategorizedStats.Copy());

            foreach (var name in partSkill.name)
            {
                if (categorizedStatsByName.ContainsKey(name))
                    stats.Add(categorizedStatsByName[name]);
            }

            foreach (var category in partSkill.category)
            {
                if (categorizedStatsByCategory.ContainsKey(category))
                    stats.Add(categorizedStatsByCategory[category]);
            }

            foreach (var type in partSkill.type)
            {
                if (categorizedStatsByType.ContainsKey(type))
                    stats.Add(categorizedStatsByType[type]);
            }

            foreach (var attackType in partSkill.attackType)
            {
                if (categorizedStatsByAttackType.ContainsKey(attackType))
                    stats.Add(categorizedStatsByAttackType[attackType]);
            }

            foreach (var classEngraving in partSkill.classEngraving)
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
    

    public class ValueM
    {
        public enum Group { None, AttackPowerPer_Engraving, AttackPowerPer_Supporter, Damage_PeaceMakerRifleStance };
        private Dictionary<Group, decimal> groupValues = new Dictionary<Group, decimal>();

        public ValueM() { }
        public ValueM(decimal value, Group group = Group.None) { Add(value, group); }
        public ValueM(ValueM valuem) { Add(valuem); }
       
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
       
        public void clear() { groupValues.Clear(); }
      
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
        public ValueP(decimal value) { Add(value); }
        public ValueP(ValueP valueP) { Add(valueP); }
        public void Add(decimal value) { Value += value; }
        public void Add(ValueP valueP) { Add(valueP.Value); }
        public ValueP Copy() => new ValueP(Value);
        public void clear() { Value = 0; }
        public decimal GetValue() => Value;
        public new string ToString() => " - Value: " + GetValue().ToString() + Environment.NewLine;
    }

}