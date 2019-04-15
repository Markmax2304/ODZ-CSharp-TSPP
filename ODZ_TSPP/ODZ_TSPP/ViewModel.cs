using System;
using System.Collections.Generic;
using ODZ_TSPP.Commands;

namespace ODZ_TSPP
{
    public class ViewModel
    {
        public Model Context { get; private set; }
        private View _view;

        private Dictionary<string, ICommandButton> commands;

        public ViewModel(View view)
        {
            Context = new Model();
            _view = view;

            commands = new Dictionary<string, ICommandButton>();
            commands.Add(Constants.SHOW_COMMAND, new ShowCommand(Context, _view));
            commands.Add(Constants.REMOVE_COMMAND, new RemoveCommand(Context, _view));
            commands.Add(Constants.ADD_COMMAND, new AddCommand(Context, _view));
            commands.Add(Constants.CLEAR_COMMAND, new ClearCommand(Context, _view));

            commands.Add(Constants.FIND_COMMAND, new FindByConfines(Context, _view));
            commands.Add(Constants.CHEAPEST_COMMAND, new FindCheapestCommand(Context, _view));
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
