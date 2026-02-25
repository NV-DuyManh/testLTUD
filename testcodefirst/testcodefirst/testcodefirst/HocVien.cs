using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcodefirst
{
    public class HocVien
    {
        [Key] public int Id { get; set; }
        [Required][StringLength(100)] public string Email { get; set; }
        [Required][StringLength(100)] public string TenHocVien { get; set; }
        [Required][StringLength(20)] public string MaHocVien { get; set; }
    }
}
