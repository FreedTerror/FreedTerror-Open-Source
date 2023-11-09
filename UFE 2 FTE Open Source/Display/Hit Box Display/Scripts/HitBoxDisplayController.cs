using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class HitBoxDisplayController : MonoBehaviour
    {
        //Animation Maps aren't supported in this code right now?
        //Rectangles aren't supported in UFE's 3D gameplay modes.

        [SerializeField]
        private HitBoxDisplayScriptableObject hitBoxDisplayScriptableObject;

        private GameObject hitBoxDisplayCirclePrefab;
        private GameObject hitBoxDisplayRectanglePrefab;
        private Sprite hitBoxDisplayCircleFillSprite = null;
        private Sprite hitBoxDisplayRectangleFillSprite = null;
        private MaterialPropertyBlock myMaterialPropertyBlock = null;

        private readonly string hitBoxesCircleMessage = "Hit Boxes Circle ";
        private readonly string hitBoxesRectangleMessage = "Hit Boxes Rectangle ";
        private readonly string activeHurtBoxesCircleMessage = "Active Hurt Boxes Circle ";
        private readonly string activeHurtBoxesRectangleMessage = "Active Hurt Boxes Rectangle ";
        private readonly string blockableAreaCircleMessage = "Blockable Area Circle";
        private readonly string blockableAreaRectangleMessage = "Blockable Area Rectangle";
        private readonly string hitAreaCircleMessage = "Hit Area Circle";
        private readonly string hitAreaRectangleMessage = "Hit Area Rectangle";
        private readonly string gizmosMessage = "Gizmos";

        [System.Serializable]
        private class HitBoxDisplayGameObject
        {
            public GameObject hitBoxDisplayGameObject;
            public Transform hitBoxDisplayTransform;
            public GameObject hitBoxDisplaySpriteRendererGameObject;
            public SpriteRenderer hitBoxDisplaySpriteRenderer;
            public GameObject hitBoxDisplayMeshRendererGameObject;
            public MeshRenderer hitBoxDisplayMeshRenderer;
            public GameObject hitBoxDisplayGizmosGameObject;
            public Transform hitBoxDisplayGizmosTransform;
        }
        [System.Serializable]
        private class CharacterHitBoxDisplay
        {
            public ControlsScript controlsScript;
            public HitBoxesScript hitBoxesScript;
            public HitBoxDisplayGameObject[] hitBoxesCircleGameObjectArray;
            public HitBoxDisplayGameObject[] hitBoxesRectangleGameObjectArray;
            public HitBoxDisplayGameObject[] activeHurtBoxesCircleGameObjectArray;
            public HitBoxDisplayGameObject[] activeHurtBoxesRectangleGameObjectArray;
            public HitBoxDisplayGameObject blockableAreaCircleGameObject;
            public HitBoxDisplayGameObject blockableAreaRectangleGameObject;
        }
        private List<CharacterHitBoxDisplay> characterHitBoxDisplayList = new List<CharacterHitBoxDisplay>();
        private void RemoveInvaildEntriesFromCharacterHitBoxDisplayList()
        {
            int count = characterHitBoxDisplayList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (characterHitBoxDisplayList[i].controlsScript != null)
                {
                    continue;
                }

                characterHitBoxDisplayList.RemoveAt(i);
            }
        }

        [System.Serializable]
        private class ProjectileHitboxDisplay
        {
            public ProjectileMoveScript projectileMoveScript;
            public GameObject projectileMoveScriptGameObject;
            public Transform projectileMoveScriptTransform;
            public HitBoxDisplayGameObject hitAreaCircleGameObject;
            public HitBoxDisplayGameObject hitAreaRectangleGameObject;
            public HitBoxDisplayGameObject blockableAreaCircleGameObject;
            public HitBoxDisplayGameObject blockableAreaRectangleGameObject;
        }
        private List<ProjectileHitboxDisplay> projectileHitBoxDisplayList = new List<ProjectileHitboxDisplay>();
        private void RemoveInvaildEntriesFromProjectileHitBoxDisplayList()
        {
            int count = projectileHitBoxDisplayList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScript != null)
                {
                    continue;
                }

                Destroy(projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject);

                Destroy(projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject);

                Destroy(projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject);

                Destroy(projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject);

                projectileHitBoxDisplayList.RemoveAt(i);
            }
        }

        private void Start()
        {
            hitBoxDisplayCirclePrefab = hitBoxDisplayScriptableObject.GetHitBoxDisplayCirclePrefab();
            hitBoxDisplayRectanglePrefab = hitBoxDisplayScriptableObject.GetHitBoxDisplayRectanglePrefab();

            hitBoxDisplayCircleFillSprite = hitBoxDisplayScriptableObject.hitBoxDisplayCircleFilledSprite;
            hitBoxDisplayRectangleFillSprite = hitBoxDisplayScriptableObject.hitBoxDisplayRectangleFilledSprite;

            myMaterialPropertyBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            if (hitBoxDisplayCirclePrefab == null
                || hitBoxDisplayRectanglePrefab == null               
                || UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            hitBoxDisplayScriptableObject.SetAllColliderColorAlphaValue((byte)UFE2FTE.Instance.hitBoxDisplayAlphaValue);

            SetHitBoxDisplayLists();

            SetCharacterHitBoxesGameObjectsPositionAndScale();

            SetCharacterActiveHurtBoxesGameObjectsPositionAndScale();

            SetCharacterBlockableAreaGameObjectsPositionAndScale();

            SetEqualCharacterColliders();

            SetAllCharacterGameObjectsSpriteRendererSprite();

            SetAllCharacterGameObjectsColor();

            SetAllCharacterGameObjectsSpriteRendererOrderInLayer();

            SetUnusedCharacterHitBoxesGameObjects();

            SetCharacterHitBoxesGameObjectsByShape();

            SetInvincibleCharacterHitBoxesGameObjects();

            SetUnusedCharacterActiveHurtBoxesGameObjects();

            SetCharacterActiveHurtBoxesGameObjectsByShape();

            SetUnusedCharacterBlockableAreaGameObjects();

            SetCharacterBlockableAreaGameObjectsByShape();

            SetCharacterGameObjectsByDisableCollider();

            SetCharacterGameObjectsByDisplayMode();

            SetAllProjectileGameObjectsPositionAndScale();

            SetAllProjectileGameObjectsSpriteRendererSprite();

            SetAllProjectileGameObjectsColor();

            SetAllProjectileGameObjectsSpriteRendererOrderInLayer();

            SetProjectileGameObjectsByShape();

            SetProjectileGameObjectsByShapeByDisableCollider();

            SetProjectileGameObjectsByDisplayMode();

            SetAllProjectileGameObjectsActive();

            SetGizmos();
        }

        #region Hit Box Display Lists Methods

        private void SetHitBoxDisplayLists()
        {
            RemoveInvaildEntriesFromCharacterHitBoxDisplayList();

            RemoveInvaildEntriesFromProjectileHitBoxDisplayList();

            AddToCharacterHitBoxDisplayList(UFE.GetPlayer1ControlsScript());

            int count = UFE.GetPlayer1ControlsScript().projectiles.Count;
            for (int i = 0; i < count; i++)
            {
                AddToProjectileHitBoxDisplayList(UFE.GetPlayer1ControlsScript().projectiles[i]);
            }

            count = UFE.GetPlayer1ControlsScript().assists.Count;
            for (int i = 0; i < count; i++)
            {
                AddToCharacterHitBoxDisplayList(UFE.GetPlayer1ControlsScript().assists[i]);

                int countA = UFE.GetPlayer1ControlsScript().assists[i].projectiles.Count;
                for (int a = 0; a < countA; a++)
                {
                    AddToProjectileHitBoxDisplayList(UFE.GetPlayer1ControlsScript().assists[i].projectiles[i]);
                }
            }

            AddToCharacterHitBoxDisplayList(UFE.GetPlayer2ControlsScript());

            count = UFE.GetPlayer2ControlsScript().projectiles.Count;
            for (int i = 0; i < count; i++)
            {
                AddToProjectileHitBoxDisplayList(UFE.GetPlayer2ControlsScript().projectiles[i]);
            }

            count = UFE.GetPlayer2ControlsScript().assists.Count;
            for (int i = 0; i < count; i++)
            {
                AddToCharacterHitBoxDisplayList(UFE.GetPlayer2ControlsScript().assists[i]);

                int countA = UFE.GetPlayer2ControlsScript().assists[i].projectiles.Count;
                for (int a = 0; a < countA; a++)
                {
                    AddToProjectileHitBoxDisplayList(UFE.GetPlayer2ControlsScript().assists[i].projectiles[i]);
                }
            }
        }

        private void AddToCharacterHitBoxDisplayList(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (player == characterHitBoxDisplayList[i].controlsScript)
                {
                    return;
                }
            }

            CharacterHitBoxDisplay newCharacterHitBoxDisplay = new CharacterHitBoxDisplay();
            newCharacterHitBoxDisplay.controlsScript = player;
            newCharacterHitBoxDisplay.hitBoxesScript = player.MoveSet.hitBoxesScript;

            HitBoxDisplayGameObject[] newHitBoxesCircleGameObjects = new HitBoxDisplayGameObject[hitBoxDisplayScriptableObject.hitBoxDisplayHitBoxesPoolSize];
            int length = newHitBoxesCircleGameObjects.Length;
            for (int i = 0; i < length; i++)
            {              
                HitBoxDisplayGameObject newHitBoxDisplayGameObject = new HitBoxDisplayGameObject();
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayCirclePrefab, newCharacterHitBoxDisplay.controlsScript.transform);
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject.name = hitBoxesCircleMessage + i;
                newHitBoxDisplayGameObject.hitBoxDisplayTransform = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.transform;        
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject.hitBoxDisplayTransform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0, 0, 0);
                newHitBoxesCircleGameObjects[i] = newHitBoxDisplayGameObject;
            }
            newCharacterHitBoxDisplay.hitBoxesCircleGameObjectArray = newHitBoxesCircleGameObjects;

            HitBoxDisplayGameObject[] newHitBoxesRectangleGameObjects = new HitBoxDisplayGameObject[hitBoxDisplayScriptableObject.hitBoxDisplayHitBoxesPoolSize];
            length = newHitBoxesRectangleGameObjects.Length;
            for (int i = 0; i < length; i++)
            {
                HitBoxDisplayGameObject newHitBoxDisplayGameObject = new HitBoxDisplayGameObject();
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayRectanglePrefab, newCharacterHitBoxDisplay.controlsScript.transform);
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject.name = hitBoxesRectangleMessage + i;
                newHitBoxDisplayGameObject.hitBoxDisplayTransform = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject.hitBoxDisplayTransform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0.5f, 0.5f, 0);
                newHitBoxesRectangleGameObjects[i] = newHitBoxDisplayGameObject;         
            }
            newCharacterHitBoxDisplay.hitBoxesRectangleGameObjectArray = newHitBoxesRectangleGameObjects;

            HitBoxDisplayGameObject[] newActiveHurtBoxesCircleGameObjects = new HitBoxDisplayGameObject[hitBoxDisplayScriptableObject.hitBoxDisplayActiveHurtBoxesPoolSize];
            length = newActiveHurtBoxesCircleGameObjects.Length;
            for (int i = 0; i < length; i++)
            {
                HitBoxDisplayGameObject newHitBoxDisplayGameObject = new HitBoxDisplayGameObject();
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayCirclePrefab, newCharacterHitBoxDisplay.controlsScript.transform);
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject.name = activeHurtBoxesCircleMessage + i;
                newHitBoxDisplayGameObject.hitBoxDisplayTransform = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject.hitBoxDisplayTransform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0, 0, 0);
                newActiveHurtBoxesCircleGameObjects[i] = newHitBoxDisplayGameObject;
            }
            newCharacterHitBoxDisplay.activeHurtBoxesCircleGameObjectArray = newActiveHurtBoxesCircleGameObjects;

            HitBoxDisplayGameObject[] newActiveHurtBoxesRectangleGameObjects = new HitBoxDisplayGameObject[hitBoxDisplayScriptableObject.hitBoxDisplayActiveHurtBoxesPoolSize];
            length = newActiveHurtBoxesRectangleGameObjects.Length;
            for (int i = 0; i < length; i++)
            {
                HitBoxDisplayGameObject newHitBoxDisplayGameObject = new HitBoxDisplayGameObject();
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayRectanglePrefab, newCharacterHitBoxDisplay.controlsScript.transform);
                newHitBoxDisplayGameObject.hitBoxDisplayGameObject.name = activeHurtBoxesRectangleMessage + i;
                newHitBoxDisplayGameObject.hitBoxDisplayTransform = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
                newHitBoxDisplayGameObject.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer.gameObject;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject.transform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject.hitBoxDisplayTransform;
                newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0.5f, 0.5f, 0);
                newActiveHurtBoxesRectangleGameObjects[i] = newHitBoxDisplayGameObject;
            }
            newCharacterHitBoxDisplay.activeHurtBoxesRectangleGameObjectArray = newActiveHurtBoxesRectangleGameObjects;

            HitBoxDisplayGameObject newHitBoxDisplayGameObject1 = new HitBoxDisplayGameObject();
            newHitBoxDisplayGameObject1.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayCirclePrefab, newCharacterHitBoxDisplay.controlsScript.transform);
            newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.name = blockableAreaCircleMessage;
            newHitBoxDisplayGameObject1.hitBoxDisplayTransform = newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.transform;
            newHitBoxDisplayGameObject1.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayGameObject1.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject1.hitBoxDisplaySpriteRenderer.gameObject;
            newHitBoxDisplayGameObject1.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
            newHitBoxDisplayGameObject1.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject1.hitBoxDisplayMeshRenderer.gameObject;
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject1.hitBoxDisplayGizmosGameObject.transform;
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject1.hitBoxDisplayTransform;
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0, 0, 0);
            newCharacterHitBoxDisplay.blockableAreaCircleGameObject = newHitBoxDisplayGameObject1;

            HitBoxDisplayGameObject newHitBoxDisplayGameObject2 = new HitBoxDisplayGameObject();
            newHitBoxDisplayGameObject2.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayRectanglePrefab, newCharacterHitBoxDisplay.controlsScript.transform);
            newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.name = blockableAreaRectangleMessage;
            newHitBoxDisplayGameObject2.hitBoxDisplayTransform = newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.transform;
            newHitBoxDisplayGameObject2.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayGameObject2.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject2.hitBoxDisplaySpriteRenderer.gameObject;
            newHitBoxDisplayGameObject2.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
            newHitBoxDisplayGameObject2.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject2.hitBoxDisplayMeshRenderer.gameObject;
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject2.hitBoxDisplayGizmosGameObject.transform;
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject2.hitBoxDisplayTransform;
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0.5f, 0.5f, 0);
            newCharacterHitBoxDisplay.blockableAreaRectangleGameObject = newHitBoxDisplayGameObject2;

            characterHitBoxDisplayList.Add(newCharacterHitBoxDisplay);
        }

        private void AddToProjectileHitBoxDisplayList(ProjectileMoveScript projectileMoveScript)
        {
            if (projectileMoveScript == null)
            {
                return;
            }

            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileMoveScript == projectileHitBoxDisplayList[i].projectileMoveScript)
                {
                    return;
                }
            }

            ProjectileHitboxDisplay newProjectileHitboxDisplay = new ProjectileHitboxDisplay();
            newProjectileHitboxDisplay.projectileMoveScript = projectileMoveScript;
            newProjectileHitboxDisplay.projectileMoveScriptGameObject = projectileMoveScript.gameObject;
            newProjectileHitboxDisplay.projectileMoveScriptTransform = projectileMoveScript.transform;

            HitBoxDisplayGameObject newHitBoxDisplayGameObject = new HitBoxDisplayGameObject();
            newHitBoxDisplayGameObject.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayScriptableObject.GetHitBoxDisplayCirclePrefab());
            //newHitBoxDisplayGameObject.hitBoxDisplayGameObject = Instantiate(UFE2FTE.hitBoxDisplayOptions.prefabOptions.GetHitBoxDisplayCirclePrefab(), newProjectileHitboxDisplay.projectileMoveScript.transform);
            newHitBoxDisplayGameObject.hitBoxDisplayGameObject.name = hitAreaCircleMessage;
            newHitBoxDisplayGameObject.hitBoxDisplayTransform = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.transform;
            newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayGameObject.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplaySpriteRenderer.gameObject;
            newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
            newHitBoxDisplayGameObject.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject.hitBoxDisplayMeshRenderer.gameObject;
            newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
            newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject.hitBoxDisplayGizmosGameObject.transform;
            newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject.hitBoxDisplayTransform;
            newHitBoxDisplayGameObject.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0, 0, 0);
            newProjectileHitboxDisplay.hitAreaCircleGameObject = newHitBoxDisplayGameObject;

            HitBoxDisplayGameObject newHitBoxDisplayGameObject1 = new HitBoxDisplayGameObject();
            newHitBoxDisplayGameObject1.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayScriptableObject.GetHitBoxDisplayRectanglePrefab());
            //newHitBoxDisplayGameObject1.hitBoxDisplayGameObject = Instantiate(UFE2FTE.hitBoxDisplayOptions.prefabOptions.GetHitBoxDisplayRectanglePrefab(), newProjectileHitboxDisplay.projectileMoveScript.transform);
            newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.name = hitAreaRectangleMessage;
            newHitBoxDisplayGameObject1.hitBoxDisplayTransform = newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.transform;
            newHitBoxDisplayGameObject1.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayGameObject1.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject1.hitBoxDisplaySpriteRenderer.gameObject;
            newHitBoxDisplayGameObject1.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject1.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
            newHitBoxDisplayGameObject1.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject1.hitBoxDisplayMeshRenderer.gameObject;
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject1.hitBoxDisplayGizmosGameObject.transform;
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject1.hitBoxDisplayTransform;
            newHitBoxDisplayGameObject1.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0.5f, 0.5f, 0);
            newProjectileHitboxDisplay.hitAreaRectangleGameObject = newHitBoxDisplayGameObject1;

            HitBoxDisplayGameObject newHitBoxDisplayGameObject2 = new HitBoxDisplayGameObject();
            newHitBoxDisplayGameObject2.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayScriptableObject.GetHitBoxDisplayCirclePrefab());
            //newHitBoxDisplayGameObject2.hitBoxDisplayGameObject = Instantiate(UFE2FTE.hitBoxDisplayOptions.prefabOptions.GetHitBoxDisplayCirclePrefab(), newProjectileHitboxDisplay.projectileMoveScript.transform);    
            newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.name = blockableAreaCircleMessage;
            newHitBoxDisplayGameObject2.hitBoxDisplayTransform = newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.transform;
            newHitBoxDisplayGameObject2.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayGameObject2.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject2.hitBoxDisplaySpriteRenderer.gameObject;
            newHitBoxDisplayGameObject2.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject2.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
            newHitBoxDisplayGameObject2.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject2.hitBoxDisplayMeshRenderer.gameObject;
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject2.hitBoxDisplayGizmosGameObject.transform;
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject2.hitBoxDisplayTransform;
            newHitBoxDisplayGameObject2.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0, 0, 0);
            newProjectileHitboxDisplay.blockableAreaCircleGameObject = newHitBoxDisplayGameObject2;

            HitBoxDisplayGameObject newHitBoxDisplayGameObject3 = new HitBoxDisplayGameObject();
            newHitBoxDisplayGameObject3.hitBoxDisplayGameObject = Instantiate(hitBoxDisplayScriptableObject.GetHitBoxDisplayRectanglePrefab());
            //newHitBoxDisplayGameObject3.hitBoxDisplayGameObject = Instantiate(UFE2FTE.hitBoxDisplayOptions.prefabOptions.GetHitBoxDisplayRectanglePrefab(), newProjectileHitboxDisplay.projectileMoveScript.transform);
            newHitBoxDisplayGameObject3.hitBoxDisplayGameObject.name = blockableAreaRectangleMessage;
            newHitBoxDisplayGameObject3.hitBoxDisplayTransform = newHitBoxDisplayGameObject3.hitBoxDisplayGameObject.transform;
            newHitBoxDisplayGameObject3.hitBoxDisplaySpriteRenderer = newHitBoxDisplayGameObject3.hitBoxDisplayGameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayGameObject3.hitBoxDisplaySpriteRendererGameObject = newHitBoxDisplayGameObject3.hitBoxDisplaySpriteRenderer.gameObject;
            newHitBoxDisplayGameObject3.hitBoxDisplayMeshRenderer = newHitBoxDisplayGameObject3.hitBoxDisplayGameObject.GetComponentInChildren<MeshRenderer>();
            newHitBoxDisplayGameObject3.hitBoxDisplayMeshRendererGameObject = newHitBoxDisplayGameObject3.hitBoxDisplayMeshRenderer.gameObject;
            newHitBoxDisplayGameObject3.hitBoxDisplayGizmosGameObject = new GameObject(gizmosMessage);
            newHitBoxDisplayGameObject3.hitBoxDisplayGizmosTransform = newHitBoxDisplayGameObject3.hitBoxDisplayGizmosGameObject.transform;
            newHitBoxDisplayGameObject3.hitBoxDisplayGizmosTransform.parent = newHitBoxDisplayGameObject3.hitBoxDisplayTransform;
            newHitBoxDisplayGameObject3.hitBoxDisplayGizmosTransform.localPosition = new Vector3(0.5f, 0.5f, 0);
            newProjectileHitboxDisplay.blockableAreaRectangleGameObject = newHitBoxDisplayGameObject3;

            projectileHitBoxDisplayList.Add(newProjectileHitboxDisplay);
        }

        #endregion

        #region Set Character Hit Boxes GameObjects Position And Scale Methods

        private void SetCharacterHitBoxesGameObjectsPositionAndScale()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.customHitBoxes != null)
                    {
                        if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].shape == HitBoxShape.circle)
                        {
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(true);

                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localPosition = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].localPosition.x * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].localPosition.y, GetZPositionFromHitbox(characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]));
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._radius * 2);
                        }
                        else if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].shape == HitBoxShape.rectangle)
                        {
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(true);

                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localPosition = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].localPosition.x * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].localPosition.y, GetZPositionFromHitbox(characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]));
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._rect.width * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._rect.height, 0.01f);
                        }
                    }
                    else
                    {
                        if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].shape == HitBoxShape.circle)
                        {
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(true);

                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position = new Vector3(characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].position.position.x + (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._offSet.x * -characterHitBoxDisplayList[i].controlsScript.mirror, characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].position.position.y + (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._offSet.y, GetZPositionFromHitbox(characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]));
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._radius * 2);
                        }
                        else if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].shape == HitBoxShape.rectangle)
                        {
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(true);

                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.position = new Vector3(characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].position.position.x + (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._rect._x, characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].position.position.y + (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._rect._y, GetZPositionFromHitbox(characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]));
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._rect.width, (float)characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a]._rect.height, 0.01f);
                        }
                    }
                }
            }
        }

        #endregion

        #region Set Character Active Hurt Boxes GameObjects Position And Scale Methods

        private void SetCharacterActiveHurtBoxesGameObjectsPositionAndScale()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes == null)
                {
                    continue;
                }

                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].shape == HitBoxShape.circle)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(true);

                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].position.x + (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._offSet.x, (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].position.y + (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._offSet.y, GetZPositionFromHurtBox(characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]));
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._radius * 2);
                    }
                    else if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].shape == HitBoxShape.rectangle)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(true);

                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.position = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].position.x + (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._rect._x * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].position.y + (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._rect._y, GetZPositionFromHurtBox(characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]));
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._rect.width * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a]._rect.height, 0.01f);
                    }
                }
            }
        }

        #endregion

        #region Set Character Blockable Area GameObjects Position And Scale Methods

        private void SetCharacterBlockableAreaGameObjectsPositionAndScale()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea == null)
                {
                    continue;
                }

                if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.shape == HitBoxShape.circle)
                {
                    characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(true);

                    characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.position = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.position.x + (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._offSet.x * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.position.y + (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._offSet.y, GetZPositionFromBlockArea(characterHitBoxDisplayList[i].hitBoxesScript.blockableArea));
                    characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._radius * 2, (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._radius * 2);
                }
                else if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.shape == HitBoxShape.rectangle)
                {
                    characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(true);

                    characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.position = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.position.x + (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._rect._x * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.position.y + (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._rect._y, GetZPositionFromBlockArea(characterHitBoxDisplayList[i].hitBoxesScript.blockableArea));
                    characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale = new Vector3((float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._rect.width * -characterHitBoxDisplayList[i].controlsScript.mirror, (float)characterHitBoxDisplayList[i].hitBoxesScript.blockableArea._rect.height, 0.01f);
                }
            }
        }

        #endregion

        #region Set Equal Character Colliders Methods

        private void SetEqualCharacterColliders()
        {
            if (hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.disableCharacterCollidersEqualToCharacterActiveHurtBoxColliders == true)
            {
                int count = characterHitBoxDisplayList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes == null)
                    {
                        continue;
                    }

                    int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        int lengthB = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                        for (int b = 0; b < lengthB; b++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[b].hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[b].hitBoxDisplayGameObject.SetActive(false);
                            }
                        }

                        lengthB = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                        for (int b = 0; b < lengthB; b++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[b].hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale)
                            {
                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[b].hitBoxDisplayGameObject.SetActive(false);
                            }
                        }
                    }
                }
            }

            if (hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.disableCharacterBlockableAreaCollidersEqualToCharacterActiveHurtBoxCollider == true)
            {
                int count = characterHitBoxDisplayList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes == null)
                    {
                        continue;
                    }

                    int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale)
                        {
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale)
                        {
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                        }
                    }
                }
            }

            if (hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.disableCharacterCollidersEqualToCharacterBlockableAreaCollider == true)
            {
                int count = characterHitBoxDisplayList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea == null)
                    {
                        continue;
                    }

                    int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale)
                        {
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                        }
                    }

                    lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale)
                        {
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                        }
                    }
                }
            }

            if (hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.disableCharacterActiveHurtBoxCollidersEqualToCharacterBlockableAreaCollider == true)
            {
                int count = characterHitBoxDisplayList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea == null)
                    {
                        continue;
                    }

                    int lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale)
                        {
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                        }
                    }

                    lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale == characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale)
                        {
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        #endregion

        #region Set All Character GameObjects Sprite Renderer Sprite Methods

        private void SetAllCharacterGameObjectsSpriteRendererSprite()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayCircleFillSprite;
                }

                lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayRectangleFillSprite;
                }

                lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayCircleFillSprite;
                }

                lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayRectangleFillSprite;
                }

                if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                {
                    characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayCircleFillSprite;
                }

                if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                {
                    characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayRectangleFillSprite;
                }
            }
        }

        #endregion

        #region Set All Character GameObjects Color Methods

        private void SetAllCharacterGameObjectsColor()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    switch (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].collisionType)
                    {
                        case CollisionType.bodyCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }
                            break;

                        case CollisionType.hitCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }
                            break;

                        case CollisionType.noCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }
                            break;

                        case CollisionType.throwCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }
                            break;

                        case CollisionType.projectileInvincibleCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }
                            break;

                        case CollisionType.physicalInvincibleCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                            {
                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor;
                                }

                                if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                                {
                                    myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor);
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                                }
                            }
                            break;
                    }
                }

                lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor;
                    }

                    if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                    {
                        myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor);
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                    }
                }

                lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor;
                    }

                    if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                    {
                        myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor);
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                    }
                }

                if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                {
                    if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.colliderColor;
                    }

                    if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                    {
                        myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.colliderColor);
                        characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                    }
                }

                if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                {
                    if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.colliderColor;
                    }

                    if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                    {
                        myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.colliderColor);
                        characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                    }
                }
            }
        }

        #endregion

        #region Set All Character GameObjects Sprite Renderer Order In Layer Methods

        private void SetAllCharacterGameObjectsSpriteRendererOrderInLayer()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    switch (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].collisionType)
                    {
                        case CollisionType.bodyCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBodyColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBodyColliderOptions.orderInLayerBehind;
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBodyColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBodyColliderOptions.orderInLayerBehind;
                                }
                            }
                            break;

                        case CollisionType.hitCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterHitColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterHitColliderOptions.orderInLayerBehind;
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterHitColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterHitColliderOptions.orderInLayerBehind;
                                }
                            }
                            break;

                        case CollisionType.noCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterNoColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterNoColliderOptions.orderInLayerBehind;
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterNoColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterNoColliderOptions.orderInLayerBehind;
                                }
                            }
                            break;

                        case CollisionType.throwCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterThrowColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterThrowColliderOptions.orderInLayerBehind;
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterThrowColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterThrowColliderOptions.orderInLayerBehind;
                                }
                            }
                            break;

                        case CollisionType.projectileInvincibleCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.orderInLayerBehind;
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.orderInLayerBehind;
                                }
                            }
                            break;

                        case CollisionType.physicalInvincibleCollider:
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.orderInLayerBehind;
                                }
                            }

                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                            {
                                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.orderInLayerInfront;
                                }
                                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                                {
                                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.orderInLayerBehind;
                                }
                            }
                            break;
                    }
                }

                lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.orderInLayerInfront;
                    }
                    else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.orderInLayerBehind;
                    }
                }

                lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.orderInLayerInfront;
                    }
                    else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.orderInLayerBehind;
                    }
                }

                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                {
                    if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.orderInLayerInfront;
                    }

                    if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.orderInLayerInfront;
                    }
                }
                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                {
                    if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.orderInLayerBehind;
                    }

                    if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.characterBlockableAreaColliderOptions.orderInLayerBehind;
                    }
                }
            }
        }

        #endregion

        #region Set Unused Character Hit Boxes GameObjects Methods

        private void SetUnusedCharacterHitBoxesGameObjects()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int disableIndex = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;

                int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                for (int a = disableIndex; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                }

                lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                for (int a = disableIndex; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Set Character Hit Boxes GameObjects By Shape Methods

        private void SetCharacterHitBoxesGameObjectsByShape()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].shape == HitBoxShape.circle)
                    {
                        characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }
                    else if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].shape == HitBoxShape.rectangle)
                    {
                        characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        #region Set Invincible Character Hit Boxes GameObjects Methods

        private void SetInvincibleCharacterHitBoxesGameObjects()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].hide == false)
                    {
                        continue;
                    }

                    characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                    characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Set Unused Character Active Hurt Boxes GameObjects Methods

        private void SetUnusedCharacterActiveHurtBoxesGameObjects()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes != null)
                {
                    int disableIndex = characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes.Length;

                    int lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                    for (int a = disableIndex; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                        {
                            continue;
                        }

                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }

                    lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                    for (int a = disableIndex; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                        {
                            continue;
                        }

                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }
                }
                else
                {
                    int lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                        {
                            continue;
                        }

                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }

                    lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                        {
                            continue;
                        }

                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        #region Set Character Active Hurt Boxes GameObjects By Shape Methods

        private void SetCharacterActiveHurtBoxesGameObjectsByShape()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes == null)
                {
                    continue;
                }

                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].shape == HitBoxShape.circle)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }
                    else if (characterHitBoxDisplayList[i].hitBoxesScript.activeHurtBoxes[a].shape == HitBoxShape.rectangle)
                    {
                        characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        #region Set Unused Character Blockable Area GameObjects Methods

        private void SetUnusedCharacterBlockableAreaGameObjects()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea != null)
                {
                    continue;
                }

                if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                {
                    characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                }

                if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                {
                    characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Set Character Blockable Area GameObjects By Shape Methods

        private void SetCharacterBlockableAreaGameObjectsByShape()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea == null)
                {
                    continue;
                }

                if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.shape == HitBoxShape.circle)
                {
                    characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                }
                else if (characterHitBoxDisplayList[i].hitBoxesScript.blockableArea.shape == HitBoxShape.rectangle)
                {
                    characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Set Character GameObjects By Disable Collider Methods

        private void SetCharacterGameObjectsByDisableCollider()
        {
            int count = characterHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    switch (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].collisionType)
                    {
                        case CollisionType.bodyCollider:
                            if (hitBoxDisplayScriptableObject.characterBodyColliderOptions.disableCollider == true)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            }
                            break;

                        case CollisionType.hitCollider:
                            if (hitBoxDisplayScriptableObject.characterHitColliderOptions.disableCollider == true)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            }
                            break;

                        case CollisionType.noCollider:
                            if (hitBoxDisplayScriptableObject.characterNoColliderOptions.disableCollider == true)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            }
                            break;

                        case CollisionType.throwCollider:
                            if (hitBoxDisplayScriptableObject.characterThrowColliderOptions.disableCollider == true)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            }
                            break;

                        case CollisionType.projectileInvincibleCollider:
                            if (hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.disableCollider == true)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            }
                            break;

                        case CollisionType.physicalInvincibleCollider:
                            if (hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.disableCollider == true)
                            {
                                characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);

                                characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            }
                            break;
                    }

                    if (hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.disableCollider == true)
                    {
                        int lengthB = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int b = 0; b < lengthB; b++)
                        {
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[b].hitBoxDisplayGameObject.SetActive(false);
                        }

                        lengthB = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int b = 0; b < lengthB; b++)
                        {
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[b].hitBoxDisplayGameObject.SetActive(false);
                        }
                    }

                    if (hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.disableCollider == true)
                    {
                        characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);

                        characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        #region Set Character GameObjects By Display Mode Methods

        private void SetCharacterGameObjectsByDisplayMode()
        {
            switch (UFE2FTE.Instance.hitBoxDisplayMode)
            {
                case UFE2FTE.HitBoxDisplayMode.Off:
                    int count = characterHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);              
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);                        
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);           
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);                 
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);                  
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront:
                case UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind:
                    count = characterHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.MeshRenderer3D:
                    count = characterHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.PopcronGizmos2D:
                case UFE2FTE.HitBoxDisplayMode.PopcronGizmos3D:
                    count = characterHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int lengthA = characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        lengthA = characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            //characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region Set All Projectile GameObjects Position And Scale Methods

        private void SetAllProjectileGameObjectsPositionAndScale()
        {
            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScriptGameObject.activeInHierarchy == false)
                {
                    continue;
                }

                if (projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox.shape == HitBoxShape.circle)
                {
                    projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.position = new Vector3(projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.x + (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._offSet.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.y + (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._offSet.y, 0);
                    //projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.localPosition = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._offSet.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._offSet.y, 0);
                    projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.localScale = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._radius * 2, (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._radius * 2, 1);
                }
                else if (projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox.shape == HitBoxShape.rectangle)
                {
                    projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.position = new Vector3(projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.x + (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._rect.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.y + (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._rect.y, 0);
                    //projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.localPosition = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._rect.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._rect.y, 0);
                    projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.localScale = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._rect.width * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, (float)projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox._rect.height, 1);
                }

                if (projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea.shape == HitBoxShape.circle)
                {
                    projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.position = new Vector3(projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.x + (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._offSet.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.y + (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._offSet.y, 0);
                    //projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localPosition = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._offSet.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._offSet.y, 0);
                    projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._radius * 2, (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._radius * 2, 1);
                }
                else if (projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea.shape == HitBoxShape.rectangle)
                {
                    projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.position = new Vector3(projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.x + (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._rect.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, projectileHitBoxDisplayList[i].projectileMoveScriptTransform.position.y + (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._rect.y, 0);
                    //projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localPosition = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._rect.x * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._rect.y, 0);
                    projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale = new Vector3((float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._rect.width * -projectileHitBoxDisplayList[i].projectileMoveScript.mirror, (float)projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea._rect.height, 1);
                }
            }
        }

        #endregion

        #region Set All Projectile GameObjects Sprite Renderer Sprite Methods

        private void SetAllProjectileGameObjectsSpriteRendererSprite()
        {
            switch (UFE2FTE.Instance.hitBoxDisplayMode)
            {
                case UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront:
                case UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind:
                    break;

                default:
                    return;
            }

            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                {
                    projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayCircleFillSprite;
                }

                if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                {
                    projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayRectangleFillSprite;
                }

                if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                {
                    projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayCircleFillSprite;
                }

                if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                {
                    projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sprite = hitBoxDisplayRectangleFillSprite;
                }
            }
        }

        #endregion

        #region Set All Projectile GameObjects Color Methods

        private void SetAllProjectileGameObjectsColor()
        {
            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScript.data.projectileCollision == true
                    && projectileHitBoxDisplayList[i].projectileMoveScript.data.groundHit == false
                    && projectileHitBoxDisplayList[i].projectileMoveScript.data.airHit == false
                    && projectileHitBoxDisplayList[i].projectileMoveScript.data.downHit == false)
                {
                    if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.colliderColor;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                        {
                            myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.colliderColor);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                        }
                    }

                    if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.colliderColor;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                        {
                            myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.colliderColor);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                        }
                    }
                }
                else
                {
                    if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                        {
                            myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                        }
                    }

                    if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                        {
                            myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                        }
                    }
                }

                if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                {
                    if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor;
                    }

                    if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                    {
                        myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor);
                        projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                    }
                }

                if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                {
                    if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.color = hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor;
                    }

                    if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.activeInHierarchy == true)
                    {
                        myMaterialPropertyBlock.SetColor("_Color", hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor);
                        projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRenderer.SetPropertyBlock(myMaterialPropertyBlock);
                    }
                }
            }
        }

        #endregion

        #region Set All Projectile GameObjects Sprite Renderer Order In Layer Methods

        private void SetAllProjectileGameObjectsSpriteRendererOrderInLayer()
        {
            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScript.data.projectileCollision == true
                    && projectileHitBoxDisplayList[i].projectileMoveScript.data.groundHit == false
                    && projectileHitBoxDisplayList[i].projectileMoveScript.data.airHit == false
                    && projectileHitBoxDisplayList[i].projectileMoveScript.data.downHit == false)
                {
                    if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.orderInLayerInfront;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.orderInLayerInfront;
                        }
                    }
                    else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.orderInLayerBehind;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.orderInLayerBehind;
                        }
                    }
                }
                else
                {
                    if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.orderInLayerInfront;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.orderInLayerInfront;
                        }
                    }
                    else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.orderInLayerBehind;
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.orderInLayerBehind;
                        }
                    }
                }

                if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront)
                {
                    if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.orderInLayerInfront;
                    }

                    if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.orderInLayerInfront;
                    }
                }
                else if (UFE2FTE.Instance.hitBoxDisplayMode == UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind)
                {
                    if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.orderInLayerBehind;
                    }

                    if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.activeInHierarchy == true)
                    {
                        projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.orderInLayerBehind;
                    }
                }
            }
        }

        #endregion

        #region Set Projectile GameObjects By Shape Methods

        private void SetProjectileGameObjectsByShape()
        {
            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox.shape == HitBoxShape.circle)
                {
                    projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(true);
                    projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);             
                }
                else if (projectileHitBoxDisplayList[i].projectileMoveScript.hurtBox.shape == HitBoxShape.rectangle)
                {
                    projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                    projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(true);          
                }

                if (projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea.shape == HitBoxShape.circle)
                {
                    projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(true);
                    projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);        
                }
                else if (projectileHitBoxDisplayList[i].projectileMoveScript.blockableArea.shape == HitBoxShape.rectangle)
                {
                    projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                    projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(true);
                }
            }
        }

        #endregion

        #region Set Projectile GameObjects By Disable Collider Methods

        private void SetProjectileGameObjectsByShapeByDisableCollider()
        {
            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScriptGameObject.activeInHierarchy == false)
                {
                    continue;
                }

                if (hitBoxDisplayScriptableObject.projectileOnlyColliderOptions.disableCollider == true)
                {
                    if (projectileHitBoxDisplayList[i].projectileMoveScript.data.projectileCollision == true
                        && projectileHitBoxDisplayList[i].projectileMoveScript.data.groundHit == false
                        && projectileHitBoxDisplayList[i].projectileMoveScript.data.airHit == false
                        && projectileHitBoxDisplayList[i].projectileMoveScript.data.downHit == false)
                    {
                        projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                        projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                    }
                }

                if (hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.disableCollider == true)
                {
                    if (projectileHitBoxDisplayList[i].projectileMoveScript.data.groundHit == true
                        || projectileHitBoxDisplayList[i].projectileMoveScript.data.airHit == true
                        || projectileHitBoxDisplayList[i].projectileMoveScript.data.downHit == true)
                    {
                        projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                        projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                    }
                }

                if (hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.disableCollider == true)
                {
                    projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                    projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Set Projectile GameObjects By Display Mode Methods

        private void SetProjectileGameObjectsByDisplayMode()
        {
            switch (UFE2FTE.Instance.hitBoxDisplayMode)
            {
                case UFE2FTE.HitBoxDisplayMode.Off:
                    int count = projectileHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DInfront:
                case UFE2FTE.HitBoxDisplayMode.SpriteRenderer2DBehind:
                    count = projectileHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.MeshRenderer3D:
                    count = projectileHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(true);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(false);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.PopcronGizmos2D:
                case UFE2FTE.HitBoxDisplayMode.PopcronGizmos3D:
                    count = projectileHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGizmosGameObject.SetActive(true);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            //projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplaySpriteRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayMeshRendererGameObject.SetActive(false);
                            projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosGameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region Set All Projectile GameObjects Active

        private void SetAllProjectileGameObjectsActive()
        {
            int count = projectileHitBoxDisplayList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileHitBoxDisplayList[i].projectileMoveScriptGameObject.activeInHierarchy == true)
                {
                    continue;
                }

                projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);

                projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);

                projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.SetActive(false);

                projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.SetActive(false);
            }
        }

        #endregion

        #region Gizmos Methods

        private void SetGizmos()
        {
            switch (UFE2FTE.Instance.hitBoxDisplayMode)
            {
                case UFE2FTE.HitBoxDisplayMode.PopcronGizmos2D:
                    Popcron.Gizmos.Enabled = true;

                    int count = characterHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            switch (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].collisionType)
                            {
                                case CollisionType.bodyCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.hitCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.noCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.throwCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.projectileInvincibleCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.physicalInvincibleCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor);
                                    }
                                    break;
                            }
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            Popcron.Gizmos.Square(characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Circle(characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.y) / 4, characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Square(characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor);
                        }
                    }

                    count = projectileHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Circle(projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.position, (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.localScale.x + projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.localScale.y) / 4, projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Square(projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGizmosTransform.position, projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.rotation, projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Circle(projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.position, (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.x + projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.y) / 4, projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.rotation, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Square(projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosTransform.position, projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.rotation, projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor);
                        }
                    }
                    break;

                case UFE2FTE.HitBoxDisplayMode.PopcronGizmos3D:
                    Popcron.Gizmos.Enabled = true;

                    count = characterHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int lengthA = characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            switch (characterHitBoxDisplayList[i].hitBoxesScript.hitBoxes[a].collisionType)
                            {
                                case CollisionType.bodyCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterBodyColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.hitCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterHitColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.noCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterNoColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.throwCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterThrowColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.projectileInvincibleCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterProjectileInvincibleColliderOptions.colliderColor);
                                    }
                                    break;

                                case CollisionType.physicalInvincibleCollider:
                                    if (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].hitBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                                    }

                                    if (characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == true)
                                    {
                                        Popcron.Gizmos.Square(characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].hitBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterPhysicalInvincibleColliderOptions.colliderColor);
                                    }
                                    break;
                            }
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].activeHurtBoxesCircleGameObjectArray[a].hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        lengthA = characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            if (characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGameObject.activeInHierarchy == false)
                            {
                                continue;
                            }

                            Popcron.Gizmos.Square(characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].activeHurtBoxesRectangleGameObjectArray[a].hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.characterActiveHurtBoxColliderOptions.colliderColor);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Sphere(characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.position, (characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.x + characterHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        if (characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Square(characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosTransform.position, characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.rotation, characterHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor);
                        }
                    }

                    count = projectileHitBoxDisplayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Sphere(projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.position, (projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.localScale.x + projectileHitBoxDisplayList[i].hitAreaCircleGameObject.hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        if (projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Square(projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayGizmosTransform.position, projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.rotation, projectileHitBoxDisplayList[i].hitAreaRectangleGameObject.hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.projectileHitAreaColliderOptions.colliderColor);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Sphere(projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.position, (projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.x + projectileHitBoxDisplayList[i].blockableAreaCircleGameObject.hitBoxDisplayTransform.localScale.y) / 4, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor, false, hitBoxDisplayScriptableObject.hitBoxDisplayPopcronGizmosLinesAmount);
                        }

                        if (projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGameObject.activeInHierarchy == true)
                        {
                            Popcron.Gizmos.Square(projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayGizmosTransform.position, projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.rotation, projectileHitBoxDisplayList[i].blockableAreaRectangleGameObject.hitBoxDisplayTransform.localScale, hitBoxDisplayScriptableObject.projectileBlockableAreaColliderOptions.colliderColor);
                        }
                    }
                    break;
            }
        }

        #endregion

        private float GetZPositionFromHitbox(HitBox hitBox)
        {
            if (hitBox == null)
            {
                return 0;
            }

            if (UFE.config.detect3D_Hits == true)
            {
                return hitBox.position.position.z;
            }

            return 0;
        }

        private float GetZPositionFromHurtBox(HurtBox hurtBox)
        {
            if (hurtBox == null)
            {
                return 0;
            }

            if (UFE.config.detect3D_Hits == true)
            {
                return (float)hurtBox.position.z;
            }

            return 0;
        }

        private float GetZPositionFromBlockArea(BlockArea blockArea)
        {
            if (blockArea == null)
            {
                return 0;
            }

            if (UFE.config.detect3D_Hits == true)
            {
                return (float)blockArea.position.z;
            }

            return 0;
        }
    }
}
