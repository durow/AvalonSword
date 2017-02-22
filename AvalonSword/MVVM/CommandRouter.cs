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
        public object ViewModel { get; set; }
        public Type ViewModelType { get; set; }
        private Dictionary<string, Action> commandList = new Dictionary<string, Action>();
        private Dictionary<string, Func<bool>> checkList = new Dictionary<string, Func<bool>>();
        private Dictionary<string, Action<object>> eventList = new Dictionary<string, Action<object>>();
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

        private void InitMethodList()
        {
            foreach (var method in ViewModelType.GetMethods())
            {
                if (!method.IsPublic) continue;
                var paramNumber = method.GetParameters().Length;
                if (paramNumber > 1) continue;
                var methodName = method.Name;

                if(paramNumber == 0)
                {
                    if (methodName.EndsWith("Check") && method.ReturnType == typeof(bool))
                        AddCommandCheck(method);
                    else
                        AddCommand(method);
                }

                if (paramNumber == 1)
                    AddEvent(method);
            }
        }

        private void AddCommand(MethodInfo method)
        {
            if (commandList.ContainsKey(method.Name)) return;
            commandList[method.Name] = new Action(() =>
            {
                method.Invoke(ViewModel, new object[] { });
            });
        }

        private void AddCommandCheck(MethodInfo method)
        {
            if (checkList.ContainsKey(method.Name)) return;
            checkList[method.Name] = new Func<bool>(() =>
            {
                return (bool)method.Invoke(ViewModel, new object[] { });
            });
        }

        private void AddEvent(MethodInfo method)
        { }

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
