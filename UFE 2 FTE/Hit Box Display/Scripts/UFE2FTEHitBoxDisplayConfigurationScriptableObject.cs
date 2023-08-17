using System;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Hit Box Display Configuration", menuName = "U.F.E. 2 F.T.E./Hit Box Display/Hit Box Display Configuration")]
    public class UFE2FTEHitBoxDisplayConfigurationScriptableObject : ScriptableObject
    {
        [Serializable]
        public class PrefabOptions
        {
            public GameObject hitBoxDisplayCirclePrefab;
            public GameObject hitBoxDisplayRectanglePrefab;
        }
        public PrefabOptions prefabOptions;

        [Serializable]
        public class PoolOptions
        {
            public int hitBoxesPoolSize = 20;
            public int activeHurtBoxesPoolSize = 20;
        }
        public PoolOptions poolOptions;

        [Serializable]
        public class SpriteOptions
        {
            public Sprite circleFillSprite;
            public Sprite rectangleFillSprite;
        }
        public SpriteOptions spriteOptions;

        [Serializable]
        public class PopcronGizmosOptions
        {
            [Range(0, 100)]
            public int pointsCount = 16;
        }
        public PopcronGizmosOptions popcronGizmosOptions;

        [Serializable]
        public class InitialOptions
        {
            [Range(0, 255)]
            public byte initialAlphaValue = 128;
        }
        public InitialOptions initialOptions;

        [Serializable]
        public class CharacterHitBoxOptions
        {
            [Serializable]
            public class BodyColliderOptions
            {
                public Color32 colliderColor = new Color32(255, 255, 0, 255);
                public int orderInLayerInfront = 10090;
                public int orderInLayerBehind = -10030;
                public bool disableCollider;
            }
            public BodyColliderOptions bodyColliderOptions;

            [Serializable]
            public class HitColliderOptions
            {
                public Color32 colliderColor = new Color32(255, 0, 0, 255);
                public int orderInLayerInfront = 10070;
                public int orderInLayerBehind = -10050;
                public bool disableCollider;
            }
            public HitColliderOptions hitColliderOptions;

            [Serializable]
            public class ThrowColliderOptions
            {
                public Color32 colliderColor = new Color32(0, 128, 0, 255);
                public int orderInLayerInfront = 10080;
                public int orderInLayerBehind = -10040;
                public bool disableCollider;
            }
            public ThrowColliderOptions throwColliderOptions;

            [Serializable]
            public class PhysicalInvincibleColliderOptions
            {
                public Color32 colliderColor = new Color32(255, 128, 255, 255);
                public int orderInLayerInfront = 10060;
                public int orderInLayerBehind = -10060;
                public bool disableCollider;
            }
            public PhysicalInvincibleColliderOptions physicalInvincibleColliderOptions;

            [Serializable]
            public class ProjectileInvincibleColliderOptions
            {
                public Color32 colliderColor = new Color32(128, 0, 128, 255);
                public int orderInLayerInfront = 10050;
                public int orderInLayerBehind = -10070;
                public bool disableCollider;
            }
            public ProjectileInvincibleColliderOptions projectileInvincibleColliderOptions;

            [Serializable]
            public class NoColliderOptions
            {
                public Color32 colliderColor = new Color32(0, 0, 255, 255);
                public int orderInLayerInfront = 10040;
                public int orderInLayerBehind = -10080;
                public bool disableCollider;
            }
            public NoColliderOptions noColliderOptions;

            [Serializable]
            public class ActiveHurtBoxOptions
            {
                public Color32 colliderColor = new Color32(255, 0, 0, 255);
                public int orderInLayerInfront = 10100;
                public int orderInLayerBehind = -10000;
                public bool disableCollider;
            }
            public ActiveHurtBoxOptions activeHurtBoxOptions;

            [Serializable]
            public class BlockableAreaOptions
            {
                public Color32 colliderColor = new Color32(255, 255, 255, 255);
                public int orderInLayerInfront = 10030;
                public int orderInLayerBehind = -10090;
                public bool disableCollider;
                public bool disableCharacterCollidersEqualToBlockableAreaCollider = true;
                public bool disableActiveHurtBoxCollidersEqualToBlockableAreaCollider;
            }
            public BlockableAreaOptions blockableAreaOptions;
        }
        public CharacterHitBoxOptions characterHitBoxOptions;

        [Serializable]
        public class ProjectileHitBoxOptions
        {
            [Serializable]
            public class HitAreaOptions
            {
                public Color32 colliderColor = new Color32(255, 0, 0, 255);
                public int orderInLayerInfront = 10100;
                public int orderInLayerBehind = -10000;
                public bool disableCollider;
            }
            public HitAreaOptions hitAreaOptions;

            [Serializable]
            public class ProjectileOnlyColliderOptions
            {
                public Color32 colliderColor = new Color32(255, 128, 0, 255);
                public int orderInLayerInfront = 10099;
                public int orderInLayerBehind = -10001;
                public bool disableCollider;
            }
            public ProjectileOnlyColliderOptions projectileOnlyColliderOptions;

            [Serializable]
            public class BlockableAreaOptions
            {
                public Color32 colliderColor = new Color32(255, 255, 255, 255);
                public int orderInLayerInfront = 10030;
                public int orderInLayerBehind = -10090;
                public bool disableCollider;
            }
            public BlockableAreaOptions blockableAreaOptions;

            [Serializable]
            public class TotalHitsTextOptions
            {
                public int orderInLayer = 10110;
            }
            public TotalHitsTextOptions totalHitsTextOptions;

        }
        public ProjectileHitBoxOptions projectileHitBoxOptions;
    }
}
