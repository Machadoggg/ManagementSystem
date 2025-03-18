namespace ManagementSystem.Domain.Entities
{
    public class CuentaPorCobrar
    {
        public int Id { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string MesAnio { get; set; } = default!;
        public string Tipo { get; set; } = default!;
        public string Documento { get; set; } = default!;
        public string Cliente { get; set; } = default!;
        public decimal Valor { get; set; }
        public decimal NetoR { get; set; }
        public decimal Cartera { get; set; }
        public decimal NetoF { get; set; }
        public decimal IVA { get; set; }
        public decimal Pagado { get; set; }
        public DateTime FechaPago { get; set; }
        public int Dias { get; set; }
        public decimal EfectivoOtroMeses { get; set; }
        public decimal Colombia { get; set; }
        public decimal Davivienda { get; set; }
        public decimal Bancolombia { get; set; }
        public decimal Efectivo { get; set; }

        //public decimal IVA { get; set; }
        //public decimal Pagado { get; set; }
        //public DateTime FechaPago { get; set; }
        //public int Dias { get; set; }
        //public decimal Efectivo { get; set; }
        //public decimal Bancolombia { get; set; }
        //public decimal Auxiliar { get; set; }
        //public decimal Davivienda { get; set; }
    }
}
