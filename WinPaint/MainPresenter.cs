using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using WinPaint.BL;

namespace WinPaint
{
  public   class MainPresenter
    {

        private readonly IWinPaint _view;
        private readonly IPaintManager _manager;
        private readonly IMessageService _messageService;

        private Image _currentImage;
        Inverter _invert;

        public MainPresenter(IWinPaint view, IPaintManager manager, IMessageService messageService)
        {
            _view = view;
            _manager = manager;
            _messageService = messageService;

            _view.CurrentItem = Item.Pencil;

            _view.ImageOpenClick += _view_ImageOpenClick;
            _view.ImageSaveClick += _view_ImageSaveClick;
            _view.ImageChanged += _view_ImageChanged;
        }

        private async  void _view_ImageChanged(object sender, EventArgs e)
        {
            _currentImage = _view.ContentImage;
            _view.SetContorlEnable(false);
            _view.SetMassPoint();
            _invert = new Inverter(_currentImage);
            _invert.ProcessChanged += _view.SetImage;   
            
                    
             bool cancelled =  await Task<bool>.Factory.StartNew(_invert.Work);

            string message = cancelled ? "Процесс отменен" : "Процесс завершен!";
            _view.SetContorlEnable(true);
            _messageService.ShowMessage(message);
        }

       
        private void _view_ImageOpenClick(object sender, System.EventArgs e)
        {
            try
            {
                string filePath = _view.ImagePath;
                bool isExist = _manager.IsExist(filePath);
                if (!isExist)
                {
                    _messageService.ShowExclamation("Выбранный файл не существует");
                    return;
                }
                _currentImage = _manager.GetImage(filePath);
                _view.SetImage((Bitmap)_currentImage);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        private void _view_ImageSaveClick(object sender, System.EventArgs e)
        {
            try
            {
                _manager.GetImagePath = _view.ImagePath;                                              
                _manager.SaveImage(_view.ContentImage);
                _messageService.ShowMessage("Файл успешно сохранён");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

    }
}
