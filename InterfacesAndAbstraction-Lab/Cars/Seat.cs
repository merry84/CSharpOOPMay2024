using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Seat : ICar
    {
        public Seat(string model, string color)
        {
            Model = model;
            Color = color;
        }

        public string Model { get; set; }
        public string Color { get; set; }

        public string Start()
        => "Engine start";

        public string Stop()
        => "Breaaak!";
        public override string ToString()
        {
            return $"{Color} Seat {Model}";
        }
    }
}
