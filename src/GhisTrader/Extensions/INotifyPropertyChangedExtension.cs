// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Extensions;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
// To Create any VM
public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : INotifyPropertyChanged;
public delegate void NotifyPropertyChangedHandler(string propertyName); // for many Properties to notiy
public static class INotifyPropertyChangedExtension
{
    public static bool InvokePropertyChanged<TSender, TProperty>(this TSender sender, PropertyChangedEventHandler ?@event, ref TProperty old, TProperty @new, [CallerMemberName] string propertyName = "") where TSender : INotifyPropertyChanged
    {
        Func<(TProperty, TProperty), bool> equalsFunc = (Func<(TProperty, TProperty), bool>)(values => object.Equals((object)values.Item1, objB: (object)values.Item2));
        return sender.InvokePropertyChanged<TSender, TProperty>(@event, ref old, @new, equalsFunc, propertyName);
    }

    public static void InvokePropertiesChanged<TSender>(this TSender sender, NotifyPropertyChangedHandler handler, params string[] propertyNames)where TSender : INotifyPropertyChanged
    {
        NotifyPropertyChangedHandler notifyPropertyChnagedHandler = handler;
        if(handler is null)
        {
            return;
        }
        propertyNames?.ToList().ForEach(item => handler(item));
       
    }
    public static void InvokePropertyChanged<TSender>(this TSender sender, PropertyChangedEventHandler ?handler, [CallerMemberName]string  propertyName="") where TSender : INotifyPropertyChanged
    {
     
        if (handler is null)
        {
            return;
        }
      handler((object)sender, new PropertyChangedEventArgs(propertyName));

    }
    public static void InvokePropertyChanged<TSender>(this TSender sender, NotifyPropertyChangedHandler handler,  string propertyName) where TSender : INotifyPropertyChanged
    {
        NotifyPropertyChangedHandler notifyPropertyChnagedHandler = handler;
        if (handler is null)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return;
        }
        notifyPropertyChnagedHandler(propertyName);

    }

    public static bool InvokePropertyChanged<TSender, TProperty>(this TSender sender, PropertyChangedEventHandler? @event, ref TProperty old, TProperty @new, Func<(TProperty, TProperty), bool> areEqual, [CallerMemberName] string propertyName = "") where TSender : INotifyPropertyChanged
    {
        if (areEqual is null)
        {
            throw new ArgumentNullException(nameof(propertyName));
        }
       
        int num = !areEqual((old, @new)) ? 1 : 0;
        if (num == 0)
        {
            return num != 0;
        }
        old = @new;
        if (@event is null)
        {
            return num != 0;
        }
        // TODO@GZe - Difficult to read
        @event((object)sender, new PropertyChangedEventArgs(propertyName));
        return num != 0;
    }


}

