/**
This is the BETA version of Gaston, an Interactive Fiction game framework.  
All that is required to author a game is a JSON file of the correct format.
I will soon upload instructions on the required syntax of the JSON file on
my website but for now please refer to the LocationNodeData.json file in the
resources folder.
**/


using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace GastonIF
{
    public class Gaston
    {
        public InteractiveFictionData gameData;


        public static int Main()
        {
            var gameObject = new Gaston();
            gameObject.Play();
            return 0;
        }


        public void Play()
        {
            // Initialize the game objects, Init takes a LocationNode.Name literal to indicate the player's current LocationNode
            gameData = Init("Stasis Pod");

            // Create the UserInput object for handling and processing user input
            var userInput = new UserInput();

            // Setup the Console Handler for getting input and displaying output
            var consoleHandler = new ConsoleHandler();
            consoleHandler.Clear();

            do
            {
                // Display the OpeningParagraphs text to the console
                consoleHandler.OutputLocationText(gameData); 
                // Listen
                consoleHandler.DrawPrompt();
                userInput = consoleHandler.GetUserInput();
                // Think
                userInput.ProcessText(gameData);
                // Speak
                consoleHandler.WriteOutput(userInput.Result);
            }
            while (userInput.Result.ToLower() != "quit");

            // clear the console before quitting
            consoleHandler.Clear();
        }


        private InteractiveFictionData Init(string pStartingLocationName)
        {
            string path = GetResourceFilePath();
            Dictionary<string, dynamic> jsonData = 
                LoadJson(@"C:\Users\Alex\Documents\visual studio 2015\Projects\GastonIF\GastonIF\Resources\LocationNodeData.json");

            return new InteractiveFictionData(jsonData, pStartingLocationName);
        }


        private string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }


        private string GetResourceFilePath()
        {
            string path = GetCurrentDirectory();

            string fullPath = Path.GetFullPath(Path.Combine(path, @"..\..\Resources\LocationNodeData.json"));
            return fullPath;
        }


        private Dictionary<string, dynamic> LoadJson(string pPathName)
        {
            using (StreamReader streamReader = new StreamReader(pPathName))
            {
                string rawJson = streamReader.ReadToEnd();
                Dictionary<string, dynamic> jsonData = JsonConvert.DeserializeObject<Dictionary<string,dynamic>>(rawJson);
                return jsonData;
            }
        }
    }
}
