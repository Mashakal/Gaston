


using GastonIF;
using System.Collections.Generic;

namespace GastonIF
{
    public class UserInput
    {
        private string _result;
        private string _rawInput;
        private bool _isProcessed = false;
        private string[] validCommands = {
        // Commented out items are not currently implemented.
            "look",         // Look around.
            "inspect",      // Look at a specific item.
            "take",         // Attempt to pick something up.
            "drop",         // Drop something from inventory.
            "quit",         // Quit the game.
            "i",            // Display inventory.
            "inventory",    // Display inventory.
            //"use",          // Use an item from inventory.
            "help",         // Display options.
            //"equip",        // Equip an item
            //"unequip",      // Unequip an item
            "go",           // Go a specific direction (north, south, east or west)
            "exit",         // Exit from a small space
        };

        // PROPERTIES //
        public string Result
        {
            get { return _result; }
        }


        public bool isProcessed
        {
            get { return _isProcessed; }
        }


        public string RawInput
        {
            set { _rawInput = value; }
        }


        // METHODS //
        public void ProcessText(InteractiveFictionData pGameData)
        {
            if (!_isProcessed)
            {
                _result = "";
                var parsedInputString = _rawInput.Split(' ');
                string command = SearchForHardCommand(parsedInputString[0]);

                if (command != null)
                {
                    LocationNode nextLocation;
                    switch (command)
                    {
                        case "look":
                            Look(pGameData);
                            break;

                        case "inspect":
                            Inspect(parsedInputString, pGameData);
                            break;

                        case "take":
                            Take(parsedInputString, pGameData);
                            break;

                        case "drop":
                            if (pGameData.PlayerCharacter.Inventory.Count == 0)
                            {
                                _result += "You're not carrying anything to drop.";
                                _result += "\n";
                            }
                            else
                            {
                                Drop(parsedInputString, pGameData);
                            }
                            break;

                        case "i":
                            if (pGameData.PlayerCharacter.Inventory.Count == 0)
                            {
                                _result += "Your bag is empty.";
                                _result += "\n";
                            }
                            Inventory(pGameData);
                            break;

                        case "inventory":
                            if (pGameData.PlayerCharacter.Inventory.Count == 0)
                            {
                                _result += "Your bag is empty";
                                _result += "\n";
                            }
                            Inventory(pGameData);
                            break;

                        case "go":
                            if (parsedInputString.Length <= 1)
                            {
                                _result += "Go where?";
                                _result += "\n";
                                break;
                            }
                            
                            nextLocation = FindNextLocation(parsedInputString, pGameData);
                            if (nextLocation != null)
                            {
                                pGameData.CurrentLocation = nextLocation;
                                pGameData.HasLocationChanged = true;
                            }
                            else
                            {
                                _result += "You cannot go that way.";
                                _result += "\n";
                            }
                            break;

                        case "exit":
                            nextLocation = FindNextLocation(command, pGameData);
                            if (nextLocation != null)
                            {
                                pGameData.CurrentLocation = nextLocation;
                                pGameData.HasLocationChanged = true;
                            }
                            else
                            {
                                _result += "You are unsure of where to exit.";
                                _result += "\n";
                            }
                            break;

                        case "help":
                            Help();
                            break;

                        default:
                            _result = command;
                            break;
                    }
                }
                else
                {
                    _result = GetUniqueCommandResult(parsedInputString, pGameData);
                    if (_result == null)
                    {
                        _result = this.ToString();
                    }
                    else
                    {
                        _result += "\n";
                    }
                }
                _isProcessed = true;
            }
        }


        // HELPER FUNCTIONS PROCESSING USER INPUT //
        private LocationNode FindNextLocation(string[] parsedInputString, InteractiveFictionData pGameData)
        {
            foreach (string direction in pGameData.CurrentLocation.ConnectingLocationDict.Keys)
            {
                if (parsedInputString[1].ToLower().Equals(direction.ToLower()))
                {
                    // find the next location based on the direction input (key)
                    string nextLocationName = pGameData.CurrentLocation.ConnectingLocationDict[direction];
                    foreach (LocationNode location in pGameData.LocationList)
                    {
                        if (location.Name.Equals(nextLocationName))
                        {
                            return location;
                        }
                    }
                }
            }
            return null;
        }

        private LocationNode FindNextLocation(string exit, InteractiveFictionData pGameData)
        {
            foreach (string direction in pGameData.CurrentLocation.ConnectingLocationDict.Keys)
            {
                if(exit.ToLower().Equals(direction.ToLower()))
                {
                    string nextLocationName = pGameData.CurrentLocation.ConnectingLocationDict[direction];
                    foreach (LocationNode location in pGameData.LocationList)
                    {
                        if (location.Name.Equals(nextLocationName))
                        {
                            return location;
                        }
                    }
                }
            }
            return null;
        }

        private void Look(InteractiveFictionData pGameData)
        {
            _result += pGameData.CurrentLocation.Description;
            _result += "\n\n  You can see:\n\n";
            foreach (Item item in pGameData.CurrentLocation.ItemList)
            {
                _result += "\t" + item.LookText + "\n";
            }
            _result = _result.Substring(0, _result.Length - 1);
            _result += "\n";
        }

        private void Inspect(string[] parsedInputString, InteractiveFictionData pGameData)
        {
            try
            {
                // Determine the item we are inspecting
                Item itemToInspect = FindItemToOperateOn(pGameData, parsedInputString);
                _result += itemToInspect.InspectText;
                _result += "\n";
            }
            catch (System.NullReferenceException e)
            {
                if (parsedInputString.Length > 1)
                {
                    _result = "I don't see any ";
                    _result += parsedInputString[1].ToLower();
                    _result += " to inspect.";
                    _result += "\n";
                }
                else
                {
                    _result += "Some more information will be helpful.";
                    _result += "\n";
                }
            }
        }

