using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinPaint.BL;

namespace WinPaint
{
   public class MainPresenter
    {

        private readonly IWinPaint<Image> _view;
        private readonly IPaintManager _manager;
        private readonly IMessageService _messageService;

        public MainPresenter(IWinPaint<Image> view, IPaintManager manager, IMessageService messageService)
        {

        }
    }
}
