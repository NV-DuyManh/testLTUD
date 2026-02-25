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
        public QuanLy() : base("name=QuanLy") { }

        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<DangKyHoc> DangKyHocs { get; set; }
    }

}