        private void Take(string[] parsedInputString, InteractiveFictionData pGameData)
        {
            try
            {
                // Determine the item we are inspecting
                Item itemToTake = FindItemToOperateOn(pGameData, parsedInputString);
                
                //System.Console.WriteLine(itemToTake.GetType());

                if (itemToTake.GetType() != typeof(CarriableItem) &&
                    itemToTake.GetType() != typeof(Weapon) &&
                    itemToTake.GetType() != typeof(Armor))
                {
                    _result += "That item cannot be picked up.";
                    _result += "\n";
                }
                else
                {
                    _result += "You have taken the ";
                    _result += itemToTake.Keywords[0].ToLower();
                    _result += ".\n"; // add period and new line
                    pGameData.PlayerCharacter.AddToInventory(itemToTake);
                    pGameData.CurrentLocation.ItemList.Remove(itemToTake);
                }
            }
            catch (System.NullReferenceException e)
            {
                if (parsedInputString.Length > 1)
                {
                    _result = "I don't see any ";
                    _result += parsedInputString[1].ToLower();
                    _result += " to take.";
                    _result += "\n";
                }
                else
                {
                    _result += "Some more information will be helpful.";
                    _result += "\n";
                }
            }
        }

        private void Drop(string[] parsedInputString, InteractiveFictionData pGameData)
        {
            try
            {
                Item itemToDrop = FindItemToOperateOn(pGameData, parsedInputString);
                pGameData.PlayerCharacter.RemoveFromInventory(itemToDrop);
                //pGameData.CurrentLocation.AddItem(itemToDrop);
                pGameData.CurrentLocation.ItemList.Add(itemToDrop);
                _result += "You have dropped the ";
                _result += itemToDrop.Keywords[0];
                _result += ".\n";
            }
            catch (System.NullReferenceException e)
            {
                if (parsedInputString.Length > 1)
                {
                    _result += "There is no ";
                    _result += parsedInputString[1];
                    _result += " to drop?";
                    _result += "\n";
                }
                else
                {
                    _result += "Some more information will be helpful.";
                }
            }
        }

        private void Quit(InteractiveFictionData pGameData)
        {
            
        }

        private void Inventory(InteractiveFictionData pGameData)
        {
            foreach (Item item in pGameData.PlayerCharacter.Inventory)
            {
                _result += "\t" + item.Keywords[0].ToCharArray()[0].ToString().ToUpper();
                _result += item.Keywords[0].Substring(1);
                _result += "\t\t";
                _result += item.InspectText;
                _result += "\n";
            }
        }

        private void Use(string[] parsedInputString, InteractiveFictionData pGameData)
        {

        }

        private void Help()
        {
            _result += "\tLook\t\t\tLook around.  Sometimes gain clues on what to do.\n";
            _result += "\tInspect {item}\t\tTake a closer look at something noteworthy you can see.\n";
            _result += "\tTake {item}\t\tTake an item that you can see.  Not all items can be carried.\n";
            _result += "\tDrop {item}\t\tDrop something from your inventory.\n";
            _result += "\tQuit\t\t\tQuit the game.\n";
            _result += "\tInventory\t\tDisplay the contents of your inventory.\n";
            _result += "\tI\t\t\tShorthand for 'Inventory'\n";
            _result += "\tGo {direction}\t\tMove your character a cardinal direction (north, south, etc).\n";
            _result += "\tExit\t\t\tAttempt to exit from the current location.\n";
            _result += "\n";

        }

        private void Equip(string[] parsedInputString, InteractiveFictionData pGameData)
        {

        }

        private void Unequip(string[] parsedInputString, InteractiveFictionData pGameData)
        {

        }

        private Item FindItemToOperateOn(InteractiveFictionData gameData, string[] pParsedUserInputArray)
        {
            foreach (string word in pParsedUserInputArray)
            {
                foreach (Item item in gameData.CurrentLocation.ItemList)
                {
                    foreach(string keyword in item.Keywords)
                    {
                        if (word.ToLower().Equals(keyword))
                        {
                            return item;
                        }
                    }
                }

                foreach (Item item in gameData.PlayerCharacter.Inventory)
                {
                    foreach (string keyword in item.Keywords)
                    {
                        if (word.ToLower().Equals(keyword))
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// A helper function for ProcessText(), return's the command given
        /// by the user or null if the command is not valid.
        /// </summary>
        private string SearchForHardCommand(string pUserCommand)
        {
            foreach (string command in validCommands)
            {
                if (pUserCommand.ToLower() == command)
                {
                    return command;
                }
            }
            return null;
        }

        private string GetUniqueCommandResult(string[] parsedInputString, InteractiveFictionData pGameData)
        {
            foreach (string word in parsedInputString)
            {
                foreach (Item item in pGameData.CurrentLocation.ItemList)
                {
                    foreach (string keyword in item.Keywords)
                    {
                        if (keyword == word)
                        {
                            // determine if the command matches this item
                            foreach (string key in item.UniqueCommands.Keys)
                            {
                                if (key == parsedInputString[0])
                                {
                                    return item.UniqueCommands[key];
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        // ToString //
        public override string ToString()
        {
            if (isProcessed)
            {
                return _result;
            }
            return "You're not making sense.  (type 'help' for a list of commands)\n";
        }
    }
}
