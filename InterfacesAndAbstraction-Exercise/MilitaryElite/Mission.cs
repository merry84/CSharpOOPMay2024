using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Mission:IMission
    {
        public Mission(string codeName, string state)
        {
            this.CodeName = codeName;
            this.State = state;
        }

        //mission holds a code name and a state (inProgress or Finished). A Mission can be finished through the method CompleteMission().
        public string CodeName { get; private set; }
        public string State { get; private set; }
        public void CompleteMission()
        {
            if (State == "inProgress")
            {
                State = "Finished";

            }
        }
        public override string ToString()
        => $"{CodeName} - {State}";

    }
}
