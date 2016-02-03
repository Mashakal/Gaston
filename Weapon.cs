using System.Collections.Generic;


namespace GastonIF
{
    public class Weapon : EquippableItem
    {
        private double _attackDamage;
        private double _attackSpeed;

        public Weapon(string pLookText,
                      string pInspectText,
                      string[] pKeywords,
                      Dictionary<string, string> pUniqueCommands,
                      double pHindrance,
                      double pAttackSpeed,
                      double pAttackDamage)
                      : base(pLookText, pInspectText, pKeywords, pUniqueCommands, pHindrance)
        {
            _attackSpeed = pAttackSpeed;
            _attackDamage = pAttackDamage;
        }

        public double AttackSpeed
        {
            get { return _attackSpeed; }
        }

        public double AttackDamage
        {
            get { return _attackDamage; }
        }
    }
}
