using System;
using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEVectorGridForceController : MonoBehaviour
    {
        private Transform myTransform;

        [Serializable]
        public class VectorGridForceOptions
        {
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;

            public bool useUpdate;
            public bool useOnDisable;
            public bool useOnDestroy;

            public bool useAddForceImmediately;
            [HideInInspector]
            public bool stopAddForceImmediately;

            public bool useAddForceTimer;
            [HideInInspector]
            public float addForceTimer;
            [HideInInspector]
            public bool stopAddForceTimer;
            public enum AddForceTimerMode
            {
                Once,
                Loop,
                RandomLoop
            }
            public AddForceTimerMode addForceTimerMode;
            public float addForceDelay;
            public float addForceDelayRandomMin;
            public float addForceDelayRandomMax;
        }
        [SerializeField]
        private VectorGridForceOptions[] vectorGridForceOptions;

        private void Awake()
        {
            myTransform = transform;
        }

        private void OnEnable()
        {
            SetVectorGridForceOptions(true);
        }

        private void Update()
        {
            SetVectorGridForceOptions(false, true);
        }

        private void OnDisable()
        {
            SetVectorGridForceOptions(false, false, true);
        }

        private void OnDestroy()
        {
            SetVectorGridForceOptions(false, false, false, true);
        }

        #region Vector Grid Force Methods

        private void SetVectorGridForceOptions(bool useOnEnable = false, bool useOnUpdate = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            if (useOnEnable == true)
            {
                int length = vectorGridForceOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    vectorGridForceOptions[i].stopAddForceImmediately = false;

                    switch (vectorGridForceOptions[i].addForceTimerMode)
                    {
                        case VectorGridForceOptions.AddForceTimerMode.Once:
                        case VectorGridForceOptions.AddForceTimerMode.Loop:
                            vectorGridForceOptions[i].addForceTimer = vectorGridForceOptions[i].addForceDelay;
                            break;

                        case VectorGridForceOptions.AddForceTimerMode.RandomLoop:
                            vectorGridForceOptions[i].addForceTimer = UnityEngine.Random.Range(vectorGridForceOptions[i].addForceDelayRandomMin, vectorGridForceOptions[i].addForceDelayRandomMax);
                            break;
                    }

                    vectorGridForceOptions[i].stopAddForceTimer = false;
                }
            }

            if (useOnUpdate == true)
            {
                int length = vectorGridForceOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    if (vectorGridForceOptions[i].useUpdate == true)
                    {
                        UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                    }

                    if (vectorGridForceOptions[i].useAddForceImmediately == true
                        && vectorGridForceOptions[i].stopAddForceImmediately == false)
                    {
                        vectorGridForceOptions[i].stopAddForceImmediately = true;

                        UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                    }

                    if (vectorGridForceOptions[i].useAddForceTimer == true)
                    {
                        vectorGridForceOptions[i].addForceTimer -= Time.deltaTime;

                        if (vectorGridForceOptions[i].addForceTimer < 0)
                        {
                            switch (vectorGridForceOptions[i].addForceTimerMode)
                            {
                                case VectorGridForceOptions.AddForceTimerMode.Once:
                                    vectorGridForceOptions[i].addForceTimer = vectorGridForceOptions[i].addForceDelay;

                                    if (vectorGridForceOptions[i].stopAddForceTimer == false)
                                    {
                                        vectorGridForceOptions[i].stopAddForceTimer = true;

                                        UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                                    }
                                    break;

                                case VectorGridForceOptions.AddForceTimerMode.Loop:
                                    vectorGridForceOptions[i].addForceTimer = vectorGridForceOptions[i].addForceDelay;

                                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                                    break;

                                case VectorGridForceOptions.AddForceTimerMode.RandomLoop:
                                    vectorGridForceOptions[i].addForceTimer = UnityEngine.Random.Range(vectorGridForceOptions[i].addForceDelayRandomMin, vectorGridForceOptions[i].addForceDelayRandomMax);

                                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                                    break;
                            }
                        }
                    }
                }
            }

            if (useOnDisable == true)
            {
                int length = vectorGridForceOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    if (vectorGridForceOptions[i].useOnDisable == false)
                    {
                        continue;
                    }

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                }
            }

            if (useOnDestroy == true)
            {
                int length = vectorGridForceOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    if (vectorGridForceOptions[i].useOnDestroy == false)
                    {
                        continue;
                    }

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, vectorGridForceOptions[i].vectorGridForceScriptableObjectArray);
                }
            }
        }

        #endregion
    }
}