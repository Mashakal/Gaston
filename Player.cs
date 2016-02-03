using System.Collections.Generic;


namespace GastonIF
{
    public class Player : Character
    {
        private static int FIST_DAMAGE = 5;
        protected int _strength = 5;
        protected Weapon _equippedWeapon;
        private string _errorMessage;
        // Damage output, default is 100%, as a decimal
        private double _damageOutputPercent = 1.0;
        // Capacity decreases as a character arms themselves for combat
        // It slows down their attack speed.
        protected double _capacity = 1;
        private List<Item> _inventory = new List<Item>();


        // CONSTRUCTOR //
        public Player(string pName, string pDescription, int pHealth)
        {
            _name = pName;
            _description = pDescription;
            _health = pHealth;
            _equippedWeapon = null;

            // Set the value of _damage
            UpdateDamage();
        }


        // PROPERTIES //
        public string ErrorMessage
        {
            get
            {
                if (_errorMessage != null)
                {
                    return _errorMessage;
                }
                return "";
            }
        }

        public List<Item> Inventory
        {
            get { return _inventory; }
        }

        
        // FUNCTIONS //
        public double DamageOutputPercent
        {
            get { return _damageOutputPercent; }
        }

        public void AddToInventory(Item pItemToAdd)
        {
            _inventory.Add(pItemToAdd);
        }

        public void RemoveFromInventory(Item pItemToRemove)
        {
            _inventory.Remove(pItemToRemove);
        }

        /// <summary>
        /// Method UpdateDamage calculates the value of _damage.
        /// It is called by method Attack to make sure
        /// that _damage is defined before being passed into method
        /// TakeDamage.  It also ensures that any changes to equipment
        /// are reflected in _damage.
        /// </summary>
        protected void UpdateDamage()
        {
            if (_equippedWeapon != null)
            {
                _damage =
                    (_strength * _equippedWeapon.AttackDamage /
                    _equippedWeapon.AttackSpeed * _capacity)
                    * _damageOutputPercent;
            }
            else
            {
                _damage = FIST_DAMAGE;
            }
        }

        public override void Attack(Character pCharacterToAttack)
        {
            UpdateDamage();
            pCharacterToAttack.TakeDamage((int)_damage);
        }

        public int Equip(Weapon pItemToEquip)
        {
            _equippedWeapon = pItemToEquip;
            return 0;
        }

        public int Equip(Armor pItemToEquip)
        {
            if (_capacity >= pItemToEquip.Hindrance)
            {
                // TODO: fix this
                // Console.WriteLine("You have equipped {0}",
                //                   pItemToEquip.Description.ToLower());
                _capacity -= pItemToEquip.Hindrance;
                return 0;
            }
            else
            {
                // Carrying too much
                _errorMessage = "You cannot manage all of this armor.\n" +
                                "You must remove something if you wish " +
                                "equip that.";
                return -1;
            }
        }
    }
}
