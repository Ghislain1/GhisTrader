// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

 

namespace GhisTrader.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

public class RelayCommand : RelayCommandBase
{
    Action executeMethod;
    Func<bool> canExecuteMethod;

    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Creates a new instance of <see cref="DelegateCommand"/> with the <see cref="Action"/> to invoke on execution.
    /// </summary>
    /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute(object)"/> is called.</param>
    public RelayCommand(Action executeMethod)
        : this(executeMethod, () => true)
    {

    }

    /// <summary>
    /// Creates a new instance of <see cref="DelegateCommand"/> with the <see cref="Action"/> to invoke on execution
    /// and a <see langword="Func" /> to query for determining if the command can execute.
    /// </summary>
    /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
    /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
    public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        : base()
    {
        if (executeMethod == null || canExecuteMethod == null)
        {
            throw new ArgumentNullException(nameof(executeMethod));
        }


        this.executeMethod = executeMethod;
        this.canExecuteMethod = canExecuteMethod;
    }

    ///<summary>
    /// Executes the command.
    ///</summary>
    public void Execute()
    {
        this.executeMethod();
    }

    /// <summary>
    /// Determines if the command can be executed.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the command can execute,otherwise returns <see langword="false"/>.</returns>
    public bool CanExecute()
    {
        return this.canExecuteMethod();
    }

    /// <summary>
    /// Handle the internal invocation of <see cref="ICommand.Execute(object)"/>
    /// </summary>
    /// <param name="parameter">Command Parameter</param>
    protected override void Execute(object parameter)
    {
        this.Execute();
    }

    /// <summary>
    /// Handle the internal invocation of <see cref="ICommand.CanExecute(object)"/>
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns><see langword="true"/> if the Command Can Execute, otherwise <see langword="false" /></returns>
    protected override bool CanExecute(object parameter)
    {
        return CanExecute();
    }

    /// <summary>
    /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
    /// </summary>
    /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
    /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
    /// <returns>The current instance of DelegateCommand</returns>
    public RelayCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
    {
        this.ObservesPropertyInternal(propertyExpression);
        return this;
    }

    /// <summary>
    /// Observes a property that is used to determine if this command can execute, and if it implements INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
    /// </summary>
    /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
    /// <returns>The current instance of DelegateCommand</returns>
    public RelayCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
    {
       this.canExecuteMethod = canExecuteExpression.Compile();
        ObservesPropertyInternal(canExecuteExpression);
        return this;
    }


}

