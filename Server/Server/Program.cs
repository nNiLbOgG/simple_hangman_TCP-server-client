using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        int zivoty = 10;

        //Slovnik slovicek
        string[] slovo = { "hora", "dům", "strom", "město", "muž", "žena", "auto", "pes", "kočka", "hlava", "kniha", "pole", "les", "hory", "náměstí", "kolo", "kytka", "řeka", "večer", "ranní", "den", "noc", "rok", "měsíc", "týden", "ulice", "míč", "dítě", "slunce", "mrak", "pláž", "hory", "voda", "stavení", "skála", "práce", "píseň", "stromy", "knihy", "věc", "dárky", "peníze", "škola", "třída", "skříň", "kabelka", "vůz", "okno", "jabko", "mouka", "sůl", "hory", "řeka", "položka", "nápad", "ryba", "zahrada", "balkón", "vítr", "slon", "delfín", "pták", "tráva", "káva", "cukr", "hrad", "cesta", "most", "důl", "sníh", "mráz", "květina", "list", "dešť", "plamen", "broskev", "včela", "med", "houska", "džus", "položka", "loď", "kamión", "letadlo", "dáma", "pán", "rádio", "televize", "film", "příběh", "hudba", "barva", "malba", "brýle", "ruce", "nohy", "vlasy", "kůže", "ocas", "nos", "uši", "pysk", "oči", "jezero", "hora", "řeka", "potok", "les", "louka", "příroda", "květ", "listí", "strom", "lesník", "krmítko", "husa", "rak", "pavouk", "slimák", "včera", "zítra", "dnes", "pohár", "lžíce", "vidlička", "nůž", "hrníček", "šálek", "sklenka", "talíř", "jídlo", "pití", "stůl", "židle", "postel", "polštář", "peřina", "matrace", "obraz", "ručník", "ubrus", "polštářek", "závěs", "zrcadlo", "teploměr", "hodiny", "budík", "telefon", "klávesnice", "myš", "monitor", "tiskárna", "papír", "pero", "tužka", "guma", "nůžky", "lepidlo", "farba", "štětec", "obálka", "razítko", "kalkulačka", "knižník", "knihovna", "knižnice", "stůl", "židle", "lampa", "světlo", "žárovka", "zásuvka", "kabel", "zástrčka", "závit", "matka", "otec", "sourozenci", "rodina", "přítel", "přítelkyně", "manžel", "manželka", "dědeček", "babka", "strýc", "teta", "bratr", "sestra", "syn", "dcera", "kamarád", "kamarádka", "důchodce", "student", "učitel", "lékař", "zdravotní sestra", "zubař", "policista", "hasič", "voják", "umělec", "spisovatel", "herec", "zpěvák", "tanečník", "sportovec", "vědec", "inženýr", "architekt", "advokát", "soudce", "manažer", "prodavač", "ředitel", "vedoucí", "pracovník", "zaměstnanec", "šéf", "kuchař", "číšník", "číšnice", "kadeřník", "řidič", "mechanik", "strojvůdce", "pilot", "námořník", "řemeslník", "prodavačka", "ředitelka", "vedoucíka", "pracovnice", "zaměstnankyně", "šéfka", "kuchařka", "řidička", "strojvůdkyně", "pilotka", "námořnice", "řemeslnice", "dům", "byt", "bytek", "příbytek", "domek", "bouda", "vila", "palác", "sídlo", "hradba", "obora", "věž", "sloup", "sloupek", "klenba", "strop", "podlaha", "schody", "schod", "stoupání", "klesání", "rovnání", "sklon", "svah", "poušť", "jungle", "prales", "pláň", "pohoří", "kopce", "kopeček", "křoví", "trn", "list", "borovice", "dub", "buk", "smrk", "jalovec", "topol", "vrba", "bříza", "javor", "jíva", "lípa", "jabloň", "hrušeň", "broskev", "třešeň", "višeň", "jahoda", "malina", "brambor", "mrkev", "cibule", "rajče", "zelenina"};

        //povolene charaktery
        Regex validCharacters = new Regex("^[a-zA-ZáčďéěíňóřšťúůýžÁČĎÉĚÍŇÓŘŠŤÚŮÝŽ]+$");

     

        IPEndPoint serverovyBod = new IPEndPoint(IPAddress.Parse("192.168.0.107"), 666);
        TcpListener naslouchac = new TcpListener(serverovyBod);

        naslouchac.Start();
        string vybraneSlovo = slovo[new Random().Next(0, slovo.Length - 1)];
        int delkaSlova = vybraneSlovo.Length;

        Console.WriteLine("Server spuštěn na IPv4: " + serverovyBod.Address.ToString() + ":" + serverovyBod.Port);
        Console.WriteLine("");
        Console.WriteLine("");
        Console.Write($"[Písmen: {delkaSlova}] - Slovo: ");

        List<string> pismena = new List<string>();

        byte[] buffer;
        while (zivoty != 0 )
        {
            buffer = new byte[1024];

            int zbylePismena = 0;

            //Zkontroloje jestli je písmeno obsaženo ve slově
            foreach (char character in vybraneSlovo)
            {
                string pismeno = character.ToString();

                if (pismena.Contains(pismeno))
                {
                    Console.Write(pismeno);
                }
                else
                {
                    Console.Write("_");
                    zbylePismena++;
                }
            }
            Console.WriteLine("");

            //Az nebude co hadat tak se hra zastavi 
            if (zbylePismena == 0)
            {
                break;
            }

            Console.WriteLine("");
            Console.Write("Napiš písmeno... ");

            TcpClient odesilatel = naslouchac.AcceptTcpClient();
            odesilatel.GetStream().Read(buffer, 0, buffer.Length).ToString();
            string key = ZiskaniZpravy(buffer);
            Console.WriteLine("");
            Console.WriteLine("Odesílatel " + odesilatel.Client.RemoteEndPoint.ToString() + " napsal: " + key);


            Console.WriteLine("");

            if (!validCharacters.IsMatch(key))
            {
                Console.WriteLine($"Charakter '{key}' nelze použít !");
                continue;
            }

            // Je toto písmeno duplikátem ?
            if (pismena.Contains(key))
            {
                Console.WriteLine("Tohle písmeno již bylo použito !");
                continue;
            }

            //Přidá nové písmeno do listu
            pismena.Add(key);

            //Když je to špatně odebrat 1 živůtek
            if (!vybraneSlovo.Contains(key))
            {
                zivoty--;

                if (zivoty > 0)
                {
                    Console.WriteLine($"Písmeno '{key}' není obsaženo ve slovu. Zbývá '{zivoty}' život/ů...");
                }
            }
        }
    }

    private static string ZiskaniZpravy(byte[] vstupniPole)
    {
        string kNavraceni = "";
        string pomocny = Encoding.UTF8.GetString(vstupniPole);

        foreach (char c in pomocny)
        {
            if (c != '\0')
            {
                kNavraceni += c;
                //kNavraceni = kNavraceni + c;
            }
        }
        return kNavraceni;
    }
}