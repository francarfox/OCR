using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OCR
{
    class Entrenador
    {
        public static int CantidadEntSal = 26;

        private static Entrenador instancia = null;
        private Dictionary<string, string> datos;

        private double[][] entradas, salidas;
        private int cantidad;

        private Entrenador()
        {
            entradas = new double[CantidadEntSal][];
            salidas = new double[CantidadEntSal][];
            cantidad = 0;
            datos = new Dictionary<string, string>();

            InicializarDatos();
        }

        public double EntrenarRed()
        {
            Estandar.CargarEntradaSalidas(entradas, salidas, cantidad);

            RedNeuronal red = RedNeuronal.Instancia;
            return red.Entrenar(entradas, salidas);
        }

        public string ProcesarCaracter(string clave)
        {
            string caracter;

            try
            {
                caracter = datos[clave];
            }
            catch
            {
                caracter = "_";
            }

            return caracter;
        }

        private void InicializarDatos()
        {
            datos.Add("0,0,0,0,1", "A");
            datos.Add("0,0,0,1,0", "B");
            datos.Add("0,0,0,1,1", "C");
            datos.Add("0,0,1,0,0", "D");
            datos.Add("0,0,1,0,1", "E");
            datos.Add("0,0,1,1,0", "F");
            datos.Add("0,0,1,1,1", "G");
            datos.Add("0,1,0,0,0", "H");
            datos.Add("0,1,0,0,1", "I");
            datos.Add("0,1,0,1,0", "J");
            datos.Add("0,1,0,1,1", "K");
            datos.Add("0,1,1,0,0", "L");
            datos.Add("0,1,1,0,1", "M");
            datos.Add("0,1,1,1,0", "N");
            datos.Add("0,1,1,1,1", "O");
            datos.Add("1,0,0,0,0", "P");
            datos.Add("1,0,0,0,1", "Q");
            datos.Add("1,0,0,1,0", "R");
            datos.Add("1,0,0,1,1", "S");
            datos.Add("1,0,1,0,0", "T");
            datos.Add("1,0,1,0,1", "U");
            datos.Add("1,0,1,1,0", "V");
            datos.Add("1,0,1,1,1", "W");
            datos.Add("1,1,0,0,0", "X");
            datos.Add("1,1,0,0,1", "Y");
            datos.Add("1,1,0,1,0", "Z");
        }

        private double[] ProcesarImagen(Bitmap imagen)
        {
            double[] entrada = new double[Visor.AnchoCaracter * Visor.AltoCaracter];
            int contador = 0;
            int bitsNegro = 0;

            for (int j = 0; j < Visor.AltoCaracter; j++)
            {
                for (int i = 0; i < Visor.AnchoCaracter; i++)
                {
                    int bit = 0;

                    if (imagen.GetPixel(i, j).R < Visor.TonoMinimo &
                        imagen.GetPixel(i, j).G < Visor.TonoMinimo &
                        imagen.GetPixel(i, j).B < Visor.TonoMinimo)
                    {
                        bit = 1;
                        bitsNegro++;
                    }

                    entrada.SetValue(bit, contador);
                    contador++;
                }
            }

            return entrada;
        }

        public static Entrenador Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new Entrenador();

                return instancia;
            }
        }

    }
}