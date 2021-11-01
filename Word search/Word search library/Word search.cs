using System;

namespace Word_search_library
{
    public class Word_search
    {
        private const int maxLettersInWord = 141;
        private int n;
        private int m;
        private int k;
        private int[] wordLength;
        private string[,] letters;
        private string[,] words;
        private bool[,] usedLetters;

        public int N{
            get { return this.n; }
            set { this.n = value; }
        }

        public int M{
            get { return this.m; }
            set { this.m = value; }
        }

        public int K{
            get { return this.k; }
            set { this.k = value; }
        }

        public void MakeWordSearch(){
            letters = new string[m, n];
            usedLetters = new bool[m, n];
            int i, j;
            for (i = 0; i < m; i++) for (j = 0; j < n; j++) usedLetters[i, j] = false;
        }

        public void SetLetters(int i, int j, string val) { this.letters[i, j] = val; }
        public string GetLetters(int i, int j) { return this.letters[i, j]; }

        public void SetUsedLetters(int i, int j) { this.usedLetters[i, j] = true; }
        public bool GetUsedLetters(int i, int j) { return this.usedLetters[i, j]; }

        public void SetWords(int i, int j, string val) { this.words[i, j] = val; }
        public string GetWords(int i, int j) { return this.words[i, j]; }

        public void MakeWords(){
            words = new string[k, maxLettersInWord];
            wordLength = new int[k];
        }

        public void SetWordLength(int i, int duljina) { this.wordLength[i] = duljina; }
        public int GetWordLength(int i) { return this.wordLength[i]; }
    }
}
