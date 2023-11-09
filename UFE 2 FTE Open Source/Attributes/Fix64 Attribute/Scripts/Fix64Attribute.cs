using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using FPLibrary;

namespace UFE2FTE
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Fix64))]
    public class Fix64PropertyDrawer : PropertyDrawer
    {
        private readonly string unsupportedUsageErrorMessage = $"Use of {nameof(Fix64)} is not compatible with this field's type.";
        private readonly string coreFieldName = "_serializedValue";
        private readonly string xFieldName = "x";
        private readonly string _xFieldName = "_x";
        private readonly string yFieldName = "y";
        private readonly string _yFieldName = "_y";
        private readonly string zFieldName = "z";
        private readonly string widthFieldName = "width";
        private readonly string heightFieldName = "height";
        private bool useRectFieldTypePropertyHeight = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType == typeof(Fix64)
                || fieldInfo.FieldType == typeof(Fix64[])
                || fieldInfo.FieldType == typeof(List<Fix64>))
            {
                SerializedProperty coreFieldLongValueProperty = property.FindPropertyRelative(coreFieldName);

                if (coreFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
                    return;
                }

                float newFloat = EditorGUI.FloatField(
                    position,
                    label,
                    (float)Fix64.FromRaw(coreFieldLongValueProperty.longValue));

                coreFieldLongValueProperty.longValue = ((Fix64)newFloat).RawValue;
            }
            else if (fieldInfo.FieldType == typeof(FPVector2)
                || fieldInfo.FieldType == typeof(FPVector2[])
                || fieldInfo.FieldType == typeof(List<FPVector2>))
            {
                SerializedProperty xFieldLongValueProperty = property.FindPropertyRelative(xFieldName).FindPropertyRelative(coreFieldName);
                SerializedProperty yFieldLongValueProperty = property.FindPropertyRelative(yFieldName).FindPropertyRelative(coreFieldName);

                if (xFieldLongValueProperty == null
                    || yFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
                    return;
                }

                Vector2 newVector = EditorGUI.Vector2Field(
                    position,
                    label,
                    new Vector2(
                        (float)Fix64.FromRaw(xFieldLongValueProperty.longValue),
                        (float)Fix64.FromRaw(yFieldLongValueProperty.longValue)));

                xFieldLongValueProperty.longValue = ((Fix64)newVector.x).RawValue;
                yFieldLongValueProperty.longValue = ((Fix64)newVector.y).RawValue;
            }
            else if (fieldInfo.FieldType == typeof(FPVector)
                || fieldInfo.FieldType == typeof(FPVector[])
                || fieldInfo.FieldType == typeof(List<FPVector>))
            {
                SerializedProperty xFieldLongValueProperty = property.FindPropertyRelative(xFieldName).FindPropertyRelative(coreFieldName);
                SerializedProperty yFieldLongValueProperty = property.FindPropertyRelative(yFieldName).FindPropertyRelative(coreFieldName);
                SerializedProperty zFieldLongValueProperty = property.FindPropertyRelative(zFieldName).FindPropertyRelative(coreFieldName);

                if (xFieldLongValueProperty == null
                    || yFieldLongValueProperty == null
                    || zFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
                    return;
                }

                Vector3 newVector = EditorGUI.Vector3Field(
                    position,
                    label,
                    new Vector3(
                        (float)Fix64.FromRaw(xFieldLongValueProperty.longValue),
                        (float)Fix64.FromRaw(yFieldLongValueProperty.longValue),
                        (float)Fix64.FromRaw(zFieldLongValueProperty.longValue)));

                xFieldLongValueProperty.longValue = ((Fix64)newVector.x).RawValue;
                yFieldLongValueProperty.longValue = ((Fix64)newVector.y).RawValue;
                zFieldLongValueProperty.longValue = ((Fix64)newVector.z).RawValue;
            }
            else if (fieldInfo.FieldType == typeof(FPRect)
                || fieldInfo.FieldType == typeof(FPRect[])
                || fieldInfo.FieldType == typeof(List<FPRect>))
            {
                SerializedProperty xFieldLongValueProperty = property.FindPropertyRelative(_xFieldName).FindPropertyRelative(coreFieldName);
                SerializedProperty yFieldLongValueProperty = property.FindPropertyRelative(_yFieldName).FindPropertyRelative(coreFieldName);
                SerializedProperty widthFieldLongValueProperty = property.FindPropertyRelative(widthFieldName).FindPropertyRelative(coreFieldName);
                SerializedProperty heightFieldLongValueProperty = property.FindPropertyRelative(heightFieldName).FindPropertyRelative(coreFieldName);

                if (xFieldLongValueProperty == null
                    || yFieldLongValueProperty == null
                    || widthFieldLongValueProperty == null
                    || heightFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
                    return;
                }

                Rect newRect = EditorGUI.RectField(
                    position,
                    label,
                    new Rect(
                        (float)Fix64.FromRaw(xFieldLongValueProperty.longValue),
                        (float)Fix64.FromRaw(yFieldLongValueProperty.longValue),
                        (float)Fix64.FromRaw(widthFieldLongValueProperty.longValue),
                        (float)Fix64.FromRaw(heightFieldLongValueProperty.longValue)));

                xFieldLongValueProperty.longValue = ((Fix64)newRect.x).RawValue;
                yFieldLongValueProperty.longValue = ((Fix64)newRect.y).RawValue;
                widthFieldLongValueProperty.longValue = ((Fix64)newRect.width).RawValue;
                heightFieldLongValueProperty.longValue = ((Fix64)newRect.height).RawValue;

                useRectFieldTypePropertyHeight = true;
            }
            else
            {
                EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (useRectFieldTypePropertyHeight == true)
            {
                return (EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight) * 2;
            }

            return base.GetPropertyHeight(property, label);
        }
    }
#endif

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

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Fix64RangeAttribute))]
    public class Fix64RangeAttributePropertyDrawer : PropertyDrawer
    {
        private readonly string unsupportedUsageErrorMessage = $"Use of {nameof(Fix64RangeAttribute)} is not compatible with this field's type.";
        private readonly string coreFieldName = "_serializedValue";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty coreFieldLongValueProperty = property.FindPropertyRelative(coreFieldName);

            if (coreFieldLongValueProperty == null)
            {
                EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
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
                EditorGUI.LabelField(position, unsupportedUsageErrorMessage);
            }
        }
    }
#endif
}