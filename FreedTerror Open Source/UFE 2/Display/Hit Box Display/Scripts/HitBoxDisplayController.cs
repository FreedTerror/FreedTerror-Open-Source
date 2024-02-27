using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class HitBoxDisplayController : MonoBehaviour
    {
        public enum HitBoxDisplayMode
        {
            Off,
            SpriteInfront,
            SpriteBehind,
            Mesh
        }

        [System.Serializable]
        private class HitBoxDisplayData
        {
            public GameObject gameObject;
            public Transform transform;
            public SpriteRenderer spriteRenderer;
            public MeshRenderer meshRenderer;
        }

        private HitBoxDisplayData GetNewHitBoxDisplayData(GameObject gameObjectToSpawn, string gameObjectName, Transform parent, Sprite sprite)
        {
            HitBoxDisplayData newHitBoxDisplayData = new HitBoxDisplayData();
            newHitBoxDisplayData.gameObject = Instantiate(gameObjectToSpawn, parent);
            newHitBoxDisplayData.gameObject.name = gameObjectName;
            newHitBoxDisplayData.transform = newHitBoxDisplayData.gameObject.transform;
            newHitBoxDisplayData.spriteRenderer = newHitBoxDisplayData.gameObject.GetComponentInChildren<SpriteRenderer>();
            newHitBoxDisplayData.spriteRenderer.sprite = sprite;
            newHitBoxDisplayData.meshRenderer = newHitBoxDisplayData.gameObject.GetComponentInChildren<MeshRenderer>();
            return newHitBoxDisplayData;
        }

        [System.Serializable]
        private class CharacterHitBoxDisplayData
        {
            public ControlsScript controlsScript;
            public HitBoxesScript hitBoxesScript;
            public List<HitBoxDisplayData> hurtBoxesCircleDataList;
            public List<HitBoxDisplayData> hurtBoxesRectangleDataList;
            public List<HitBoxDisplayData> activeHitBoxesCircleDataList;
            public List<HitBoxDisplayData> activeHitBoxesRectangleDataList;
            //public HitBoxDisplayData blockableAreaCircleData;
            //public HitBoxDisplayData blockableAreaRectangleData;
        }

        [System.Serializable]
        private class ProjectileHitboxDisplayData
        {
            public ProjectileMoveScript projectileMoveScript;
            public GameObject gameObject;
            public Transform transform;
            public HitBoxDisplayData activeHitBoxCircleData;
            public HitBoxDisplayData activeHitBoxRectangleData;
            public HitBoxDisplayData blockableAreaCircleData;
            public HitBoxDisplayData blockableAreaRectangleData;
        }

        [SerializeField]
        private HitBoxDisplayScriptableObject hitBoxDisplayScriptableObject;

        private GameObject circlePrefab;
        private GameObject rectanglePrefab;
        private Sprite circleSprite;
        private Sprite rectangleSprite;

        private List<CharacterHitBoxDisplayData> characterHitBoxDisplayDataList = new List<CharacterHitBoxDisplayData>();
        private List<ProjectileHitboxDisplayData> projectileHitBoxDisplayDataList = new List<ProjectileHitboxDisplayData>();

        private MaterialPropertyBlock materialPropertyBlock;
        private readonly int _ColorShaderPropertyID = Shader.PropertyToID("_Color");

        private readonly string hurtBoxCircleName = "Hurt Box Circle ";
        private readonly string hurtBoxRectangleName = "Hurt Box Rectangle ";
        private readonly string activeHitBoxCircleName = "Active Hit Box Circle ";
        private readonly string activeHitBoxRectangleName = "Active Hit Box Rectangle ";
        private readonly string blockableAreaCircleName = "Blockable Area Circle";
        private readonly string blockableAreaRectangleName = "Blockable Area Rectangle";

        private void Start()
        {
            if (hitBoxDisplayScriptableObject != null)
            {
                circlePrefab = hitBoxDisplayScriptableObject.GetCirclePrefab();
                rectanglePrefab = hitBoxDisplayScriptableObject.GetRectanglePrefab();

                circleSprite = hitBoxDisplayScriptableObject.GetCircleSprite();
                rectangleSprite = hitBoxDisplayScriptableObject.GetRectangleSprite();

                materialPropertyBlock = new MaterialPropertyBlock();
            }
        }

        private void Update()
        {
            if (hitBoxDisplayScriptableObject != null)
            {
                hitBoxDisplayScriptableObject.UpdateColliderColorAlphaValues((byte)hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue);

                UpdateHitBoxDisplayLists();

                UpdateCharacterHitboxes();

                UpdateProjectileHitBoxes();
            }
        }

        #region Hit Box Display Lists Methods

        private void UpdateHitBoxDisplayLists()
        {
            int count = characterHitBoxDisplayDataList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (characterHitBoxDisplayDataList[i].controlsScript != null)
                {
                    continue;
                }

                characterHitBoxDisplayDataList.RemoveAt(i);
            }

            count = projectileHitBoxDisplayDataList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                var item = projectileHitBoxDisplayDataList[i];

                if (item.projectileMoveScript != null)
                {
                    continue;
                }

                Destroy(item.activeHitBoxCircleData.gameObject);

                Destroy(item.activeHitBoxRectangleData.gameObject);

                Destroy(item.blockableAreaCircleData.gameObject);

                Destroy(item.blockableAreaRectangleData.gameObject);

                projectileHitBoxDisplayDataList.RemoveAt(i);
            }

            if (UFE.p1ControlsScript != null)
            {
                AddToCharacterHitBoxDisplayList(UFE.p1ControlsScript);

                AddToCharacterHitBoxDisplayList(UFE.p1ControlsScript.assists);

                AddToProjectileHitBoxDisplayList(UFE.p1ControlsScript);

                AddToProjectileHitBoxDisplayList(UFE.p1ControlsScript.assists);
            }

            if (UFE.p2ControlsScript != null)
            {
                AddToCharacterHitBoxDisplayList(UFE.p2ControlsScript);

                AddToCharacterHitBoxDisplayList(UFE.p2ControlsScript.assists);

                AddToProjectileHitBoxDisplayList(UFE.p2ControlsScript);

                AddToProjectileHitBoxDisplayList(UFE.p2ControlsScript.assists);
            }

            count = characterHitBoxDisplayDataList.Count;
            for (int i = 0; i < count; i++)
            {
                //Hurt Boxes
                if (characterHitBoxDisplayDataList[i].hitBoxesScript.hitBoxes != null)
                {
                    if (characterHitBoxDisplayDataList[i].hurtBoxesCircleDataList.Count < characterHitBoxDisplayDataList[i].hitBoxesScript.hitBoxes.Length)
                    {
                        int addAmount = characterHitBoxDisplayDataList[i].hitBoxesScript.hitBoxes.Length - characterHitBoxDisplayDataList[i].hurtBoxesCircleDataList.Count;
                        for (int a = 0; a < addAmount; a++)
                        {
                            characterHitBoxDisplayDataList[i].hurtBoxesCircleDataList.Add(GetNewHitBoxDisplayData(circlePrefab, hurtBoxCircleName, characterHitBoxDisplayDataList[i].controlsScript.transform, circleSprite));
                        }
                    }

                    if (characterHitBoxDisplayDataList[i].hurtBoxesRectangleDataList.Count < characterHitBoxDisplayDataList[i].hitBoxesScript.hitBoxes.Length)
                    {
                        int addAmount = characterHitBoxDisplayDataList[i].hitBoxesScript.hitBoxes.Length - characterHitBoxDisplayDataList[i].hurtBoxesRectangleDataList.Count;
                        for (int a = 0; a < addAmount; a++)
                        {
                            characterHitBoxDisplayDataList[i].hurtBoxesRectangleDataList.Add(GetNewHitBoxDisplayData(rectanglePrefab, hurtBoxRectangleName, characterHitBoxDisplayDataList[i].controlsScript.transform, rectangleSprite));
                        }
                    }
                }

                //Active Hit Boxes
                if (characterHitBoxDisplayDataList[i].hitBoxesScript.activeHurtBoxes != null)
                {
                    if (characterHitBoxDisplayDataList[i].activeHitBoxesCircleDataList.Count < characterHitBoxDisplayDataList[i].hitBoxesScript.activeHurtBoxes.Length)
                    {
                        int addAmount = characterHitBoxDisplayDataList[i].hitBoxesScript.activeHurtBoxes.Length - characterHitBoxDisplayDataList[i].activeHitBoxesCircleDataList.Count;
                        for (int a = 0; a < addAmount; a++)
                        {
                            characterHitBoxDisplayDataList[i].activeHitBoxesCircleDataList.Add(GetNewHitBoxDisplayData(circlePrefab, activeHitBoxCircleName, characterHitBoxDisplayDataList[i].controlsScript.transform, circleSprite));
                        }
                    }

                    if (characterHitBoxDisplayDataList[i].activeHitBoxesRectangleDataList.Count < characterHitBoxDisplayDataList[i].hitBoxesScript.activeHurtBoxes.Length)
                    {
                        int addAmount = characterHitBoxDisplayDataList[i].hitBoxesScript.activeHurtBoxes.Length - characterHitBoxDisplayDataList[i].activeHitBoxesRectangleDataList.Count;
                        for (int a = 0; a < addAmount; a++)
                        {
                            characterHitBoxDisplayDataList[i].activeHitBoxesRectangleDataList.Add(GetNewHitBoxDisplayData(rectanglePrefab, activeHitBoxRectangleName, characterHitBoxDisplayDataList[i].controlsScript.transform, rectangleSprite));
                        }
                    }
                }
            }
        }

        private void AddToCharacterHitBoxDisplayList(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            int count = characterHitBoxDisplayDataList.Count;
            for (int i = 0; i < count; i++)
            {
                if (player != characterHitBoxDisplayDataList[i].controlsScript)
                {
                    continue;
                }

                return;
            }

            CharacterHitBoxDisplayData newCharacterHitBoxDisplay = new CharacterHitBoxDisplayData();
            newCharacterHitBoxDisplay.controlsScript = player;
            newCharacterHitBoxDisplay.hitBoxesScript = player.HitBoxes;
            newCharacterHitBoxDisplay.hurtBoxesCircleDataList = new List<HitBoxDisplayData>();
            newCharacterHitBoxDisplay.hurtBoxesRectangleDataList = new List<HitBoxDisplayData>();
            newCharacterHitBoxDisplay.activeHitBoxesCircleDataList = new List<HitBoxDisplayData>();
            newCharacterHitBoxDisplay.activeHitBoxesRectangleDataList = new List<HitBoxDisplayData>();
            characterHitBoxDisplayDataList.Add(newCharacterHitBoxDisplay);
        }

        private void AddToCharacterHitBoxDisplayList(List<ControlsScript> player)
        {
            if (player == null)
            {
                return;
            }

            int count = player.Count;
            for (int i = 0; i < count; i++)
            {
                AddToCharacterHitBoxDisplayList(player[i]);
            }
        }

        private void AddToProjectileHitBoxDisplayList(ProjectileMoveScript projectileMoveScript)
        {
            if (projectileMoveScript == null)
            {
                return;
            }

            int count = projectileHitBoxDisplayDataList.Count;
            for (int i = 0; i < count; i++)
            {
                if (projectileMoveScript != projectileHitBoxDisplayDataList[i].projectileMoveScript)
                {
                    continue;
                }

                return;
            }

            ProjectileHitboxDisplayData newProjectileHitboxDisplay = new ProjectileHitboxDisplayData();
            newProjectileHitboxDisplay.projectileMoveScript = projectileMoveScript;
            newProjectileHitboxDisplay.gameObject = projectileMoveScript.gameObject;
            newProjectileHitboxDisplay.transform = projectileMoveScript.transform;
            newProjectileHitboxDisplay.activeHitBoxCircleData = GetNewHitBoxDisplayData(circlePrefab, activeHitBoxCircleName, newProjectileHitboxDisplay.transform, circleSprite);
            newProjectileHitboxDisplay.activeHitBoxRectangleData = GetNewHitBoxDisplayData(rectanglePrefab, activeHitBoxRectangleName, newProjectileHitboxDisplay.transform, rectangleSprite);
            newProjectileHitboxDisplay.blockableAreaCircleData = GetNewHitBoxDisplayData(circlePrefab, blockableAreaCircleName, newProjectileHitboxDisplay.transform, circleSprite);
            newProjectileHitboxDisplay.blockableAreaRectangleData = GetNewHitBoxDisplayData(rectanglePrefab, blockableAreaRectangleName, newProjectileHitboxDisplay.transform, rectangleSprite);
            projectileHitBoxDisplayDataList.Add(newProjectileHitboxDisplay);
        }

        private void AddToProjectileHitBoxDisplayList(List<ProjectileMoveScript> projectileMoveScript)
        {
            if (projectileMoveScript == null)
            {
                return;
            }

            int count = projectileMoveScript.Count;
            for (int i = 0; i < count; i++)
            {
                AddToProjectileHitBoxDisplayList(projectileMoveScript[i]);
            }
        }

        private void AddToProjectileHitBoxDisplayList(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            AddToProjectileHitBoxDisplayList(player.projectiles);
        }

        private void AddToProjectileHitBoxDisplayList(List<ControlsScript> player)
        {
            if (player == null)
            {
                return;
            }

            int count = player.Count;
            for (int i = 0; i < count; i++)
            {
                var item = player[i];

                if (item == null)
                {
                    continue;
                }

                AddToProjectileHitBoxDisplayList(item.projectiles);
            }
        }

        #endregion

        private void UpdateCharacterHitboxes()
        {
            int count = 0;
            int countA = 0;

            switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
            {
                case HitBoxDisplayMode.Off:
                    count = characterHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var characterHitBoxDisplayListItem = characterHitBoxDisplayDataList[i];
              
                        countA = characterHitBoxDisplayListItem.hurtBoxesCircleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            characterHitBoxDisplayListItem.hurtBoxesCircleDataList[a].gameObject.SetActive(false);
                        }

                        countA = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            characterHitBoxDisplayListItem.hurtBoxesRectangleDataList[a].gameObject.SetActive(false);
                        }

                        countA = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            characterHitBoxDisplayListItem.activeHitBoxesCircleDataList[a].gameObject.SetActive(false);
                        }

                        countA = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList[a].gameObject.SetActive(false);
                        }
                    }
                    return;

                case HitBoxDisplayMode.SpriteInfront:
                case HitBoxDisplayMode.SpriteBehind:
                    count = characterHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var characterHitBoxDisplayListItem = characterHitBoxDisplayDataList[i];

                        countA = characterHitBoxDisplayListItem.hurtBoxesCircleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.hurtBoxesCircleDataList[a];
                         
                            item.spriteRenderer.enabled = true;
                            item.meshRenderer.enabled = false;
                        }

                        countA = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList[a];

                            item.spriteRenderer.enabled = true;
                            item.meshRenderer.enabled = false;
                        }

                        countA = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList[a];

                            item.spriteRenderer.enabled = true;
                            item.meshRenderer.enabled = false;
                        }

                        countA = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList[a];

                            item.spriteRenderer.enabled = true;
                            item.meshRenderer.enabled = false;
                        }
                    }
                    break;

                case HitBoxDisplayMode.Mesh:
                    count = characterHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var characterHitBoxDisplayListItem = characterHitBoxDisplayDataList[i];

                        countA = characterHitBoxDisplayListItem.hurtBoxesCircleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.hurtBoxesCircleDataList[a];

                            item.spriteRenderer.enabled = false;
                            item.meshRenderer.enabled = true;
                        }

                        countA = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList[a];

                            item.spriteRenderer.enabled = false;
                            item.meshRenderer.enabled = true;
                        }

                        countA = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList[a];

                            item.spriteRenderer.enabled = false;
                            item.meshRenderer.enabled = true;
                        }

                        countA = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList.Count;
                        for (int a = 0; a < countA; a++)
                        {
                            var item = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList[a];

                            item.spriteRenderer.enabled = false;
                            item.meshRenderer.enabled = true;
                        }
                    }
                    break;
            }

            count = characterHitBoxDisplayDataList.Count;
            for (int i = 0; i < count; i++)
            {
                var characterHitBoxDisplayListItem = characterHitBoxDisplayDataList[i];

                #region Hurt Boxes

                if (characterHitBoxDisplayListItem.hitBoxesScript.hitBoxes != null)
                {
                    int lengthA = characterHitBoxDisplayListItem.hitBoxesScript.hitBoxes.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        var hitBoxesItem = characterHitBoxDisplayListItem.hitBoxesScript.hitBoxes[a];
                        var hurtBoxesCircleDataListItem = characterHitBoxDisplayListItem.hurtBoxesCircleDataList[a];
                        var hurtBoxesRectangleDataListItem = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList[a];

                        if (hitBoxesItem.shape == HitBoxShape.circle)
                        {
                            hurtBoxesCircleDataListItem.gameObject.SetActive(true);
                            hurtBoxesRectangleDataListItem.gameObject.SetActive(false);

                            if (characterHitBoxDisplayListItem.controlsScript.myInfo.useAnimationMaps == true
                                || characterHitBoxDisplayListItem.hitBoxesScript.customHitBoxes != null)
                            {
                                hurtBoxesCircleDataListItem.transform.position = new Vector3((float)hitBoxesItem.mappedPosition.x + (float)hitBoxesItem._offSet.x * -characterHitBoxDisplayListItem.controlsScript.mirror, (float)hitBoxesItem.mappedPosition.y + (float)hitBoxesItem._offSet.y, GetZPositionFromHitbox(hitBoxesItem));
                                hurtBoxesCircleDataListItem.transform.localScale = new Vector3((float)hitBoxesItem._radius * 2, (float)hitBoxesItem._radius * 2, (float)hitBoxesItem._radius * 2);
                            }
                            else if (characterHitBoxDisplayListItem.controlsScript.myInfo.useAnimationMaps == false)
                            {
                                hurtBoxesCircleDataListItem.transform.position = new Vector3((float)hitBoxesItem.position.position.x + (float)hitBoxesItem._offSet.x * -characterHitBoxDisplayListItem.controlsScript.mirror, hitBoxesItem.position.position.y + (float)hitBoxesItem._offSet.y, GetZPositionFromHitbox(hitBoxesItem));
                                hurtBoxesCircleDataListItem.transform.localScale = new Vector3((float)hitBoxesItem._radius * 2, (float)hitBoxesItem._radius * 2, (float)hitBoxesItem._radius * 2);
                            }

                            switch (hitBoxesItem.collisionType)
                            {
                                case CollisionType.bodyCollider:                                 
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.bodyColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.bodyColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.bodyColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.bodyColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.bodyColliderOptions.colliderColor);
                                            hurtBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.hitCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.hitColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.hitColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.hitColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.hitColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.hitColliderOptions.colliderColor);
                                            hurtBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.noCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.noColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.noColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.noColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.noColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.noColliderOptions.colliderColor);
                                            hurtBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.throwCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.throwColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.throwColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.throwColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.throwColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.throwColliderOptions.colliderColor);
                                            hurtBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.projectileInvincibleCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.colliderColor);
                                            hurtBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.physicalInvincibleCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.colliderColor;
                                            hurtBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.colliderColor);
                                            hurtBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else if (hitBoxesItem.shape == HitBoxShape.rectangle)
                        {
                            hurtBoxesCircleDataListItem.gameObject.SetActive(false);
                            hurtBoxesRectangleDataListItem.gameObject.SetActive(true);

                            if (characterHitBoxDisplayListItem.controlsScript.myInfo.useAnimationMaps == true
                                || characterHitBoxDisplayListItem.hitBoxesScript.customHitBoxes != null)
                            {
                                hurtBoxesRectangleDataListItem.transform.position = new Vector3((float)hitBoxesItem.mappedPosition.x + (float)hitBoxesItem._rect._x, (float)hitBoxesItem.mappedPosition.y + (float)hitBoxesItem._rect._y, GetZPositionFromHitbox(hitBoxesItem));
                                hurtBoxesRectangleDataListItem.transform.localScale = new Vector3((float)hitBoxesItem._rect.width, (float)hitBoxesItem._rect.height, 1);
                            }
                            else if (characterHitBoxDisplayListItem.controlsScript.myInfo.useAnimationMaps == false)
                            {
                                hurtBoxesRectangleDataListItem.transform.position = new Vector3((float)hitBoxesItem.position.position.x + (float)hitBoxesItem._rect._x, (float)hitBoxesItem.position.position.y + (float)hitBoxesItem._rect._y, GetZPositionFromHitbox(hitBoxesItem));
                                hurtBoxesRectangleDataListItem.transform.localScale = new Vector3((float)hitBoxesItem._rect.width, (float)hitBoxesItem._rect.height, 1);
                            }

                            switch (hitBoxesItem.collisionType)
                            {
                                case CollisionType.bodyCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.bodyColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.bodyColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.bodyColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.bodyColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.bodyColliderOptions.colliderColor);
                                            hurtBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.hitCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.hitColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.hitColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.hitColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.hitColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.hitColliderOptions.colliderColor);
                                            hurtBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.noCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.noColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.noColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.noColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.noColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.noColliderOptions.colliderColor);
                                            hurtBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.throwCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.throwColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.throwColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.throwColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.throwColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.throwColliderOptions.colliderColor);
                                            hurtBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.projectileInvincibleCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.projectileInvincibleColliderOptions.colliderColor);
                                            hurtBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;

                                case CollisionType.physicalInvincibleCollider:
                                    switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                                    {
                                        case HitBoxDisplayMode.SpriteInfront:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.orderInLayerInfront;
                                            break;

                                        case HitBoxDisplayMode.SpriteBehind:
                                            hurtBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.colliderColor;
                                            hurtBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.orderInLayerBehind;
                                            break;

                                        case HitBoxDisplayMode.Mesh:
                                            hurtBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                            materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.physicalInvincibleColliderOptions.colliderColor);
                                            hurtBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                            break;
                                    }
                                    break;
                            }
                        }

                        if (hitBoxesItem.hide == true)
                        {
                            hurtBoxesCircleDataListItem.gameObject.SetActive(false);
                            hurtBoxesRectangleDataListItem.gameObject.SetActive(false);
                        }
                    }

                    countA = characterHitBoxDisplayListItem.hurtBoxesCircleDataList.Count;
                    for (int a = lengthA; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.hurtBoxesCircleDataList[a].gameObject.SetActive(false);
                    }

                    countA = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList.Count;
                    for (int a = lengthA; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.hurtBoxesRectangleDataList[a].gameObject.SetActive(false);
                    }
                }
                else
                {
                    countA = characterHitBoxDisplayListItem.hurtBoxesCircleDataList.Count;
                    for (int a = 0; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.hurtBoxesCircleDataList[a].gameObject.SetActive(false);
                    }

                    countA = characterHitBoxDisplayListItem.hurtBoxesRectangleDataList.Count;
                    for (int a = 0; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.hurtBoxesRectangleDataList[a].gameObject.SetActive(false);
                    }
                }

                #endregion

                #region Active Hit Boxes

                if (characterHitBoxDisplayListItem.hitBoxesScript.activeHurtBoxes != null)
                {
                    int lengthA = characterHitBoxDisplayListItem.hitBoxesScript.activeHurtBoxes.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        var activeHurtBoxesItem = characterHitBoxDisplayListItem.hitBoxesScript.activeHurtBoxes[a];
                        var activeHitBoxesCircleDataListItem = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList[a];
                        var activeHitBoxesRectangleDataListItem = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList[a];

                        if (activeHurtBoxesItem.shape == HitBoxShape.circle)
                        {                         
                            activeHitBoxesCircleDataListItem.gameObject.SetActive(true);
                            activeHitBoxesRectangleDataListItem.gameObject.SetActive(false);

                            activeHitBoxesCircleDataListItem.transform.position = new Vector3((float)activeHurtBoxesItem.position.x + (float)activeHurtBoxesItem._offSet.x, (float)activeHurtBoxesItem.position.y + (float)activeHurtBoxesItem._offSet.y, GetZPositionFromHurtBox(activeHurtBoxesItem));
                            activeHitBoxesCircleDataListItem.transform.localScale = new Vector3((float)activeHurtBoxesItem._radius * 2, (float)activeHurtBoxesItem._radius * 2, (float)activeHurtBoxesItem._radius * 2);

                            switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                            {
                                case HitBoxDisplayMode.SpriteInfront:
                                    activeHitBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                                    activeHitBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerInfront;
                                    break;

                                case HitBoxDisplayMode.SpriteBehind:
                                    activeHitBoxesCircleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                                    activeHitBoxesCircleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerBehind;
                                    break;

                                case HitBoxDisplayMode.Mesh:
                                    activeHitBoxesCircleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                    materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor);
                                    activeHitBoxesCircleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                    break;
                            }
                        }
                        else if (activeHurtBoxesItem.shape == HitBoxShape.rectangle)
                        {
                            activeHitBoxesCircleDataListItem.gameObject.SetActive(false);
                            activeHitBoxesRectangleDataListItem.gameObject.SetActive(true);

                            activeHitBoxesRectangleDataListItem.transform.position = new Vector3((float)activeHurtBoxesItem.position.x + (float)activeHurtBoxesItem._rect._x * -characterHitBoxDisplayListItem.controlsScript.mirror, (float)activeHurtBoxesItem.position.y + (float)activeHurtBoxesItem._rect._y, GetZPositionFromHurtBox(activeHurtBoxesItem));
                            activeHitBoxesRectangleDataListItem.transform.localScale = new Vector3((float)activeHurtBoxesItem._rect.width * -characterHitBoxDisplayListItem.controlsScript.mirror, (float)activeHurtBoxesItem._rect.height, 1);

                            switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                            {
                                case HitBoxDisplayMode.SpriteInfront:
                                    activeHitBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                                    activeHitBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerInfront;
                                    break;

                                case HitBoxDisplayMode.SpriteBehind:
                                    activeHitBoxesRectangleDataListItem.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                                    activeHitBoxesRectangleDataListItem.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerBehind;
                                    break;

                                case HitBoxDisplayMode.Mesh:
                                    activeHitBoxesRectangleDataListItem.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                                    materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor);
                                    activeHitBoxesRectangleDataListItem.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                                    break;
                            }
                        }
                    }

                    countA = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList.Count;
                    for (int a = lengthA; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.activeHitBoxesCircleDataList[a].gameObject.SetActive(false);
                    }

                    countA = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList.Count;
                    for (int a = lengthA; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList[a].gameObject.SetActive(false);
                    }
                }
                else
                {
                    countA = characterHitBoxDisplayListItem.activeHitBoxesCircleDataList.Count;
                    for (int a = 0; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.activeHitBoxesCircleDataList[a].gameObject.SetActive(false);
                    }

                    countA = characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList.Count;
                    for (int a = 0; a < countA; a++)
                    {
                        characterHitBoxDisplayListItem.activeHitBoxesRectangleDataList[a].gameObject.SetActive(false);
                    }
                }

                #endregion
            }
        }

        private void UpdateProjectileHitBoxes()
        {
            int count = 0;

            switch (hitBoxDisplayScriptableObject.hitBoxDisplayMode)
            {
                case HitBoxDisplayMode.Off:
                    count = projectileHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var item = projectileHitBoxDisplayDataList[i];

                        item.activeHitBoxCircleData.gameObject.SetActive(false);

                        item.activeHitBoxRectangleData.gameObject.SetActive(false);

                        item.blockableAreaCircleData.gameObject.SetActive(false);

                        item.blockableAreaRectangleData.gameObject.SetActive(false);
                    }
                    return;

                case HitBoxDisplayMode.SpriteInfront:
                    count = projectileHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var item = projectileHitBoxDisplayDataList[i];

                        item.activeHitBoxCircleData.spriteRenderer.enabled = true;
                        item.activeHitBoxCircleData.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                        item.activeHitBoxCircleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerInfront;
                        item.activeHitBoxCircleData.meshRenderer.enabled = false;

                        item.activeHitBoxRectangleData.spriteRenderer.enabled = true;
                        item.activeHitBoxRectangleData.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                        item.activeHitBoxRectangleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerInfront;
                        item.activeHitBoxRectangleData.meshRenderer.enabled = false;

                        item.blockableAreaCircleData.spriteRenderer.enabled = true;
                        item.blockableAreaCircleData.spriteRenderer.color = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.colliderColor;
                        item.blockableAreaCircleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.orderInLayerInfront;
                        item.blockableAreaCircleData.meshRenderer.enabled = false;

                        item.blockableAreaRectangleData.spriteRenderer.enabled = true;
                        item.blockableAreaRectangleData.spriteRenderer.color = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.colliderColor;
                        item.blockableAreaRectangleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.orderInLayerInfront;
                        item.blockableAreaRectangleData.meshRenderer.enabled = false;
                    }
                    break;

                case HitBoxDisplayMode.SpriteBehind:
                    count = projectileHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var item = projectileHitBoxDisplayDataList[i];

                        item.activeHitBoxCircleData.spriteRenderer.enabled = true;
                        item.activeHitBoxCircleData.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                        item.activeHitBoxCircleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerBehind;
                        item.activeHitBoxCircleData.meshRenderer.enabled = false;

                        item.activeHitBoxRectangleData.spriteRenderer.enabled = true;
                        item.activeHitBoxRectangleData.spriteRenderer.color = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor;
                        item.activeHitBoxRectangleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.orderInLayerBehind;
                        item.activeHitBoxRectangleData.meshRenderer.enabled = false;

                        item.blockableAreaCircleData.spriteRenderer.enabled = true;
                        item.blockableAreaCircleData.spriteRenderer.color = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.colliderColor;
                        item.blockableAreaCircleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.orderInLayerBehind;
                        item.blockableAreaCircleData.meshRenderer.enabled = false;

                        item.blockableAreaRectangleData.spriteRenderer.enabled = true;
                        item.blockableAreaRectangleData.spriteRenderer.color = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.colliderColor;
                        item.blockableAreaRectangleData.spriteRenderer.sortingOrder = hitBoxDisplayScriptableObject.blockableAreaColliderOptions.orderInLayerBehind;
                        item.blockableAreaRectangleData.meshRenderer.enabled = false;
                    }
                    break;

                case HitBoxDisplayMode.Mesh:
                    count = projectileHitBoxDisplayDataList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var item = projectileHitBoxDisplayDataList[i];

                        item.activeHitBoxCircleData.spriteRenderer.enabled = false;
                        item.activeHitBoxCircleData.meshRenderer.enabled = true;
                        item.activeHitBoxCircleData.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                        materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor);
                        item.activeHitBoxCircleData.meshRenderer.SetPropertyBlock(materialPropertyBlock);

                        item.activeHitBoxRectangleData.spriteRenderer.enabled = false;
                        item.activeHitBoxRectangleData.meshRenderer.enabled = true;
                        item.activeHitBoxRectangleData.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                        materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.activeHitBoxColliderOptions.colliderColor);
                        item.activeHitBoxRectangleData.meshRenderer.SetPropertyBlock(materialPropertyBlock);

                        item.blockableAreaCircleData.spriteRenderer.enabled = false;
                        item.blockableAreaCircleData.meshRenderer.enabled = true;
                        item.blockableAreaCircleData.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                        materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.blockableAreaColliderOptions.colliderColor);
                        item.blockableAreaCircleData.meshRenderer.SetPropertyBlock(materialPropertyBlock);

                        item.blockableAreaRectangleData.spriteRenderer.enabled = false;
                        item.blockableAreaRectangleData.meshRenderer.enabled = true;
                        item.blockableAreaRectangleData.meshRenderer.GetPropertyBlock(materialPropertyBlock);
                        materialPropertyBlock.SetColor(_ColorShaderPropertyID, hitBoxDisplayScriptableObject.blockableAreaColliderOptions.colliderColor);
                        item.blockableAreaRectangleData.meshRenderer.SetPropertyBlock(materialPropertyBlock);
                    }
                    break;
            }

            count = projectileHitBoxDisplayDataList.Count;
            for (int i = 0; i < count; i++)
            {
                var projectileHitBoxDisplayListItem = projectileHitBoxDisplayDataList[i];

                if (projectileHitBoxDisplayListItem.gameObject.activeInHierarchy == false)
                {
                    continue;
                }

                if (projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox.shape == HitBoxShape.circle)
                {
                    projectileHitBoxDisplayListItem.activeHitBoxCircleData.gameObject.SetActive(true);
                    projectileHitBoxDisplayListItem.activeHitBoxRectangleData.gameObject.SetActive(false);

                    projectileHitBoxDisplayListItem.activeHitBoxCircleData.transform.position = new Vector3(projectileHitBoxDisplayListItem.transform.position.x + (float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._offSet.x * -projectileHitBoxDisplayListItem.projectileMoveScript.mirror, projectileHitBoxDisplayListItem.transform.position.y + (float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._offSet.y, GetZPositionFromHurtBox(projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox));
                    projectileHitBoxDisplayListItem.activeHitBoxCircleData.transform.localScale = new Vector3((float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._radius * 2, (float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._radius * 2, 1);
                }
                else if (projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox.shape == HitBoxShape.rectangle)
                {
                    projectileHitBoxDisplayListItem.activeHitBoxCircleData.gameObject.SetActive(false);
                    projectileHitBoxDisplayListItem.activeHitBoxRectangleData.gameObject.SetActive(true);

                    projectileHitBoxDisplayListItem.activeHitBoxRectangleData.transform.position = new Vector3(projectileHitBoxDisplayListItem.transform.position.x + (float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._rect.x * -projectileHitBoxDisplayListItem.projectileMoveScript.mirror, projectileHitBoxDisplayListItem.transform.position.y + (float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._rect.y, GetZPositionFromHurtBox(projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox));
                    projectileHitBoxDisplayListItem.activeHitBoxRectangleData.transform.localScale = new Vector3((float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._rect.width * -projectileHitBoxDisplayListItem.projectileMoveScript.mirror, (float)projectileHitBoxDisplayListItem.projectileMoveScript.hurtBox._rect.height, 1);
                }

                if (projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea.shape == HitBoxShape.circle)
                {
                    projectileHitBoxDisplayListItem.blockableAreaCircleData.gameObject.SetActive(true);
                    projectileHitBoxDisplayListItem.blockableAreaRectangleData.gameObject.SetActive(false);

                    projectileHitBoxDisplayListItem.blockableAreaCircleData.transform.position = new Vector3(projectileHitBoxDisplayListItem.transform.position.x + (float)projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea._offSet.x * -projectileHitBoxDisplayListItem.projectileMoveScript.mirror, projectileHitBoxDisplayListItem.transform.position.y + (float)projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea._offSet.y, GetZPositionFromBlockArea(projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea));
                    projectileHitBoxDisplayListItem.blockableAreaCircleData.transform.localScale = new Vector3((float)projectileHitBoxDisplayDataList[i].projectileMoveScript.blockableArea._radius * 2, (float)projectileHitBoxDisplayDataList[i].projectileMoveScript.blockableArea._radius * 2, 1);
                }
                else if (projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea.shape == HitBoxShape.rectangle)
                {
                    projectileHitBoxDisplayListItem.blockableAreaCircleData.gameObject.SetActive(false);
                    projectileHitBoxDisplayListItem.blockableAreaRectangleData.gameObject.SetActive(true);

                    projectileHitBoxDisplayListItem.blockableAreaRectangleData.transform.position = new Vector3(projectileHitBoxDisplayListItem.transform.position.x + (float)projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea._rect.x * -projectileHitBoxDisplayListItem.projectileMoveScript.mirror, projectileHitBoxDisplayListItem.transform.position.y + (float)projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea._rect.y, GetZPositionFromBlockArea(projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea));
                    projectileHitBoxDisplayListItem.blockableAreaRectangleData.transform.localScale = new Vector3((float)projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea._rect.width * -projectileHitBoxDisplayListItem.projectileMoveScript.mirror, (float)projectileHitBoxDisplayListItem.projectileMoveScript.blockableArea._rect.height, 1);
                }
            }
        }

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
            if (hurtBox == null
                || UFE.config == null)
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
            if (blockArea == null
                || UFE.config == null)
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
