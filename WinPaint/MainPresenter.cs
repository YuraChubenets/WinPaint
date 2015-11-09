 using System;
using WinPaint.BL;

namespace WinPaint
{
  public   class MainPresenter
    {
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
