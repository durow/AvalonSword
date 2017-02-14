/*
 * Author:durow
 * route command to action/func
 * Date:2017.02.14
 */

using System;
using System.Collections.Generic;

namespace Ayx.AvalonSword.MVVM
{
    public class CommandRouter
    {
        public object ViewModel { get; set; }
        public Type ViewModelType { get; set; }
        private Dictionary<string, Action> commandList = new Dictionary<string, Action>();
        private Dictionary<string, Func<bool>> checkList = new Dictionary<string, Func<bool>>();

        public CommandRouter(object viewmodel)
        {
            ViewModel = viewmodel;
            ViewModelType = viewmodel.GetType();
        }

        public void ExecuteCommand(string cmdName)
        {
            if (ViewModelType == null)
                throw new NullReferenceException("ViewModel can't be null!");

            if(!commandList.ContainsKey(cmdName))
                commandList[cmdName] = GetAction(cmdName);

            commandList[cmdName].Invoke();
        }

        public bool CheckCommand(string cmdName)
        {
            if (ViewModelType == null)
                throw new NullReferenceException("ViewModel can't be null!");

            if(!checkList.ContainsKey(cmdName))
                checkList[cmdName] = GetCheck(cmdName);

            return checkList[cmdName].Invoke();
        }

        private Action GetAction(string cmdName)
        {
            var method = ViewModelType.GetMethod(cmdName);
            if (method == null)
                throw new NullReferenceException($"can't find public method {cmdName} in {ViewModelType}!");

            return new Action(() =>
            {
                method.Invoke(ViewModel, new object[] { });
            });
        }

        private Func<bool> GetCheck(string cmdName)
        {
            var method = ViewModelType.GetMethod(cmdName+"Check");
            if (method == null)
                return new Func<bool>(() => true);
            else
                return new Func<bool>(() => (bool)method.Invoke(ViewModel, new object[] { }));
        }
    }
}
