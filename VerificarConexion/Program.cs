using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
namespace VerificarConexion
{
    internal class Program
    {
        static void Main()
        {
            // Obtener y mostrar el tipo de conexión y la dirección IP
            Tuple<string, string> informacionConexion = ObtenerInformacionConexion();
            Console.WriteLine($"Tipo de conexión: {informacionConexion.Item1}");
            Console.WriteLine($"Dirección IP: {informacionConexion.Item2}");
            Console.ReadKey();
        }

        static Tuple<string, string> ObtenerInformacionConexion()
        {
            // Obtener todas las interfaces de red disponibles
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Iterar sobre cada interfaz de red
            foreach (NetworkInterface interfaz in interfaces)
            {
                // Verificar si la interfaz está activa
                if (interfaz.OperationalStatus == OperationalStatus.Up)
                {
                    // Verificar si la interfaz es de tipo Ethernet o WiFi
                    if (interfaz.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                        interfaz.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    {
                        // Obtener las propiedades de configuración de la interfaz
                        IPInterfaceProperties propiedades = interfaz.GetIPProperties();

                        // Obtener la primera dirección IPv4 asignada a la interfaz
                        IPAddress direccionIP = propiedades.UnicastAddresses
                            .FirstOrDefault(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)?.Address;

                        // Devolver el tipo de conexión y la dirección IP
                        return new Tuple<string, string>(
                            interfaz.NetworkInterfaceType == NetworkInterfaceType.Ethernet ? "Ethernet" : "WiFi",
                            direccionIP?.ToString() ?? "Sin dirección IP"
                        );
                    }
                }
            }

            // Si no se determina la conexión, devolver un mensaje predeterminado
            return new Tuple<string, string>("No se pudo determinar la conexión", "Sin dirección IP");
        }
    }
}
