using System;
using System.Windows.Forms;

namespace WinPaint.BL
{
  public static  class ConsoleHelper
    {
        public static void InvokeEx(this Control control, Action action)
        {
                    if (control.InvokeRequired)
                        control.Invoke(new MethodInvoker(action));
                    else
                        action();
        }

        //public static void Enable(this Control con, bool flag)
        //{
        //    if (con != null)
        //    {
        //        foreach (Control c in con.Controls)
        //        {
        //            c.Enable(flag);
        //        }

        //        try
        //        {
        //            con.Invoke((MethodInvoker)(() => con.Enabled = flag));
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
    }
}
