using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer :Product,IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();
        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals.AsReadOnly();
        public void AddComponent(IComponent component)
        {
            if (components.Any(x=>x.GetType().Name == component.GetType().Name))
            {
                string.Format(ExceptionMessages.ExistingComponent, GetType().Name, Id);
            }
            components.Add(component);
        }
        //•	OverallPerformance – override the base functionality (if the components collection is empty,
        //it should return only the computer overall performance, otherwise return the sum of
        //the computer overall performance and the average overall performance from all components)
        public override double OverallPerformance
        {
            get
            {
                if(!components.Any()) return base.OverallPerformance;

                return OverallPerformance + components.Average((x => x.OverallPerformance));
            }
        }
        //•	Price – override the base functionality
        //(The price is equal to the total sum of the computer price with the sum of all component prices and the sum of all peripheral prices)
        public override decimal Price
        {
            get
            {
                return base.Price + components.Sum(x=>x.Price) + peripherals.Sum(x=>x.Price);
            }
        }

        public IComponent RemoveComponent(string componentType)
        {
           /*If the components collection is empty or does not have a component of that type,
            throw an ArgumentException with the message "Component {component type} does not exist in {computer type} with Id {id}."
              Otherwise, remove the component of that type and return it.
              */
           if (!components.Any(x => x.GetType().Name == componentType))
           {
               string.Format(ExceptionMessages.NotExistingComponent,componentType, GetType().Name, Id);
           }

           IComponent component = components.Find(x => x.GetType().Name == componentType);
           components.Remove(component);
           return component;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
           /*If the peripherals collection contains a peripheral with the same peripheral type,
            throw an ArgumentException with the message "Peripheral {peripheral type} already exists in {computer type} with Id {id}."
              Otherwise, add the peripheral to the peripherals collection.
              */
           if(!peripherals.Any(x=>x.GetType().Name == peripheral.GetType().Name))
           {
               string.Format(ExceptionMessages.ExistingPeripheral,peripheral.GetType().Name, GetType().Name, Id);
           }
           peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
           /*If the peripherals collection is empty or does not have a peripheral of that type,
            throw an ArgumentException with the message "Peripheral {peripheral type} does not exist in {computer type} with Id {id}."
              Otherwise, remove the peripheral of that type and return it.
              */
           if(!peripherals.Any(x=>x.GetType().Name == peripheralType))
           {
               string.Format(ExceptionMessages.NotExistingPeripheral,peripheralType,GetType().Name,Id);
           }

           IPeripheral peripheral = peripherals.Find(x => x.GetType().Name == peripheralType);
           peripherals.Remove(peripheral);
           return peripheral;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({Components.Count}):");
            foreach (var component in Components)
            {
                sb.AppendLine($"  {component.ToString()}");
            }

            sb.AppendLine($" Peripherals({ Peripherals.Count}); Average Overall Performance({Peripherals.Average(x=>x.OverallPerformance)}):");
            foreach (var peripheral in Peripherals)
            {
                sb.AppendLine($"  {peripheral.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
