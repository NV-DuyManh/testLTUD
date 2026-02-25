using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcodefirst
{
    public class KhoaHoc
    {
        [Key] public int Id { get; set; }
        public int ThoiGianHoc { get; set; }
        [Required][StringLength(100)] public string TenKhoaHoc { get; set; }
        [Required][StringLength(20)] public string MaKhoaHoc { get; set; }
    }

    }
