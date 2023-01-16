using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4_19
{
    /// <summary>
    /// Darbuotoju duoenu klase
    /// </summary>
    public class Darbuotojas
    {
        public string Pavadynimas { get; set; }   // Padalynio pavadinimas 
        public string PavVrd { get; set; }        // Darbuotojo pavardė vardas
        public string Diena { get; set; }            // Darbo dienu skaicius
        public TimeSpan DarboPrad { get; set; }   // Darbo pradzia
        public TimeSpan DarboPabaiga { get; set; }// Darbo pabaiga

        /// <summary>
        /// Tuscias konstruktorius
        /// </summary>
        /// <param name="pavadynimas"></param>
        /// <param name="pavVrd"></param>
        /// <param name="diena"></param>
        /// <param name="darboPrad"></param>
        /// <param name="darboPabaiga"></param>
        public Darbuotojas(string pavadynimas = "", string pavVrd = "", string diena = "",
            TimeSpan darboPrad = default(TimeSpan), TimeSpan darboPabaiga = default(TimeSpan))
        {
            this.Pavadynimas = pavadynimas;
            this.PavVrd = pavVrd;
            this.Diena = diena;
            this.DarboPrad = darboPrad;
            this.DarboPabaiga = darboPabaiga;
        }

        /// <summary>
        /// Duomenu eilutes formatavimas
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("| {0,-19} | {1,-16} | {2,-14} | {3,12} |",
            PavVrd,Pavadynimas,Diena,DarboPabaiga-DarboPrad);
            return eilute;
        }

        /// <summary>
        /// Palyginimo operatorius
        /// </summary>
        /// <param name="da1"></param>
        /// <param name="da2"></param>
        /// <returns></returns>
        public static bool operator >=(Darbuotojas da1, Darbuotojas da2)
        {
            return (da1.DarboPabaiga - da1.DarboPrad > da2.DarboPabaiga - da2.DarboPrad) ||
                ((da1.DarboPabaiga - da1.DarboPrad == da2.DarboPabaiga - da2.DarboPrad) &&
                String.Compare(da1.PavVrd, da2.PavVrd) > 0);
        }

        /// <summary>
        /// palyginimo operatorius
        /// </summary>
        /// <param name="da1"></param>
        /// <param name="da2"></param>
        /// <returns></returns>
        public static bool operator <=(Darbuotojas da1, Darbuotojas da2)
        {
            return (da1.DarboPabaiga - da1.DarboPrad < da2.DarboPabaiga - da2.DarboPrad) ||
                ((da1.DarboPabaiga - da1.DarboPrad == da2.DarboPabaiga - da2.DarboPrad) &&
                String.Compare(da1.PavVrd, da2.PavVrd) < 0);
        }
    }
}
