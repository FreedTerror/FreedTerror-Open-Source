using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Move List Display Character Info", menuName = "U.F.E. 2 F.T.E./Work In Progress/Move List Display/Move List Display Character Info")]
    public class UFE2FTEMoveListDisplayCharacterInfo : ScriptableObject
    {
        public string characterName;

        [HideInInspector]
        public Vector2 scrollRectAnchoredPosition;

        [Serializable]
        public class CategoryInfoOptions
        {
            public int categoryIndex;
            public enum CategoryName
            {
                Universal,
                Movement,
                Normals,
                CommandNormals,
                Overheads,
                Throws,
                CommandThrows,
                Specials,
                Supers
            }
            public CategoryName categoryName;
        }
        [Header("CATEGORY INFO OPTIONS")]
        public CategoryInfoOptions[] categoryInfoOptions;

        [Serializable]
        public class MoveInfoOptions
        {
            public MoveInfo moveInfo;

            [Serializable]
            public class OverrideOptions
            {
                public bool useCustomButtonSequenceName;
                [TextArea(0, 100)]
                public string customButtonSequenceName;

                public bool useCustomButtonExecutionDirectionName;
                public string customButtonExecutionDirectionName;

                public bool useCustomButtonExecutionButtonName;
                [TextArea(0, 100)]
                public string customButtonExecutionButtonName;

                public bool useMoveNameOverride;
                [TextArea(0, 100)]
                public string moveName;

                public bool useMoveDescriptionOverride;
                [TextArea(0, 100)]
                public string moveDescription;
            }
            [Header("OVERRIDE OPTIONS")]
            public OverrideOptions overrideOptions;
        }
        [Header("MOVE INFO OPTIONS")]
        public MoveInfoOptions[] moveInfoOptions;
    }
}
