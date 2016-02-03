using GastonIF;

namespace GastonIF
{
    interface ICharacter
    {
        string Name { get; }
        string Description { get; }
        int Health { get; }
        void Attack(Character pCharacterToAttack);
        void TakeDamage(int pDamageAmount);
    }


    public abstract class Character : ICharacter
    {
        protected string _name;
        protected string _description;
        protected int _health;
        protected double _damage;

        public string Description
        {
            get { return _description; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Health
        {
            get { return _health; }
        }

        public double Damage
        {
            get { return _damage; }
        }

        public abstract void Attack(Character pCharacterToAttack);
        public virtual void TakeDamage(int pDamageAmount)
        {
            _health -= pDamageAmount;
        }
    }
}
