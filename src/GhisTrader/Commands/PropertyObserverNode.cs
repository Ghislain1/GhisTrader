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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents each node of nested properties expression and takes care of 
/// subscribing/unsubscribing INotifyPropertyChanged.PropertyChanged listeners on it.
/// </summary>
internal class PropertyObserverNode
{
    private readonly Action action;
    private INotifyPropertyChanged inpcObject;

    public PropertyInfo? PropertyInfo { get; }
    public PropertyObserverNode Next { get; set; }

    public PropertyObserverNode(PropertyInfo propertyInfo, Action action)
    {
        PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        this.action = () =>
        {
            action?.Invoke();
            if (Next == null) return;
            Next.UnsubscribeListener();
            GenerateNextNode();
        };
    }

    public void SubscribeListenerFor(INotifyPropertyChanged inpcObject)
    {
        this.inpcObject = inpcObject;
        this.inpcObject.PropertyChanged += this.OnPropertyChanged;

        if (Next != null) GenerateNextNode();
    }

    private void GenerateNextNode()
    {
        var nextProperty = PropertyInfo.GetValue(this.inpcObject);
        if (nextProperty == null) return;
        if (!(nextProperty is INotifyPropertyChanged nextInpcObject))
            throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that " +
                                                $"owns '{Next.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");

        Next.SubscribeListenerFor(nextInpcObject);
    }

    private void UnsubscribeListener()
    {
        if (this.inpcObject != null)
            this.inpcObject.PropertyChanged -= OnPropertyChanged;

        Next?.UnsubscribeListener();
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e?.PropertyName == PropertyInfo.Name || string.IsNullOrEmpty(e?.PropertyName))
        {
            this.action?.Invoke();
        }
    }
}