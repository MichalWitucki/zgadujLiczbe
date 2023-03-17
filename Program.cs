using System;

namespace guessNumber
{
    internal class Program
    {

        static string[] top5 = new string[5];    //globalna zmienna przech. 5 najlepszych wyników

        static void Main(string[] args)
        {
            int numberToGuess, steps, stepsPoints, points; // x = losowa liczba, zwrot z metody Losuj 
                                                 // kroki = zwrot z metody DecydujKroki (liczba zadeklarowanych kroków) 
                                                 // punktyZaKroki = zwrot z metody DecydujPunkty (liczba dodatkowych punktów =100/kroki
                                                 // punkty = zwrot z metody Wynik (suma punktów)
            int[] resultTable = new int[2];        // zwrot z metody Zgaduj (indeks 0 = liczba wykonanych kroków,indeks 1 = wygrana (1) lub przegrana (0)
            bool onceAgain=false;
            string newGame;                      
            
            CreateRanking();                    // tworzy plik rankingu top5 ranking.txt jeżeli nie istnieje i wpisuje 5 linii z zerami

            Console.WriteLine("PROGRAM WYLOSOWAŁ LICZBĘ Z ZAKRESU 0-99. SPRÓBUJ JĄ ODGADNĄĆ.");
            
            do
            {
                numberToGuess = Randomize();
                Console.WriteLine();
                steps = DecideSteps();
                stepsPoints = DecidePoints(steps);
                resultTable = Guess(numberToGuess, steps);
                if (resultTable[1] == 1)
                {
                    points = Result(stepsPoints, resultTable[0]);
                    Ranking(points);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"NIE UDAŁO CI SIĘ ODGADNĄĆ LICZBY {numberToGuess}. SPRÓBUJ JESZCZE RAZ");
                }
                Console.WriteLine();
                Console.WriteLine("CZY CHCESZ ZAGRAĆ JESZCZE RAZ ? (T)AK, (R)ANKING");
                newGame = Console.ReadLine();
                if (newGame == "R" || newGame == "r")
                {
                    Console.WriteLine();
                    ShowRanking();
                    Console.WriteLine();
                    Console.WriteLine("CZY CHCESZ ZAGRAĆ JESZCZE RAZ ? (T)AK");
                    newGame = Console.ReadLine();
                    if (newGame == "T" || newGame == "t")
                    {
                        onceAgain = true;
                        Console.Clear();
                    }
                    else
                    {
                        onceAgain = false;
                        Console.WriteLine("DZIĘKI ZA GRĘ.");
                    }
                }
                else if (newGame == "T" || newGame == "t")
                {
                    onceAgain = true;
                    Console.Clear();
                }
                else
                {
                    onceAgain = false;
                    Console.WriteLine("DZIĘKI ZA GRĘ.");
                }
            }
            while (onceAgain == true);
        }

        
        static int Randomize()
        {
            int randomNumber;
            Random random = new Random();
            randomNumber = random.Next(0, 100);
            return randomNumber;
        }

        static int DecideSteps()
        {
            int decideSteps = -1; // ???
            bool incorrectSteps = false;

            Console.WriteLine("ZADEKLARUJ MAKSYMALNĄ LICZBĘ KROKÓW, W JAKIEJ POSTARASZ SIĘ ODGADNĄĆ LICZBĘ.\nUZYSKASZ ZA TO DODATKOWE PUNKTY (100/liczba kroków).");
            do
            {
                try
                {
                    decideSteps = int.Parse(Console.ReadLine());
                    incorrectSteps = false;
                }
                catch
                {
                    Console.WriteLine("WPROWADŹ LICZBĘ CAŁKOWITĄ <1-100>.");
                    incorrectSteps = true;
                    continue;
                }

                if (decideSteps < 1 || decideSteps > 100)
                {
                    Console.WriteLine("WPROWADŹ POPRAWNĄ LICZBĘ <1-100>.");
                    incorrectSteps = true;
                }
             }
            while (incorrectSteps == true);

            return decideSteps;
        }
                      
        static int DecidePoints(int decideSteps)
        {          
            int decidePoints;
            decidePoints = 100 / decideSteps;
         
            if (decidePoints == 1)
            {
                Console.WriteLine($"DODATKOWO OTRZYMASZ {decidePoints} PUNKT." );
            }
            else if (decidePoints == 12 || decidePoints == 13 || decidePoints == 14)
            {
                Console.WriteLine($"DODATKOWO OTRZYMASZ {decidePoints} PUNKTÓW.");
            }
            else if (decidePoints % 10 == 2 || decidePoints % 10 == 3 || decidePoints % 10 == 4)
            {
                Console.WriteLine($"DODATKOWO OTRZYMASZ {decidePoints} PUNKTY.");
            }
            else Console.WriteLine($"DODATKOWO OTRZYMASZ {decidePoints} PUNKTÓW.");

            Console.WriteLine();

            if (decideSteps % 10 == 1)
            {
                Console.WriteLine($"MASZ {decideSteps} PRÓBĘ. ZACZYNAMY.");
            }
            else if (decideSteps == 12 || decideSteps == 13 || decideSteps == 14)
            {
                Console.WriteLine($"MASZ {decideSteps} PRÓB. ZACZYNAMY.");
            }
            else if (decideSteps % 10 == 2 || decideSteps % 10 == 3 || decideSteps % 10 == 4)
            {
                Console.WriteLine($"MASZ {decideSteps} PRÓBY. ZACZYNAMY.");
            }
            else Console.WriteLine($"MASZ {decideSteps} PRÓB. ZACZYNAMY.");

            return decidePoints;
        }

