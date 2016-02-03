using System.Collections.Generic;


namespace GastonIF
{
    /// <summary>
    /// An empty derivation of AbstractItem.  A CarriableItem is an item that can be picked up
    /// and carried as a part of inventory by the player but cannot be equipped.
    /// </summary>
    public class CarriableItem : Item
    {
        public CarriableItem(string pLookText,
                     string pInspectText,
                     string[] pKeywords,
                     Dictionary<string, string> pUniqueItems)
                     : base(pLookText, pInspectText, pKeywords, pUniqueItems)
        {
            // pass
        }
    }
}
