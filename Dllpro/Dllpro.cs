using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLpro
{
    public static class Metod
    {
        public static double AgeAVG(DateTime[] Massive)
        {
            double age;
            double avg = 0;
            foreach (DateTime BirthDay in Massive)
            {
                age = (DateTime.Now - BirthDay).TotalDays;
                avg += age / 365;
            }

            avg = Math.Round(avg / Massive.Length, 2);
            return avg;
        }
        public static List<string> metodnmsr(List<string> name, string foundname)
        {
            name = name.Where(x => x.Contains(foundname)).ToList();
            return name;
        }

    }

}

