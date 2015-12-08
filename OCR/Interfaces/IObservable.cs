using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCR
{
    interface IObservable
    {
        void AgregarObservador(IObservador observador);
        void NotificarProgreso();
        void NotificarTerminado();
    }
}
