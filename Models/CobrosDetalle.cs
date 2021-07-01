using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apli2SegundoParcial.Models
{
    public class CobrosDetalle
    {
        public int Id { get; set; }
        public int CobrosId { get; set; }
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public decimal Cobrado { get; set; }
        public bool Pagar { get; set; }
    }
}
