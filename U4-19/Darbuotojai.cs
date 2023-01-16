using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4_19
{
    /// <summary>
    /// Darbuotoju konteineris
    /// </summary>
    public sealed class Darbuotojai
    {
        Mazgas pradzia;
        Mazgas pabaiga;
        Mazgas d;    

        public Darbuotojai()
        {
            pradzia = null;
            pabaiga = null;
            d = null;
        }

        public void Pradzia() { d = pradzia; }
        public void Kitas() { d = d.Kitas; }
        public bool Yra() { return d != null; }

        public Darbuotojas ImtiDuomenis() { return d.Duom; }

        public void DetiPradzia(Darbuotojas naujas)
        {
            pradzia = new Mazgas(naujas, null);
            pabaiga = pradzia;
        }

        public void DetiElementaPabaigoje(Darbuotojas naujas)
        {
            pabaiga.Kitas = new Mazgas(naujas, null);
            pabaiga = pabaiga.Kitas;
        }

        public void DetiElementa(Darbuotojas naujas)
        {
            if (pradzia != null)
                DetiElementaPabaigoje(naujas);
            else
                DetiPradzia(naujas);
        }

        /// <summary>
        /// Rikiavimo metodas
        /// </summary>
        public void Burbulas()
        {
            bool buvoKeitimas = true;
            Mazgas d1, d2;

            while (buvoKeitimas)
            {
                buvoKeitimas = false;
                d1 = d2 = pradzia;
                while (d2 != null)
                {
                    if (d2.Duom >= d1.Duom)
                    {
                        Darbuotojas k = d1.Duom;
                        d1.Duom = d2.Duom;
                        d2.Duom = k;
                        buvoKeitimas = true;
                    }
                    d1 = d2;
                    d2 = d2.Kitas;
                }
            }
        }
        
        /// <summary>
        /// Grazina konteinerio duomenu kieki
        /// </summary>
        /// <returns></returns>
        public int Kiek()
        {
            int kiekis = 0;
            for (Pradzia(); Yra(); Kitas())
            {
                kiekis++;
            }
            return kiekis;
        }

        /// <summary>
        /// Iterpimo metodas
        /// </summary>
        /// <param name="da"></param>
        public void Iterpimas(Darbuotojas da)
        {
            Mazgas s;
            for (Pradzia(); Yra(); Kitas())
            {
                s = pradzia;
                if (da <=s.Duom)
                {
                    Mazgas m = new Mazgas();
                    m.Duom = da;
                    m.Kitas = null;
                    m.Kitas = s.Kitas;
                    s.Kitas = m;
                    break;
                }
            }
        }
    }
}
