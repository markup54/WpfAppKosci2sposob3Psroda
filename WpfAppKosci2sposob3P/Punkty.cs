using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppKosci2sposob3P
{
    public class Punkty : NotifyPropertyChanged
    {
        private String _nazwa;
        public String Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }

        //public String Nazwa { get;set; } to samo co 12 do 17

        private int _liczbaPkt;
        public int LiczbaPkt
        {
            get { return _liczbaPkt; }
            set { _liczbaPkt = value;
                onPropertyChanged();
            }

        }

        private bool _czyZaznaczone;
        public bool CzyZaznaczone
        {
            get => _czyZaznaczone;
            set { _czyZaznaczone = value;
            onPropertyChanged();
            }
        }

        public Punkty(string nazwa)
        {
            Nazwa = nazwa;
            CzyZaznaczone = false;
            LiczbaPkt = 0;  
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
