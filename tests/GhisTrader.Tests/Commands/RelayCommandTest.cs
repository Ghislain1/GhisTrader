// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>



namespace GhisTrader.Tests.Commands;

 
using Microsoft.VisualStudio.TestTools.UnitTesting;
[TestClass]
public class RelayCommandTest
{


    [TestMethod]
    public void NonGenericDelegateCommandExecuteShouldInvokeExecuteAction()
    {
        bool executed = false;
       
 

       // Assert.True(executed);
    }

    [TestMethod]
    public void NonGenericDelegateCommandCanExecuteShouldInvokeCanExecuteFunc()
    {
        bool invoked = false;
      

    

      //  Assert.True(invoked);
      //  Assert.True(canExecute);
    }

}
