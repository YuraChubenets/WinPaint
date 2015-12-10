using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaint.BL
{
    public class Progressived
    {       
        private int _progress;

        public Progressived(){ }
        public Progressived(int progress)
        {
            _progress = progress;
        }             

        private bool _cancell = false;
        public void Cancel()
        {
            _cancell = true;
        }

        public bool WorkProgress()
        {
            for (int i = 0; i < _progress; i++)
            {
                if (_cancell)
                    break;
                Thread.Sleep(5);
                OnProgressChanged(i);          
            }
            return _cancell;
        }
        public void OnProgressChanged(int i)
        {
            if (ProcessChanged != null)
                ProcessChanged(i);
        }
        public event Action<int> ProcessChanged;      
    }
}
