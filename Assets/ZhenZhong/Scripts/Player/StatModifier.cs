
namespace CharacterStatus
{
    public enum StatType
    {
        Flat = 100,
        PercentMult = 200,
        PercentAdditive = 300,
    }

    public class StatModifier
    {   /*
       readonly:
       We cannot modify the variable except when we are in
       the constructor of the class or in the declaration itself.
    */
        public readonly float value;
        public readonly StatType statType;
        public readonly int order;

        /*
            object: literally anything: class, enum, primitive data type, object type, etc.
        */
        // It's used to tell where each modifier came from.
        public readonly object source;

        public StatModifier(float val, StatType type, int order, object source)
        {
            value = val;
            statType = type;
            this.order = order;
            this.source = source;
        }

        // This will pass the parameters to the above constructor.
        // It's different from C++ initializer list
        public StatModifier(float value, StatType type) : this(value, type, (int)type, null) { }

        public StatModifier(float value, StatType type, int order) : this(value, type, order, null) { }

        public StatModifier(float value, StatType type, object source) : this(value, type, (int)type, source) { }
    }
}

