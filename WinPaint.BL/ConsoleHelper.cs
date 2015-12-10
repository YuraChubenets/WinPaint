using System;
using System.Windows.Forms;

namespace WinPaint
{
    public static class ConsoleHelper
    {
        public static void InvokeEx(this Control control, Action action)
        {
            try
            {
                if (control.InvokeRequired)
                    control.Invoke(action);
                else
                    action();
            }
            catch (ObjectDisposedException err)
            {
                Application.ExitThread();    
                          
            }
        }
    }
}