using System;

namespace zgadujLiczbe
{
    internal class Program
    {

        static string[] top5 = new string[5];    //globalna zmienna przech. 5 najlepszych wyników

        static void Main(string[] args)
        {
            int x, kroki, punktyZaKroki, punkty; // x = losowa liczba, zwrot z metody Losuj 
                                                 // kroki = zwrot z metody DecydujKroki (liczba zadeklarowanych kroków) 
                                                 // punktyZaKroki = zwrot z metody DecydujPunkty (liczba dodatkowych punktów =100/kroki
                                                 // punkty = zwrot z metody Wynik (suma punktów)
            int[] wynikiTab = new int[2];        // zwrot z metody Zgaduj (indeks 0 = liczba wykonanych kroków,indeks 1 = wygrana (1) lub przegrana (0)
            bool jeszczeRaz=false;              
            string nowaGra;                      
            
            UtworzRanking();                    // tworzy plik rankingu top5 ranking.txt jeżeli nie istnieje i wpisuje 5 linii z zerami

            Console.WriteLine("PROGRAM WYLOSOWAŁ LICZBĘ Z ZAKRESU 0-99. SPRÓBUJ JĄ ODGADNĄĆ.");
            
            do
            {
                x = Losuj();
                Console.WriteLine();
                kroki = DecydujKroki();
                punktyZaKroki = DecydujPunkty(kroki);
                wynikiTab = Zgaduj(x, kroki);
                if (wynikiTab[1] == 1)
                {
                    punkty = Wynik(punktyZaKroki, wynikiTab[0]);
                    Ranking(punkty);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"NIE UDAŁO CI SIĘ ODGADNĄĆ LICZBY {x}. SPRÓBUJ JESZCZE RAZ");
                }
                Console.WriteLine();
                Console.WriteLine("CZY CHCESZ ZAGRAĆ JESZCZE RAZ ? (T)AK, (R)ANKING");
                nowaGra = Console.ReadLine();
                if (nowaGra == "R" || nowaGra == "r")
                {
                    Console.WriteLine();
                    PokazRanking();
                    Console.WriteLine();
                    Console.WriteLine("CZY CHCESZ ZAGRAĆ JESZCZE RAZ ? (T)AK");
                    nowaGra = Console.ReadLine();
                    if (nowaGra == "T" || nowaGra == "t")
                    {
                        jeszczeRaz = true;
                        Console.Clear();
                    }
                    else
                    {
                        jeszczeRaz = false;
                        Console.WriteLine("DZIĘKI ZA GRĘ.");
                    }
                }
                else if (nowaGra == "T" || nowaGra == "t")
                {
                    jeszczeRaz = true;
                    Console.Clear();
                }
                else
                {
                    jeszczeRaz = false;
                    Console.WriteLine("DZIĘKI ZA GRĘ.");
                }
            }
            while (jeszczeRaz == true);
        }

        
        static int Losuj()
        {
            int x;
            Random random = new Random();
            x = random.Next(0, 100);
            return x;
        }

        static int DecydujKroki()
        {
            int decydujKroki = -1; // ???
            bool zleKroki = false;

            Console.WriteLine("ZADEKLARUJ MAKSYMALNĄ LICZBĘ KROKÓW, W JAKIEJ POSTARASZ SIĘ ODGADNĄĆ LICZBĘ.\nUZYSKASZ ZA TO DODATKOWE PUNKTY (100/liczba kroków).");
            do
            {
                try
                {
                    decydujKroki = int.Parse(Console.ReadLine());
                    zleKroki = false;
                }
                catch
                {
                    Console.WriteLine("WPROWADŹ LICZBĘ CAŁKOWITĄ <1-100>.");
                    zleKroki = true;
                    continue;
                }

                if (decydujKroki < 1 || decydujKroki > 100)
                {
                    Console.WriteLine("WPROWADŹ POPRAWNĄ LICZBĘ <1-100>.");
                    zleKroki = true;
                }
             }
            while (zleKroki == true);

            return decydujKroki;
        }
                      
        static int DecydujPunkty(int decydujKroki)
        {          
            int decydujPunkty;
            decydujPunkty = 100 / decydujKroki;
         
            if (decydujPunkty == 1)
            {
                Console.WriteLine($"DODATKOWO OTRZYMASZ {decydujPunkty} PUNKT." );
            }
            else if (decydujPunkty == 12 || decydujPunkty == 13 || decydujPunkty == 14)
            {
                Console.WriteLine($"DODATKOWO OTRZYMASZ {decydujPunkty} PUNKTÓW.");
            }
            else if (decydujPunkty % 10 == 2 || decydujPunkty % 10 == 3 || decydujPunkty % 10 == 4)
            {
                Console.WriteLine($"DODATKOWO OTRZYMASZ {decydujPunkty} PUNKTY.");
            }
            else Console.WriteLine($"DODATKOWO OTRZYMASZ {decydujPunkty} PUNKTÓW.");

            Console.WriteLine();

            if (decydujKroki % 10 == 1)
            {
                Console.WriteLine($"MASZ {decydujKroki} PRÓBĘ. ZACZYNAMY.");
            }
            else if (decydujKroki == 12 || decydujKroki == 13 || decydujKroki == 14)
            {
                Console.WriteLine($"MASZ {decydujKroki} PRÓB. ZACZYNAMY.");
            }
            else if (decydujKroki % 10 == 2 || decydujKroki % 10 == 3 || decydujKroki % 10 == 4)
            {
                Console.WriteLine($"MASZ {decydujKroki} PRÓBY. ZACZYNAMY.");
            }
            else Console.WriteLine($"MASZ {decydujKroki} PRÓB. ZACZYNAMY.");

            return decydujPunkty;
        }

