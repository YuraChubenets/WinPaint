using System;
using System.Windows.Forms;

namespace WinPaint.BL
{
    public interface IMessageService
    {
        void ShowError(string error);
        void ShowExclamation(string exclamation);
        void ShowMessage(string message);
    }

    public  class MessageService : IMessageService
    {
        public void ShowExclamation(string exclamation)
        {
            MessageBox.Show(exclamation, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