        static int[] Guess(int numberToGuess, int decideSteps)
        {
            int attempt=-1; // ???
            int step, win = 0; // 0 = przegrana, 1=wygrana
            int[] result = new int[2];
            bool wrongAttempt = false;
            for (step = 1; step <= decideSteps; step++)
            {

                do
                {
                    Console.Write($"PRÓBA {step}: ");
                    try
                    {
                        attempt = int.Parse(Console.ReadLine());
                        wrongAttempt = false;
                    }
                    catch
                    {
                        Console.WriteLine("WPROWADŹ LICZBĘ CAŁKOWITĄ <0-99>.");
                        wrongAttempt = true;
                        continue;
                    }
                    if (attempt < 0 || attempt > 99)
                    {
                        Console.WriteLine("WPROWADŹ POPRAWNĄ LICZBĘ <0-99>.");
                        wrongAttempt = true;
                    }
                }
                while (wrongAttempt == true);

                if (attempt == numberToGuess)
                {
                    Console.WriteLine();
                    Console.WriteLine($"ZGADŁEŚ. WYLOSOWANA LICZBA TO {numberToGuess}.");
                    win = 1;
                    break;
                }
                else
                {
                    if (attempt > numberToGuess)
                    {
                        Console.WriteLine("ZŁY STRZAŁ. WYLOSOWANA LICZBA JEST MNIEJSZA.");
                    }
                    else Console.WriteLine("ZŁY STRZAŁ. WYLOSOWANA LICZBA JEST WIĘKSZA.");
                }
            }
                result[0] = step;
                result[1] = win;

            return result;
        }
        static int Result(int decidePoints, int noOfSteps)
        {
            int totalPoints, speedPoints;
            if (noOfSteps <= 10)
            {
                speedPoints = 11 - noOfSteps;
            } 
            else speedPoints = 0;
            totalPoints = 10 + decidePoints + speedPoints;
            Console.WriteLine();
            Console.WriteLine("PUNKTY ZA ODGADNIĘCIE\t10.");
            Console.WriteLine($"PUNKTY DODATKOWE\t{decidePoints}.");
            Console.WriteLine($"PUNKTY ZA SZYBKOŚĆ\t{speedPoints}.");
            Console.WriteLine($"RAZEM\t{totalPoints}.");

            return totalPoints;
        }
        static void CreateRanking()
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

            foreach (string line in File.ReadLines("ranking.txt"))
            {
                top5[i] = line;
                i++;
            }

        }

        static void Ranking(int totalPoints)
        {
            int l = 0;
                   
            Console.WriteLine();

            if (totalPoints <= int.Parse(top5[4]))
            {
                Console.WriteLine("TWÓJ WYNIK NIE JEST W TOP5.");
            }
            else if (totalPoints > int.Parse(top5[0]))
            {
                top5[4] = top5[3];
                top5[3] = top5[2];
                top5[2] = top5[1];
                top5[1] = top5[0];
                top5[0] = totalPoints.ToString();
                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA 1 MIEJSCU.");
            }
            else
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (totalPoints <= int.Parse(top5[j]))
                    {
                        switch (j)
                        {
                            case 3:
                                top5[j + 1] = totalPoints.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;
                            case 2:
                                top5[j + 2] = top5[j + 1];
                                top5[j + 1] = totalPoints.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;
                            case 1:
                                top5[j + 3] = top5[j + 2];
                                top5[j + 2] = top5[j + 1];
                                top5[j + 1] = totalPoints.ToString();
                                Console.WriteLine($"TWÓJ WYNIK JEST W TOP5 NA {j + 2} MIEJSCU.");
                                break;
                            case 0:
                                top5[j + 4] = top5[j + 3];
                                top5[j + 3] = top5[j + 2];
                                top5[j + 2] = top5[j + 1];
                                top5[j + 1] = totalPoints.ToString();
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

            foreach (string line in File.ReadLines("ranking.txt"))
            {
                top5[l] = line;
                Console.WriteLine(top5[l]);
                l++;
            }

        }

        static void ShowRanking()
        {
            int l = 0;
            foreach (string line in File.ReadLines("ranking.txt"))
            {
                Console.WriteLine(line);
                l++;
            }
        }



    }
}