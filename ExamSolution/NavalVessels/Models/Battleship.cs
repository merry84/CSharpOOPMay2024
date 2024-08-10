using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Battleship : Vessel ,IBattleship
    {
        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, 300)
        {
            SonarMode = false;
        }
        public bool SonarMode { get; private set; }
        public void ToggleSonarMode()
        {
            /*Flips SonarMode (false -> true or true -> false). 
            When SonarMode is activated (false -> true):
            •	The main weapon caliber is increased by 40 points
            •	Speed is decreased by 5 points
            When SonarMode is deactivated (true -> false):
            •	The main weapon caliber is decreased by 40 points
            •	Speed is increased by 5 points
            */
            SonarMode = !SonarMode;
            if (SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed -= 5;
            }
        }
        public override void RepairVessel()
        {
            if (ArmorThickness < 300)
            {
                ArmorThickness = 300;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine( base.ToString());
            //" *Sonar mode: {ON/OFF}"
            string sonarMode = SonarMode ? "ON" : "OFF"; ;
            sb.AppendLine($" *Sonar mode: {sonarMode}");  
           
            return sb.ToString().TrimEnd();
        }
    }
}
