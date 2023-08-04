using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEUFEEventController : MonoBehaviour
    {
        private Transform myTransform;
        private ControlsScript myControlsScript;

        [SerializeField]
        private bool useCharacterName;
        [SerializeField]
        private string[] characterNameArray;

        public bool IsCharacterNameMatch(ControlsScript player)
        {
            if (useCharacterName == false)
            {
                return true;
            }

            if (player == null)
            {
                return false;
            }

            int length = characterNameArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (player.myInfo.characterName != characterNameArray[i])
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        [Serializable]
        public class OnBasicMoveOptions
        {
            public BasicMoveReference[] basicMoveArray;
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;             
        }
        [SerializeField]
        private OnBasicMoveOptions[] onBasicMoveOptionsArray;

        [Serializable]
        public class OnMoveOptions
        {
            public string[] moveNameArray;
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;
        }
        [SerializeField]
        private OnMoveOptions[] onMoveOptionsArray;

        [Serializable]
        public class OnHitOptions
        {        
            public HitType[] hitTypeArray;
            public HitStrengh[] hitStrengthArray;
            public string[] moveNameArray;
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;
        }
        [SerializeField]
        private OnHitOptions[] onHitOptionsArray;

        [Serializable]
        public class OnBlockOptions
        {   
            public HitType[] hitTypeArray;
            public HitStrengh[] hitStrengthArray;
            public string[] moveNameArray;
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;
        }
        [SerializeField]
        private OnBlockOptions[] onBlockOptionsArray;

        [Serializable]
        public class OnParryOptions
        {
            public HitType[] hitTypeArray;
            public HitStrengh[] hitStrengthArray;
            public string[] moveNameArray;
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;
        }
        [SerializeField]
        private OnParryOptions[] onParryOptionsArray;

        [Serializable]
        public class MoveInfoOptions
        {     
            public string[] moveNameArray;
            public int[] spawnFrameArray;
            [HideInInspector]
            public int ignoredSpawnFrame;
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;
        }
        [SerializeField]
        private MoveInfoOptions[] moveInfoOptionsArray;

        private void Awake()
        {
            myTransform = transform;
        }

        private void OnEnable()
        {
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
            UFE.OnBlock += OnBlock;
            UFE.OnParry += OnParry;
        }

        private void Start()
        {
            myControlsScript = GetComponentInParent<ControlsScript>();
        }

        private void Update()
        {
            SetMoveInfoOptions();
        }

        private void OnDisable()
        {
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnParry -= OnParry;
        }

        #region On Basic Move Methods

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            if (IsCharacterNameMatch(player) == false)
            {
                return;
            }

            int length = onBasicMoveOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onBasicMoveOptionsArray[i].basicMoveArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (basicMove != onBasicMoveOptionsArray[i].basicMoveArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onBasicMoveOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onBasicMoveOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, null);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onBasicMoveOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onBasicMoveOptionsArray[i].vectorGridForceScriptableObjectArray);
                }     
            }  
        }

        #endregion

        #region On Move Methods

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (IsCharacterNameMatch(player) == false
                || player.currentMove == null)
            {
                return;
            }

            int length = onMoveOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onMoveOptionsArray[i].moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != onMoveOptionsArray[i].moveNameArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onMoveOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onMoveOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, null);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onMoveOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onMoveOptionsArray[i].vectorGridForceScriptableObjectArray);
                }
            }
        }

        #endregion

        #region On Hit Methods

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (IsCharacterNameMatch(player) == false)
            {
                return;
            }

            int length = onHitOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onHitOptionsArray[i].hitTypeArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (hitInfo.hitType != onHitOptionsArray[i].hitTypeArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onHitOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onHitOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onHitOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onHitOptionsArray[i].vectorGridForceScriptableObjectArray);
                }

                lengthA = onHitOptionsArray[i].hitStrengthArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (hitInfo.hitStrength != onHitOptionsArray[i].hitStrengthArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onHitOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onHitOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onHitOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onHitOptionsArray[i].vectorGridForceScriptableObjectArray);
                }

                lengthA = onHitOptionsArray[i].moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != onHitOptionsArray[i].moveNameArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onHitOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onHitOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onHitOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onHitOptionsArray[i].vectorGridForceScriptableObjectArray);
                }
            }
        }

        #endregion

        #region On Block Methods

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (IsCharacterNameMatch(player) == false)
            {
                return;
            }

            int length = onBlockOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onBlockOptionsArray[i].hitTypeArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (hitInfo.hitType != onBlockOptionsArray[i].hitTypeArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onBlockOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onBlockOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onBlockOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onBlockOptionsArray[i].vectorGridForceScriptableObjectArray);
                }

                lengthA = onBlockOptionsArray[i].hitStrengthArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (hitInfo.hitStrength != onBlockOptionsArray[i].hitStrengthArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onBlockOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onBlockOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onBlockOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onBlockOptionsArray[i].vectorGridForceScriptableObjectArray);
                }

                lengthA = onBlockOptionsArray[i].moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != onBlockOptionsArray[i].moveNameArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onBlockOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onBlockOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onBlockOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onBlockOptionsArray[i].vectorGridForceScriptableObjectArray);
                }
            }
        }

        #endregion

        #region On Parry Methods

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (IsCharacterNameMatch(player) == false)
            {
                return;
            }

            int length = onParryOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onParryOptionsArray[i].hitTypeArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (hitInfo.hitType != onParryOptionsArray[i].hitTypeArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onParryOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onParryOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onParryOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onParryOptionsArray[i].vectorGridForceScriptableObjectArray);
                }

                lengthA = onParryOptionsArray[i].hitStrengthArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (hitInfo.hitStrength != onParryOptionsArray[i].hitStrengthArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onParryOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onParryOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onParryOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onParryOptionsArray[i].vectorGridForceScriptableObjectArray);
                }

                lengthA = onParryOptionsArray[i].moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != onParryOptionsArray[i].moveNameArray[a])
                    {
                        continue;
                    }

                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(onParryOptionsArray[i].audioClipGroupScriptableObjectArray);

                    UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(onParryOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, player, null, strokeHitBox);

                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, onParryOptionsArray[i].transformShakeScriptableObjectArray, player);

                    UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, onParryOptionsArray[i].vectorGridForceScriptableObjectArray);
                }
            }
        }

        #endregion

        #region Move Info Methods

        private void SetMoveInfoOptions()
        {
            if (IsCharacterNameMatch(myControlsScript) == false
                || myControlsScript == null)
            {
                return;
            }

            if (myControlsScript.currentMove != null)
            {
                int length = moveInfoOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = moveInfoOptionsArray[i].moveNameArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (myControlsScript.currentMove.moveName != moveInfoOptionsArray[i].moveNameArray[a])
                        {
                            continue;
                        }

                        int lengthB = moveInfoOptionsArray[i].spawnFrameArray.Length;
                        for (int b = 0; b < lengthB; b++)
                        {
                            if (myControlsScript.currentMove.currentFrame == moveInfoOptionsArray[i].ignoredSpawnFrame)
                            {
                                return;
                            }
                            else
                            {
                                moveInfoOptionsArray[i].ignoredSpawnFrame = -1;
                            }

                            if (myControlsScript.currentMove.currentFrame != moveInfoOptionsArray[i].spawnFrameArray[b])
                            {
                                continue;
                            }    

                            moveInfoOptionsArray[i].ignoredSpawnFrame = moveInfoOptionsArray[i].spawnFrameArray[b];

                            UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(moveInfoOptionsArray[i].audioClipGroupScriptableObjectArray);

                            UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(moveInfoOptionsArray[i].objectPoolScriptableObjectOptionsArray, transform, myControlsScript, null, null);

                            UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, moveInfoOptionsArray[i].transformShakeScriptableObjectArray, myControlsScript);

                            UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(myTransform, moveInfoOptionsArray[i].vectorGridForceScriptableObjectArray);
                        }
                    }
                }
            }
            else
            {
                int length = moveInfoOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    moveInfoOptionsArray[i].ignoredSpawnFrame = -1;
                }
            }
        }

        #endregion
    }
}
