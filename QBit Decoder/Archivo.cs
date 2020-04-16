using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QBit_Decoder
{
    class Archivo
    {
        public static string Parsear(string filenameOriginal, string direccion)
        {
            string pathParseado = direccion + @"\";
            DateTime fecha = DateTime.Now;
            string fechaFN = fecha.ToString("yyyyMMdd_HHmmss");
            string filenameParseado = "LogQBit" + fechaFN + ".txt";
            string fullFilenameParseado = pathParseado + filenameParseado;
            using (StreamReader lector = new StreamReader(filenameOriginal))
            {
                using (StreamWriter sw = new StreamWriter(fullFilenameParseado))
                {
                    int contadorPaquete = 0;
                    while (lector.Peek() > -1)
                    {
                        string linea = lector.ReadLine();
                        Paquete paquete = new Paquete(linea);

                        if (!String.IsNullOrEmpty(linea))
                        {
                            contadorPaquete++;
                            sw.Write("========================================================================");
                            sw.WriteLine();
                            sw.Write("Paquete " + contadorPaquete);
                            sw.WriteLine();
                            sw.Write(linea.Substring(0,45));
                            sw.WriteLine();
                            if (linea.Contains("Client"))
                            {
                                sw.Write(linea.Substring(linea.IndexOf("Client")));
                            }
                            sw.WriteLine();
                            
                            Escribir(sw, paquete);
                            sw.WriteLine();
                            sw.Write("========================================================================");

                        }

                    }
                }
            }
            return fullFilenameParseado;
        }

        public static void Escribir(StreamWriter sw, Paquete paquete)
        {
            Dictionary<string, string> diccionarioParseado = new Dictionary<string, string>();
            diccionarioParseado = paquete.Parsear();


            foreach (KeyValuePair<string, string> elemento in diccionarioParseado)
            {
                if (elemento.Value != "E")
                {
                    sw.Write(elemento.Key + ": " + elemento.Value);
                    sw.WriteLine();
                }
                
            }

        }
    }
}
