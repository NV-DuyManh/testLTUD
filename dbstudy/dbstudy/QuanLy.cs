using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // Thư viện Entity
namespace dbstudy
{
    namespace DemoEntity
    {
        class QuanLy : DbContext
        {
            public DbSet<Student> Students { get; set; }
            public QuanLy() : base("name=QuanLy")
            {
            }
        }
    }
}
