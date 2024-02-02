using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
namespace VerificarConexion
{
    internal class Program
    {
        static void Main()
        {
            // Obtener y mostrar el tipo de conexión
            string tipoConexion = ObtenerTipoConexion();
            Console.WriteLine(tipoConexion);
            Console.ReadKey();
        }

        static string ObtenerTipoConexion()
        {
            // Obtener todas las interfaces de red disponibles
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Iterar sobre cada interfaz de red
            foreach (NetworkInterface interfaz in interfaces)
            {
                // Verificar si la interfaz está activa
                if (interfaz.OperationalStatus == OperationalStatus.Up)
                {
                    // Verificar si la interfaz es de tipo Ethernet
                    if (interfaz.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        return "Conectado a Ethernet";
                        
                    }
                    // Verificar si la interfaz es de tipo WiFi
                    else if (interfaz.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    {
                        return "Conectado a WiFi";
                    }
                }
            }


            // Si no se determina la conexión, devolver un mensaje predeterminado
            return "No se pudo determinar la conexión";
        }
    }
}
