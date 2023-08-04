using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Object Pool Manager", menuName = "U.F.E. 2 F.T.E./Object Pool/Object Pool Manager")]
    public class UFE2FTEObjectPoolScriptableObjectManager : ScriptableObject
    {
        public string[] objectPoolNameArray;
        public UFE2FTEObjectPoolScriptableObject[] objectPoolScriptableObjectArray;

        [NaughtyAttributes.Button]
        private void SpawnAllObjectPools()
        {
            UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolScriptableObjectManager(this);
        }

        [NaughtyAttributes.Button]
        private void SpawnObjectPoolsByObjectPoolName()
        {
            UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolName(this, null, objectPoolNameArray);
        }

        [NaughtyAttributes.Button]
        private void SpawnObjectPoolsByUFEPreload()
        {
            UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByUFEPreload(this);
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
