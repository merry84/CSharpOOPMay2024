using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products.Components
{
    public class SolidStateDrive : Component
    {
        //•	SolidStateDrive – multiplier is 1.20
        public SolidStateDrive(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance*1.20, generation)
        {
        }
    }
}
