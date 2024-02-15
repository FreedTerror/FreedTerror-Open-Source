#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FreedTerror.UFE2
{
    [CustomEditor(typeof(StagePreviewScriptableObject))]
    public class StagePreviewScriptableObjectEditor : Editor
    {
        private StagePreviewScriptableObject stagePreviewScriptableObject;

        private void OnEnable()
        {
            UnityEngine.Object[] selectionArray = Selection.GetFiltered(typeof(StagePreviewScriptableObject), SelectionMode.Assets);
            if (selectionArray.Length > 0)
            {
                if (selectionArray[0] == null)
                {
                    return;
                }

                stagePreviewScriptableObject = (StagePreviewScriptableObject)selectionArray[0];
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
                if (Application.isPlaying == false)
                {
                    return;
                }

                stagePreviewScriptableObject.LoadAllStagePreview();
            }

            if (GUILayout.Button("Unload All Stage Preview"))
            {
                if (Application.isPlaying == false)
                {
                    return;
                }

                stagePreviewScriptableObject.UnloadAllStagePreview();
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Load Stage Preview " + i))
                {
                    if (Application.isPlaying == false)
                    {
                        return;
                    }

                    stagePreviewScriptableObject.LoadStagePreview(stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
                }

                if (GUILayout.Button("Unload Stage Preview " + i))
                {
                    if (Application.isPlaying == false)
                    {
                        return;
                    }

                    stagePreviewScriptableObject.UnloadStagePreview(stagePreviewScriptableObject.stagePreviewOptionsArray[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
#endif
