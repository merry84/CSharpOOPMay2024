using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class Route : IRoute
    {
        private string startPoint;
        private string endPoint;
        private double lenght;
        private int routeId;
        private bool isLocked;

        public Route(string startPoint, string endPoint, double length, int routeId)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Length = length;
            this.routeId = routeId;
            this.isLocked = false;
        }

        public string StartPoint
        {
            get
            =>startPoint;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.StartPointNull);
                }
                startPoint = value;
            }
        }

        public string EndPoint
        {
            get
            => endPoint;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                string.Format (ExceptionMessages.EndPointNull);
                endPoint = value;
            }
        }

        public double Length
        {
            get
            =>lenght;
            private set
            {
                if(value < 1)
                {
                    string.Format(ExceptionMessages.RouteLengthLessThanOne);
                }
                lenght = value;
            }
        }

        public int RouteId => routeId;

        public bool IsLocked => isLocked;

        public void LockRoute()
        {
            isLocked = true;
        }
    }
}
