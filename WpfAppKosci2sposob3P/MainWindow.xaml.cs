using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppKosci2sposob3P
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Kosc> rezultaty { get; set; }
        public ObservableCollection<Punkty> punkty { get; set; }
        public int LiczbaKosci { get; set; }
        public int LiczbaProb { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            rezultaty = new ObservableCollection<Kosc>();
            DataContext = this; // żeby działało bindowanie do XAML
            LiczbaKosci = 5;
            przygotujGre();


        }
        private void przygotujGre()
        {
            punkty = new ObservableCollection<Punkty>();
            punkty.Add(new Punkty("jedynki"));
            punkty.Add(new Punkty("dwójki"));
            punkty.Add(new Punkty("trójki"));
            punkty.Add(new Punkty("czwórki"));
            punkty.Add(new Punkty("piątki"));
            punkty.Add(new Punkty("szóstki"));
            punkty.Add(new Punkty("para"));
            punkty.Add(new Punkty("dwie pary"));
            punkty.Add(new Punkty("trzy jednakowe"));
            punkty.Add(new Punkty("full"));
            punkty.Add(new Punkty("kareta"));
            punkty.Add(new Punkty("poker"));
            punkty.Add(new Punkty("mały strit"));
            punkty.Add(new Punkty("duży strit"));
            punkty.Add(new Punkty("szansa"));
            LiczbaProb = 3;
        }

        private int sumaWszystkich()
        {
            //szansa
            int suma = 0;
            foreach (Kosc k in rezultaty)
            {
                suma = suma + k.Wartosc;
            }
            return suma;
        }

        private int takieSame(int ile)
        {
            int licznik ;
            for(int i = 6; i>0; i--)
            {
                licznik = 0;
               foreach(Kosc k in rezultaty)
                {
                    if(k.Wartosc == i)
                    {
                        licznik++;
                    }
                }
               if(licznik >= ile)
                {
                    return ile * i;
                }
            }
            return 0;
        }
        private int szkola(int numer)
        {
            int ile = 0;
            foreach(Kosc k in rezultaty)
            {
                if(k.Wartosc == numer)
                {
                    ile++;
                }
            }

            int s = ile * numer - 3 * numer;

            return s;
        }

        private int dwiePary()
        {
            int para = takieSame(2);
            if (takieSame(2) > 0)
            {
                if (takieSame(4) > 0)
                {
                    return takieSame(4);
                }
                List<int> Pozostale = new List<int>();
                foreach(Kosc k in rezultaty)
                {
                    if(k.Wartosc == para / 2)
                    {
                        continue;
                    }
                    Pozostale.Add(k.Wartosc);
                }
                if (Pozostale[0] == Pozostale[1])
                {
                    return Pozostale[0] * 2 + para;
                }
                if (Pozostale.Count > 2)
                {
                    if (Pozostale[1] == Pozostale[2])
                    {
                        return Pozostale[1] * 2 + para;
                    }
                    if (Pozostale[0] == Pozostale[2])
                    {
                        return Pozostale[0] * 2 + para;
                    }
                }
            }
            return 0;
        }
        private void pokazPunkty()
        {
            for(int i = 0; i < 6; i++)
            {
                if (!punkty[i].CzyZaznaczone)
                {
                    punkty[i].LiczbaPkt = szkola(i+1);
                }
                
            }
            if (!punkty[14].CzyZaznaczone)
            {
                punkty[14].LiczbaPkt = sumaWszystkich();
            }
            if (!punkty[6].CzyZaznaczone)
            {
                punkty[6].LiczbaPkt = takieSame(2);
            }
            if (!punkty[7].CzyZaznaczone)
            {
                punkty[7].LiczbaPkt = dwiePary();
            }
            if (!punkty[8].CzyZaznaczone)
            {
                punkty[8].LiczbaPkt = takieSame(3);
            }
            if (!punkty[10].CzyZaznaczone)
            {
                punkty[10].LiczbaPkt = takieSame(4);
            }
            if (!punkty[11].CzyZaznaczone)
            {
                punkty[11].LiczbaPkt = takieSame(5);
            }
        }

        private void rzuc_btn_Click(object sender, RoutedEventArgs e)
        {
           if(rezultaty.Count ==0) {
                for (int i = 0; i < LiczbaKosci; i++)
                {
                    rezultaty.Add(new Kosc());
                }

            }
            Random random = new Random();
            foreach (Kosc k in rezultaty)
            {
                if (k.CzyZaznaczona == false)

                    k.Wartosc = random.Next(1, 7);
            }
            pokazPunkty();
        }

        private void wyczysc_btn_Click(object sender, RoutedEventArgs e)
        {
            rezultaty.Clear();
            for(int i =0;i<LiczbaKosci;i++)
            {
                rezultaty.Add(new Kosc());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Kosc kosc = (Kosc)button.DataContext;
            kosc.CzyZaznaczona = !kosc.CzyZaznaczona;
        }
    }
}
