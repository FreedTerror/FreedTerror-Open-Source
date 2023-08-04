using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Stage Preview", menuName = "U.F.E. 2 F.T.E./Stage Preview/Stage Preview")]
    public class UFE2FTEStagePreviewScriptableObject : ScriptableObject
    {
        [Serializable]
        public class StagePreviewOptions
        {
            [SerializeField]
            private string[] stageNameArray;
            [SerializeField]
            private GameObject stagePreviewPrefab;
            private GameObject stagePreviewGameObject;
            [SerializeField]
            private string stagePreviewPrefabResourcesPath;
            private GameObject stagePreviewResourcesGameObject;
            [SerializeField]
            private string stagePreviewScenePath;

            public static void SetStagePreviewByStageName(string stageName, StagePreviewOptions stagePreviewOptions)
            {
                if (stagePreviewOptions == null)
                {
                    return;
                }

                int length = stagePreviewOptions.stageNameArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (stageName != stagePreviewOptions.stageNameArray[i])
                    {
                        continue;
                    }

                    LoadStagePreview(stagePreviewOptions);

                    return;
                }

                UnloadStagePreview(stagePreviewOptions);
            }

            public static void SetStagePreviewByStageName(string stageName, StagePreviewOptions[] stagePreviewOptionsArray)
            {
                if (stagePreviewOptionsArray == null)
                {
                    return;
                }

                int length = stagePreviewOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetStagePreviewByStageName(stageName, stagePreviewOptionsArray[i]);
                }
            }

            public static void LoadStagePreview(StagePreviewOptions stagePreviewOptions)
            {
                if (stagePreviewOptions == null
                    || Application.isPlaying == false)
                {
                    return;
                }

                if (stagePreviewOptions.stagePreviewPrefab != null
                    && stagePreviewOptions.stagePreviewGameObject == null)
                {
                    stagePreviewOptions.stagePreviewGameObject = Instantiate(stagePreviewOptions.stagePreviewPrefab);
                }

                if (stagePreviewOptions.stagePreviewResourcesGameObject == null)
                {
                    GameObject newGameObject = Resources.Load<GameObject>(stagePreviewOptions.stagePreviewPrefabResourcesPath);

                    if (newGameObject != null)
                    {
                        stagePreviewOptions.stagePreviewResourcesGameObject = Instantiate(newGameObject);
                    }       
                }

                if (stagePreviewOptions.stagePreviewScenePath != "")
                {
                    bool loadScene = true;
                    for (int i = 0; i < SceneManager.sceneCount; i++)
                    {
                        if (SceneManager.GetSceneAt(i).path != stagePreviewOptions.stagePreviewScenePath)
                        {
                            continue;
                        }

                        loadScene = false;

                        break;
                    }

                    if (loadScene == true)
                    {
                        SceneManager.LoadScene(stagePreviewOptions.stagePreviewScenePath, LoadSceneMode.Additive);
                    }
                }
            }

            public static void UnloadStagePreview(StagePreviewOptions stagePreviewOptions)
            {
                if (stagePreviewOptions == null)
                {
                    return;
                }

                Destroy(stagePreviewOptions.stagePreviewGameObject);

                Destroy(stagePreviewOptions.stagePreviewResourcesGameObject);

                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    if (SceneManager.GetSceneAt(i).path != stagePreviewOptions.stagePreviewScenePath)
                    {
                        continue;
                    }

                    SceneManager.UnloadSceneAsync(stagePreviewOptions.stagePreviewScenePath);
                }
            }
        }
        public StagePreviewOptions[] stagePreviewOptionsArray;
    }
}