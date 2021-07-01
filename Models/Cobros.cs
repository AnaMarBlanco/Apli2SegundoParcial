using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apli2SegundoParcial.Models
{
    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int ClienteId { get; set; }
        public decimal Totales { get; set; }
        public string Observaciones { get; set; }
        [ForeignKey("CobroId")]
        public List<CobrosDetalle> Detalle = new List<CobrosDetalle>();

    }
}
