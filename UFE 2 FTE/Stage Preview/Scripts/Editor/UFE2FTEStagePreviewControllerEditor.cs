#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UFE2FTE
{
    [CustomEditor(typeof(UFE2FTEStagePreviewController))]
    public class UFE2FTEStagePreviewControllerEditor : Editor
    {
        private UFE2FTEStagePreviewController stagePreviewController;

        private void OnEnable()
        {
            UnityEngine.Object[] selectionArray = Selection.GetFiltered(typeof(UFE2FTEStagePreviewController), SelectionMode.Assets);
            if (selectionArray.Length > 0)
            {
                if (selectionArray[0] == null)
                {
                    return;
                }

                stagePreviewController = (UFE2FTEStagePreviewController)selectionArray[0];
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (stagePreviewController == null)
            {
                return;
            }

            if (stagePreviewController.stagePreviewScriptableObject != null)
            {
                if (stagePreviewController.stagePreviewScriptableObject.stagePreviewOptionsArray != null)
                {
                    int length = stagePreviewController.stagePreviewScriptableObject.stagePreviewOptionsArray.Length;

                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Load All Stage Preview"))
                    {
                        if (Application.isPlaying == true)
                        {
                            for (int i = 0; i < length; i++)
                            {
                                UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.LoadStagePreview(stagePreviewController.stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
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
                                UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.UnloadStagePreview(stagePreviewController.stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
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
                                UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.LoadStagePreview(stagePreviewController.stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
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
                                UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.UnloadStagePreview(stagePreviewController.stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
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
    }
}
#endif
