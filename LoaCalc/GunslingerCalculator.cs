using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoaCalc
{

    /// <summary>
    /// 건슬링어 클래스의 데미지 계산이 구현되어 있는 클래스.
    /// </summary>
    public class GunslingerCalculator
    {
        public class Result
        {
            public string DetailedInfo_BeforeHalf = "";
            public string DetailedInfo_AfterHalf = "";

            public decimal damageBeforeHalf = 0;
            public decimal damageAfterHalf = 0;

            public decimal damageArithmeticMean = 0;
            public decimal damageHarmonicMean = 0;

            public decimal dpsArithmeticMean = 0;
            public decimal dpsHarmonicMean = 0;

            public decimal cooldownTime = 0;
        }


        public static decimal AttackPower = 50000;  // 고정 공격력



        /* 
        로직 변경

        직업 클래스: AbstractLostArkJob
        {
            class Result
            {

            }

            List<Result> GetAllSkillDamage()
            {
                List<Result> resultList;

                for(this.스킬세팅정보)
                    Calculator.CalculateSkillDamage(combatSkill);

                return resultList;
            }

            // 최적 특성도 여기서 구현?
        }

        */

        public static List<Result> CalculateSkillDamage(Gunslinger character)
        {
            // 캐릭터 스탯 불러오기
            var characterStats = new Dictionary<string, PackedStats>
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
                    result.damageBeforeHalf = DamageFormula(skill);
                    result.cooldownTime = CooldownFormula(skill);

                    // 약무 후
                    skill = character.GetSkill(skillOption, 50, characterStats["Hp50"]);
                    result.DetailedInfo_AfterHalf = skill.GetDetailedInfo();
                    result.damageAfterHalf = DamageFormula(skill);

                    // 평균 데미지 및 DPS
                    result.damageArithmeticMean = ArithmeticMean(new List<decimal> { result.damageBeforeHalf, result.damageAfterHalf });
                    result.damageHarmonicMean = HarmonicMean(new List<decimal> { result.damageBeforeHalf, result.damageAfterHalf });
                    result.dpsArithmeticMean = result.damageArithmeticMean / result.cooldownTime;
                    result.dpsHarmonicMean = result.damageHarmonicMean / result.cooldownTime;

                    // 소수 부분 제거 (쿨타임만 소수 둘째자리 까지 표현)
                    result.damageBeforeHalf = Math.Round(result.damageBeforeHalf);
                    result.damageAfterHalf = Math.Round(result.damageAfterHalf);
                    result.damageArithmeticMean = Math.Round(result.damageArithmeticMean);
                    result.damageHarmonicMean = Math.Round(result.damageHarmonicMean);
                    result.dpsArithmeticMean = Math.Round(result.dpsArithmeticMean);
                    result.dpsHarmonicMean = Math.Round(result.dpsHarmonicMean);
                    result.cooldownTime = Math.Round(result.cooldownTime, 2);
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

            foreach (var num in numList)
            {
                if (num > 0)
                {
                    sum += 1 / num;
                    cnt++;
                }
            }

            return sum == 0 ? 0 : cnt / sum;
        }
    }
}