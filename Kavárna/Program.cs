using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //tento using nám slouží pro ukládání souborů, kontrétně obsahuje třídu streamwriter
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;


//Musím mít dva stejné namespace v programucs i v třídě kavárna
namespace Objednavka
{
    public class Program
    { // Nastavení seznamu objednávek a cenníku
        //Vytvoření seznamu objednávek - vytvořil jsem si pole objednavky ve třídě Objednavka
        static List<Objednavka> objednavky = new List<Objednavka>();

        //Cenník (Název položky a Cena)
        // Static = Člen patří k samotné třídě
        //Dictionary = Jedná se o generickou kolekci, která umožňuje uložení párů klíč-hodnota
        //Vytvoření nového cenníku
        static Dictionary<string, double> cennik = new Dictionary<string, double>()
        { {"Latte",125},
          {"Espresso",90},
          {"Macha",155},
          {"Cappuchino",125},
          {"Picolo",100},

        };
        // Definice proměnné Poslední ID objednávky
        static int PosledniID = 1;

        static void PridaniObjednavky()
        {
            //Aby uživatel viděl čistou obrazovku a věděl, že začíná nová objednávka.
            Console.WriteLine("Přidání nové objednávky");
            //1.  Zadání jméno zákazníka (musíme vědět, pro koho objednávku vytváříme)
            Console.WriteLine("Zadej jméno zákazníka: ");
            //definice proměnné zakaznik
            string zakaznik = Console.ReadLine();
            //2.  Vybrání položek v listu (seznamu), které si zákazník objedná
            //Vytvoření seznamu list - vytvořil jsem si pole list ve namespace Objednávka
            //Připrav si prázdný seznam položek a cenu
            List<string> list = new List<string>();
            // Snulování ceny (cenaCelkem) - cena celé objednávky
            double cenaCelkem = 0;
            // 3. Budeme kontrolovat přidávání dalších položek do objendavky
            bool pridavat = true;
            //Dokud chce zákazník přidávat další věci (káva, croissant...), smyčka poběží.
            //Chceme zákazníkovi umožnit přidat více položek — třeba Latte +Croissant + Espresso.
            while (pridavat)
            {   // vypsání všech důležitých položek a cen z cenníku
                Console.WriteLine("Dostupné položky: ");
                foreach (var polozka in cennik)
                {
                    //$ znamená, že uvnitř závorek {} můžeme přímo vkládat proměnné do textu
                    //polozka je jedna položka z ceníku (Dictionary<string, double>) a .Key znamená název položky.
                    //.Value je hodnota, která patří k tomu názvu - cena dané hodnoty (125kc napriklad)
                    Console.WriteLine($"{polozka.Key}- {polozka.Value} Kč");

                    //4. Ptám se zákazníka, co za položku si chce vybrat
                    // console.wtrite ti to neodradkuje a write line ano
                    Console.Write("Zadejte název polozky (nebo zadjete 'konec' pro ukončení výběru položek): ");
                    // definice proměnné vybranáPolozka a následný výpis této proměnné
                    string vybranaPolozka = Console.ReadLine();

                    // 5. zkontrolujeme, co si zákazník objednal
                    //Pokud zákazník napíše "konec", smyčka bude považována jako false, čímž se ukončí 
                    if (vybranaPolozka.ToLower() == "konec") {
                        pridavat = false;
                    }
                    else {
                        //Contains.Key = používá se hlavně u slovníků (Dictionary<TKey, TValue>), kde chceš zjistit, jestli daný klíč už existuje, než třeba něco přidáš nebo přečteš
                        // Vytvořili jsme si podmínku, abychom zjistili, zda existuje proměnná vybraná polozka v dictionary
                        if (cennik.ContainsKey(vybranaPolozka)) {
                            list.Add(vybranaPolozka);//Přidání proměnné do listu
                            cenaCelkem += cennik[vybranaPolozka];//Přičtení ceny k vybrané položce
                            Console.WriteLine($"{vybranaPolozka} byla přidána do seznamu! ");
                        }
                        else { Console.WriteLine("Položka nebyla nalezena, zkuste to znovu"); }
                    }
                }
            }
            // Vytvoříme si objekt nová objednáka a tím získáme konkrétní model naší objednávky
            // Objekt je skutečný prvek, který se vytvoří podle tohoto plánu a naplní konkrétními daty. Každý objekt je tedy jiný, protože obsahuje jiná data.
            // Když vytváříme objekt novaObjednavka a předáváme mu konkrétní hodnoty (např. ID, jméno zákazníka, seznam položek atd.), tak tento objekt představuje jednu skutečnou objednávku v systému.
            // Pokud používáš proměnnou list (jak jsi ji vytvořil dříve v metodě pro přidání objednávky), měl bys ji použít při vytváření objednávky. Takto budeš předávat seznam položek (list), který jsi už vytvořil dříve.
            // Vytvořím si objekt novaObjednavka s atributy ,které jsem definoval ve třídě objednavka a využíval v metodě PridaniObjednavky!!!
            // A ve tříde objednávky jsme využívali list <string>polozky,takže musíme atribut list využít i v objektu novaObjednavka
            Objednavka novaObjednavka = new Objednavka(PosledniID++, zakaznik, list, cenaCelkem, DateTime.Now);
            objednavky.Add(novaObjednavka);// Přidávám novou objednávku do listu všech objednávek (objednavky)
            // Vypsání potvrzení o přidání objednávky
            Console.WriteLine("Objednávka byla úspěšně přidána");
            Console.WriteLine("Stikněte libovoulnou klávesu pro návrat do menu...");
            Console.ReadLine(); //Console.ReadKey(); znamená: čekej, až uživatel stiskne nějakou klávesu.


        }
        static void VypisVsechObjednavek()
        {   //Zaopatříme, že v případě, když nejsou žádné onjednávky ,program vypíše že žádné nebyly nalezeny
            if (objednavky.Count == 0)
            {
                Console.WriteLine("Nebyly nalezeny žádné objednávky");
            }
            else
            {
                //Pokud program objednavky nalezne, projede je pomocí foreach
                Console.WriteLine("Seznam všech objednávek: ");
                //foreach je cyklus, který prochází každý prvek v kolekci.
                //var objednavka je pomocná proměnná, která představuje jednu objednávku v seznamu objednavky
                foreach (var objednavka in objednavky) {
                    //Vytiskneme informace o každé jedné objednávce!!
                    // ${} znamená: vlož hodnotu proměnné přímo do textu
                    //Atributy vypisujeme přímo z konstruktoru (kde jsou psány velkými počátečními písmeny)
                    Console.WriteLine($"ID: {objednavka.ID}");//Vypíše ID objednávky
                    Console.WriteLine($"Zakaznik: {objednavka.JmenoZakaznika}");//Vypíše jméno zákazníka, který objednávku vytvořil
                    //objednavka.Polozky je seznam(List<string>) — například["Latte", "Croissant"]
                    //string.Join(", ", objednavka.Polozky) spojí všechny položky do jednoho textu odděleného čárkou
                    Console.WriteLine($"Položky: {string.Join(",", objednavka.Polozky) }");
                    Console.WriteLine($"Cena: {objednavka.CenaCelkem} Kč");//Vypíše celkovou cenu za objednávku
                    Console.WriteLine($"Čas: {objednavka.CasObjednavky}");//Vypíše čas vytvoření objednávky
                    //Vytiskne 30krát znak - pod každou objednávku, tak aby byl text přehlednější
                    Console.WriteLine(new string('-',30)); // Oddělovač pro lepší přehlednost
                }
                Console.WriteLine("Stiskněte libovolnou klávesu pro návrat do meny...");
                Console.ReadLine ();
                
            
            }
        }
        static void VyhledaniObjednavkyPodleJmenaZakaznika()
        {
            Console.WriteLine("Zadejte jméno zákazníka pro vyhledání jeho objednavek: ");
            //Program se zastaví a čeká, až uživatel něco napíše na klávesnici a stiskne Enter
            //To, co uživatel napíše, se vezme jako textový řetězec (string).
            //Tento text se uloží do proměnné hledaneJmeno
            string hledaneJmeno = Console.ReadLine();

            //Vyfiltrujeme všechny objednávky podle hledaného jména
            //var znamená, že kompilátor C# sám odhadne typ proměnné podle toho, co jí přiřadíš
            //Where = "Vyber objednávky, kde jméno zákazníka odpovídá tomu, co zadal uživatel."
            //Použití .ToLower() je chytré — když zadáš „HONZA“, „Honza“, nebo „honza“, vždy to správně najde!
            // o => je jedna objednávka o.JmenoZakaznika = vezmeme jméno zákazníka z aktuální objednávky
            // Objednavky = náš List (List<Objednavka>)	= seznam všech objednávek
            //.ToList()	= všechny shodné objednávky uložíme do nového Listu	= Už to není "jen vyfiltrovaný výsledek", ale normální seznam objednávek!!
            var nalezeneObjednavky = objednavky.Where(o => o.JmenoZakaznika.ToLower() == hledaneJmeno.ToLower()).ToList();
            //Pokud je počet nalezených objednávek 0...
            if (nalezeneObjednavky.Count == 0)
            {
                Console.WriteLine("Nebyly nalezeny žádné objednávky pod tímto jménem");
            }
            else
            {
                //Každou položku si postupně uložím do proměnné objednavka a projdu všechny položky v seznamu nalezeneObjednavky a vypíši jejich atributy"
                foreach (var objednavka in nalezeneObjednavky)
                {   // Vytiskneme informace o každé jedné objednávce!!
                    // ${} znamená: vlož hodnotu proměnné přímo do textu
                    //Atributy vypisujeme přímo z konstruktoru (kde jsou psány velkými počátečními písmeny)
                    Console.WriteLine($"ID: {objednavka.ID}");//Vypíše ID objednávky
                    //objednavka.Polozky je seznam(List<string>) — například["Latte", "Croissant"]
                    //string.Join(", ", objednavka.Polozky) spojí všechny položky do jednoho textu odděleného čárkou
                    Console.WriteLine($"Položky: {string.Join(",", objednavka.Polozky)}");
                    Console.WriteLine($"Cena: {objednavka.CenaCelkem} Kč");//Vypíše celkovou cenu za objednávku
                    Console.WriteLine($"Čas: {objednavka.CasObjednavky}");//Vypíše čas vytvoření objednávky
                    //Vytiskne 30krát znak - pod každou objednávku, tak aby byl text přehlednější
                    Console.WriteLine(new string('-', 30)); // Oddělovač pro lepší přehlednost
                }

            }
            Console.WriteLine("Pro vrácení do meny stiskněre libovolnou klávesu");
            Console.ReadLine ();
          }
        static void VypisDenniZisk()
        {
            //Sečte všechny ceny (CenaCelkem) ze seznamu objednávek
            //Říkáme: "Vem z každé objednávky její cenu a tu sečti"
            //Sečtený denní zisk se poté uloží do proměnné denniZisk
            double denniZisk = objednavky.Sum(objednavka => objednavka.CenaCelkem);
            Console.WriteLine($"\n Celkový denní zisk je: {denniZisk}Kč");//Vypíše celkový denní zisk z uložené úproměnné
            Console.ReadKey();//Počká na stisk klávesy, aby si uživatel mohl přečíst výsledek
        }
        static void UlozitObjednavkyDoSouboru ()
        {
            Console.WriteLine("Zadejte název souboru pro uložení (např. objednavky.txt): ");
            // Založili jsme si novou proměnnou a vyčkáváme, dokud uživatel nenapíše název souboru a nestiskne enter
            string soubor = Console.ReadLine();
            //StreamWriter je třída v .NET, která slouží k zápisu textových dat do souboru
            //V tomto případě se StreamWriter inicializuje s názvem souboru, který je uložen v proměnné soubor. Tento soubor bude vytvořen, pokud ještě neexistuje.
            using (StreamWriter writer = new StreamWriter(soubor)) {
                //Tento cyklus foreach prochází seznam objednavky, což je seznam všech objednávek v aplikaci
                //Každý prvek v tomto seznamu je instance třídy Objednavka, která obsahuje informace o jedné konkrétní objednávce. V každé iteraci cyklu se do proměnné objednavka přiřadí jeden objekt Objednavka.
                foreach (var objednavka in objednavky) 
                {
                    //Uvnitř cyklu foreach používáme metodu WriteLine z StreamWriter, která slouží k zápisu jednoho řádku do souboru. Tento řádek bude obsahovat informace o objednávce.
                    writer.WriteLine($"{objednavka.ID};{objednavka.JmenoZakaznika};{string.Join(",", objednavka.Polozky)};{objednavka.CenaCelkem};{objednavka.CasObjednavky}");
                    //1. Toto je ID objednávky, které je celkové číslo(int).Bude zapsáno jako první hodnota na řádku.
                    //2. Toto je jméno zákazníka, který objednávku vytvořil. Tato hodnota bude zapsána jako druhá položka na řádku.
                    //3. objednavka.Polozky je seznam položek objednávky (List<string>), tedy seznam řetězců, které reprezentují názvy položek.
                    //   string.Join(",", objednavka.Polozky) vezme všechny položky v seznamu Polozky a spojí je do jednoho řetězce, kde jednotlivé položky budou odděleny čárkou
                    //4. Toto je cena objednávky (double), která bude zapsána jako čtvrtá hodnota na řádku
                    //5. Toto je čas, kdy byla objednávka vytvořena (DateTime). Výchozí formát pro DateTime je například 2025-04-23 14:30:00
                    // KAŽDOU POLOŽKU ODDĚLÍME STŘEDNÍkEM!! = běžně používaný formát pro export dat
                    // Jakmile blok using skončí, StreamWriter automaticky zavře soubor, čímž se zajistí, že všechny změny jsou správně uloženy.
                }
            }
            Console.WriteLine("Objednávky byly úspěšně uloženy");
            Console.ReadLine ();
            

        }


