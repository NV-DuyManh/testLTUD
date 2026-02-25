using BT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcodefirst
{
    public class DangKyHoc
    {
        [Key]
        public int Id { get; set; }

        public int HocVienId { get; set; }
        [ForeignKey("HocVienId")]
        public virtual HocVien HocVien { get; set; }

        public int KhoaHocId { get; set; }
        [ForeignKey("KhoaHocId")]
        public virtual KhoaHoc KhoaHoc { get; set; }

        public DateTime NgayDangKy { get; set; }
    }
}
