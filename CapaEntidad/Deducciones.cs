using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Deducciones
    {
        decimal deduccionSalud, deduccionVida, deduccionAccidentes, afp, deduccionImpuestos;

        public Deducciones(decimal deduccionSalud, decimal deduccionVida, decimal deduccionAccidentes, decimal afp, decimal deduccionImpuestos)
        {
            this.deduccionSalud = deduccionSalud;
            this.deduccionVida = deduccionVida;
            this.deduccionAccidentes = deduccionAccidentes;
            this.afp = afp;
            this.deduccionImpuestos = deduccionImpuestos;
        }

        public decimal DeduccionSalud { get => deduccionSalud; set => deduccionSalud = value; }
        public decimal DeduccionVida { get => deduccionVida; set => deduccionVida = value; }
        public decimal DeduccionAccidentes { get => deduccionAccidentes; set => deduccionAccidentes = value; }
        public decimal Afp { get => afp; set => afp = value; }
        public decimal DeduccionImpuestos { get => deduccionImpuestos; set => deduccionImpuestos = value; }

        public decimal TotalDeducciones
        {
            get
            {
                return deduccionSalud + deduccionVida + deduccionAccidentes + afp + deduccionImpuestos;
            }
        }
    }

}
