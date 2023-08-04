using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEObjectPoolController : MonoBehaviour
    {
        private Transform myTransform;
        private ProjectileMoveScript myProjectileMoveScript;

        public UFE2FTEObjectPoolOptionsManager.UpdateObjectPoolsOptions updateObjectPoolsOptions;

        [Serializable]
        private class ObjectPoolOptions
        {
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            [Header("Unity Method Options")]
            public bool useOnEnable;
            public bool useOnStart;
            public bool useOnDisable;
            public bool useOnDestroy;
            [HideInInspector]
            public int myProjectileMoveScriptTotalHits;
            [HideInInspector]
            public bool myProjectileMoveScriptDestroyMe;
            [Header("UFE Projectile Method Options")]
            public bool useOnEachHit;       
            public bool useOnDestroyMe;
            public bool useOnDestroyMeIgnore0TotalHits;   
        }
        [SerializeField]
        private ObjectPoolOptions[] objectPoolOptions;

        private void Awake()
        {
            myTransform = transform;       
        }

        private void OnEnable()
        {
            SetObjectPoolsOptions(true);

            SetObjectPoolOptions(true);
        }

        private void Start()
        {
            myProjectileMoveScript = GetComponent<ProjectileMoveScript>();

            SetObjectPoolsOptions(false, true);

            SetObjectPoolOptions(false, true);
        }

        private void Update()
        {
            SetObjectPoolOptions();
        }

        private void OnDisable()
        {
            SetObjectPoolsOptions(false, false, true);

            SetObjectPoolOptions(false, false, true);
        }

        private void OnDestroy()
        {
            SetObjectPoolsOptions(false, false, false, true);

            SetObjectPoolOptions(false, false, false, true);
        }

        #region Set Object Pools Options Methods

        private void SetObjectPoolsOptions(bool useOnEnable = false, bool useOnStart = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            if (updateObjectPoolsOptions.spawnAllObjectPoolsOptions.useOnEnable == true
                && useOnEnable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolScriptableObjectManager(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnAllObjectPoolsOptions.useOnStart == true
                && useOnStart == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolScriptableObjectManager(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnAllObjectPoolsOptions.useOnDisable == true
                && useOnDisable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolScriptableObjectManager(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnAllObjectPoolsOptions.useOnDestroy == true
                && useOnDestroy == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolScriptableObjectManager(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.useOnEnable == true
                && useOnEnable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolName(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray, null, updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.objectPoolNameArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.useOnStart == true
                && useOnStart == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolName(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray, null, updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.objectPoolNameArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.useOnDisable == true
                && useOnDisable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolName(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray, null, updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.objectPoolNameArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.useOnDestroy == true
                && useOnDestroy == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolName(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray, null, updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.objectPoolNameArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByUFEPreloadOptions.useOnEnable == true
                && useOnEnable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByUFEPreload(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByUFEPreloadOptions.useOnStart == true
                && useOnStart == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByUFEPreload(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByUFEPreloadOptions.useOnDisable == true
                && useOnDisable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByUFEPreload(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.spawnObjectPoolsByUFEPreloadOptions.useOnDestroy == true
                && useOnDestroy == true)
            {
                UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByUFEPreload(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
            }

            if (updateObjectPoolsOptions.disableAllObjectPoolsOptions.useOnEnable == true
                && useOnEnable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SetActiveAllPooledGameObjects(false);
            }

            if (updateObjectPoolsOptions.disableAllObjectPoolsOptions.useOnStart == true
                && useOnStart == true)
            {
                UFE2FTEObjectPoolOptionsManager.SetActiveAllPooledGameObjects(false);
            }

            if (updateObjectPoolsOptions.disableAllObjectPoolsOptions.useOnDisable == true
                && useOnDisable == true)
            {
                UFE2FTEObjectPoolOptionsManager.SetActiveAllPooledGameObjects(false);
            }

            if (updateObjectPoolsOptions.disableAllObjectPoolsOptions.useOnDestroy == true
                && useOnDestroy == true)
            {
                UFE2FTEObjectPoolOptionsManager.SetActiveAllPooledGameObjects(false);
            }

            if (updateObjectPoolsOptions.destroyAllObjectPoolsOptions.useOnEnable == true
                && useOnEnable == true)
            {
                UFE2FTEObjectPoolOptionsManager.DestroyAllPooledGameObjects();
            }

            if (updateObjectPoolsOptions.destroyAllObjectPoolsOptions.useOnStart == true
                && useOnStart == true)
            {
                UFE2FTEObjectPoolOptionsManager.DestroyAllPooledGameObjects();
            }

            if (updateObjectPoolsOptions.destroyAllObjectPoolsOptions.useOnDisable == true
                && useOnDisable == true)
            {
                UFE2FTEObjectPoolOptionsManager.DestroyAllPooledGameObjects();
            }

            if (updateObjectPoolsOptions.destroyAllObjectPoolsOptions.useOnDestroy == true
                && useOnDestroy == true)
            {
                UFE2FTEObjectPoolOptionsManager.DestroyAllPooledGameObjects();
            }
        }

        #endregion

        #region Set Object Pool Options Methods

        private void SetObjectPoolOptions(bool useOnEnable = false, bool useOnStart = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            int length = objectPoolOptions.Length;
            for (int i = 0; i < length; i++)
            {
                if (objectPoolOptions[i].useOnEnable == true
                    && useOnEnable == true
                    || (objectPoolOptions[i].useOnStart == true
                    && useOnStart == true)
                    || (objectPoolOptions[i].useOnDisable == true
                    && useOnDisable == true)
                    || (objectPoolOptions[i].useOnDestroy == true
                    && useOnDestroy == true))
                {
                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(objectPoolOptions[i].objectPoolScriptableObjectOptionsArray, myTransform, null, myProjectileMoveScript, null);
                }

                if (myProjectileMoveScript != null)
                {
                    objectPoolOptions[i].myProjectileMoveScriptTotalHits = myProjectileMoveScript.totalHits;

                    objectPoolOptions[i].myProjectileMoveScriptDestroyMe = myProjectileMoveScript.destroyMe;

                    if (objectPoolOptions[i].useOnEachHit == true
                        && myProjectileMoveScript != null
                        && myProjectileMoveScript.totalHits < objectPoolOptions[i].myProjectileMoveScriptTotalHits)
                    {
                        UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(objectPoolOptions[i].objectPoolScriptableObjectOptionsArray, myTransform, null, myProjectileMoveScript, null);
                    }

                    if (objectPoolOptions[i].useOnDestroyMe == true
                        && objectPoolOptions[i].myProjectileMoveScriptDestroyMe == false
                        && myProjectileMoveScript.destroyMe == true)
                    {
                        UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(objectPoolOptions[i].objectPoolScriptableObjectOptionsArray, myTransform, null, myProjectileMoveScript, null);
                    }

                    if (objectPoolOptions[i].useOnDestroyMeIgnore0TotalHits == true
                        && objectPoolOptions[i].myProjectileMoveScriptDestroyMe == false
                        && myProjectileMoveScript.destroyMe == true
                        && myProjectileMoveScript.totalHits > 0)
                    {
                        UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(objectPoolOptions[i].objectPoolScriptableObjectOptionsArray, myTransform, null, myProjectileMoveScript, null);
                    }
                }
            }
        }

        #endregion

        [NaughtyAttributes.Button]
        private void SpawnAllObjectPools()
        {
            UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolScriptableObjectManager(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
        }

        [NaughtyAttributes.Button]
        private void SpawnObjectPoolsByObjectPoolName()
        {
            UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByObjectPoolName(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray, null, updateObjectPoolsOptions.spawnObjectPoolsByObjectPoolNameOptions.objectPoolNameArray);
        }

        [NaughtyAttributes.Button]
        private void SpawnObjectPoolsByUFEPreload()
        {
            UFE2FTEObjectPoolOptionsManager.SpawnObjectPoolsByUFEPreload(updateObjectPoolsOptions.objectPoolScriptableObjectManagerArray);
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