        static int[] Zgaduj(int x, int decydujKroki)
        {
            int strzal=-1; // ???
            int krok, wygrana = 0; // 0 = przegrana, 1=wygrana
            int[] wynik = new int[2];
            bool zlyStrzal = false;
            for (krok = 1; krok<= decydujKroki; krok++)
            {

                do
                {
                    Console.Write($"PRÓBA {krok}: ");
                    try
                    {
                        strzal = int.Parse(Console.ReadLine());
                        zlyStrzal = false;
                    }
                    catch
                    {
                        Console.WriteLine("WPROWADŹ LICZBĘ CAŁKOWITĄ <0-99>.");
                        zlyStrzal = true;
                        continue;
                    }
                    if (strzal < 0 || strzal > 99)
                    {
                        Console.WriteLine("WPROWADŹ POPRAWNĄ LICZBĘ <0-99>.");
                        zlyStrzal = true;
                    }
                }
                while (zlyStrzal == true);

                if (strzal == x)
                {
                    Console.WriteLine();
                    Console.WriteLine($"ZGADŁEŚ. WYLOSOWANA LICZBA TO {x}.");
                    wygrana = 1;
                    break;
                }
                else
                {
                    if (strzal > x)
                    {
                        Console.WriteLine("ZŁY STRZAŁ. WYLOSOWANA LICZBA JEST MNIEJSZA.");
                    }
                    else Console.WriteLine("ZŁY STRZAŁ. WYLOSOWANA LICZBA JEST WIĘKSZA.");
                }
            }
                wynik[0] = krok;
                wynik[1] = wygrana;

            return wynik;
        }
        static int Wynik(int decydujPunkty, int zgadujKrok)
        {
            int punkty, punktyZaSzybkosc;
            if (zgadujKrok <= 10)
            {
                punktyZaSzybkosc = 11 - zgadujKrok;
            } 
            else punktyZaSzybkosc = 0;
            punkty = 10 + decydujPunkty + punktyZaSzybkosc;
            Console.WriteLine();
            Console.WriteLine("PUNKTY ZA ODGADNIĘCIE\t10.");
            Console.WriteLine($"PUNKTY DODATKOWE\t{decydujPunkty}.");
            Console.WriteLine($"PUNKTY ZA SZYBKOŚĆ\t{punktyZaSzybkosc}.");
            Console.WriteLine($"RAZEM\t{punkty}.");

            return punkty;
        }
        static void UtworzRanking()
        {
            int i = 0;

            if (!File.Exists("ranking.txt"))
            {
                FileStream fs = new FileStream("ranking.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                for (int z = 0; z < 4; z++)
                {
                    sw.WriteLine("0");
                }
                sw.Write("0");
                sw.Close();
                fs.Close();
            }

            foreach (string linia in File.ReadLines("ranking.txt"))
            {
                top5[i] = linia;
                i++;
            }

        }

        static void Ranking(int wynikPunkty)
        {
            int l = 0;
                   
            Console.WriteLine();

            if (wynikPunkty <= int.Parse(top5[4]))
            {
                Console.WriteLine("TWÓJ WYNIK NIE JEST W TOP5.");
            }
            else if (wynikPunkty > int.Parse(top5[0]))
            {
                top5[4] = top5[3];
                top5[3] = top5[2];
                top5[2] = top5[1];
                top5[1] = top5[0];
                top5[0] = wynikPunkty.ToString();
                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA 1 MIEJSCU.");
            }
            else
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (wynikPunkty <= int.Parse(top5[j]))
                    {
                        switch (j)
                        {
                            case 3:
                                top5[j + 1] = wynikPunkty.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;
                            case 2:
                                top5[j + 2] = top5[j + 1];
                                top5[j + 1] = wynikPunkty.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;
                            case 1:
                                top5[j + 3] = top5[j + 2];
                                top5[j + 2] = top5[j + 1];
                                top5[j + 1] = wynikPunkty.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;
                            case 0:
                                top5[j + 4] = top5[j + 3];
                                top5[j + 3] = top5[j + 2];
                                top5[j + 2] = top5[j + 1];
                                top5[j + 1] = wynikPunkty.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;

                        }
                        break;

                    }
                }
            }
            FileStream fs = new FileStream("ranking.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            for (int y = 0; y < 4; y++)
            {
                sw.WriteLine(top5[y]);
            }
            sw.Write(top5[4]);
            sw.Close();
            fs.Close();

            Console.WriteLine();

            foreach (string linia in File.ReadLines("ranking.txt"))
            {
                top5[l] = linia;
                Console.WriteLine(top5[l]);
                l++;
            }

        }

        static void PokazRanking()
        {
            int l = 0;
            foreach (string linia in File.ReadLines("ranking.txt"))
            {
                Console.WriteLine(linia);
                l++;
            }
        }



    }
}