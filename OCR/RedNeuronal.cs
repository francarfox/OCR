using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AForge.Neuro;
using AForge.Neuro.Learning;

namespace OCR
{
    class RedNeuronal : IObservable
    {
        public static int Iteraciones = 5000;
        private static RedNeuronal instancia = null;

        private ActivationNetwork red;
        private bool entrenada;

        private IObservador observador;

        private RedNeuronal()
        {
            red = new ActivationNetwork(new SigmoidFunction(-0.5), 100, 10, 5);
            entrenada = false;
        }

        public double Entrenar(double[][] entradas, double[][] salidas)
        {
            BackPropagationLearning tutor = new BackPropagationLearning(red);
            tutor.LearningRate = 0.1;
            tutor.Momentum = 0;

            bool detener = false;
            int iteration = 0;
            double x = 0;

            while (!detener)
            {       
                double error = tutor.RunEpoch(entradas, salidas);

                if (error == 0)
                {
                    NotificarTerminado();
                    break;
                }
                else
                {
                    if (iteration < Iteraciones)
                    {
                        iteration++;
                        NotificarProgreso();
                    }
                    else
                    {
                        NotificarTerminado();
                        detener = true;
                    }
                }

                x = error;
            }

            entrenada = true;
            return x;
        }

        public bool Entrenada
        {
            get { return entrenada; }
        }

        public string Calcular(double[] prueba)
        {
            double[] salida = red.Compute(prueba);

            int num1 = (int)(salida[0] + 0.5);
            int num2 = (int)(salida[1] + 0.5);
            int num3 = (int)(salida[2] + 0.5);
            int num4 = (int)(salida[3] + 0.5);
            int num5 = (int)(salida[4] + 0.5);

            return (num1 + "," + num2 + "," + num3 + "," + num4 + "," + num5);
        }

        public static RedNeuronal Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new RedNeuronal();

                return instancia;
            }
        }

        //IObservador
        public void AgregarObservador(IObservador observador)
        {
            this.observador = observador;
        }

        public void NotificarProgreso()
        {
            observador.ActualizarProgreso();
        }

        public void NotificarTerminado()
        {
            observador.ActualizarTerminado();
        }
    }
}
