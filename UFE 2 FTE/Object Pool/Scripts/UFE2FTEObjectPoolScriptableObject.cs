using System.Collections.Generic;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Object Pool", menuName = "U.F.E. 2 F.T.E./Object Pool/Object Pool")]
    public class UFE2FTEObjectPoolScriptableObject : ScriptableObject
    {
        private enum StorageMode
        {
            Prefab,
            ResourcesFolder
        }
        [SerializeField]
        private StorageMode prefabLoadingMode;
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private string prefabResourcesPath;
        [SerializeField]
        private int maxObjectPoolSize;
        [SerializeField]
        private bool useObjectPoolCanGrow;

        private List<UFE2FTEObjectPoolOptionsManager.PooledGameObjectData> pooledGameObjectDataList;
        private List<UFE2FTEObjectPoolOptionsManager.PooledGameObjectData> GetPooledGameObjectDataList()
        {
            if (pooledGameObjectDataList == null)
            {
                pooledGameObjectDataList = new List<UFE2FTEObjectPoolOptionsManager.PooledGameObjectData>();
            }

            int count = pooledGameObjectDataList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (pooledGameObjectDataList[i] != null
                    && pooledGameObjectDataList[i].pooledGameObject != null
                    && pooledGameObjectDataList[i].pooledGameObjectTransform)
                {
                    continue;
                }

                pooledGameObjectDataList.RemoveAt(i);
            }

            return pooledGameObjectDataList;
        }

        public static void SpawnAllPooledGameObjects(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null)
            {
                return;       
            }

            SpawnAllPooledGameObjectsData(objectPoolScriptableObject);
        }

        public static void SpawnAllPooledGameObjects(UFE2FTEObjectPoolScriptableObject[] objectPoolScriptableObjectArray)
        {
            if (objectPoolScriptableObjectArray == null)
            {
                return;       
            }

            int length = objectPoolScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnAllPooledGameObjects(objectPoolScriptableObjectArray[i]);
            }
        }

        private static void SpawnAllPooledGameObjectsData(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            CleanUpOverSizedObjectPool(objectPoolScriptableObject);

            if (objectPoolScriptableObject == null)
            {
                return;
            }

            int remainingObjectPoolSize = objectPoolScriptableObject.maxObjectPoolSize - objectPoolScriptableObject.GetPooledGameObjectDataList().Count;
            for (int i = 0; i < remainingObjectPoolSize; i++)
            {
                SpawnPooledGameObjectData(objectPoolScriptableObject);
            }
        }

        private static void SpawnPooledGameObjectData(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null)
            {
                return;
            }

            GetNewPooledGameObjectData(objectPoolScriptableObject);
        }

        public static UFE2FTEObjectPoolOptionsManager.PooledGameObjectData GetNewPooledGameObjectData(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null
                || Application.isPlaying == false)
            {
                return null;
            }

            if (objectPoolScriptableObject.prefabLoadingMode == StorageMode.Prefab)
            {
                if (objectPoolScriptableObject.prefab == null)
                {
                    return null;
                }

                UFE2FTEObjectPoolOptionsManager.PooledGameObjectData newPooledGameObjectData = new UFE2FTEObjectPoolOptionsManager.PooledGameObjectData();
                newPooledGameObjectData.pooledGameObject = Instantiate(objectPoolScriptableObject.prefab);
                newPooledGameObjectData.pooledGameObject.SetActive(false);
                newPooledGameObjectData.pooledGameObjectTransform = newPooledGameObjectData.pooledGameObject.transform;

                objectPoolScriptableObject.GetPooledGameObjectDataList().Add(newPooledGameObjectData);

                UFE2FTEObjectPoolOptionsManager.AddPooledGameObjectToPooledGameObjectList(newPooledGameObjectData.pooledGameObject);

                UFE2FTEObjectPoolEventsManager.CallOnNewPooledGameObjectData(newPooledGameObjectData);

                return newPooledGameObjectData;
            }
            else if (objectPoolScriptableObject.prefabLoadingMode == StorageMode.ResourcesFolder)
            {
                GameObject newGameObject = Resources.Load<GameObject>(objectPoolScriptableObject.prefabResourcesPath);

                if (newGameObject == null)
                {
                    return null;
                }

                UFE2FTEObjectPoolOptionsManager.PooledGameObjectData newPooledGameObjectData = new UFE2FTEObjectPoolOptionsManager.PooledGameObjectData();
                newPooledGameObjectData.pooledGameObject = newGameObject;
                newPooledGameObjectData.pooledGameObject.SetActive(false);
                newPooledGameObjectData.pooledGameObjectTransform = newPooledGameObjectData.pooledGameObject.transform;

                objectPoolScriptableObject.GetPooledGameObjectDataList().Add(newPooledGameObjectData);

                UFE2FTEObjectPoolOptionsManager.AddPooledGameObjectToPooledGameObjectList(newPooledGameObjectData.pooledGameObject);

                UFE2FTEObjectPoolEventsManager.CallOnNewPooledGameObjectData(newPooledGameObjectData);
            }

            return null;
        }

        public static UFE2FTEObjectPoolOptionsManager.PooledGameObjectData GetPooledGameObjectData(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null)
            {
                return null;
            }

            int count = objectPoolScriptableObject.GetPooledGameObjectDataList().Count;
            for (int i = 0; i < count; i++)
            {
                if (objectPoolScriptableObject.GetPooledGameObjectDataList()[i].pooledGameObject == null
                    || objectPoolScriptableObject.GetPooledGameObjectDataList()[i].pooledGameObject.activeInHierarchy == true)
                {
                    continue;
                }

                return objectPoolScriptableObject.GetPooledGameObjectDataList()[i];
            }

            if (objectPoolScriptableObject.useObjectPoolCanGrow == true)
            {
                return GetNewPooledGameObjectData(objectPoolScriptableObject);
            }

            return null;
        }

        private static void CleanUpOverSizedObjectPool(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null)
            {
                return;
            }

            int count = objectPoolScriptableObject.GetPooledGameObjectDataList().Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (i < objectPoolScriptableObject.maxObjectPoolSize)
                {
                    continue;
                }

                DestroyPooledGameObject(objectPoolScriptableObject.pooledGameObjectDataList[i]);
            }

            objectPoolScriptableObject.GetPooledGameObjectDataList();
        }

        public static void SetActiveAllPooledGameObjects(bool active, UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null)
            {
                return;
            }

            int count = objectPoolScriptableObject.GetPooledGameObjectDataList().Count;
            for (int i = 0; i < count; i++)
            {
                SetActivePooledGameObject(active, objectPoolScriptableObject.GetPooledGameObjectDataList()[i]);
            }
        }

        private static void SetActivePooledGameObject(bool active, UFE2FTEObjectPoolOptionsManager.PooledGameObjectData pooledGameObjectData)
        {
            if (pooledGameObjectData == null
                || pooledGameObjectData.pooledGameObject == null)
            {
                return;
            }

            pooledGameObjectData.pooledGameObject.SetActive(active);
        }

        public static void DestroyAllPooledGameObjects(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            if (objectPoolScriptableObject == null)
            {
                return;
            }

            int count = objectPoolScriptableObject.GetPooledGameObjectDataList().Count;
            for (int i = 0; i < count; i++)
            {
                DestroyPooledGameObject(objectPoolScriptableObject.pooledGameObjectDataList[i]);
            }

            objectPoolScriptableObject.GetPooledGameObjectDataList();
        }

        private static void DestroyPooledGameObject(UFE2FTEObjectPoolOptionsManager.PooledGameObjectData pooledGameObjectData)
        {
            if (pooledGameObjectData == null
                || pooledGameObjectData.pooledGameObject == null)
            {
                return;
            }

            Destroy(pooledGameObjectData.pooledGameObject);
        }

        [NaughtyAttributes.Button]
        private void SpawnAllObjectPoolGameObjects()
        {
            SpawnAllPooledGameObjects(this);
        }

        [NaughtyAttributes.Button]
        private void SpawnNewObjectPoolGameObject()
        {
            GetNewPooledGameObjectData(this);
        }

        [NaughtyAttributes.Button]
        private void GetObjectPoolGameObject()
        {
            UFE2FTEObjectPoolOptionsManager.PooledGameObjectData pooledGameObjectData = GetPooledGameObjectData(this);

            if (UFE2FTEObjectPoolOptionsManager.PooledGameObjectData.IsValidPooledGameObjectData(pooledGameObjectData) == true)
            {
                pooledGameObjectData.pooledGameObject.SetActive(true);
            }
        }

        [NaughtyAttributes.Button]
        private void DisableAllObjectPoolGameObjects()
        {
            SetActiveAllPooledGameObjects(false, this);
        }

        [NaughtyAttributes.Button]
        private void EnableAllObjectPoolGameObjects()
        {
            SetActiveAllPooledGameObjects(true, this);
        }

        [NaughtyAttributes.Button]
        private void DestroyAllObjectPoolGameObjects()
        {
            DestroyAllPooledGameObjects(this);
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
