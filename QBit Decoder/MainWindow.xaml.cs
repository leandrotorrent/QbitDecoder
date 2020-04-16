using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;

namespace QBit_Decoder
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public struct Datos
        {
            public string Campo { get; set; }
            public string Valor { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Tabla.Visibility = Visibility.Hidden;
            Ventana.Height = 120;
        }

        private void BTNenviarPaquete_Click(object sender, RoutedEventArgs e)
        {

            Paquete paqueteUDP = new Paquete(TBpaqueteUDP.Text);
            Dictionary<string, string> diccionarioParseado = new Dictionary<string, string>();
            diccionarioParseado = paqueteUDP.Parsear();
            if (diccionarioParseado["Start Bit"] == "E")    //En caso de que el paquete sea inválido
            {
                MessageBox.Show("No se ha ingresado un paquete válido.");
            }
            else
            {
                Tabla.Visibility = Visibility.Visible;
                Tabla.Items.Clear();
                Ventana.Height = 700;
                foreach (KeyValuePair<string, string> elemento in diccionarioParseado)  //Parseo Datos Caiquen
                {
                    Tabla.Items.Add(new Datos { Campo = elemento.Key, Valor = elemento.Value });
                }
            }
        }

        private void TBpaqueteUDP_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TBpaqueteUDP.Text == "Inserte Paquete UDP")
            {
                TBpaqueteUDP.Text = "";
            }
        }

        private void BTNenviarArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog abrirArchivo = new OpenFileDialog();
            abrirArchivo.DefaultExt = ".txt";
            abrirArchivo.Title = "Seleccione el log a decodificar";
            abrirArchivo.Filter = "Documentos de texto (*.txt)|*.txt" + "|Todos los archivos (*.*)|*.*";

            if (abrirArchivo.ShowDialog() == true)
            {
                string filename = abrirArchivo.FileName;
                string direccionGuardado;

                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                dialog.Description = "Indique donde guardar el log decodificado";

                if (dialog.ShowDialog(this).GetValueOrDefault())
                {
                    direccionGuardado = dialog.SelectedPath;
                    string logParseado = Archivo.Parsear(filename, direccionGuardado);
                    Process.Start("notepad.exe", logParseado);
                }

                
            }

        }
    }
}
