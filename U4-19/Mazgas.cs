using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4_19
{
    /// <summary>
    /// Vienkriptie mazgo klase
    /// </summary>
    public sealed class Mazgas
    {
        public Darbuotojas Duom { get; set; }
        public Mazgas Kitas { get; set; }

        public Mazgas()
        { }

        public Mazgas(Darbuotojas duom, Mazgas adresas)
        {
            Duom = duom;
            Kitas = adresas;
        }
    }
}
