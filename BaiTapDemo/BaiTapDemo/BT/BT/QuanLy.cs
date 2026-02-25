using BT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcodefirst
{
    class QuanLy : DbContext
    {
        public QuanLy() : base("name=QuanLy")
        {
        }

        // Khai báo các bảng dữ liệu
        public DbSet<KhoaHoc> KhoaHocs { get; set; }

        // --- THÊM 2 DÒNG NÀY ---
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<DangKyHoc> DangKyHocs { get; set; }
    }

}
