using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace U4_19
{



    public partial class Form1 : Form
    {
        const string CFd1 = "..\\..\\L1.txt"; //Pirmo padalinio duomenys
        const string CFd2 = "..\\..\\L2.txt"; //Antro padalinio duomenys
        const string CFd3 = "..\\..\\L4.txt"; //Nauju darbuotoju duomenys
        const string CFr = "..\\..\\Rezultatai.txt"; //Rezultatu failas

        Darbuotojai Dard1 = new Darbuotojai(); //Pirmo padalinio duomenys
        Darbuotojai Dard2 = new Darbuotojai(); //Antro padalinio duomenys
        Darbuotojai Dard3 = new Darbuotojai(); //Nauju darbuotoju duomenys
        Darbuotojai Naujas = new Darbuotojai(); //Atrinkti duomenys

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(CFr))
                File.Delete(CFr);
        }

        /// <summary>
        /// Nuskaitimo metodas
        /// </summary>
        /// <param name="fv">Duomenu failas</param>
        /// <param name="D">Duomenu saraso konteineris</param>
        static void Nuskaitymas(string fv, Darbuotojai D)
        {
            using (StreamReader failas = new StreamReader(fv, Encoding.GetEncoding(1257)))
            {
                string[] dalys;
                string eilute;
                eilute = failas.ReadLine();
                string pavadynimas = eilute;

                while ((eilute = failas.ReadLine()) != null)
                {
                    dalys = eilute.Split(';');

                    string pavVard = dalys[0];
                    string diena =dalys[1];
                    TimeSpan darboPra =TimeSpan.Parse(dalys[2]);
                    TimeSpan darboPabaig = TimeSpan.Parse(dalys[3]);

                    Darbuotojas da = new Darbuotojas(pavadynimas, pavVard,
                        diena, darboPra, darboPabaig);
                    D.DetiElementa(da);
                }
            }
        }

        /// <summary>
        /// Lenteles spausdynimo metodas
        /// </summary>
        /// <param name="Darb">Duomenys</param>
        /// <param name="fv">Spausdynamas fialas</param>
        /// <param name="title">Lenteles pavadynimas</param>
        static void Lentele(Darbuotojai Darb, string fv, string title)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("|{0,1}|",new string('-',72));
                fr.WriteLine("| {0,-70} |", title);
                fr.WriteLine("|{0,1}|", new string('-', 72));
                fr.WriteLine("|{0,-21}|{1,-18}|{2,-16}|{3,-14}|",
                    " Pavarde ir vardas", " Padalinis", " Diena", " Darbo laikas");
                fr.WriteLine("|{0,1}|", new string('-', 72));
                for (Darb.Pradzia();Darb.Yra();Darb.Kitas())
                {
                    fr.WriteLine(Darb.ImtiDuomenis().ToString());
                }
            }
        }

        /// <summary>
        /// Bendro darbo laiko skaiciavimo metodas
        /// </summary>
        /// <param name="D">Duomenys</param>
        /// <returns>Grazina bendra padalinio laika</returns>
        static TimeSpan KiekDirbo(Darbuotojai D)
        {
            TimeSpan dirbo = TimeSpan.Parse("00:00");
            for (D.Pradzia();D.Yra();D.Kitas())
            {
                dirbo = dirbo + D.ImtiDuomenis().DarboPabaiga -
                    D.ImtiDuomenis().DarboPrad;
            }
            return dirbo;
        }

        /// <summary>
        /// Darbciausio darbotojo radymo metodas
        /// </summary>
        /// <param name="Darb">Darbuotoju duomenys</param>
        /// <returns></returns>
        static Darbuotojas Darbsciausias(Darbuotojai Darb)
        {
            Darbuotojas D = null;

            Darb.Pradzia();
            D = Darb.ImtiDuomenis();
            for (Darb.Pradzia();Darb.Yra();Darb.Kitas())
            {
                if (D.DarboPabaiga-D.DarboPrad < 
                    Darb.ImtiDuomenis().DarboPabaiga - Darb.ImtiDuomenis().DarboPrad)
                {
                    D = Darb.ImtiDuomenis();
                }
            }
            return D;
        }

        /// <summary>
        /// Darbotoju kure dirbo pirmam padalini atrinkimo metodas
        /// </summary>
        /// <param name="D">Darbotoju sarasas</param>
        /// <param name="Naujas">Pirmos pamainuos darbuotoju sarasas</param>
        static void Formuoti(Darbuotojai D, Darbuotojai Naujas)
        {
            TimeSpan l1 = TimeSpan.Parse("08:00");
            TimeSpan l2 = TimeSpan.Parse("17:00");

            for (D.Pradzia(); D.Yra(); D.Kitas())
            {
                if (D.ImtiDuomenis().DarboPabaiga<=l2 &&
                    l1<=D.ImtiDuomenis().DarboPrad)
                {
                    Naujas.DetiElementa(D.ImtiDuomenis());
                }
            }
        }

        /// <summary>
        /// Saraso papildymo metodas
        /// </summary>
        /// <param name="Naujas">Nauju darbuotoju sarasas</param>
        /// <param name="D">Pirmos pamainuos sarasas</param>
        static void Papildo(Darbuotojai Naujas,Darbuotojai D)
        {
            for (D.Pradzia(); D.Yra(); D.Kitas())
            {
                Darbuotojas da = D.ImtiDuomenis();
                Naujas.Iterpimas(da);
            }
        }

        /// <summary>
        /// Duomenu ivedimo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void įvestiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nuskaitymas(CFd1, Dard1);
            Nuskaitymas(CFd2, Dard2);
            Nuskaitymas(CFd3, Dard3);

            Lentele(Dard1, CFr, "Pirmas padalinis:");
            Lentele(Dard2, CFr, "Antras padalinis:");
            Lentele(Dard3, CFr, "Nauji darbuotojai:");

            richTextBox1.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Bendro darbo laiko apskaiciavimo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kiekDirboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("|{0,1}|", new string('-', 72));
                fr.WriteLine("|{0,-36}{1,36}|",
                    "Imones darbotojai is viso dirbo:", KiekDirbo(Dard1) + KiekDirbo(Dard2));
                fr.WriteLine("|{0,1}|", new string('-', 72));
            }
            richTextBox1.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Darbsciausio darbuotojo radimo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void darbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fr = File.AppendText(CFr))
            {
                if(Darbsciausias(Dard1).DarboPabaiga- Darbsciausias(Dard1).DarboPrad>
                    Darbsciausias(Dard2).DarboPabaiga - Darbsciausias(Dard2).DarboPrad)
                {
                    fr.WriteLine("|{0,-19} {1,-39}|", "Darbsciausias imones darbotojas:",Darbsciausias(Dard1).PavVrd);
                }
                else
                {
                    fr.WriteLine("|{0,-19} {1,-39}|", "Darbsciausias imones darbotojas:", Darbsciausias(Dard2).PavVrd);
                }
                fr.WriteLine("|{0,1}|", new string('-', 72));
            }
            richTextBox1.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Pirmuos pamainuos saraso sudarimo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void naujasSąrašasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formuoti(Dard1,Naujas);
            Formuoti(Dard2, Naujas);

            Lentele(Naujas, CFr, "Darbuotojai dirbantis pirmoje pamainoje:");

            richTextBox1.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Saraso rikevimo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rikuotiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Naujas.Burbulas();

            Lentele(Naujas, CFr, "Surikuotas sarasas:");

            richTextBox1.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Saraso papildymo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void papildytiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Papildo(Naujas, Dard3);

            Lentele(Naujas, CFr, "Papildytas sarasas");

            richTextBox1.LoadFile(CFr, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Programos uzdarymo mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baigtiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
