using System.Collections.Generic;

namespace GastonIF
{
    public class LocationSpecificItem : Item
    {
        public LocationSpecificItem(string pLookText,
                                    string pInspectText,
                                    string[] pKeywords,
                                    Dictionary<string, string> pUniqueCommands)
                                    : base(pLookText, pInspectText, pKeywords, pUniqueCommands)
        {
            // pass
        }
    }
}
