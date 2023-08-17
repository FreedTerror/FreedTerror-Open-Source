using UnityEngine;

/// <summary>
/// Attribute used to make a Fix64 variable in a script be restricted to a specific range.
/// </summary>
public class Fix64RangeAttribute : PropertyAttribute
{
    public readonly float? minFloat;
    public readonly float? maxFloat;

    public Fix64RangeAttribute(float minFloat, float maxFloat)
    {
        this.minFloat = minFloat;
        this.maxFloat = maxFloat;
    }

    public readonly int? minInt;
    public readonly int? maxInt;

    public Fix64RangeAttribute(int minInt, int maxInt)
    {
        this.minInt = minInt;
        this.maxInt = maxInt;
    }
}
