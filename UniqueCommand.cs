using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastonIF
{
    public class UniqueCommand
    {
        private string _command;
        private string _result;
        // _actsOn tells us the value that this command can act, input by the user
        private string[] _actsOn;

        public UniqueCommand(string pCommand, string[] pActsOn, string pResult)
        {
            _command = pCommand;
            _result = pResult;
            _actsOn = pActsOn;
        }

        public string Command
        {
            get { return _command; }
        }

        public string Result
        {
            get { return _result; }
        }

        public string[] ActsOn
        {
            get { return _actsOn; }
        }

    }
}
