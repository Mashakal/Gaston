using System.Collections.Generic;


namespace GastonIF
{
    /// <summary>
    /// Type Armor is an empty derivation of type EquippableItem.
    /// </summary>
    public class Armor : EquippableItem
    {
        public Armor(string pLookText,
                     string pInspectText,
                     string[] pKeywords,
                     Dictionary<string, string> pUniqueCommands,
                     double pHindrance)
                     : base(pLookText, pInspectText, pKeywords, pUniqueCommands, pHindrance)
        {
            // pass
        }
    }
}
