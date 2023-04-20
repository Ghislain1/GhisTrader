// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class RelayCommand : ICommand
    {
        private Predicate<object?>? canExecutePredicate;
        private Action<object> executeAction;
        private IDictionary<EventHandler, Dispatcher> eventHandlers;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.eventHandlers.Add(value, Dispatcher.CurrentDispatcher);
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                this.eventHandlers.Remove(value);
            }
        }
        public RelayCommand(Action<object> execute, Predicate<object?>? canExecute = null)
        {
            this.eventHandlers = (IDictionary<EventHandler, Dispatcher>)new Dictionary<EventHandler, Dispatcher>();
            this.executeAction = execute;
            this.canExecutePredicate = canExecute;

        }


        public bool CanExecute(object? parameter)=> this.canExecutePredicate is not null && this.canExecutePredicate(parameter);    
        

        public void Execute(object? parameter) => this.executeAction(parameter);

    }
}
