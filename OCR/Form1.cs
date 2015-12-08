using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OCR
{
    public partial class Form1 : Form, IObservador
    {
        private static string PathImage = "C:\\";

        private RedNeuronal red;
        private Entrenador entrenador;
        private Imagen imagen;

        private double aux;

        public Form1()
        {
            red = RedNeuronal.Instancia;
            red.AgregarObservador(this);
            entrenador = Entrenador.Instancia;
            imagen = null;
            aux = 0;

            InitializeComponent();
        }

        private void btnIdentificar_Click(object sender, EventArgs e)
        {
            if (imagen != null & red.Entrenada)
            {
                imagen.IdentificarCaracteres();
                tbxCaracteres.Text = imagen.Texto;
            }
            else
                tbxCaracteres.Text = "Error al identificar";
        }

        private void cargarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile = new OpenFileDialog();
            openFile.Filter = "Archivos JPEG(*.jpg)| *.jpg";
            openFile.InitialDirectory = PathImage;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = openFile.FileName;
                imagen = new Imagen(new Bitmap(path));

                pictureBox1.Image = imagen.Image;
                tbxCaracteres.Text = "";
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void entrenarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            double error  = entrenador.EntrenarRed();

            tbxError.Text = error.ToString();
            tbxCaracteres.Text = "Red Entrenada";
            pictureBox1.Image = null;
        }

        //IObservador
        public void ActualizarProgreso()
        {
            if (progressBar.Value < 100 & aux >= 1)
            {
                progressBar.Value++;
                aux = 0;
            }
            else
                aux += (100.0 / RedNeuronal.Iteraciones);
        }

        public void ActualizarTerminado()
        {
            progressBar.Value = 100;
            progressBar.Visible = false;
            progressBar.Value = 0;
        }
    }
}
