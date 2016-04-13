using System;
using System.Windows.Forms;

namespace WinPaint
{
    public static class WinHelper
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
            catch (Exception e)
            {
                Application.ExitThread();
            }
        }
    }
}
