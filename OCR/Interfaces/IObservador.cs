using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCR
{
    interface IObservador
    {
        void ActualizarProgreso();
        void ActualizarTerminado();
    }
}
