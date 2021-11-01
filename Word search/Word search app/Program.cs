using System;
using System.Text.RegularExpressions;
using Word_search_library;

namespace Word_search_app
{
    class Program
    {
        static void Main(string[] args)
        {

            Word_search wordSearch = new Word_search();

            do{
                Console.WriteLine("Enter dimension of the letter matrix (matrix type: MxN) in the form 'M N':\n2<=M<=100 and 2<=N<=100\n");
                string[] temp1 = Console.ReadLine().Split(' ');
                Console.WriteLine();
                wordSearch.M = int.Parse(temp1[0]);
                wordSearch.N = int.Parse(temp1[1]);
                if (wordSearch.M < 2 || wordSearch.M > 100 || wordSearch.N < 2 || wordSearch.N > 100)
                    Console.WriteLine("Out of bounds, try again.");
            } while (wordSearch.M < 2 || wordSearch.M > 100 || wordSearch.N < 2 || wordSearch.N > 100);

            wordSearch.MakeWordSearch();
            int i, j, l, m, count;
            bool check = true;

            for (i = 0; i < wordSearch.M; i++){
                Console.WriteLine($"Enter the row {i + 1} of the letter matrix:\nSeparate each letter with a space.\n");
                string[] temp2 = Console.ReadLine().Split(' ');
                Console.WriteLine();
                for (j = 0; j < wordSearch.N; j++)
                    wordSearch.SetLetters(i, j, temp2[j]);
            }

            do{
                Console.WriteLine("Enter the number of words you want the program to find in the letter matrix:\n1<=K<=100\n");
                string[] temp3 = Console.ReadLine().Split(' ');
                Console.WriteLine();
                wordSearch.K = int.Parse(temp3[0]);
                if (wordSearch.K < 1 || wordSearch.K > 100)
                    Console.WriteLine("Out of bounds, try again.");
            } while (wordSearch.K < 1 || wordSearch.K > 100);

            wordSearch.MakeWords();

            for (i = 0; i < wordSearch.K; i++){

                j = 0;
                Console.WriteLine($"Enter the word {i + 1} that you want the program to find in the letter matrix:\n");
                string temp4 = Console.ReadLine();
                string[] temp5 = Regex.Split(temp4, string.Empty);
                wordSearch.SetWordLength(i, temp5.Length - 2);

                do{
                    wordSearch.SetWords(i, j, temp5[j + 1]);
                    j++;
                } while (j < temp5.Length - 2);

                for (j = 0; j < wordSearch.GetWordLength(i); j++)
                    if (j + 1 < wordSearch.GetWordLength(i))
                        if (wordSearch.GetWords(i, j) == "N" && wordSearch.GetWords(i, j + 1) == "J"){
                            wordSearch.SetWords(i, j, "NJ");
                            wordSearch.SetWords(i, j + 1, string.Empty);
                        }

                for (j = 0; j < wordSearch.GetWordLength(i); j++)
                    if (j + 1 < wordSearch.GetWordLength(i))
                        if (wordSearch.GetWords(i, j) == "L" && wordSearch.GetWords(i, j + 1) == "J"){
                            wordSearch.SetWords(i, j, "LJ");
                            wordSearch.SetWords(i, j + 1, string.Empty);
                        }

                for (j = 0; j < wordSearch.GetWordLength(i); j++)
                    if (j + 1 < wordSearch.GetWordLength(i))
                        if (wordSearch.GetWords(i, j) == "D" && wordSearch.GetWords(i, j + 1) == "Ž"){
                            wordSearch.SetWords(i, j, "DŽ");
                            wordSearch.SetWords(i, j + 1, string.Empty);
                        }

                count = 0;
                for (j = 0; j < wordSearch.GetWordLength(i); j++)
                    if (string.IsNullOrEmpty(wordSearch.GetWords(i, j))) count++;
                for (j = 0; j < wordSearch.GetWordLength(i); j++)
                    if (j + 1 < wordSearch.GetWordLength(i))
                        if (string.IsNullOrEmpty(wordSearch.GetWords(i, j))){
                            wordSearch.SetWords(i, j, wordSearch.GetWords(i, j + 1));
                            wordSearch.SetWords(i, j + 1, string.Empty);
                        }
                wordSearch.SetWordLength(i, wordSearch.GetWordLength(i) - count);

            }

            for (i = 0; i < wordSearch.K; i++){
                check = true;
                for (j = 0; j < wordSearch.M; j++){
                    check = true;
                    for (l = 0; l < wordSearch.N; l++){
                        check = true;

                        if (wordSearch.GetLetters(j, l) == wordSearch.GetWords(i, 0)){
                            for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                if (j - m > -1 && l - m > -1)
                                    check = wordSearch.GetLetters(j - m, l - m) == wordSearch.GetWords(i, m);
                                else check = false;
                            }
                            if (check)
                                for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                    wordSearch.SetUsedLetters(j - m, l - m);

                            else{
                                check = true;
                                for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                    if (j - m > -1)
                                        check = wordSearch.GetLetters(j - m, l) == wordSearch.GetWords(i, m);
                                    else check = false;
                                }
                                if (check)
                                    for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                        wordSearch.SetUsedLetters(j - m, l);

                                else{
                                    check = true;
                                    for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                        if (j - m > -1 && l + m < wordSearch.N)
                                            check = wordSearch.GetLetters(j - m, l + m) == wordSearch.GetWords(i, m);
                                        else check = false;
                                    }
                                    if (check)
                                        for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                            wordSearch.SetUsedLetters(j - m, l + m);

                                    else{
                                        check = true;
                                        for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                            if (l + m < wordSearch.N)
                                                check = wordSearch.GetLetters(j, l + m) == wordSearch.GetWords(i, m);
                                            else check = false;
                                        }
                                        if (check)
                                            for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                                wordSearch.SetUsedLetters(j, l + m);

                                        else{
                                            check = true;
                                            for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                                if (j + m < wordSearch.M && l + m < wordSearch.N)
                                                    check = wordSearch.GetLetters(j + m, l + m) == wordSearch.GetWords(i, m);
                                                else check = false;
                                            }
                                            if (check)
                                                for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                                    wordSearch.SetUsedLetters(j + m, l + m);

                                            else{
                                                check = true;
                                                for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                                    if (j + m < wordSearch.M)
                                                        check = wordSearch.GetLetters(j + m, l) == wordSearch.GetWords(i, m);
                                                    else check = false;
                                                }
                                                if (check)
                                                    for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                                        wordSearch.SetUsedLetters(j + m, l);

                                                else{
                                                    check = true;
                                                    for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                                        if (j + m < wordSearch.M && l - m > -1)
                                                            check = wordSearch.GetLetters(j + m, l - m) == wordSearch.GetWords(i, m);
                                                        else check = false;
                                                    }
                                                    if (check)
                                                        for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                                            wordSearch.SetUsedLetters(j + m, l - m);

                                                    else{
                                                        check = true;
                                                        for (m = 1; m < wordSearch.GetWordLength(i) && check; m++){
                                                            if (l - m > -1)
                                                                check = wordSearch.GetLetters(j, l - m) == wordSearch.GetWords(i, m);
                                                            else check = false;
                                                        }
                                                        if (check)
                                                            for (m = 0; m < wordSearch.GetWordLength(i); m++)
                                                                wordSearch.SetUsedLetters(j, l - m);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            check = false;

            for (i = 0; i < wordSearch.M; i++)
                for (j = 0; j < wordSearch.N && !check; j++)
                    check = (wordSearch.GetUsedLetters(i, j) == false);

            if (check){
                for (i = 0; i < wordSearch.M; i++)
                    for (j = 0; j < wordSearch.N; j++)
                        if (!wordSearch.GetUsedLetters(i, j))
                            Console.Write(wordSearch.GetLetters(i, j));
            }

            else Console.WriteLine("-");
        }
    }
}
