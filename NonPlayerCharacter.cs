using GastonIF;


namespace GastonIF
{
    public class NonPlayerCharacter : Character
    {
        public NonPlayerCharacter(string pName,
                                  string pDescription,
                                  int pHealth,
                                  double pInitDamage)
        {
            _name = pName;
            _description = pDescription;
            _health = pHealth;
            _damage = pInitDamage;
        }

        public override void Attack(Character pCharacterToAttack)
        {
            pCharacterToAttack.TakeDamage((int)_damage);
        }
    }
}
