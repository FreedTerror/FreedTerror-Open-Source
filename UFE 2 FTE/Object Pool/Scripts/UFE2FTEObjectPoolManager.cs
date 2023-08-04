using UnityEngine;

namespace UFE2FTE
{
    [ExecuteInEditMode]
    public class UFE2FTEObjectPoolManager : MonoBehaviour
    {
        [SerializeField]
        private int maxPooledGameObjects;

        private void Awake()
        {
            UFE2FTEObjectPoolOptionsManager.maxPooledGameObjects = maxPooledGameObjects;
        }

        private void Update()
        {
            UFE2FTEObjectPoolOptionsManager.maxPooledGameObjects = maxPooledGameObjects;
        }

        private void OnDestroy()
        {
            DestroyAllPooledGameObjects();
        }

        [NaughtyAttributes.Button]
        private void DisableAllPooledGameObjects()
        {
            UFE2FTEObjectPoolOptionsManager.SetActiveAllPooledGameObjects(false);
        }

        [NaughtyAttributes.Button]
        private void EnableAllPooledGameObjects()
        {
            UFE2FTEObjectPoolOptionsManager.SetActiveAllPooledGameObjects(true);
        }

        [NaughtyAttributes.Button]
        private void DestroyAllPooledGameObjects()
        {
            UFE2FTEObjectPoolOptionsManager.DestroyAllPooledGameObjects();
        }
    }
}