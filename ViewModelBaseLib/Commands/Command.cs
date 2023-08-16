using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBaseLib.Commands
{
    public class Command : CommandsBase.CommandBase
    {

        private readonly Action<object> execute;

        private readonly Func<object, bool> canexecute;
        public override bool CanExecute(object parameter) =>
            canexecute?.Invoke(parameter) ?? true;


        public override void Execute(object parameter) =>
            execute?.Invoke(parameter);
       
        public Command(Func<object, bool> CanExecute, Action<object>Execute)
        {
            execute = Execute;

            canexecute = CanExecute;
        }
    }
}
