using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.PortableExecutable;


namespace StringManipulation
{
    public class positionOfCharacter
    {
        public string? nameOfWord { get; set; }
        public string? c { get; set; }
        public int pos { get; set; }
    }
    internal class Program
    {
        static void Main()
        {
            string contents = "";
            List<positionOfCharacter> list = new List<positionOfCharacter>();

            try
            {
                // Get file name.
                string path = @"Files/Words.txt";
                // Get path name.
                string filename = Path.GetFileName(path);
                // Open the text file using a stream reader. Read into a string
                using (var sr = new StreamReader(path))
                {
                    // Read the stream as a string, and write the string to the console.
                    contents = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }

            //Store each word into an array using split on '\n'
            var array = contents.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach(var word in array)
            {
                Console.WriteLine($"{word}");
            }

            // Choose a random word from the array
            Random random = new Random();
            string selectedWord = array[random.Next(array.Length)].Trim().ToLower();

            // Initialize the display word with underscores
            string displayWord = new string('_', selectedWord.Length);

            // Main game loop
            int attempts = 0;
            while (attempts < 8)
            {
                Console.WriteLine($"Current word: {displayWord}");
                Console.Write("Enter a letter: ");

                string letterToSearch = Console.ReadLine()?.ToLower();

                if (letterToSearch != null && letterToSearch.Length == 1)
                {
                    char letter = letterToSearch[0];

                    // Check if the letter is in the word
                    int index = selectedWord.IndexOf(letter);
                    if (index != -1)
                    {
                        while (index != -1)
                        {
                            displayWord = displayWord.Remove(index, 1).Insert(index, letter.ToString());
                            index = selectedWord.IndexOf(letter, index + 1);
                        }

                        if (displayWord.Equals(selectedWord))
                        {
                            Console.WriteLine($"Congratulations! You guessed the word: {selectedWord}, with {7 - attempts} attempts remaining!");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect guess. Attempts remaining: {7 - attempts}");
                        attempts++;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a single letter.");
                }
            }

            if(attempts == 8){
                Console.WriteLine("OUT OF ATTEMPTS, word was: " + selectedWord);
            }
            Console.ReadLine();
        }
    }
}
