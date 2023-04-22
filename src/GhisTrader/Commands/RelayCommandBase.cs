// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>


namespace GhisTrader.Commands;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using System;

/// <summary>
/// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
/// </summary>
public abstract class RelayCommandBase : ICommand 
{

    private SynchronizationContext  synchronizationContext;
    private readonly HashSet<string> observedPropertiesExpressions = new HashSet<string>();

    /// <summary>
    /// Creates a new instance of a <see cref="DelegateCommandBase"/>, specifying both the execute action and the can execute function.
    /// </summary>
    protected RelayCommandBase()
    {
         this.synchronizationContext = SynchronizationContext.Current;
    }

    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public virtual event EventHandler CanExecuteChanged;

    /// <summary>
    /// Raises <see cref="ICommand.CanExecuteChanged"/> so every 
    /// command invoker can requery <see cref="ICommand.CanExecute"/>.
    /// </summary>
    protected virtual void OnCanExecuteChanged()
    {
        var handler = CanExecuteChanged;
        if (handler != null)
        {
            if (this.synchronizationContext != null && this.synchronizationContext != SynchronizationContext.Current)
                this.synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
            else
                handler.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Raises <see cref="CanExecuteChanged"/> so every command invoker
    /// can requery to check if the command can execute.
    /// </summary>
    /// <remarks>Note that this will trigger the execution of <see cref="CanExecuteChanged"/> once for each invoker.</remarks>
    [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
    public void RaiseCanExecuteChanged()
    {
        OnCanExecuteChanged();
    }

    void ICommand.Execute(object parameter)
    {
        Execute(parameter);
    }

    bool ICommand.CanExecute(object parameter)
    {
        return CanExecute(parameter);
    }

    /// <summary>
    /// Handle the internal invocation of <see cref="ICommand.Execute(object)"/>
    /// </summary>
    /// <param name="parameter">Command Parameter</param>
    protected abstract void Execute(object parameter);

    /// <summary>
    /// Handle the internal invocation of <see cref="ICommand.CanExecute(object)"/>
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns><see langword="true"/> if the Command Can Execute, otherwise <see langword="false" /></returns>
    protected abstract bool CanExecute(object parameter);

    /// <summary>
    /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
    /// </summary>
    /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
    /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
    protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
    {
        if (this.observedPropertiesExpressions.Contains(propertyExpression.ToString()))
        {
            throw new ArgumentException($"{propertyExpression.ToString()} is already being observed.",
                nameof(propertyExpression));
        }
        else
        {
            this.observedPropertiesExpressions.Add(propertyExpression.ToString());
            PropertyObserver.Observes(propertyExpression, RaiseCanExecuteChanged);
        }
    }

 

    

 
}
