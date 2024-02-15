#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class BulkUIComponentModifierWindow : EditorWindow
    {
        private Vector2 verticalScrollPosition = new Vector2(0, 0);

        private SerializedObject cachedSerializedObject;    
        public GameObject[] targetGameObjectArray;
        //private Sprite test;
        private Font font;

        [MenuItem("Window/FreedTerror/Bulk UI Component Modifier")]
        public static void ShowWindow()
        {
            EditorWindow editorWindow = GetWindow<BulkUIComponentModifierWindow>();
            editorWindow.titleContent = new GUIContent("Bulk UI Component Modifier");
        }

        private void OnEnable()
        {
            ScriptableObject target = this;
            cachedSerializedObject = new SerializedObject(target);
        }

        private void OnGUI()
        {
            verticalScrollPosition = EditorGUILayout.BeginScrollView(verticalScrollPosition);
            //test = (Sprite)EditorGUILayout.ObjectField("Test", test, typeof(Sprite), false);
            cachedSerializedObject.Update();
            SerializedProperty property = cachedSerializedObject.FindProperty(nameof(targetGameObjectArray));
            EditorGUILayout.PropertyField(property, true);
            cachedSerializedObject.ApplyModifiedProperties();

            font = (Font)EditorGUILayout.ObjectField("Font", font, typeof(Font), false);

            EditorGUILayout.LabelField("Canvas Renderer Component Options");
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(DisableCullTransparentMeshOnAllCanvasRendererComponents))))
            {
                DisableCullTransparentMeshOnAllCanvasRendererComponents();
            }
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(EnableCullTransparentMeshOnAllCanvasRendererComponents))))
            {
                EnableCullTransparentMeshOnAllCanvasRendererComponents();
            }

            EditorGUILayout.LabelField("Image Component Options");
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(DisableRaycastTargetOnAllImageComponents))))
            {
                DisableRaycastTargetOnAllImageComponents();
            }
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(EnableRaycastTargetOnAllImageComponents))))
            {
                EnableRaycastTargetOnAllImageComponents();
            }

            EditorGUILayout.LabelField("Raw Image Component Options");
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(DisableRaycastTargetOnAllRawImageComponents))))
            {
                DisableRaycastTargetOnAllRawImageComponents();
            }
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(EnableRaycastTargetOnAllRawImageComponents))))
            {
                EnableRaycastTargetOnAllRawImageComponents();
            }

            EditorGUILayout.LabelField("Text Component Options");
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(ReplaceFontOnAllTextComponents))))
            {
                ReplaceFontOnAllTextComponents();
            }
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(DisableRaycastTargetOnAllTextComponents))))
            {
                DisableRaycastTargetOnAllTextComponents();
            }
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(EnableRaycastTargetOnAllTextComponents))))
            {
                EnableRaycastTargetOnAllTextComponents();
            }

            EditorGUILayout.LabelField("Graphic Component Options");
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(DisableRaycastTargetOnAllGraphicComponents))))
            {
                DisableRaycastTargetOnAllGraphicComponents();
            }
            if (GUILayout.Button(Utility.AddSpacesBeforeCapitalLetters(nameof(EnableRaycastTargetOnAllGraphicComponents))))
            {
                EnableRaycastTargetOnAllGraphicComponents();
            }
            EditorGUILayout.EndScrollView();
        }

        private void DisableCullTransparentMeshOnAllCanvasRendererComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<CanvasRenderer>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.cullTransparentMesh = false;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void EnableCullTransparentMeshOnAllCanvasRendererComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<CanvasRenderer>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.cullTransparentMesh = true;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void DisableRaycastTargetOnAllImageComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Image>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = false;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void EnableRaycastTargetOnAllImageComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Image>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = true;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void DisableRaycastTargetOnAllRawImageComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<RawImage>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = false;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void EnableRaycastTargetOnAllRawImageComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<RawImage>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = true;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void ReplaceFontOnAllTextComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Text>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.font = font;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void DisableRaycastTargetOnAllTextComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Text>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = false;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void EnableRaycastTargetOnAllTextComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Text>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = true;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void DisableRaycastTargetOnAllGraphicComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Graphic>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = false;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }

        private void EnableRaycastTargetOnAllGraphicComponents()
        {
            int length = targetGameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = targetGameObjectArray[i];
                if (item == null)
                {
                    continue;
                }

                var foundObjects = targetGameObjectArray[i].GetComponentsInChildren<Graphic>(true);
                if (foundObjects == null)
                {
                    continue;
                }
                int lengthA = foundObjects.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    var itemA = foundObjects[a];
                    if (itemA == null)
                    {
                        continue;
                    }

                    itemA.raycastTarget = true;
                }

                if (PrefabUtility.IsPartOfAnyPrefab(item) == true)
                {
                    PrefabUtility.SavePrefabAsset(item);
                }
            }
        }
    }
}
#endif