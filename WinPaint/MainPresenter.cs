using System;
using System.Drawing;
using System.IO;
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

        private string _currentFilePath;

        public MainPresenter(IWinPaint view, IPaintManager manager, IMessageService messageService)
        {
            _view = view;
            _manager = manager;
            _messageService = messageService;

            _view.ImageOpenClick += _view_ImageOpenClick;
            _view.ImageSaveClick += _view_ImageSaveClick;
           
                
        }

        private void _view_ImageChanged(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
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
                _currentFilePath = filePath;
                _view.Image = (Bitmap)_manager.GetImage(filePath);
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
                var content = _view.Image;
                _manager.GetImagePath = _view.ImagePath;
                _manager.SaveImage(content);
                _messageService.ShowMessage("Файл успешно сохранён");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

    }
}
