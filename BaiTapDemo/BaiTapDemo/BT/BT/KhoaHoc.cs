using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BT
{
    public class KhoaHoc
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string MaKhoaHoc { get; set; }
        [StringLength(100)]
        public string TenKhoaHoc { get; set; }

        public int ThoiGianHoc { get; set; }
    }
}
