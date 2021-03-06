﻿using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.ViewModels
{
    public class TestViewModel:ViewModelBase
    {
        public AyxCommand CmdClose
        {
            get
            {
                return CmdGenerator.GetCmd(o =>
                {
                    CloseViewAsWindow();
                });
            }
        }
    }
}
