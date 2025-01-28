using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class TokenRevocado
    {
        [Key]
        public string Token {  get; set; }
        public DateTime Expiracion { get; set; }
    }
}
