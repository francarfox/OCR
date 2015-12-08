using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OCR
{
    class Imagen
    {
        private string texto;
        private Bitmap imagen;
        private Visor visor;

        public Imagen(Bitmap imagen)
        {
            texto = "";
            this.imagen = imagen;
            visor = new Visor(imagen);
        }

        public void IdentificarCaracteres()
        {
            texto = visor.IdentificarCaracteres();
        }

        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        public Bitmap Image
        {
            get { return imagen; }
        }
    }
}
