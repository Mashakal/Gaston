using GastonIF;


namespace GastonIF
{
    /// <summary>
    /// Creates a Size object that represents the size of an object within
    /// the game world.  Provides methods for discovering if the Object
    /// can be picked up or moved by the player.  When an invalid parameter
    /// is passed in, the size will default to VeryLarge, which means the
    /// item is not movable and cannot be picked up.  Rooms, buildings,
    /// and zones are most often of size VeryLarge.
    /// </summary>
    public class ObjectSize
    {
        public enum EnumSize
        {
            Small,      // Can be carried.
            Medium,     // Can be carried.
            Large,      // Cannot be carried, can be moved.
            VeryLarge   // Cannot be carried or moved.
        }

        private EnumSize _size;

        public ObjectSize(string pSize)
        {
            switch (pSize.ToLower())
            {
                case "small":
                    _size = EnumSize.Small;
                    break;
                case "medium":
                    _size = EnumSize.Medium;
                    break;
                case "large":
                    _size = EnumSize.Large;
                    break;
                case "verylarge":
                    _size = EnumSize.VeryLarge;
                    break;
                default:
                    _size = EnumSize.VeryLarge;
                    break;
            }
        }

        public EnumSize Size
        {
            get
            {
                return _size;
            }
        }

        public bool IsCarriable()
        {
            if (_size <= EnumSize.Medium)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method IsMovable() returns true if an object is considered
        /// movable within the game world. A movable object is one that
        /// is too large to be carried around but not
        /// so large that it cannot be moved (pushed, pulled, etc).
        /// </summary>
        public bool IsMovable()
        {
            if (_size == EnumSize.Large)
            {
                return true;
            }
            return false;
        }
    }
}
