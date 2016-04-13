using System;
using System.Windows.Forms;
using WinPaint.BL;

namespace WinPaint
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            MainForm form = new MainForm();
            MessageService service = new MessageService();
            PaintManager manager = new PaintManager();

            MainPresenter presenter = new MainPresenter(form, manager, service);

            Application.Run(form);
        }
    }
}
