using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Object Pool Transform", menuName = "U.F.E. 2 F.T.E./Object Pool/Object Pool Transform")]
    public class UFE2FTEObjectPoolTransformScriptableObject : ScriptableObject
    {
        [Header("Transform Options")]
        [SerializeField]
        private bool useTransform;
        [SerializeField]
        private bool useControlsScriptTransform;
        [SerializeField]
        private bool useProjectileMoveScriptTransform;

        [Header("Position Options")]  
        [SerializeField]
        private bool useXPosition;
        [SerializeField]
        private bool useXPositionControlsScriptMirror;
        [SerializeField]
        private bool useXPositionProjectileMoveScriptMirror;
        [SerializeField]
        private bool useYPosition;
        [SerializeField]
        private bool useZPosition;
        [SerializeField]
        private Vector3 position;

        [Header("Rotation Options")]
        [SerializeField]
        private bool useXRotation;
        [SerializeField]
        private bool useYRotation;
        [SerializeField]
        private bool useYRotationControlsScriptMirror;
        [SerializeField]
        private bool useYRotationProjectileMoveScriptMirror;
        [SerializeField]
        private bool useZRotation;
        [SerializeField]
        private Vector3 rotation;

        [Header("Scale Options")]
        [SerializeField]
        private bool useXScale;
        [SerializeField]
        private bool useYScale;
        [SerializeField]
        private bool useZScale;
        [SerializeField]
        private Vector3 scale;

        public static Vector3 GetPosition(UFE2FTEObjectPoolTransformScriptableObject objectPoolTransformScriptableObject, Transform transform, ControlsScript controlsScript, ProjectileMoveScript projectileMoveScript, HitBox strokeHitBox)
        {
            Vector3 pooledGameObjectPosition = new Vector3(0, 0, 0);

            if (objectPoolTransformScriptableObject == null)
            {
                return pooledGameObjectPosition;
            }

            if (objectPoolTransformScriptableObject.useXPosition == true)
            {
                pooledGameObjectPosition.x += objectPoolTransformScriptableObject.position.x;
            }

            if (objectPoolTransformScriptableObject.useYPosition == true)
            {
                pooledGameObjectPosition.y += objectPoolTransformScriptableObject.position.y;
            }

            if (objectPoolTransformScriptableObject.useZPosition == true)
            {
                pooledGameObjectPosition.z += objectPoolTransformScriptableObject.position.z;
            }

            if (transform != null)
            {
                if (objectPoolTransformScriptableObject.useTransform == true)
                {
                    pooledGameObjectPosition = transform.position;

                    if (objectPoolTransformScriptableObject.useXPosition == true)
                    {
                        pooledGameObjectPosition.x += objectPoolTransformScriptableObject.position.x;
                    }

                    if (objectPoolTransformScriptableObject.useYPosition == true)
                    {
                        pooledGameObjectPosition.y += objectPoolTransformScriptableObject.position.y;
                    }

                    if (objectPoolTransformScriptableObject.useZPosition == true)
                    {
                        pooledGameObjectPosition.z += objectPoolTransformScriptableObject.position.z;
                    }
                }
            }

            if (controlsScript != null)
            {
                if (objectPoolTransformScriptableObject.useControlsScriptTransform == true)
                {
                    pooledGameObjectPosition = controlsScript.transform.position;

                    if (objectPoolTransformScriptableObject.useXPosition == true)
                    {
                        pooledGameObjectPosition.x += objectPoolTransformScriptableObject.position.x;
                    }

                    if (objectPoolTransformScriptableObject.useYPosition == true)
                    {
                        pooledGameObjectPosition.y += objectPoolTransformScriptableObject.position.y;
                    }

                    if (objectPoolTransformScriptableObject.useZPosition == true)
                    {
                        pooledGameObjectPosition.z += objectPoolTransformScriptableObject.position.z;
                    }
                }

                if (objectPoolTransformScriptableObject.useXPositionControlsScriptMirror == true)
                {
                    if (controlsScript.mirror == -1)
                    {
                        pooledGameObjectPosition.x = -pooledGameObjectPosition.x;
                    }
                    else
                    {
                        pooledGameObjectPosition.x = Mathf.Abs(pooledGameObjectPosition.x);
                    }
                }
            }

            if (projectileMoveScript != null)
            {
                if (objectPoolTransformScriptableObject.useProjectileMoveScriptTransform == true)
                {
                    pooledGameObjectPosition = projectileMoveScript.transform.position;

                    if (objectPoolTransformScriptableObject.useXPosition == true)
                    {
                        pooledGameObjectPosition.x += objectPoolTransformScriptableObject.position.x;
                    }

                    if (objectPoolTransformScriptableObject.useYPosition == true)
                    {
                        pooledGameObjectPosition.y += objectPoolTransformScriptableObject.position.y;
                    }

                    if (objectPoolTransformScriptableObject.useZPosition == true)
                    {
                        pooledGameObjectPosition.z += objectPoolTransformScriptableObject.position.z;
                    }
                }

                if (objectPoolTransformScriptableObject.useXPositionControlsScriptMirror == true)
                {
                    if (projectileMoveScript.mirror == -1)
                    {
                        pooledGameObjectPosition.x = -pooledGameObjectPosition.x;
                    }
                    else
                    {
                        pooledGameObjectPosition.x = Mathf.Abs(pooledGameObjectPosition.x);
                    }
                }
            }

            if (strokeHitBox != null)
            {
                pooledGameObjectPosition = UFE2FTEHelperMethodsManager.GetCenterPositionFromHitBox(strokeHitBox);
            }

            return pooledGameObjectPosition;
        }

        public static Vector3 GetRotation(UFE2FTEObjectPoolTransformScriptableObject objectPoolTransformScriptableObject, Transform transform, ControlsScript controlsScript, ProjectileMoveScript projectileMoveScript)
        {
            Vector3 pooledGameObjectRotation = new Vector3(0, 0, 0);

            if (objectPoolTransformScriptableObject == null)
            {
                return pooledGameObjectRotation;
            }

            if (transform != null)
            {
                if (objectPoolTransformScriptableObject.useTransform == true)
                {
                    pooledGameObjectRotation = transform.eulerAngles;
                }
            }

            if (controlsScript != null)
            {
                if (objectPoolTransformScriptableObject.useYRotationControlsScriptMirror == true)
                {
                    if (objectPoolTransformScriptableObject.useControlsScriptTransform == true)
                    {
                        pooledGameObjectRotation = controlsScript.transform.eulerAngles;
                    }

                    if (controlsScript.mirror == -1)
                    {
                        pooledGameObjectRotation.y = 0;
                    }
                    else
                    {
                        pooledGameObjectRotation.y = 180;
                    }
                }
            }

            if (projectileMoveScript != null)
            {
                if (objectPoolTransformScriptableObject.useProjectileMoveScriptTransform == true)
                {
                    pooledGameObjectRotation = projectileMoveScript.transform.eulerAngles;
                }

                if (objectPoolTransformScriptableObject.useYRotationProjectileMoveScriptMirror == true)
                {
                    if (projectileMoveScript.mirror == -1)
                    {
                        pooledGameObjectRotation.y = 0;
                    }
                    else
                    {
                        pooledGameObjectRotation.y = 180;
                    }
                }
            }

            if (objectPoolTransformScriptableObject.useXRotation == true)
            {
                pooledGameObjectRotation.x = objectPoolTransformScriptableObject.rotation.x;
            }

            if (objectPoolTransformScriptableObject.useYRotation == true)
            {
                pooledGameObjectRotation.y = objectPoolTransformScriptableObject.rotation.y;
            }

            if (objectPoolTransformScriptableObject.useZRotation == true)
            {
                pooledGameObjectRotation.z = objectPoolTransformScriptableObject.rotation.z;
            }

            return pooledGameObjectRotation;
        }

        public static Vector3 GetScale(UFE2FTEObjectPoolTransformScriptableObject objectPoolTransformScriptableObject, Transform transform, ControlsScript controlsScript, ProjectileMoveScript projectileMoveScript)
        {
            Vector3 pooledGameObjectScale = new Vector3(0, 0, 0);

            if (objectPoolTransformScriptableObject == null)
            {
                return pooledGameObjectScale;
            }

            if (transform != null)
            {
                if (objectPoolTransformScriptableObject.useTransform == true)
                {
                    pooledGameObjectScale = transform.localScale;
                }
            }

            if (controlsScript != null)
            {
                if (objectPoolTransformScriptableObject.useControlsScriptTransform == true)
                {
                    pooledGameObjectScale = controlsScript.transform.localScale;
                }
            }

            if (projectileMoveScript != null)
            {
                if (objectPoolTransformScriptableObject.useProjectileMoveScriptTransform == true)
                {
                    pooledGameObjectScale = projectileMoveScript.transform.localScale;
                }
            }

            if (objectPoolTransformScriptableObject.useXScale == true)
            {
                pooledGameObjectScale.x = objectPoolTransformScriptableObject.scale.x;
            }

            if (objectPoolTransformScriptableObject.useYScale == true)
            {
                pooledGameObjectScale.y = objectPoolTransformScriptableObject.scale.y;
            }

            if (objectPoolTransformScriptableObject.useZScale == true)
            {
                pooledGameObjectScale.z = objectPoolTransformScriptableObject.scale.z;
            }

            return pooledGameObjectScale;
        }
    }
}
