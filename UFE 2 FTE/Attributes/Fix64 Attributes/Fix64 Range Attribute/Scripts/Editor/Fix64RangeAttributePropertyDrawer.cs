#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using FPLibrary;

[CustomPropertyDrawer(typeof(Fix64RangeAttribute))]
public class Fix64RangeAttributePropertyDrawer : PropertyDrawer
{
    private readonly string unsupportedUsageErrorMessageName = $"Use of {nameof(Fix64RangeAttribute)} is not compatible with this field's type.";

    private readonly string coreFieldName = "_serializedValue";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty coreFieldLongValueProperty = property.FindPropertyRelative(coreFieldName);

        if (coreFieldLongValueProperty == null)
        {
            EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);

            return;
        }

        Fix64RangeAttribute rangeAttribute = (Fix64RangeAttribute)attribute;

        if (rangeAttribute.minFloat != null
            && rangeAttribute.maxFloat != null
            && rangeAttribute.minInt == null
            && rangeAttribute.maxInt == null)
        {
            float newFloat = EditorGUI.Slider(
                position,
                label,
                (float)Fix64.FromRaw(coreFieldLongValueProperty.longValue),
                (float)rangeAttribute.minFloat,
                (float)rangeAttribute.maxFloat);

            coreFieldLongValueProperty.longValue = Fix64.FromFloat(newFloat).RawValue;
        }
        else if (rangeAttribute.minFloat == null
            && rangeAttribute.maxFloat == null
            && rangeAttribute.minInt != null
            && rangeAttribute.maxInt != null)
        {
            int newInt = EditorGUI.IntSlider(
                position,
                label,
                (int)Fix64.FromRaw(coreFieldLongValueProperty.longValue),
                (int)rangeAttribute.minInt,
                (int)rangeAttribute.maxInt);

            coreFieldLongValueProperty.longValue = Fix64.FromFloat(newInt).RawValue;
        }
        else
        {
            EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);
        }
    }
}
#endif
