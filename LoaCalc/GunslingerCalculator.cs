using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoaCalc
{
    // TODO: 스킬 구현
    // TODO: 총 DPS 구하는 기능 구현 (+ 스킬 선택 기능)
    // 폼 컨트롤의 데이터가 바뀌는 즉시 자동 업데이트 기능 추가
    // TODO: 세부 정보 창에서 텍스트박스 드래그 막기

    
    /// <summary>
    /// 건슬링어 클래스의 데미지 계산이 구현되어 있는 클래스.
    /// </summary>
    public class GunslingerCalculator
    {
        public class Result
        {
            public string DetailedInfo_BeforeHalf = "";
            public string DetailedInfo_AfterHalf = "";

            public decimal Damage_BeforeHalf = 0;
            public decimal Damage_AfterHalf = 0;

            public decimal Damage_ArithmeticMean = 0;
            public decimal Damage_HarmonicMean = 0;

            public decimal Dps_ArithmeticAvg = 0;
            public decimal Dps_HarmonicAvg = 0;

            public decimal CooldownTime = 0;
        }


        public static decimal AttackPower = 50000;  // 고정 공격력


        public static List<Result> Calculate(Gunslinger character)
        {
            // 캐릭터 스탯 불러오기
            var characterStats = new Dictionary<string, CharacterStats>
            {
                { "Hp100", character.GetStats(100) },
                { "Hp50", character.GetStats(50) },
            };


            // 캐릭터 스탯을 기반으로 스킬 스탯 및 데미지 계산
            var resultList = new List<Result>();

            foreach (var skillOption in character.setting.GetSkillList())
            {
                Result result = new Result();

                if (skillOption != null)
                {
                    CombatSkill skill;

                    // 약무 전
                    skill = character.GetSkill(skillOption, 100, characterStats["Hp100"]);
                    result.DetailedInfo_BeforeHalf = skill.GetDetailedInfo();
                    result.Damage_BeforeHalf = DamageFormula(skill);
                    result.CooldownTime = CooldownFormula(skill);

                    // 약무 후
                    skill = character.GetSkill(skillOption, 50, characterStats["Hp50"]);
                    result.DetailedInfo_AfterHalf = skill.GetDetailedInfo();
                    result.Damage_AfterHalf = DamageFormula(skill);

                    // 평균 데미지 및 DPS
                    result.Damage_ArithmeticMean = ArithmeticMean(new List<decimal> { result.Damage_BeforeHalf, result.Damage_AfterHalf });
                    result.Damage_HarmonicMean = HarmonicMean(new List<decimal> { result.Damage_BeforeHalf, result.Damage_AfterHalf });
                    result.Dps_ArithmeticAvg = result.Damage_ArithmeticMean / result.CooldownTime;
                    result.Dps_HarmonicAvg = result.Damage_HarmonicMean / result.CooldownTime;

                    // 소수 둘째자리 까지만 표기
                    result.Damage_BeforeHalf = Math.Round(result.Damage_BeforeHalf, 2);
                    result.Damage_AfterHalf = Math.Round(result.Damage_AfterHalf, 2);
                    result.Damage_ArithmeticMean = Math.Round(result.Damage_ArithmeticMean, 2);
                    result.Damage_HarmonicMean = Math.Round(result.Damage_HarmonicMean, 2);
                    result.Dps_ArithmeticAvg = Math.Round(result.Dps_ArithmeticAvg, 2);
                    result.Dps_HarmonicAvg = Math.Round(result.Dps_HarmonicAvg, 2);
                    result.CooldownTime = Math.Round(result.CooldownTime, 2);
                }

                resultList.Add(result);
            }
                    
            return resultList;
        }

        private static decimal DamageFormula(CombatSkill skill)   // 대미지 공식
        {
            decimal result = 0;

            foreach(var part in skill.partList)
            {
                decimal constant = part.constant;
                decimal ratio = part.ratio;
                decimal attackPowerPer = part.appliedStats.attackPowerPer.GetValue();
                decimal criticalRate = part.appliedStats.criticalRate.GetValue() < 100 ? part.appliedStats.criticalRate.GetValue() : 100;
                decimal criticalDamage = part.appliedStats.criticalDamage.GetValue();
                decimal additionalDamage = part.appliedStats.additionalDamage.GetValue();
                decimal damage = part.appliedStats.damage.GetValue();

                // ((스킬 상수 + 최종 공격력) * 스킬 계수) * ((1 - 치적/100) + (치적/100 * 치피증/100)) * (추피 * 피증)
                decimal resultP = (constant + (AttackPower * (1 + (attackPowerPer / 100)))) * ratio;
                resultP *= (1 - (criticalRate / 100)) + ((criticalRate / 100) * (criticalDamage / 100));
                resultP *= (1 + (additionalDamage / 100)) * (1 + (damage / 100));
                result += resultP;
            }

            return result;
        }

        private static decimal CooldownFormula(CombatSkill skill)   // 쿨타임 공식
        {
            decimal cooldown = 10000000;

            foreach (var part in skill.partList)
            {
                cooldown = Math.Min(cooldown, part.appliedStats.skillCoolDown.GetValue());
            }

            return skill.cooldownTime * (1 + (cooldown / 100));
        }

        private static decimal ArithmeticMean(List<decimal> numList)  // 산술 평균
        {
            decimal sum = 0;
            decimal cnt = 0;

            foreach (var num in numList)
            {
                sum += num;
                cnt++;
            }

            return cnt == 0 ? -1 : sum / cnt;
        }

        private static decimal HarmonicMean(List<decimal> numList)  // 조화 평균
        {
            decimal sum = 0;
            decimal cnt = 0;

            foreach(var num in numList)
            {
                sum += 1 / num;
                cnt++;
            }

            return sum == 0 ? -1 : cnt / sum;
        }
    }
}