using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controler : IController
    {
        private List<IComputer> computers;
        private List<IPeripheral> peripherals;
        private List<IComponent> components;
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            /*Creates a computer with the correct type and adds it to the collection of computers.
               If a computer, with the same id, already exists in the computers collection, 
            throw an ArgumentException with the message "Computer with this id already exists."
               If the computer type is invalid, throw an ArgumentException with the message "Computer type is invalid."
               If it's successful, returns "Computer with id {id} added successfully.".
               */
            IComputer computer = computers.FirstOrDefault(x=>x.GetType().Name == computerType);
            if (computer != null)
            {
                string.Format(ExceptionMessages.ExistingComputerId);
            }

            if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else
            {
                string.Format(ExceptionMessages.InvalidComputerType);
            }
            computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price,
            double overallPerformance, string connectionType)
        {
            throw new NotImplementedException();
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            throw new NotImplementedException();
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price,
            double overallPerformance, int generation)
        {
           /*Creates a component with the correct type and adds it to the computer with that id, then adds it to the collection of components in the controller.
              If a component, with the same id, already exists in the components collection,
           throw an ArgumentException with the message "Component with this id already exists."
              If the component type is invalid, throw an ArgumentException with the message "Component type is invalid."
              If it's successful, returns "Component {component type} with id {component id} added successfully in computer with id {computer id}.".
              */
           IComponent component = components.FirstOrDefault(x => x.GetType().Name == componentType);
           if (component != null)
           {
               string.Format(ExceptionMessages.ExistingComponent);
           }
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            throw new NotImplementedException();
        }

        public string BuyComputer(int id)
        {
            throw new NotImplementedException();
        }

        public string BuyBest(decimal budget)
        {
            throw new NotImplementedException();
        }

        public string GetComputerData(int id)
        {
            throw new NotImplementedException();
        }
    }
}
