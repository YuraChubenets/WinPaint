using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WinPaint.BL;

namespace WinPaint
{
  public   class MainPresenter
    {

        private readonly IWinPaint _view;
        private readonly IPaintManager _manager;
        private readonly IMessageService _messageService;


        private Inverter _invert;
        private Progressived _worker;       

        public MainPresenter(IWinPaint view, IPaintManager manager, IMessageService messageService)
        {
            _view = view;
            _manager = manager;
            _messageService = messageService;

            _view.CurrentItem = Item.Pencil;
            _view.InitialSetings();

            _view.ImageOpenClick += _view_ImageOpenClick;
            _view.ImageSaveClick += _view_ImageSaveClick;
            _view.ImageInvert += _view_ImageInvert;
            _view.ImageGrayscale += _view_ImageGrayscale;
            _view.ImageNewClick += _view_ImageNewClick;
        }

        private void _view_ImageNewClick(object sender, EventArgs e)
        {
            _view.InitialSetings();
        }

        private async void _view_ImageGrayscale(object sender, EventArgs e)
        {
           
            _view.InitialSetings();
            _view.SetContorlEnable(false);

            //-----
            _worker = new Progressived(200);
            _worker.ProcessChanged += _view.ShowProgress;
            //---

            //---
            _invert = new Inverter(_view.ContentImage);
            _invert.ProcessChanged += _view.SetImage;

            await Task<bool>.Factory.StartNew(_worker.WorkProgress);

            _invert = new Inverter(_view.ContentImage);
            _invert.ProcessChanged += _view.SetImage;

            bool cancelled = await Task<bool>.Factory.StartNew(_invert.WorkCrayscale);

            string message = cancelled ? "Процесс отменен" : "Процесс завершен!";
            _view.SetContorlEnable(true);
            _messageService.ShowMessage(message);
        }

        private async  void _view_ImageInvert(object sender, EventArgs e)
        {           
            _view.InitialSetings();
            _view.SetContorlEnable(false);           
  
            //-----
            _worker = new Progressived(200);
            _worker.ProcessChanged += _view.ShowProgress;
             //---
            _invert = new Inverter(_view.ContentImage);
            _invert.ProcessChanged += _view.SetImage;         
           
            await  Task<bool>.Factory.StartNew(_worker.WorkProgress);

            _invert = new Inverter(_view.ContentImage);
            _invert.ProcessChanged += _view.SetImage;

            bool cancelled = await Task<bool>.Factory.StartNew(_invert.WorkInvert);

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
                _view.SetImage((Bitmap) _manager.GetImage(filePath));
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
