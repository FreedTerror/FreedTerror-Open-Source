#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UFE2FTE
{
    [CustomEditor(typeof(UFE2FTEStagePreviewScriptableObject))]
    public class UFE2FTEStagePreviewScriptableObjectEditor : Editor
    {
        private UFE2FTEStagePreviewScriptableObject stagePreviewScriptableObject;

        private void OnEnable()
        {
            UnityEngine.Object[] selectionArray = Selection.GetFiltered(typeof(UFE2FTEStagePreviewScriptableObject), SelectionMode.Assets);
            if (selectionArray.Length > 0)
            {
                if (selectionArray[0] == null)
                {
                    return;
                }

                stagePreviewScriptableObject = (UFE2FTEStagePreviewScriptableObject)selectionArray[0];
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (stagePreviewScriptableObject == null
                || stagePreviewScriptableObject.stagePreviewOptionsArray == null)
            {
                return;
            }

            int length = stagePreviewScriptableObject.stagePreviewOptionsArray.Length;
            if (length <= 0)
            {
                return;
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load All Stage Preview"))
            {
                if (Application.isPlaying == true)
                {
                    for (int i = 0; i < length; i++)
                    {
                        UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.LoadStagePreview(stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
                    }
                }
                else
                {
                    Debug.Log("Can only be used in play mode.");
                }
            }

            if (GUILayout.Button("Unload All Stage Preview"))
            {
                if (Application.isPlaying == true)
                {
                    for (int i = 0; i < length; i++)
                    {
                        UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.UnloadStagePreview(stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
                    }
                }
                else
                {
                    Debug.Log("Can only be used in play mode.");
                }
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Load Stage Preview " + i))
                {
                    if (Application.isPlaying == true)
                    {
                        UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.LoadStagePreview(stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
                    }
                    else
                    {
                        Debug.Log("Can only be used in play mode.");
                    }
                }

                if (GUILayout.Button("Unload Stage Preview " + i))
                {
                    if (Application.isPlaying == true)
                    {
                        UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.UnloadStagePreview(stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
                    }
                    else
                    {
                        Debug.Log("Can only be used in play mode.");
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
#endif
