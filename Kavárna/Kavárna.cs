using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objednavka
{
    //public class je přístupná ve všech projektech a internal class je přístupná pouze v daném projektu
    //zadáme rovnou i getery a setery
    public class Objednavka
    {
        public int ID { get; set; }
        public string JmenoZakaznika { get; set; }
        // seznam položek ve formě text
        public List<string> Polozky { get; set; }
        public double CenaCelkem { get; set; }
        public DateTime CasObjednavky { get; set; }

        // konstruktor
        // automaticky připraví seznam položek, aby se s ním dalo pracovat
        public Objednavka (int iD, string jmenoZakaznika, List<string> polozky, double cenaCelkem, DateTime casObjednavky)
        {
            ID = iD;
            JmenoZakaznika = jmenoZakaznika;
            Polozky = polozky;
            CenaCelkem = cenaCelkem;
            CasObjednavky = casObjednavky;
        }

    }
}
