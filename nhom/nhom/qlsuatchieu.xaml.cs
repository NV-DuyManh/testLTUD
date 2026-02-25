using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace nhom
{
    public partial class qlsuatchieu : Window
    {
        RapEntities db = new RapEntities();
        ObservableCollection<lichchieu> dsHienThi = new ObservableCollection<lichchieu>();

        public qlsuatchieu()
        {
            InitializeComponent();
            LoadData();
            LoadComboBoxes();
        }

        private void LoadData()
        {
            try
            {
                var data = db.lichchieus.Include("phim").Include("phongchieu").OrderByDescending(x => x.ma_lich_chieu).ToList();
                dsHienThi = new ObservableCollection<lichchieu>(data);
                dtg_suat_chieu.ItemsSource = dsHienThi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                cmb_Phim.ItemsSource = db.phims.ToList();
                cmb_Phim.DisplayMemberPath = "ten_phim";
                cmb_Phim.SelectedValuePath = "ma_phim";

                cmb_Phong.ItemsSource = db.phongchieus.ToList();
                cmb_Phong.DisplayMemberPath = "ten_phong";
                cmb_Phong.SelectedValuePath = "ma_phong";

                dp_NgayChieu.SelectedDate = DateTime.Now;
            }
            catch { }
        }

        private void btn_hien_form_them_Click(object sender, RoutedEventArgs e)
        {
            pnl_NhapLieu.Visibility = Visibility.Visible;
            if (cmb_Phim.Items.Count > 0) cmb_Phim.SelectedIndex = 0;
            if (cmb_Phong.Items.Count > 0) cmb_Phong.SelectedIndex = 0;
            txt_ket_qua.Text = "Đang tạo suất chiếu mới...";
        }

        private void btn_HuyNhap_Click(object sender, RoutedEventArgs e)
        {
            pnl_NhapLieu.Visibility = Visibility.Collapsed;
            txt_ket_qua.Text = "Đã đóng.";
        }

        private void FastEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ThucHienLuuNhanh();
        }

        private void btn_LuuNhanh_Click(object sender, RoutedEventArgs e)
        {
            ThucHienLuuNhanh();
        }

        private void ThucHienLuuNhanh()
        {
            if (cmb_Phim.SelectedValue == null || cmb_Phong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Phim và Phòng chiếu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                TimeSpan gioChieu;
                if (!TimeSpan.TryParse(txt_GioChieu.Text, out gioChieu))
                {
                    MessageBox.Show("Định dạng giờ chiếu không hợp lệ. Vui lòng nhập kiểu HH:mm (VD: 19:30).", "Lỗi nhập liệu");
                    return;
                }

                decimal giaVe = 0;
                decimal.TryParse(txt_GiaVe.Text, out giaVe);

                var lichMoi = new lichchieu()
                {
                    ma_phim = (int)cmb_Phim.SelectedValue,
                    ma_phong = (int)cmb_Phong.SelectedValue,
                    ngay_chieu = dp_NgayChieu.SelectedDate ?? DateTime.Now.Date,
                    gio_bat_dau = gioChieu,
                    gia_ve_co_ban = giaVe,
                    nguoi_lap_lich = 1
                };

                db.lichchieus.Add(lichMoi);
                db.SaveChanges();

                LoadData();

                txt_ket_qua.Text = $"✅ Đã tạo suất chiếu lúc {DateTime.Now.ToString("HH:mm:ss")}";
            }
            catch (Exception ex)
            {
                string loi = ex.Message;
                if (ex.InnerException != null) loi += "\n" + ex.InnerException.Message;
                MessageBox.Show("Cơ sở dữ liệu báo lỗi:\n" + loi, "Lỗi Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_tim_kiem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_tim_kiem.Text == "🔍 Tìm kiếm suất chiếu...")
            {
                txt_tim_kiem.Text = "";
                txt_tim_kiem.Foreground = Brushes.Black;
            }
        }

        private void txt_tim_kiem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_tim_kiem.Text))
            {
                txt_tim_kiem.Text = "🔍 Tìm kiếm suất chiếu...";
                txt_tim_kiem.Foreground = Brushes.Gray;
            }
        }

        private void btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var selectedItem = btn.DataContext as lichchieu;

            if (selectedItem != null)
            {
                if (MessageBox.Show($"Xóa suất chiếu này khỏi hệ thống?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    db.lichchieus.Remove(selectedItem);
                    db.SaveChanges();
                    dsHienThi.Remove(selectedItem);
                    txt_ket_qua.Text = "🗑 Đã xóa thành công.";
                }
            }
        }

        // --- TÍNH NĂNG LỌC THEO PHÒNG BÊN TRÁI ---
        private void lst_LocPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Chống lỗi khi form đang load mà chưa có dữ liệu
            if (lst_LocPhong == null || dtg_suat_chieu == null || dsHienThi == null) return;

            // Lấy ra mục người dùng vừa click (Rạp 1, Rạp 2...)
            if (lst_LocPhong.SelectedItem is ListBoxItem selectedItem)
            {
                string tenPhong = selectedItem.Content.ToString();

                if (tenPhong == "Tất cả phòng")
                {
                    // Trả lại toàn bộ danh sách ban đầu
                    dtg_suat_chieu.ItemsSource = dsHienThi;
                }
                else
                {
                    // Lọc ra những lịch chiếu có tên phòng khớp với chữ người dùng click
                    var filtered = dsHienThi.Where(x => x.phongchieu != null && x.phongchieu.ten_phong == tenPhong).ToList();

                    // Đổ dữ liệu đã lọc lên bảng
                    dtg_suat_chieu.ItemsSource = new ObservableCollection<lichchieu>(filtered);
                }
            }
        }
    }
}