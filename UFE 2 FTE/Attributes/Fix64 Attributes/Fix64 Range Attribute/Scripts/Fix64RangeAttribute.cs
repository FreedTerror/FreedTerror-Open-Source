using UnityEngine;

/// <summary>
/// Attribute used to make a Fix64 variable in a script be restricted to a specific range.
/// </summary>
public class Fix64RangeAttribute : PropertyAttribute
{
    public float min { get; private set; }
    public float max { get; private set; }

    public Fix64RangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
