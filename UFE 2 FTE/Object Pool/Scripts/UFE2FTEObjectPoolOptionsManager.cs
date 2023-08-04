using System;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public static class UFE2FTEObjectPoolOptionsManager
    {
        [Serializable]
        public class PooledGameObjectData
        {
            public GameObject pooledGameObject;
            public Transform pooledGameObjectTransform;

            public static bool IsValidPooledGameObjectData(PooledGameObjectData pooledGameObjectData)
            {
                if (pooledGameObjectData == null
                    || pooledGameObjectData.pooledGameObject == null
                    || pooledGameObjectData.pooledGameObjectTransform == null)
                {
                    return false;
                }

                return true;
            }
        }

        [Serializable]
        public class ObjectPoolScriptableObjectOptions
        {
            public UFE2FTEObjectPoolTransformScriptableObject objectPoolTransformScriptableObject;
            public UFE2FTEObjectPoolScriptableObject[] objectPoolScriptableObjectArray;     
        }

        [Serializable]
        public class UpdateObjectPoolsOptions
        {
            public UFE2FTEObjectPoolScriptableObjectManager[] objectPoolScriptableObjectManagerArray;

            [Serializable]
            public class UpdateAllObjectPoolsOptions
            {
                public bool useOnEnable;
                public bool useOnStart;
                public bool useOnDisable;
                public bool useOnDestroy;
            }

            [Serializable]
            public class UpdateObjectPoolsByObjectPoolNameOptions
            {
                public bool useOnEnable;
                public bool useOnStart;
                public bool useOnDisable;
                public bool useOnDestroy;
                public string[] objectPoolNameArray;
            }
            public UpdateAllObjectPoolsOptions spawnAllObjectPoolsOptions;
            public UpdateObjectPoolsByObjectPoolNameOptions spawnObjectPoolsByObjectPoolNameOptions;
            public UpdateAllObjectPoolsOptions spawnObjectPoolsByUFEPreloadOptions;
            public UpdateAllObjectPoolsOptions disableAllObjectPoolsOptions;
            public UpdateAllObjectPoolsOptions destroyAllObjectPoolsOptions;
        }

        public static int maxPooledGameObjects;

        private static List<GameObject> pooledGameObjectList;
        private static List<GameObject> GetPooledGameObjectList()
        {
            if (pooledGameObjectList == null)
            {
                pooledGameObjectList = new List<GameObject>();
            }

            int count = pooledGameObjectList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (pooledGameObjectList[i] != null)
                {
                    continue;
                }

                pooledGameObjectList.RemoveAt(i);
            }

            return pooledGameObjectList;
        }

        public static void AddPooledGameObjectToPooledGameObjectList(GameObject pooledGameObject)
        {
            if (pooledGameObject == null)
            {
                return;
            }

            int count = GetPooledGameObjectList().Count;
            for (int i = 0; i < count; i++)
            {
                if (pooledGameObject != pooledGameObjectList[i])
                {
                    continue;
                }

                return;
            }

            GetPooledGameObjectList().Add(pooledGameObject);

            CheckMaxPooledGameObjects(maxPooledGameObjects);
        }

        private static void CheckMaxPooledGameObjects(int maxPooledGameObjects)
        {
            int count = GetPooledGameObjectList().Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (i < maxPooledGameObjects)
                {
                    continue;
                }

                UnityEngine.Object.Destroy(pooledGameObjectList[i]);
            }

            GetPooledGameObjectList();
        }

        public static void SpawnPooledGameObject(ObjectPoolScriptableObjectOptions objectPoolScriptableObjectOptions, Transform transform, ControlsScript player, ProjectileMoveScript projectileMoveScript, HitBox strokeHitBox)
        {
            if (objectPoolScriptableObjectOptions == null)
            {
                return;
            }

            int length = objectPoolScriptableObjectOptions.objectPoolScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                PooledGameObjectData pooledGameObjectData = UFE2FTEObjectPoolScriptableObject.GetPooledGameObjectData(objectPoolScriptableObjectOptions.objectPoolScriptableObjectArray[i]);

                if (PooledGameObjectData.IsValidPooledGameObjectData(pooledGameObjectData) == false)
                {
                    continue;
                }

                pooledGameObjectData.pooledGameObject.SetActive(true);

                pooledGameObjectData.pooledGameObjectTransform.position = UFE2FTEObjectPoolTransformScriptableObject.GetPosition(objectPoolScriptableObjectOptions.objectPoolTransformScriptableObject, transform, player, projectileMoveScript, strokeHitBox);

                pooledGameObjectData.pooledGameObjectTransform.localEulerAngles = UFE2FTEObjectPoolTransformScriptableObject.GetRotation(objectPoolScriptableObjectOptions.objectPoolTransformScriptableObject, transform, player, projectileMoveScript);

                pooledGameObjectData.pooledGameObjectTransform.localScale = UFE2FTEObjectPoolTransformScriptableObject.GetScale(objectPoolScriptableObjectOptions.objectPoolTransformScriptableObject, transform, player, projectileMoveScript);
            }
        }

        public static void SpawnPooledGameObject(ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray, Transform transform, ControlsScript player, ProjectileMoveScript projectileMoveScript, HitBox strokeHitBox)
        {
            if (objectPoolScriptableObjectOptionsArray == null)
            {
                return;
            }

            int length = objectPoolScriptableObjectOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnPooledGameObject(objectPoolScriptableObjectOptionsArray[i], transform, player, projectileMoveScript, strokeHitBox);
            }
        }

        public static void SetActiveAllPooledGameObjects(bool active)
        {
            int count = GetPooledGameObjectList().Count;
            for (int i = 0; i < count; i++)
            {
                if (pooledGameObjectList[i] == null)
                {
                    continue;
                }

                pooledGameObjectList[i].SetActive(active);
            }
        }

        public static void DestroyAllPooledGameObjects()
        {
            int count = GetPooledGameObjectList().Count;
            for (int i = 0; i < count; i++)
            {
                UnityEngine.Object.Destroy(pooledGameObjectList[i]);
            }

            GetPooledGameObjectList();
        }

        public static void SpawnObjectPoolsByObjectPoolScriptableObjectManager(UFE2FTEObjectPoolScriptableObjectManager objectPoolScriptableObjectManager)
        {
            if (objectPoolScriptableObjectManager == null)
            {
                return;
            }

            SpawnObjectPoolByObjectPoolScriptableObject(objectPoolScriptableObjectManager.objectPoolScriptableObjectArray);
        }

        public static void SpawnObjectPoolsByObjectPoolScriptableObjectManager(UFE2FTEObjectPoolScriptableObjectManager[] objectPoolScriptableObjectManagerArray = null)
        {
            if (objectPoolScriptableObjectManagerArray == null)
            {
                return;
            }

            int length = objectPoolScriptableObjectManagerArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnObjectPoolByObjectPoolScriptableObject(objectPoolScriptableObjectManagerArray[i].objectPoolScriptableObjectArray);
            }
        }

        public static void SpawnObjectPoolsByObjectPoolName(UFE2FTEObjectPoolScriptableObjectManager objectPoolScriptableObjectManager, string objectPoolName, string[] objectPoolNameArray)
        {
            if (objectPoolScriptableObjectManager == null)
            {
                return;     
            }

            if (objectPoolName != null)
            {
                int length = objectPoolScriptableObjectManager.objectPoolNameArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (objectPoolScriptableObjectManager.objectPoolNameArray[i] != objectPoolName)
                    {
                        continue;
                    }

                    SpawnObjectPoolByObjectPoolScriptableObject(objectPoolScriptableObjectManager.objectPoolScriptableObjectArray);
                }
            }

            if (objectPoolNameArray != null)
            {
                int length = objectPoolScriptableObjectManager.objectPoolNameArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = objectPoolNameArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (objectPoolScriptableObjectManager.objectPoolNameArray[i] != objectPoolNameArray[a])
                        {
                            continue;
                        }

                        SpawnObjectPoolByObjectPoolScriptableObject(objectPoolScriptableObjectManager.objectPoolScriptableObjectArray);
                    }
                }
            }
        }

        public static void SpawnObjectPoolsByObjectPoolName(UFE2FTEObjectPoolScriptableObjectManager[] objectPoolScriptableObjectManagerArray, string objectPoolName, string[] objectPoolNameArray)
        {
            if (objectPoolScriptableObjectManagerArray == null)
            {
                return;
            }

            int length = objectPoolScriptableObjectManagerArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnObjectPoolsByObjectPoolName(objectPoolScriptableObjectManagerArray[i], objectPoolName, objectPoolNameArray);
            }
        }

        public static void SpawnObjectPoolsByUFEPreload(UFE2FTEObjectPoolScriptableObjectManager objectPoolScriptableObjectManager)
        {
            if (objectPoolScriptableObjectManager == null
                || UFE.config == null
                || UFE.GetPlayer1() == null
                || UFE.GetPlayer2() == null)
            {
                return;
            }

            SpawnObjectPoolsByObjectPoolName(objectPoolScriptableObjectManager, UFE.GetPlayer1().characterName, null);

            SpawnObjectPoolsByObjectPoolName(objectPoolScriptableObjectManager, UFE.GetPlayer2().characterName, null);

            List<MoveSetData> newMoveSetDataList = new List<MoveSetData>();

            foreach (MoveSetData moveSetData in UFE.GetPlayer1().moves)
            {
                newMoveSetDataList.Add(moveSetData);
            }
            foreach (string path in UFE.GetPlayer1().stanceResourcePath)
            {
                newMoveSetDataList.Add(Resources.Load<StanceInfo>(path).ConvertData());
            }

            foreach (MoveSetData moveSetData in UFE.GetPlayer2().moves)
            {
                newMoveSetDataList.Add(moveSetData);
            }
            foreach (string path in UFE.GetPlayer2().stanceResourcePath)
            {
                newMoveSetDataList.Add(Resources.Load<StanceInfo>(path).ConvertData());
            }

            foreach (MoveSetData moveSet in newMoveSetDataList)
            {
                foreach (MoveInfo move in moveSet.attackMoves)
                {
                    foreach (CharacterAssist assist in move.characterAssist)
                    {
                        if (assist.characterInfo == null)
                        {
                            continue;
                        }

                        SpawnObjectPoolsByObjectPoolName(objectPoolScriptableObjectManager, assist.characterInfo.characterName, null);
                    }
                }
            }

            newMoveSetDataList.Clear();
        }

        public static void SpawnObjectPoolsByUFEPreload(UFE2FTEObjectPoolScriptableObjectManager[] objectPoolScriptableObjectManagerArray)
        {
            if (objectPoolScriptableObjectManagerArray == null)
            {
                return;
            }

            int length = objectPoolScriptableObjectManagerArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnObjectPoolsByUFEPreload(objectPoolScriptableObjectManagerArray[i]);
            }
        }

        public static void SpawnObjectPoolByObjectPoolScriptableObject(UFE2FTEObjectPoolScriptableObject objectPoolScriptableObject)
        {
            UFE2FTEObjectPoolScriptableObject.SpawnAllPooledGameObjects(objectPoolScriptableObject);
        }

        public static void SpawnObjectPoolByObjectPoolScriptableObject(UFE2FTEObjectPoolScriptableObject[] objectPoolScriptableObjectArray)
        {
            UFE2FTEObjectPoolScriptableObject.SpawnAllPooledGameObjects(objectPoolScriptableObjectArray);
        }
    }
}