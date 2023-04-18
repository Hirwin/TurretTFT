using static StatsManager;

[System.Serializable]
public class StatModifier {
    public int Order;
    public float Value;
    public StatModType Type;
    public StatsManager.StatTypes StatType;
    public object Source;

    public StatModifier(float value, StatModType type, int order, object source) {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }
    // Requires Value and Type. Calls the "Main" constructor and sets Order and Source to their default values: (int)type and null, respectively.
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    // Requires Value, Type and Order. Sets Source to its default value: null
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    // Requires Value, Type and Source. Sets Order to its default value: (int)Type
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}

public enum StatModType {
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}