using System;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace AhoyBrowser
{
    class PasswordGeneratorB
    {
        private static List<string> LongWords { get; set; }
        private static List<string> ShortWords { get; set; }
        private static List<string> Symbols { get; set; }
        private static List<string> GeneratedPasswords { get; set; }
        private static List<string> UsedLongWords { get; set; }
        private static List<string> UsedShortWords { get; set; }

        private static void InitializeGenerator()
        {
            if (GeneratedPasswords == null)
            {
                GeneratedPasswords = new List<string>();
                UsedLongWords = new List<string>();
                UsedShortWords = new List<string>();

                string[] longWords = { "Rullepolse", "Tastatur", "Mudder", "Flettet", "Langtids", "Simpel", "Knallert", "Afmeldt", "Batteri", "Uartig", "Frydelig", "Banjo", "Flydende", "Ensporet", "Alvorlig", "Bacon", "Boglig", "Elektro", "Halv", "Beskidt", "Brunkage", "Reserve", "Gymnastik", "Opdateret", "Tango", "Cirkus", "Roterende", "Skod", "Huslig", "Stjerne", "Plys", "Marcipan", "Dukke", "Spejl", "Teleskop", "Uendelig", "Konstant", "Heldig", "Gylden", "Rusten", "Tilmeldt", "Smeltet", "Slidt", "Smattet", "Hygge", "Gammel", "Trendy", "Sulten", "Generator", "Omvendt", "Luksus", "Plastik", "Orgel", "Smagfuld", "Mekanisk", "Mindre", "Skjult", "Filter", "Gemt", "Kroget", "Finurlig", "Lykkelig", "Pyntet", "Nydelig", "Holografisk", "Dynamisk", "Venlig", "Software", "Fedte", "System", "Turbo", "Melodisk", "Harmonisk", "Indbagt", "Foto", "Diesel", "Melon", "Automatisk", "Hotel", "Festlig", "Camping", "Naturlig", "Frisk", "Rodvin", "Perle", "Postej", "Salat", "Budding", "Flittig", "Suppe", "Peber", "Ferie", "Dygtig", "Flagrende", "Glimrende", "Kedelig", "Glad", "Flad", "Flaske", "Ballon", "Billed", "Favorit", "Tidlig", "Banan", "Mystisk", "Nisse", "Eventyr", "Kartoffel", "Familie", "Gravko", "Rejsende", "Cykel", "Billig", "Brugt", "Radiator", "Diktafon", "Mikrofon", "Badehat", "Super", "Fantastisk", "Utrolig", "Specifik", "Stolt", "Diplomatisk", "Modig", "Bagvendt", "Varm", "Kold", "Smuk", "Traktor", "Termostat", "Metal", "Stativ", "Magnetisk", "Motorcykel", "Klattet", "Kran", "Grafik", "Grafisk", "Gummi", "Hjemmebagt", "Hjemmelavet", "Hjemmesko", "Disco", "Telefon", "Tele", "Digital", "Analog", "Bambus", "Firkantet", "Kantet", "Rund", "Komplet", "Moderat", "Smart", "Motor", "Sommer", "Vinter", "Danse", "Hoppe", "Pedal", "Grisk", "Lakrids", "Ballet", "Dekoreret", "Lang", "Kort", "Planet", "Arkiv", "Taxa", "Ung", "Grill", "Transport", "Grundig", "Kompleks", "Cigaret", "Tandpasta", "Kanel", "Svedig", "Kuffert", "Panel", "Solsikke", "Sukker", "Baby", "Bingo", "Jern", "Guld", "Yndig", "Diamant", "Dramatisk", "Fin", "Stor", "Lille", "Slem", "Grim", "Elektrisk", "Gardin", "Reol", "Salt", "Syrlig", "Glas", "Pap", "Lidt", "Trekantet", "Mager", "Hurtig", "Strikke", "Krystal", "Fugtig", "Langsom", "Fornem", "Trolde", "Orkester", "Synlig", "Usynlig", "Lystig", "Trylle", "Krydret", "Magisk", "Forstenet", "Mester", "Bagt", "Portal", "Snorkel", "Mug", "Muggen", "Fuld", "Flov", "Tom", "Keramik", "Sulfo", "Spejder", "Sove", "Alarm", "Egoist", "Hummer", "Mormor", "Honning", "Farmor", "Morfar", "Farfar", "Mose", "Klog", "Luft", "Saxofon", "Elevator", "Knogle", "Lommeregner", "Trampe", "Trang", "Travl", "Ivrig", "Kontor", "Kontakt", "Teknisk", "Antenne", "Parabol", "Universal", "Panorama", "Rulle", "Skrap", "Gnaven", "Kunstig", "Dadel", "Kalkulator", "Kalkulator" };
                LongWords = new List<string>(longWords);

                string[] shortWords = { "Knap", "Agurk", "Krabbe", "Julemand", "Julegave", "Filur", "Service", "Fornemmelse", "Kvittering", "Uorden", "Marcipan", "Student", "Ben", "Test", "Snegl", "Boble", "Sylte", "Astronaut", "Data", "Fletning", "Plakat", "Lager", "Udsalg", "Depot", "Tekst", "Elektronik", "Appelsin", "Kopi", "Bestik", "Betjening", "Handel", "Stel", "Hytte", "Mave", "Talent", "Marmelade", "Heks", "Fordel", "Oplader", "Sandal", "Bowling", "Punktum", "Tegning", "Tunge", "Printer", "Tidsskrift", "Hyrde", "Hukommelse", "Blyant", "Sport", "Parfume", "Karneval", "Person", "Koncert", "Elastik", "Stige", "Tandhjul", "Trappe", "Element", "Indgang", "Udgang", "Metervare", "Kaptajn", "Program", "Kageform", "Pibe", "Kammerat", "Kalkun", "Kylling", "Kanin", "Prop", "Skammel", "Ost", "Kat", "Hund", "Hest", "Hylster", "Sav", "Skrue", "Lygte", "Hammer", "Ven", "Aftensmad", "Besked", "Automat", "Altan", "Pind", "Hjelm", "Post", "Brev", "Harmonika", "Kuvert", "Fridag", "Aften", "Weekend", "Eftermiddag", "Ugle", "Pilsner", "Aftale", "Frokost", "Lyd", "Fisk", "Feber", "Reje", "Dessert", "Onkel", "Vest", "Kalender", "Papir", "Skolemad", "Maske", "Avis", "Kage", "Design", "Sko", "Garn", "Snylter", "Gaffel", "Skoleradio", "Spade", "Stegepande", "Mad", "Spand", "Klub", "Krone", "Regel", "Protest", "Klasse", "Historie", "Klap", "Agenda", "Stempelur", "Frost", "Fest", "Fugl", "Kassette", "Folkesang", "Regn", "Bamse", "Myre", "Rejse", "Kop", "Kurv", "Papkasse", "Stue", "Hus", "Ovn", "Myggen", "Fantasi", "Viking", "Stol", "Tobak", "Brille", "Stok", "Plaster", "Burger", "Apparat", "Tallerken", "Buks", "Hat", "Munk", "Parkering", "Mus", "Bord", "Kasket", "Kompas", "Konge", "Dronning", "Olding", "Hegn", "Jakke", "Lomme", "Skuffe", "Hjul", "Giraf", "Maskine", "Skib", "Taske", "Aroma", "Sang", "Melodi", "Bog", "Kaffe", "Elefant", "Klud", "Ansigt", "Baron", "Robot", "Drik", "Arm", "Plante", "Gryde", "Greve", "Sofa", "Chips", "Komedie", "Teater", "Pude", "Dyne", "Flue", "Harpe", "Pingvin", "Kiosk", "Slange", "Tromme", "Dragt", "Okse", "Trompet", "Udsigt", "Sol", "Arrangement", "Maane", "Maling", "Klovn", "Figur", "Fod", "Indhold", "Skipper", "Rose", "Basun", "Pakke", "Lager", "Trold", "Drue", "Uld", "Lampe", "Flod", "Storm", "Bjerg", "Loft", "Mand", "Dame", "Snaps", "Mund", "Pianist", "Badekar", "Potte", "Funktion", "Rubin", "Arbejde", "Kanal", "Port", "Hjerte", "Hjerne", "Klon", "Firben", "Snude", "Vase", "Rytter", "Kringle", "Skorsten", "Hveps", "Biograf", "Opvask", "Pose", "Ventil", "Larm", "Ur", "Kikkert", "Radio", "Restaurant", "Stav", "Kam", "Kabine", "Gang", "Klokke", "Leverpostej", "Vindue", "Vinstue", "Bar", "Duft", "Butik", "Bluse", "Pomfrit", "Guitar", "Violin", "Bruger", "Kunde", "Klaver", "Tomat", "Bro", "Fisker", "Instrument", "Saddel", "Skov", "Skovl", "Skrift", "Digter", "Poesi", "Negl", "Horn", "Genbrug", "Saks", "Tunnel", "Citron", "Citron" };
                ShortWords = new List<string>(shortWords);

                string[] symbols = { "!", ".", "!", "!", "-", ".", ".", "-", "-", ".", "." };
                Symbols = new List<string>(symbols);
            }
        }

        private static string tempGeneratedLongWord;
        private static string GetRandomLongWord()
        {
            do
            {
                tempGeneratedLongWord = LongWords[RandomNumberGenerator.GetInt32(0, LongWords.Count)];
            }
            while (LongWordExists(tempGeneratedLongWord));

            UsedLongWords.Add(tempGeneratedLongWord);
            return tempGeneratedLongWord;
        }

        private static string tempGeneratedShortWord;
        private static string GetRandomShortWord()
        {
            do
            {
                tempGeneratedShortWord = ShortWords[RandomNumberGenerator.GetInt32(0, ShortWords.Count)];
            }
            while (ShortWordExists(tempGeneratedShortWord));

            UsedShortWords.Add(tempGeneratedShortWord);
            return tempGeneratedShortWord;
        }

        private static bool tempLongWordExists;
        private static bool LongWordExists(string longWord)
        {
            tempLongWordExists = false;
            if (UsedLongWords.Count > 0)
            {
                foreach (string s in UsedLongWords)
                {
                    if (s == longWord)
                    {
                        tempLongWordExists = true;
                        break;
                    }
                }
            }
            return tempLongWordExists;
        }

        private static bool tempShortWordExists;
        private static bool ShortWordExists(string shortWord)
        {
            tempShortWordExists = false;
            if (UsedShortWords.Count > 0)
            {
                foreach (string s in UsedShortWords)
                {
                    if (s == shortWord)
                    {
                        tempShortWordExists = true;
                        break;
                    }
                }
            }
            return tempShortWordExists;
        }

        // Return 9 different passwords
        private static string tempPasswordText;
        public static string GetPasswords(bool useSpecialChars)
        {
            InitializeGenerator();
            tempPasswordText = "";
            GeneratedPasswords.Clear();
            UsedLongWords.Clear();
            UsedShortWords.Clear();

            for (int i = 0; i < 9; i++)
            {
                if (useSpecialChars)
                {
                    GeneratedPasswords.Add(GetRandomLongWord() + Symbols[RandomNumberGenerator.GetInt32(0, Symbols.Count)] + GetRandomShortWord() + RandomNumberGenerator.GetInt32(30, 99).ToString("00"));
                }
                else
                {
                    GeneratedPasswords.Add(GetRandomLongWord() + GetRandomShortWord() + RandomNumberGenerator.GetInt32(30, 99).ToString("00"));
                }
               
            }

            tempPasswordText = "";
            foreach (string s in GeneratedPasswords)
            {
                tempPasswordText += s + Environment.NewLine;
            }

            return tempPasswordText;
        }

    }

}
