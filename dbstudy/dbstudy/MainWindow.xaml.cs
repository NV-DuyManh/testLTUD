using dbstudy.DemoEntity;
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

namespace dbstudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // --- Code cũ của bạn ---
                QuanLy db = new QuanLy();

                // Dòng này thường gây lỗi nếu cấu hình sai
                db.Database.CreateIfNotExists();

                var student = new Student
                {
                    Name = "Sinh Viên A",
                    Tuoi = 22
                };

                db.Students.Add(student);
                db.SaveChanges();
                // -----------------------

                MessageBox.Show("Thành công! Đã tạo Database.");
            }
            catch (Exception ex)
            {
                // Code này sẽ hiện nguyên nhân lỗi thực sự ra màn hình
                MessageBox.Show("Lỗi chi tiết: " + ex.Message + "\n"
                                + "Lỗi bên trong: " + ex.InnerException?.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
