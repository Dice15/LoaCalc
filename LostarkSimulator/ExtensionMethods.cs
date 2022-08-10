using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostarkSimulator
{
    public static class ExtensionMethods
    {
        public static string ToStr(this Enum skill)
        {
            string res = skill.ToString();

            res = res.Replace("____", "(");
            res = res.Replace("___", ")");
            res = res.Replace("__", "");
            res = res.Replace("_", " ");

            return res;
        }

        public static int ToInt(this string str)
        {
            int.TryParse(str, out int res);
            return res;
        }

        public static decimal ToDecimal(this string str)
        {
            decimal.TryParse(str, out decimal res);
            return res;
        }
    }
}
