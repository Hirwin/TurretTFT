using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

[Serializable]
public class CharacterStat{
    public float BaseValue; //make private with getter accessor
    public StatsManager.StatTypes StatType; //make private with getter accessor
    protected bool isDirty = true;
    protected float lastBaseValue;
    protected float _value;

    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    private readonly List<StatModifier> statModifiers;

    public event EventHandler<OnStatUpdateEventArgs> OnStatUpdate;
    public class OnStatUpdateEventArgs : EventArgs {
        public CharacterStat characterStat;
    }

    public CharacterStat() {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }
    public CharacterStat(float baseValue, StatsManager.StatTypes statType) : this() {
        BaseValue = baseValue;
        StatType = statType;
    }

    public virtual float Value {
        get {
            if (isDirty || lastBaseValue != BaseValue) {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
    public void PushStat() {
        OnStatUpdate?.Invoke(this, new OnStatUpdateEventArgs {
            characterStat = this
        });
    }

    public virtual void AddModifier(StatModifier mod) {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveModifier(StatModifier mod) {
        if (statModifiers.Remove(mod)) {
            isDirty = true;
            return true;
        }
        return false;
    }

    protected virtual float CalculateFinalValue() {
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            } else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
              {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd) {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            } else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
              {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }

    public bool RemoveAllModifiersFromSource(object source) {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--) {
            if (statModifiers[i].Source == source) {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b) {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

}

