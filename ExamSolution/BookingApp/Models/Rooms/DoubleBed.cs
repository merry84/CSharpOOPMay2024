using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public class DoubleBed :Room
    {
        //Has BedCapacity of 2.
        public DoubleBed() : base(2)
        {
        }
    }
}