        static void NacteniObjednavekZeSouboru()
        {
            Console.WriteLine("Zadejte název souboru pro jeho načtení(např. objednavky.txt)");
            // Založili jsme si novou proměnnou a vyčkáváme, dokud uživatel nenapíše název souboru a nestiskne enter
            string soubor = Console.ReadLine();

            //Podmínka, pokud soubor nebude existovat...
            //Zjistí, jestli na dané cestě (soubor) existuje soubor
            //A vykřičník ! znamená negaci = "ne".
            //Kontroluješ, jestli soubor opravdu existuje.
            if (!File.Exists(soubor))
            {
                Console.WriteLine("Soubor neexistuje");
                //Čeká na stisk klávesy od uživatele!!!
                Console.ReadKey();
                //Ukončí tuto metodu (NacistObjednavkyZeSouboru) okamžitě po zmačnutí libovolné klávesy
                //Už nepokračuješ dál (nezkoušíš načítat soubor, který neexistuje)
                return;

            }
            objednavky.Clear();// smaže staré objednávky
            //Přečteš všechny řádky ze souboru najednou do pole radky (string[]).
            //Každý řádek je jedna objednávka ve formátu:ID;JmenoZakaznika;Polozky,CenaCelkem,CasObjednavky
            var radky = File.ReadAllLines (soubor);
            //Procházíš všechny řádky (foreach) a každý řádek rozdělíš podle středníku ;
            //radek je jeden řádek textu načtený ze souboru
            //radek.Split(';'); "Rozděl text na části podle oddělovače ;." ==== Kdykoliv najdeš středník ;, rozdělíš text a vytvoříš z toho pole (string[]).
            foreach (var radek in radky) 
            {
                //Teď máš pole casti
                //Split(';') vrací pole řetězců (string[]), takže casti je pole stringů (tedy string[]).

                /*Když načítáš data ze souboru (řádek jako text), získáš je jako řetězce.
                  Tyto řetězce pak musíš přetypovat na správné datové typy (např. int, double, DateTime) a strukturovat je do objektu, aby byly lépe použitelné a manipulovatelné v aplikaci.*/



                var casti = radek.Split(';');
                // Proměnná id teď drží hodnotu 1.
                int id = int.Parse(casti[0]);//int.Parse("1") ➔ převede řetězec na celé číslo 1.
                //Proměnná jmenoZakaznika obsahuje "Petr Novak".
                string jmenoZakaznika = casti[1];//Jednoduše si vezmeš druhou část (casti[1]), což je jméno zákazníka "Petr Novak"
                //casti[2] je text "Pizza,Burger,Cola"
                //.Split(';') rozdělí řetězec podle středníku === Kdykoliv najdeš čárku, , rozdělíš text a vytvoříš z toho pole (string[]).
                //.ToList() převede pole (string[]) na seznam (List<string>)
                // Proměnná polozky bude seznam těchto položek.
                //Teď, když chceš rozdělit seznam položek(Pizza, Burger, Cola), musíš rozdělit podle čárky,, ne podle středníku;.
                List<string> polozky = casti[2].Split(',').ToList();
                //casti[3] je text "450.5" ==== double.Parse("450.5") ➔ převede na číslo 450.5 (typu double).
                double cenaCelkem = double.Parse(casti[3]);
                //casti[4] je text "2025-04-22T18:30:00" === DateTime.Parse rozpozná tento formát a vytvoří z toho datum a čas
                DateTime casObjednavky = DateTime.Parse(casti[4]);
                //Po těchto operacích máš všechna data připravená na vytvoření nové objednávky
                //Vytváříš novou instanci (objekt) třídy Objednavka, protože potřebuješ přetvořit surová data (přečtená ze souboru) na konkrétní objekt, který bude mít všechny potřebné atributy a metody pro správu objednávky
                //Tento objekt novaObjednavka pak přidáš do seznamu objednávek (objednavky), což ti umožní s těmito objednávkami dál pracovat v aplikaci.
                //Řetězec je pouze text, ale objekt Objednavka je struktura, která obsahuje všechny potřebné informace, které ti umožní snadno manipulovat s daty, např
                Objednavka novaObjednavka = new Objednavka(id, jmenoZakaznika, polozky, cenaCelkem, casObjednavky);
                objednavky.Add(novaObjednavka);
                //Všechno musí být ve stejném { ... } bloku, jinak proměnné nejsou "vidět"!!!!
            }
            Console.WriteLine("Objednávky byly úspěšně načteny!");
            Console.ReadLine();





        }
        static void Main(string[] args)
        { //Vytvoření hlavního menu
            bool pokracovat = true;
            // vytvoření smyčky while
            //Kód, ketrý se vykonává, dokud je podmínka pravdivá
            while (pokracovat)
            {
                //Slouží k vyčištění obrazovky
                Console.Clear();
                Console.WriteLine("Hlavní menu");
                Console.WriteLine("1. Přidat novou objednávku: ");
                Console.WriteLine("2. Vypsat všechny objednávky: ");
                Console.WriteLine("3. Vyhledat všechny objednávky podle jména zákazníka: ");
                Console.WriteLine("4. Vypsat celkový denní zisk: ");
                Console.WriteLine("5. Uložit objednávky do souboru: ");
                Console.WriteLine("6. Načíst objednávky ze souboru: ");
                Console.WriteLine("7. Ukončit program");

                Console.WriteLine("Zadejte číslo volby: ");
                //definice proměnné vobla a její následné přečtení
                string volba = Console.ReadLine();
                switch (volba)
                {
                    case "1":
                        //Přidáné objednavky
                        PridaniObjednavky();
                        break;
                    case "2":
                        //Vypsani všech objednavek
                        VypisVsechObjednavek();
                        break; 
                    case "3":
                        //Vyhledání podle jména
                        VyhledaniObjednavkyPodleJmenaZakaznika();
                        break;
                    case "4":
                        // Výpis denního zisku
                        VypisDenniZisk();
                        break;
                    case "5":
                        //Uložení objednávek do souboru
                        UlozitObjednavkyDoSouboru();
                        break;
                    case "6":
                        //Načtení objednávej ze souboru
                        NacteniObjednavekZeSouboru();
                        break;
                    case "7":
                        pokracovat = false; //Ukončení meny (ukončení cyklu while)
                        break;
                    default: Console.WriteLine("Neplatná volba, zkuste to znovu!");
                             Console.ReadKey();
                        break;    
                }
            }

        }
    }
}
