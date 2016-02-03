using System.Collections.Generic;


namespace GastonIF
{
    interface IAbstractItem
    {
        string LookText { get; }
        string InspectText { get; }
        string[] Keywords { get; }
    }

    /// <summary>
    /// This is a powerful super class that grants authors the ability to 
    /// add some functionality without needing to edit or create code.   
    /// It is all handled through the JSON input file, which uses labels to 
    /// specify the unique derivation of AbstractItem to create an object.
    /// </summary>
    public abstract class Item : IAbstractItem
    {
        protected string _lookText;
        protected string _inspectText;
        protected string[] _keywords;
        Dictionary<string, string> _uniqueCommands;

        public Item(string pLookText,
                            string pInspectText,
                            string[] pKeywords,
                            Dictionary<string, string> pUniqueCommands)
        {
            _lookText = pLookText;
            _inspectText = pInspectText;
            _keywords = pKeywords;
            _uniqueCommands = pUniqueCommands;
        }

        // PROPERTIES //
        public virtual string InspectText
        {
            get { return _inspectText; }
        }

        public virtual string LookText
        {
            get { return _lookText; }
        }

        public virtual string[] Keywords
        {
            get { return _keywords; }
        }
        
        public virtual Dictionary<string, string> UniqueCommands
        {
            get { return _uniqueCommands; }
        }
        
        // METHODS //
        public virtual string GetUniqueCommandResult(string pInputCommand)
        {
            foreach (KeyValuePair<string, string> pair in _uniqueCommands)
            {
                if (pair.Key.ToLower().Equals(pInputCommand.ToLower()))
                {
                    return pair.Value;
                }
            }
            return null;
        }

        public virtual bool ValidateUniqueCommand(string pInputCommand)
        {
            foreach (string key in _uniqueCommands.Keys)
            {
                if (key.ToLower().Equals(pInputCommand.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
