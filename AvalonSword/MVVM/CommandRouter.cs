/*
 * Author:durow
 * route command to action/func
 * Date:2017.02.14
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ayx.AvalonSword.MVVM
{
    public class CommandRouter
    {
        private object viewmodel;
        private Type vmType;
        private Dictionary<string, Action> commandList = new Dictionary<string, Action>();
        private Dictionary<string, Func<bool>> checkList = new Dictionary<string, Func<bool>>();
        private Dictionary<string, Action<object>> eventList = new Dictionary<string, Action<object>>();
        public CommandRouter(object viewmodel)
        {
            this.viewmodel = viewmodel;
            vmType = viewmodel.GetType();
        }

        public void ExecuteCommand(string cmdName)
        {
            if (vmType == null)
                throw new NullReferenceException("ViewModel can't be null!");

            if(!commandList.ContainsKey(cmdName))
                commandList[cmdName] = GetAction(cmdName);

            commandList[cmdName].Invoke();
        }

        public bool CheckCommand(string cmdName)
        {
            if (vmType == null)
                throw new NullReferenceException("ViewModel can't be null!");

            if(!checkList.ContainsKey(cmdName))
                checkList[cmdName] = GetCheck(cmdName);

            return checkList[cmdName].Invoke();
        }

        private Action GetAction(string cmdName)
        {
            var method = vmType.GetMethod(cmdName);
            if (method == null)
                throw new NullReferenceException($"can't find public method {cmdName} in {vmType}!");

            return new Action(() =>
            {
                method.Invoke(viewmodel, new object[] { });
            });
        }

        private Func<bool> GetCheck(string cmdName)
        {
            var method = vmType.GetMethod(cmdName+"Check");
            if (method == null)
                return new Func<bool>(() => true);
            else
                return new Func<bool>(() => (bool)method.Invoke(viewmodel, new object[] { }));
        }
    }
}
