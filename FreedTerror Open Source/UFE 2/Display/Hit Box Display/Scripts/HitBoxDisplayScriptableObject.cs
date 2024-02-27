using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Display/Hit Box Display", fileName = "New Hit Box Display")]
    public class HitBoxDisplayScriptableObject : ScriptableObject
    {
        public HitBoxDisplayController.HitBoxDisplayMode hitBoxDisplayMode;
        [Range(0, 255)]
        public int hitBoxDisplayAlphaValue;

        [SerializeField]
        private string circlePrefabPath;
        public GameObject GetCirclePrefab()
        {
            return Resources.Load<GameObject>(circlePrefabPath);
        }

        [SerializeField]
        private string rectanglePrefabPath;
        public GameObject GetRectanglePrefab()
        {
            return Resources.Load<GameObject>(rectanglePrefabPath);
        }

        [SerializeField]
        private string circleSpritePath;
        public Sprite GetCircleSprite()
        {
            return Resources.Load<Sprite>(circleSpritePath);
        }

        [SerializeField]
        private string rectangleSpritePath;
        public Sprite GetRectangleSprite()
        {
            return Resources.Load<Sprite>(rectangleSpritePath);
        }

        [System.Serializable]
        public class ColliderOptions
        {
            public Color32 colliderColor;
            public int orderInLayerInfront;
            public int orderInLayerBehind;

            public ColliderOptions(
                Color32 colliderColor,
                int orderInLayerInfront,
                int orderInLayerBehind)
            {
                this.colliderColor = colliderColor;
                this.orderInLayerInfront = orderInLayerInfront;
                this.orderInLayerBehind = orderInLayerBehind;
            }
        }
        public ColliderOptions bodyColliderOptions = new ColliderOptions(new Color32(255, 255, 0, 255), 10090, -10030);
        public ColliderOptions hitColliderOptions = new ColliderOptions(new Color32(255, 0, 0, 255), 10070, -10050);
        public ColliderOptions noColliderOptions = new ColliderOptions(new Color32(0, 0, 255, 255), 10040, -10080);
        public ColliderOptions throwColliderOptions = new ColliderOptions(new Color32(0, 128, 0, 255), 10080, -10040);
        public ColliderOptions physicalInvincibleColliderOptions = new ColliderOptions(new Color32(255, 128, 255, 255), 10060, -10060);
        public ColliderOptions projectileInvincibleColliderOptions = new ColliderOptions(new Color32(128, 0, 128, 255), 10050, -10070);
        public ColliderOptions activeHitBoxColliderOptions = new ColliderOptions(new Color32(255, 0, 0, 255), 10100, -10000);
        public ColliderOptions blockableAreaColliderOptions = new ColliderOptions(new Color32(255, 255, 255, 255), 10030, -10090);

        public void UpdateColliderColorAlphaValues(byte alphaValue)
        {
            bodyColliderOptions.colliderColor.a = alphaValue;
            hitColliderOptions.colliderColor.a = alphaValue;
            throwColliderOptions.colliderColor.a = alphaValue;
            physicalInvincibleColliderOptions.colliderColor.a = alphaValue;
            projectileInvincibleColliderOptions.colliderColor.a = alphaValue;
            noColliderOptions.colliderColor.a = alphaValue;
            activeHitBoxColliderOptions.colliderColor.a = alphaValue;
            blockableAreaColliderOptions.colliderColor.a = alphaValue;
        }
    }
}