using System.Windows.Forms;

namespace WinPaint.BL
{
 
        public interface IMessageService
        {
           string ShowError(string error);
           string ShowExclamation(string exclamation);
           string ShowMessage(string message);
        }

        public class MessageService : IMessageService
        {
            public string ShowExclamation(string exclamation)
            {
               return  "Предупреждение";
            }

            public string ShowMessage(string message)
            {
                 return   message;
            }

            public string ShowError(string error)
            {
                  return "Ошибка";
            }

        }
    }
