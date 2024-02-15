using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace FreedTerror
{
    /*
#if UNITY_EDITOR
    [MethodButton(
        "propertyPath",
        "MethodName1",
        nameof(MethodName2))]
    [SerializeField]
    private bool propertyPathMethodButtons;
#endif
    */

    /// <summary>
    /// Usage:
    /// <para>#if UNITY_EDITOR</para>
    /// <para>[MethodButton("propertyPath", MethodName1, nameof(MethodName2))</para>
    /// <para>[SerializeField] private bool propertyPathMethodButtons;</para>
    /// <para>#endif</para>
    /// </summary>
    public class MethodButtonAttribute : PropertyAttribute
    {
        public readonly string propertyPath = null;
        public readonly string[] methodNameArray;

        public MethodButtonAttribute(string propertyPath, params string[] args)
        {
            this.propertyPath = propertyPath;
            methodNameArray = args;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MethodButtonAttribute))]
    public class MethodButtonAttributeDrawer : PropertyDrawer
    {
        private int buttonCount = 0;
        private readonly float buttonHeight = EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            buttonCount = 0;

            MethodButtonAttribute methodButtonAttribute = (MethodButtonAttribute)attribute;

            if (methodButtonAttribute.propertyPath == null
                || methodButtonAttribute.propertyPath == "")
            {
                if (property == null)
                {
                    buttonCount++;
                    EditorGUI.LabelField(GetButtonRect(position), GetTargetPropertyErrorMessage(methodButtonAttribute.propertyPath));
                    return;
                }

                foreach (string methodName in methodButtonAttribute.methodNameArray)
                {
                    MethodInfo methodInfo = property.serializedObject.targetObject.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    if (methodInfo == null)
                    {
                        buttonCount++;
                        EditorGUI.LabelField(GetButtonRect(position), GetMethodInfoErrorMessage(methodName));
                        continue;
                    }

                    buttonCount++;
                    if (GUI.Button(GetButtonRect(position), AddSpacesBeforeCapitalLetters(methodName)))
                    {
                        methodInfo.Invoke(property.serializedObject.targetObject, null);
                    }
                }
            }
            else
            {
                SerializedProperty targetProperty = property.serializedObject.FindProperty(methodButtonAttribute.propertyPath);
                if (targetProperty == null)
                {
                    buttonCount++;
                    EditorGUI.LabelField(GetButtonRect(position), GetTargetPropertyErrorMessage(methodButtonAttribute.propertyPath));
                    return;
                }

                foreach (string methodName in methodButtonAttribute.methodNameArray)
                {
                    MethodInfo methodInfo = GetTargetObjectOfProperty(targetProperty).GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    if (methodInfo == null)
                    {
                        buttonCount++;
                        EditorGUI.LabelField(GetButtonRect(position), GetMethodInfoErrorMessage(methodName));
                        continue;
                    }

                    buttonCount++;
                    if (GUI.Button(GetButtonRect(position), AddSpacesBeforeCapitalLetters(methodName)))
                    {
                        methodInfo.Invoke(GetTargetObjectOfProperty(targetProperty), null);
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true) + (buttonHeight) * (buttonCount);
        }

        private Rect GetButtonRect(Rect position)
        {
            return new Rect(position.x, position.y + ((buttonHeight) * (buttonCount)), position.width, buttonHeight);
        }

        private string GetTargetPropertyErrorMessage(string propertyPath)
        {
            return nameof(MethodButtonAttribute) + " is unable to find " + nameof(SerializedProperty) + ": " + propertyPath;
        }

        private string GetMethodInfoErrorMessage(string methodName)
        {
            return nameof(MethodButtonAttribute) + " is unable to find " + nameof(MethodInfo) + ": " + methodName;
        }

        private static string AddSpacesBeforeCapitalLetters(string message)
        {
            return message = string.Concat(message.Select(x => System.Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static object GetTargetObjectOfProperty(UnityEditor.SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }

        private static void SetTargetObjectOfProperty(UnityEditor.SerializedProperty prop, object value)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }

            if (Object.ReferenceEquals(obj, null)) return;

            try
            {
                var element = elements.Last();

                if (element.Contains("["))
                {
                    var tp = obj.GetType();
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    var field = tp.GetField(elementName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var arr = field.GetValue(obj) as System.Collections.IList;
                    arr[index] = value;
                    //var elementName = element.Substring(0, element.IndexOf("["));
                    //var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    //var arr = DynamicUtil.GetValue(element, elementName) as System.Collections.IList;
                    //if (arr != null) arr[index] = value;
                }
                else
                {
                    var tp = obj.GetType();
                    var field = tp.GetField(element, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(obj, value);
                    }
                    //DynamicUtil.SetValue(obj, element, value);
                }
            }
            catch
            {
                return;
            }
        }
    }
#endif
}