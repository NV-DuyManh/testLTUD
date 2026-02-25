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

namespace testcodefirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            QuanLy db = new QuanLy();
            var student = new KhoaHoc
            {
                MaKhoaHoc = "CS445",
                TenKhoaHoc = "System Integration",
                ThoiGianHoc = 2
            };
            var hocvien = new HocVien
            {
                MaHocVien = "29211164747",
                TenHocVien = "Phan Quang Hieu",
                Email = "phanquanghieusuper@gmail.com"
            };
            db.KhoaHocs.Add(student);
            db.HocViens.Add(hocvien);
            db.SaveChanges();
            var dangKy = new DangKyHoc
            {
                HocVienId = hocvien.Id,
                KhoaHocId = student.Id,
                NgayDangKy = DateTime.Now
            };
            db.DangKyHocs.Add(dangKy);
            db.SaveChanges();
        }
    }
}
