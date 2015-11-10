using System;
using System.Drawing;
using System.Threading.Tasks;
using WinPaint.BL;

namespace WinPaint
{
  public   class MainPresenter
    {
        private Inverter _inverter;
        private readonly IWinPaint _view;
        private readonly IPaintManager _manager;
        private readonly IMessageService _messageService;

        public MainPresenter(IWinPaint view, IPaintManager manager, IMessageService messageService)
        {
            _view = view;
            _manager = manager;
            _messageService = messageService;

            _view.ImageOpenClick += _view_ImageOpenClick;
            _view.ImageSaveClick += _view_ImageSaveClick;
            _view.ImageChanged += _view_ImageChanged;
            _view.ImageProcessInvert += _view_ImageProcessInvert;
                
        }

        private async void _view_ImageProcessInvert(object sender, EventArgs e)
        {
            Image image = _view.Image;
            _inverter.ProcessChanged += _manager.GetInvert(image);
            
     
            bool cancelled = await Task<bool>.Factory.StartNew(_inverter.Work);

            string message = cancelled ? "Процесс отмнен" : "Процесс завершен!";
            _messageService.ShowMessage(message);
        }


        private void _view_ImageChanged(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void _view_ImageSaveClick(object sender, System.EventArgs e)
        {
            try
            {
                var content = _view.Image;
                _manager.SaveImage(content, _view.GetImagePath);
                _messageService.ShowMessage("Файл успешно сохранён");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        private void _view_ImageOpenClick(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
