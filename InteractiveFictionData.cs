using System.Collections.Generic;


namespace GastonIF
{
    public class InteractiveFictionData
    {
        private bool _hasLocationChanged = true;
        private List<LocationNode> _locationNodeList = new List<LocationNode>();
        private Player _playerCharacter;
        private LocationNode _currentLocation;

        public InteractiveFictionData(Dictionary<string, dynamic> pJsonData, string pStartingLocationName)
        {
            InitializeLocationNodes(pJsonData["LocationNode"]);
            InitializePlayerCharacter(pJsonData["PlayerCharacter"]);
            SetInitialLocation(pStartingLocationName);
        }

        // PROPERTIES//
        public LocationNode CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        public List<LocationNode> LocationList
        {
            get { return _locationNodeList; }
        }

        public Player PlayerCharacter
        {
            get { return _playerCharacter; }
        }

        public bool HasLocationChanged
        {
            get { return _hasLocationChanged; }
            set { _hasLocationChanged = value; }
        }

        // FUNCTIONS //
        private void InitializePlayerCharacter(dynamic pJsonPlayerData)
        {
            // There should only be one Player
            foreach (dynamic rawPlayerData in pJsonPlayerData)
            {
                _playerCharacter = new Player(
                    rawPlayerData["Name"].ToObject<string>(),
                    rawPlayerData["Description"].ToObject<string>(),
                    rawPlayerData["Health"].ToObject<int>());
            }
        }

        private void SetInitialLocation(string pStartLocationName)
        {
            _locationNodeList.ForEach(delegate (LocationNode location)
            {
                if (location.Name.Equals(pStartLocationName))
                {
                    _currentLocation = location;
                }
            });
        }

        private void InitializeLocationNodes(dynamic pJsonLocationNodeData)
        {
            foreach (var rawLocationNode in pJsonLocationNodeData)
            {
                string _name = rawLocationNode["Name"];
                string _desc = rawLocationNode["Description"];
                string _hint = rawLocationNode["Hint"];
                var _itemList = new List<Item>();
                var _characterList = new List<Character>();
                var _openingParagraphs = rawLocationNode["OpeningParagraphs"].ToObject<string[]>();
                var _connectingLocationDict = 
                    rawLocationNode["ConnectingLocations"].ToObject<Dictionary<string, string>>();

                foreach (var rawCarriable in rawLocationNode["CarriableItems"])
                {
                    if (rawCarriable.Count != 0)
                    {
                        CreateCarriableItem(rawCarriable, _itemList);
                    }
                }

                foreach (var locationSpecificItem in rawLocationNode["LocationSpecificItems"])
                {
                    if (locationSpecificItem.Count != 0)
                    {
                        CreateLocationSpecificItem(locationSpecificItem, _itemList);
                    }
                }

                foreach (var rawWeapon in rawLocationNode["Weapons"])
                {
                    CreateWeapon(rawWeapon, _itemList);
                }

                _locationNodeList.Add(new LocationNode(
                    _name,
                    _desc,
                    _hint,
                    _openingParagraphs,
                    _connectingLocationDict,
                    _itemList,
                    _characterList)
               );
            }
            
        }

        private void CreateWeapon(dynamic rawWeapon, List<Item> pItemList)
        {
            pItemList.Add(new Weapon(
                rawWeapon["LookText"].ToObject<string>(),
                rawWeapon["InspectText"].ToObject<string>(),
                rawWeapon["Keywords"].ToObject<string[]>(),
                rawWeapon["UniqueCommands"].ToObject<Dictionary<string, string>>(),
                rawWeapon["Hindrance"].ToObject<double>(),
                rawWeapon["AttackSpeed"].ToObject<double>(),
                rawWeapon["AttackDamage"].ToObject<double>())
            );
        }

        private void CreateCarriableItem(dynamic rawItem, List<Item> pItemList)
        {
            pItemList.Add(new CarriableItem(
                rawItem["LookText"].ToObject<string>(),
                rawItem["InspectText"].ToObject<string>(),
                rawItem["Keywords"].ToObject<string[]>(),
                rawItem["UniqueCommands"].ToObject<Dictionary<string, string>>())
            );
        }

        private void CreateLocationSpecificItem(dynamic rawItem, List<Item> pItemList)
        {
            pItemList.Add(new LocationSpecificItem(
                rawItem["LookText"].ToObject<string>(),
                rawItem["InspectText"].ToObject<string>(),
                rawItem["Keywords"].ToObject<string[]>(),
                rawItem["UniqueCommands"].ToObject<Dictionary<string, string>>())
            );
        }
    }
}
