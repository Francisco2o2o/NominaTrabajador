using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Utilidades
{
    public class MetodoSoloSonNumeros
    {
        public static bool ValidarSoloNumeros(string texto, int tipoCon, char keyChar, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (!char.IsControl(keyChar) && !char.IsDigit(keyChar))
            {
                mensajeError = "Por favor, ingresa solo números en el campo.";
                return false;
            }
            if (tipoCon == 1 && texto.Length >= 8 && keyChar != (char)Keys.Back)
            {
                mensajeError = "El documento debe tener exactamente 8 caracteres.";
                return false;
            }
            if (tipoCon == 2 && texto.Length >= 9 && keyChar != (char)Keys.Back)
            {
                mensajeError = "El Telefono debe tener exactamente 9 caracteres.";
                return false;
            }
            return true;
        }
    }
}
