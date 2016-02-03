using System.Collections.Generic;


namespace GastonIF
{
    public abstract class EquippableItem : Item
    {
        protected double _hindrance;

        public EquippableItem(string pLookText,
                              string pInspectText,
                              string[] pKeywords,
                              Dictionary<string, string> pUniqueCommands,
                              double pHindrance)
                              : base(pLookText, pInspectText, pKeywords, pUniqueCommands)
        {
            _hindrance = pHindrance;   
        }

        public double Hindrance
        {
            get { return _hindrance; }
        }

    }
}
