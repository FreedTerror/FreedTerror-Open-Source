#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FPLibrary;

[CustomPropertyDrawer(typeof(Fix64InspectorAttribute))]
public class Fix64InspectorAttributePropertyDrawer : PropertyDrawer
{
    private readonly string unsupportedUsageErrorMessageName = $"Use of {nameof(Fix64InspectorAttribute)} is not compatible with this field's type.";

    private readonly string coreFieldName = "_serializedValue";
    private readonly string xFieldName = "x";
    private readonly string _xFieldName = "_x";
    private readonly string yFieldName = "y";
    private readonly string _yFieldName = "_y";
    private readonly string zFieldName = "z";
    private readonly string widthFieldName = "width";
    private readonly string heightFieldName = "height";

    private SerializedProperty coreFieldLongValueProperty = null;
    private SerializedProperty xFieldLongValueProperty = null;
    private SerializedProperty yFieldLongValueProperty = null;
    private SerializedProperty zFieldLongValueProperty = null;
    private SerializedProperty widthFieldLongValueProperty = null;
    private SerializedProperty heightFieldLongValueProperty = null;

    private bool isRectType;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (fieldInfo.FieldType == typeof(Fix64)
            || fieldInfo.FieldType == typeof(Fix64[])
            || fieldInfo.FieldType == typeof(List<Fix64>))
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

            float newFloat = EditorGUI.FloatField(
                position, 
                label, 
                (float)Fix64.FromRaw(coreFieldLongValueProperty.longValue));

            coreFieldLongValueProperty.longValue = ((Fix64)newFloat).RawValue;
        }
        else if(fieldInfo.FieldType == typeof(FPVector2)
            || fieldInfo.FieldType == typeof(FPVector2[])
            || fieldInfo.FieldType == typeof(List<FPVector2>))
        {
            if (xFieldLongValueProperty == null
                || yFieldLongValueProperty == null)
            {
                xFieldLongValueProperty = property.FindPropertyRelative(xFieldName).FindPropertyRelative(coreFieldName);
                yFieldLongValueProperty = property.FindPropertyRelative(yFieldName).FindPropertyRelative(coreFieldName);

                if (xFieldLongValueProperty == null
                    || yFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);

                    return;
                }
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
            if (xFieldLongValueProperty == null
                || yFieldLongValueProperty == null
                || zFieldLongValueProperty == null)
            {
                xFieldLongValueProperty = property.FindPropertyRelative(xFieldName).FindPropertyRelative(coreFieldName);
                yFieldLongValueProperty = property.FindPropertyRelative(yFieldName).FindPropertyRelative(coreFieldName);
                zFieldLongValueProperty = property.FindPropertyRelative(zFieldName).FindPropertyRelative(coreFieldName);

                if (xFieldLongValueProperty == null
                    || yFieldLongValueProperty == null
                    || zFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);

                    return;
                }
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
            if (xFieldLongValueProperty == null
                || yFieldLongValueProperty == null
                || widthFieldLongValueProperty == null
                || heightFieldLongValueProperty == null)
            {
                xFieldLongValueProperty = property.FindPropertyRelative(_xFieldName).FindPropertyRelative(coreFieldName);
                yFieldLongValueProperty = property.FindPropertyRelative(_yFieldName).FindPropertyRelative(coreFieldName);
                widthFieldLongValueProperty = property.FindPropertyRelative(widthFieldName).FindPropertyRelative(coreFieldName);
                heightFieldLongValueProperty = property.FindPropertyRelative(heightFieldName).FindPropertyRelative(coreFieldName);

                if (xFieldLongValueProperty == null
                    || yFieldLongValueProperty == null
                    || widthFieldLongValueProperty == null
                    || heightFieldLongValueProperty == null)
                {
                    EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);

                    return;
                }
            }

            isRectType = true;

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
        }
        else
        {        
            EditorGUI.LabelField(position, unsupportedUsageErrorMessageName);      
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (isRectType == true)
        {
            return (EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight) * 2;
        }

        return base.GetPropertyHeight(property, label);
    }
}
#endif
