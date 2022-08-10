using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostarkSimulator
{

    /// <summary>
    /// 건슬링어 클래스의 데미지 계산이 구현되어 있는 클래스.
    /// </summary>
    public class Calculator
    {
        public static decimal AttackPower = 50000;  // 고정 공격력

        public static decimal CalculateDamage(CombatSkill skill)   // 대미지 계산
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

        public static decimal CalculateCooldown(CombatSkill skill)   // 쿨타임 계산
        {
            decimal cooldown = 10000000;

            foreach (var part in skill.partList)
            {
                cooldown = Math.Min(cooldown, part.appliedStats.skillCoolDown.GetValue());
            }

            return skill.cooldownTime * (1 + (cooldown / 100));
        }

        public static decimal ArithmeticMean(List<decimal> numList)  // 산술 평균
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

        public static decimal HarmonicMean(List<decimal> numList)  // 조화 평균
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