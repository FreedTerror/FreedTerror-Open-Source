#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using FPLibrary;

[CustomPropertyDrawer(typeof(Fix64RangeAttribute))]
public class Fix64RangeAttributePropertyDrawer : PropertyDrawer
{
    private readonly string unsupportedUsageErrorMessageName = $"Use of {nameof(Fix64RangeAttribute)} is not compatible with this field's type.";

    private readonly string coreFieldName = "_serializedValue";
    private SerializedProperty coreFieldLongValueProperty = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (coreFieldLongValueProperty == null)
        {
            coreFieldLongValueProperty = property.FindPropertyRelative(coreFieldName);

            if (coreFieldLongValueProperty == null)
            {
                EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);

                return;
            }
        }

        Fix64RangeAttribute rangeAttribute = (Fix64RangeAttribute)attribute;

        float newFloatValue = EditorGUI.Slider(
            position,
            label,
            (float)Fix64.FromRaw(coreFieldLongValueProperty.longValue),
            rangeAttribute.min,
            rangeAttribute.max);

        coreFieldLongValueProperty.longValue = Fix64.FromFloat(newFloatValue).RawValue;
    }
}
#endif
