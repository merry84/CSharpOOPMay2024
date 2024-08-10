using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Submarine : Vessel,ISubmarine
    {
        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, 200)
        {
            SubmergeMode = false;
        }
        public bool SubmergeMode { get; private set; }
        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;
            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
        }
        public override void RepairVessel()
        {
            if (ArmorThickness < 200)
            {
                ArmorThickness = 200;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            string submergeModeStatus = SubmergeMode ? "ON" : "OFF";
            sb.AppendLine($" *Submerge mode: {submergeModeStatus}");
            return sb.ToString().TrimEnd();
        }
    }

}
