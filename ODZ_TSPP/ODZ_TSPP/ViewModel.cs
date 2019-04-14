using System;
using System.Collections.Generic;
using ODZ_TSPP.Commands;

namespace ODZ_TSPP
{
    public class ViewModel
    {
        private Model context;
        private View _view;

        private Dictionary<string, ICommandButton> commands;

        public ViewModel(View view)
        {
            context = new Model();
            _view = view;

            commands = new Dictionary<string, ICommandButton>();
            commands.Add(Constants.SHOW_COMMAND, new ShowCommand(context, _view));
            commands.Add(Constants.REMOVE_COMMAND, new RemoveCommand(context, _view));
            commands.Add(Constants.ADD_COMMAND, new AddCommand(context, _view));
            commands.Add(Constants.CLEAR_COMMAND, new ClearCommand(context, _view));

            commands.Add(Constants.FIND_COMMAND, new FindByConfines(context, _view));
            commands.Add(Constants.PRINT_COMMAND, new PrintToWord(context, _view));
        }

        public ICommandButton GetCommandByName(string value)
        {
            if (commands.ContainsKey(value)) {
                return commands[value];
            }
            throw new System.Exception(String.Format("Don't exists such command as {0}", value));
        }
    }
}
