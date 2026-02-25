using System;
using System.Linq;
using System.Windows;
using testcodefirst;

namespace BT
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Gọi hàm xử lý database ngay khi bật app
            XuLyDatabase();
        }

        private void XuLyDatabase()
        {
            try
            {
                QuanLy db = new QuanLy();
                // Dòng này rất quan trọng để cập nhật bảng mới nếu bạn đã xóa DB cũ
                db.Database.CreateIfNotExists();

                // --- KIỂM TRA TRÁNH TRÙNG LẶP ---
                // Nếu mã CS445 đã có thì dừng lại, không thêm nữa
                if (db.KhoaHocs.Any(x => x.MaKhoaHoc == "CS445"))
                {
                    MessageBox.Show("Dữ liệu đã có sẵn trong Database! (Code dừng lại để không bị trùng)");
                    return;
                }

                // --- BẮT ĐẦU THÊM MỚI DỮ LIỆU ---

                // 1. Tạo Khóa Học
                var khoaHoc = new KhoaHoc
                {
                    MaKhoaHoc = "CS445",
                    TenKhoaHoc = "System Integration",
                    ThoiGianHoc = 2
                };
                db.KhoaHocs.Add(khoaHoc);

                // 2. Tạo Học Viên (Phần bạn đang thiếu)
                var hocVien = new HocVien
                {
                    MaHocVien = "HV001",
                    TenHocVien = "Nguyễn Văn A",
                    Email = "nguyenvana@gmail.com"
                };
                db.HocViens.Add(hocVien);

                // QUAN TRỌNG: Phải Lưu lần 1 để SQL sinh ra ID cho Khóa học và Học viên
                db.SaveChanges();

                // 3. Tạo Đăng Ký Học (Liên kết 2 bảng trên)
                var dangKy = new DangKyHoc
                {
                    KhoaHocId = khoaHoc.Id, // Lấy ID vừa sinh ra của khóa học
                    HocVienId = hocVien.Id, // Lấy ID vừa sinh ra của học viên
                    NgayDangKy = DateTime.Now
                };
                db.DangKyHocs.Add(dangKy);

                // Lưu lần cuối cùng
                db.SaveChanges();

                MessageBox.Show("Đã thêm đầy đủ: Khóa Học, Học Viên và Đăng Ký!");
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết nếu có
                MessageBox.Show("Lỗi: " + ex.Message + "\nChi tiết: " + ex.InnerException?.Message);
            }
        }
    }
}