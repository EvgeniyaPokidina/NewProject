using DataGrid.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.Infrastructure.Commands
{
    public class GetValueFromGridCommand : Command
    {
        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public GetValueFromGridCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
