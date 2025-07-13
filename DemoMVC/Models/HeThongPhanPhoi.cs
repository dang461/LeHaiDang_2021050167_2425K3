using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models{
    [Table("HeThongPhanPhoi")]
        public class HeThongPhanPhoi
    {
        [Key]
        public String MaHTPP { get; set; }
        public String TenHTPP { get; set; }
        public ICollection<DaiLy>? Daily { get; set; }
    }
}