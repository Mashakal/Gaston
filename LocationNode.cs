using System.Collections.Generic;

namespace GastonIF
{
    public class LocationNode
    {
        private int _numberOfVisits = 0;
        private string _hint;
        private string _name;
        private string _description;
        private string[] _openingParagraphs;
        private List<Item> _itemList;
        private List<Character> _characterList;
        private Dictionary<string, string> _connectingLocationDict;

        // CONSTRUCTOR //
        public LocationNode(string pName,
                            string pDescription,
                            string pHint,
                            string[] pOpeningParagraphs,
                            Dictionary<string, string> pConnectingLocationInformation,
                            List<Item> pItemList,
                            List<Character> pCharacterList)

        {
            _name = pName;
            _description = pDescription;
            _hint = pHint;
            _openingParagraphs = pOpeningParagraphs;
            _connectingLocationDict = pConnectingLocationInformation;
            _itemList = pItemList;
            _characterList = pCharacterList;
        }


        // PROPERTIES //
        public string Hint
        {
            get
            {
                return _hint;
            }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public int NumberOfVisits
        {
            get { return _numberOfVisits; }
        }

        public string[] OpeningParagraphs
        {
            get
            {
                return _openingParagraphs;
            }
        }

        public Dictionary<string, string> ConnectingLocationDict
        {
            get { return _connectingLocationDict; }
        }

        public List<Item> ItemList
        {
            get
            {
                return _itemList;
            }
        }


        // METHODS //
        public void Visit()
        {
            IncrementNumberOfVisits();
        }

        private void IncrementNumberOfVisits()
        {
            _numberOfVisits++;
        }

        public void AddItem(Item pItem)
        {
            _itemList.Add(pItem);
        }
    }
}
