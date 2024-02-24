using System.ComponentModel.DataAnnotations;

namespace L01_2021_AP_650.Models{
    public class platos{
        [Key]
        public int platoId {get; set;}
        public string nombrePlato {get; set;}
        public decimal precio {get; set;}
    }
}