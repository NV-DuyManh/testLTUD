using System;
using System.Collections.Generic;
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
using testcodefirst;

namespace BT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuanLy db = new QuanLy();
        public MainWindow()
        {
            db.Database.CreateIfNotExists();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var kh = new KhoaHoc
            {
                MaKhoaHoc = MaKH.Text,
                TenKhoaHoc = TenKH.Text,
                ThoiGianHoc = int.Parse(TGKH.Text)
            };
            db.KhoaHocs.Add(kh);
            db.SaveChanges();
        }

        private void MaKH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
