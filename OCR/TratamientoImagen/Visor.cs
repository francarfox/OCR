using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OCR
{
    class Visor
    {
        public static int AltoCaracter = 10;
        public static int AnchoCaracter = 10;
        public static int TonoMinimo = 100;
        private static int MinBits = 5;

        private Bitmap imagen;
        private Rectangle cuadro;
        private RedNeuronal red;
        private bool finalizado;


        public Visor(Bitmap imagen)
        {
            this.imagen = imagen;
            cuadro = new Rectangle(0, 0, AnchoCaracter, AltoCaracter);
            red = RedNeuronal.Instancia;
        }

        public string IdentificarCaracteres()
        {
            string cadena = "";
            finalizado = false;

            while(!finalizado)
            {
                double[] bitCuadro = ProcesarCuadro();

                if (bitCuadro != null)
                    cadena += ProcesarClave(bitCuadro);

                MoverCuadro();
            }

            return cadena;
        }

        private string ProcesarClave(double[] bitCuadro)
        {
            string clave = red.Calcular(bitCuadro);

            return Entrenador.Instancia.ProcesarCaracter(clave);
        }

        private double[] ProcesarCuadro()
        {
            double[] bitCaracter = new double[cuadro.Width*cuadro.Height];
            int contador = 0;
            int bitsNegro = 0;

            for (int j = 0; j < cuadro.Height; j++)
            {
                for (int i = 0; i < cuadro.Width; i++)
                {
                    int bit = 0;

                    if (imagen.GetPixel(i + cuadro.X, j + cuadro.Y).R < TonoMinimo &
                        imagen.GetPixel(i + cuadro.X, j + cuadro.Y).G < TonoMinimo &
                        imagen.GetPixel(i + cuadro.X, j + cuadro.Y).B < TonoMinimo)
                    {
                        bit = 1;
                        bitsNegro++;
                    }

                    bitCaracter.SetValue(bit, contador);
                    contador++;
                }
            }

            if (bitsNegro <= MinBits)
                bitCaracter = null;

            return bitCaracter;
        }

        private void MoverCuadro()
        {
            if ((imagen.Width - (cuadro.X + cuadro.Width)) >= AnchoCaracter)
                cuadro.X += AnchoCaracter;
            else
                MoverAbajo();
        }

        private void MoverAbajo()
        {
            if ((imagen.Height - (cuadro.Y + cuadro.Height)) >= AltoCaracter)
            {
                cuadro.X = 0;
                cuadro.Y += AltoCaracter;
            }
            else
                finalizado = true;
        }
    }
}
