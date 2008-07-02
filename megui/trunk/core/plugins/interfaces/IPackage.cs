using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using MeGUI;

namespace MeGUI.core.plugins.interfaces
{
    public interface ITool : IIDable
    {
        string Name { get;}
        void Run(MainForm info);
        System.Windows.Forms.Shortcut[] Shortcuts { get;}
    }

    public interface IOption : IIDable
    {
        string Name { get;}
        void Run(MainForm info);
        System.Windows.Forms.Shortcut[] Shortcuts { get;}
    }
}