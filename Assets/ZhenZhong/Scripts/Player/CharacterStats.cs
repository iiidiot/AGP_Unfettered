using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CharacterStatus
{
    [Serializable]
    public class CharacterStats
    {
        public float baseValue;

        public virtual float Value
        {
            get
            {
                if (m_needToRecalculate || baseValue != prevBaseValue)
                {
                    // scroll
                    prevBaseValue = baseValue;

                    m_value = CalculateFinalValue();
                    m_needToRecalculate = false;
                }

                return m_value;
            }
        }

        protected bool m_needToRecalculate = true;
        protected float m_value;
        protected float prevBaseValue = float.MinValue;

        /*
          readonly:
            We cannot modify the variable except when we are in
            the constructor of the class or in the declaration itself.

            We don't want to give permission to anything outside this class
            to modify the list.

            readonly will not prevent changes to the list itself.

            It prevents changes to this variable that happens to be pointing
            to a list.

          ReadOnlyCollection<StatModifier>:
            It is going to have a reference to the original list, 
            but it's going to prohibit changes to it.

            However, if we modify the original list, the
            ReadOnlyCollection<StatModifier> will reflect those changes too.

            readonly in ReadOnlyCollection<StatModifier> is because
            we don't want to be able to change what this variable is pointing to
            after we set it initially.
        */
        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> publicStatModifiers;

        public CharacterStats()
        {
            statModifiers = new List<StatModifier>();
            publicStatModifiers = statModifiers.AsReadOnly();
        }


        public CharacterStats(float val) : this()
        {
            baseValue = val;
        }

        public virtual void AddModifier(StatModifier statModifier)
        {
            m_needToRecalculate = true;
            statModifiers.Add(statModifier);

            // sort all the input stat modifier to
            // prevent miscalculation between flat value and percentage.
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier statModifier)
        {
            if (statModifiers.Remove(statModifier))
            {
                m_needToRecalculate = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSourceObj(object source)
        {
            bool isRemoved = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].source == source)
                {
                    isRemoved = true;
                    m_needToRecalculate = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return isRemoved;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier modifier = statModifiers[i];

                if (modifier.statType == StatType.Flat)
                {
                    finalValue += statModifiers[i].value;
                }

                else if (modifier.statType == StatType.PercentAdditive)
                {
                    sumPercentAdd += modifier.value;

                    if (i + 1 >= statModifiers.Count ||
                       statModifiers[i + 1].statType != StatType.PercentAdditive)
                    {
                        finalValue *= 1 + sumPercentAdd;

                        // reset
                        sumPercentAdd = 0;
                    }
                }

                else if (modifier.statType == StatType.PercentMult)
                {
                    // Always added flat values together first,
                    // then multiply the percentage
                    // Ex: base value = 10, increased by 10%,
                    //     1 + mod.value = 1 + 0.1 = 1.1 = 110%
                    //     10 * 1.1 = 11
                    finalValue *= 1 + modifier.value;
                }
            }

            // 12.001f != 12f
            return (float)Math.Round(finalValue, 3);
        }

        // Comparator for sorting
        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.order < b.order)
            {
                return -1;
            }
            else if (a.order > b.order)
            {
                return 1;
            }

            return 0;
        }
    }


}
