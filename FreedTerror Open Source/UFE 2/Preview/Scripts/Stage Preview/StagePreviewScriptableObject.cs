using UnityEngine;
using UnityEngine.SceneManagement;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Preview/Stage Preview", fileName = "New Stage Preview")]
    public class StagePreviewScriptableObject : ScriptableObject
    {
        [System.Serializable]
        public class StagePreviewOptions
        {
            public string[] stageNameArray;
            public GameObject stagePreviewPrefab;
            [HideInInspector]
            public GameObject stagePreviewGameObject;
            public string stagePreviewPrefabResourcesPath;
            [HideInInspector]
            public GameObject stagePreviewResourcesGameObject;
            public string stagePreviewScenePath;
        }
        public StagePreviewOptions[] stagePreviewOptionsArray;

        public void PreviewStage(string stageName)
        {
            if (stagePreviewOptionsArray == null)
            {
                return;
            }

            int length = stagePreviewOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                PreviewStage(stageName, stagePreviewOptionsArray[i]);
            }
        }

        public void PreviewStage(string stageName, StagePreviewOptions stagePreviewOptions)
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

        public void LoadStagePreview(StagePreviewOptions stagePreviewOptions)
        {
            if (stagePreviewOptions == null)
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
                GameObject loadedGameObject = Resources.Load<GameObject>(stagePreviewOptions.stagePreviewPrefabResourcesPath);

                if (loadedGameObject != null)
                {
                    stagePreviewOptions.stagePreviewResourcesGameObject = Instantiate(loadedGameObject);
                }
            }

            if (stagePreviewOptions.stagePreviewScenePath != "")
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    if (SceneManager.GetSceneAt(i).path == stagePreviewOptions.stagePreviewScenePath)
                    {
                        return;
                    }
                }

                SceneManager.LoadScene(stagePreviewOptions.stagePreviewScenePath, LoadSceneMode.Additive);
            }
        }

        public void LoadAllStagePreview()
        {
            int length = stagePreviewOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                LoadStagePreview(stagePreviewOptionsArray[i]);
            }
        }

        public void UnloadStagePreview(StagePreviewOptions stagePreviewOptions)
        {
            if (stagePreviewOptions == null)
            {
                return;
            }

            Destroy(stagePreviewOptions.stagePreviewGameObject);
            stagePreviewOptions.stagePreviewGameObject = null;

            Destroy(stagePreviewOptions.stagePreviewResourcesGameObject);
            stagePreviewOptions.stagePreviewResourcesGameObject = null;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).path != stagePreviewOptions.stagePreviewScenePath)
                {
                    continue; 
                }

                SceneManager.UnloadSceneAsync(stagePreviewOptions.stagePreviewScenePath);
            }
        }

        public void UnloadAllStagePreview()
        {
            int length = stagePreviewOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                UnloadStagePreview(stagePreviewOptionsArray[i]);
            }
        }
    }
}