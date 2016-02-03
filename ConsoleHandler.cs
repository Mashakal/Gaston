using System;

namespace GastonIF
{
    public class ConsoleHandler
    {
        private const int ROW_CHAR_COUNT_BOUNDARY = 70;

        public void Clear()
        {
            Console.Clear();
        }

        public void DrawPrompt()
        {
            Console.Write(">");
        }

        private string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string textToDisplay)
        {
            Console.Write(" ");
            Console.WriteLine(textToDisplay);
        }

        public void WriteOutput(int intToDisplay)
        {
            Console.WriteLine(intToDisplay);
        }

        public void WriteOutput(string[] pStringsToPrint)
        {
            foreach (string paragraph in pStringsToPrint)
            {
                string[] words = paragraph.Split(' ');
                Console.Write("    ");
                int rowCharCount = 4;
                for (int i = 0; i < words.Length; i++)
                {
                    Console.Write(words[i]);
                    Console.Write(" ");
                    rowCharCount += words[i].Length + 1;

                    if (rowCharCount > 70 && i != (words.Length - 1))
                    {
                        Console.WriteLine();
                        Console.Write(" ");
                        rowCharCount = 0;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void WriteOutput(bool boolToDisplay)
        {
            if (boolToDisplay)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }

        public void WriteOutput(double doubleToDisplay)
        {
            Console.WriteLine(doubleToDisplay);
        }

        public UserInput GetUserInput()
        {
            var input = new UserInput();
            input.RawInput = ReadInput();
            Console.WriteLine();
            return input;
        }

        internal void OutputLocationText(InteractiveFictionData gameData)
        {
            try
            {
                if (gameData.HasLocationChanged)
                {
                    WriteOutput(gameData.CurrentLocation.OpeningParagraphs);
                    gameData.CurrentLocation.Visit();
                    gameData.HasLocationChanged = false;
                }
            }
            catch (NullReferenceException e)
            {
                WriteOutput("You seem to have entered a wormhole...");
                if (gameData.LocationList.Capacity <= 0)
                {
                    WriteOutput("\n\nYOU NO LONGER EXIST!\n\tTHE WORLD IS LOST.\n\t\tYOU LOSE.");
                }
            }
        }   
    }
}
