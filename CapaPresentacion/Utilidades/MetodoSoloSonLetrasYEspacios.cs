using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Utilidades
{
    public class MetodoSoloSonLetrasYEspacios
    {
        public static bool ValidarSoloLetrasYEspacios(string texto, char keyChar, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (!char.IsLetter(keyChar) && !char.IsWhiteSpace(keyChar) && !char.IsControl(keyChar))
            {
                mensajeError = "Por favor, ingresa solo letras y espacios en este campo.";
                return false;
            }

            return true;

        }
    }
}
