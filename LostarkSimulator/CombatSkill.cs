using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostarkSimulator
{
    /// <summary>
    /// 전투 스킬 클래스. 스킬의 구성 요소인 '스킬 이름, 쿨타임, 부분 스킬(구성 요소), 스킬 옵션, 스킬 계수, 적용된 스탯'을 담고 있다.
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
}
