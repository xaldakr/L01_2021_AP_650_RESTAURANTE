using System.ComponentModel.DataAnnotations;

namespace L01_2021_AP_650.Models{
    public class motoristas{
        [Key]
        public int motoristaId {get; set;}
        public string nombreMotorista {get; set;}
    }

}