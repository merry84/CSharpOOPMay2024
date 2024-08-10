using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private List<string> targets;
        private double armorThickness;
        private ICaptain captain;
        private double speed;
        private double mainWeaponCaliber;

        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            
            targets = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.InvalidVesselName);
                }
                name = value;
            }
        }

        public ICaptain Captain
        {
            get => captain;
             set
            {
                if(captain == null)
                {
                    string.Format(ExceptionMessages.InvalidCaptainName);
                }
                captain = value;
            }
        }

        public double ArmorThickness
        {
            get => armorThickness;
            set
            {
                if (value < 0)
                {
                    armorThickness = 0;
                }

                armorThickness = value;
            } 
        }

        public double MainWeaponCaliber
        {
            get => mainWeaponCaliber;
            protected set
            {
                mainWeaponCaliber = value;
            }
        }

        public double Speed { get; protected set; }

        public ICollection<string> Targets => targets.AsReadOnly();

        public virtual void Attack(IVessel target)
        {
            /*If the target (defending vessel) is null throw NullReferenceException with the message
             * "Target cannot be null."
            When the attacking vessel attacks the target vessel, 
             the target's armor thickness points are reduced by the attacking vessel's main weapon caliber points. 
            Keep in mind that the target's armor thickness points can not go below zero.
            If the target's armor thickness points become a negative number, set it to zero. 
            Add the name of the target vessel to the attacker's list of targets.
            */
            if(target == null)
            {
                string.Format(ExceptionMessages.InvalidTarget);
            }
            target.ArmorThickness -= mainWeaponCaliber;
                
            targets.Add(target.Name);
        }

        public virtual void RepairVessel()
        {
            //Set the vessel’s initial armor thickness to the default value based on the vessel type.
         
        }
        public override string ToString()
        {
            /*Returns a string with information about each vessel. The returned string must be in the following format:
            "- {vessel name}
             *Type: {vessel type name}
             *Armor thickness: {vessel armor thickness points}
             *Main weapon caliber: {vessel main weapon caliber points}
             *Speed: {vessel speed points} knots
             *Targets: " – if there are no targets "None" Otherwise print "{target1}, {target2}, {target3}, {targetN}"
            */
            var sb = new StringBuilder();
            sb.AppendLine($"- {Name}");
            sb.AppendLine($" *Type: {GetType().Name}");
            sb.AppendLine($" *Armor thickness: {ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {Speed} knots");
            sb.Append($" *Targets: ");
            if(targets.Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                sb.AppendLine($"{string.Join(", ",targets)}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
