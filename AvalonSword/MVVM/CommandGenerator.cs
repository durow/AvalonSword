/*
 * Author:durow
 * generate command
 * Date:2017.02.14
 */

using System;
using System.Collections.Generic;

namespace Ayx.AvalonSword.MVVM
{
    public class CommandGenerator
    {
        private Dictionary<string, AyxCommand> commandList = new Dictionary<string, AyxCommand>();

        public AyxCommand GetCmd(Action<object> action)
        {
            var key = action.Method.ToString();
            if (!commandList.ContainsKey(key))
                commandList[key] = new AyxCommand(action);

            return commandList[key];
        }
    }
}
