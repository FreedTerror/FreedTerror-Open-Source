using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Move List Display Info", menuName = "U.F.E. 2 F.T.E./Work In Progress/Move List Display/Move List Display Info")]
    public class UFE2FTEMoveListDisplayInfo : ScriptableObject
    {
        [SerializeField]
        private string universalName = "UNIVERSAL";
        [SerializeField]
        private string movementName = "MOVEMENT";
        [SerializeField]
        private string normalsName = "NORMALS";
        [SerializeField]
        private string commandNormalsName = "COMMAND NORMALS";
        [SerializeField]
        private string overheadsName = "OVERHEADS";
        [SerializeField]
        private string throwsName = "THROWS";
        [SerializeField]
        private string commandThrowsName = "COMMAND THROWS";
        [SerializeField]
        private string specialsName = "SPECIALS";
        [SerializeField]
        private string supersName = "SUPERS";

        public string GetCategoryInfoCategoryName(UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName categoryName)
        {
            switch (categoryName)
            {
                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Universal:
                    return universalName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Movement:
                    return movementName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Normals:
                    return normalsName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.CommandNormals:
                    return commandNormalsName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Overheads:
                    return overheadsName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Throws:
                    return throwsName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.CommandThrows:
                    return commandThrowsName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Specials:
                    return specialsName;

                case UFE2FTEMoveListDisplayCharacterInfo.CategoryInfoOptions.CategoryName.Supers:
                    return supersName;
            }

            return null;
        }

        public UFE2FTEMoveListDisplayUI moveListDisplayCharacterInfoPrefab;
        public UFE2FTEMoveListDisplayUI moveListDisplayCategoryInfoPrefab;
        public UFE2FTEMoveListDisplayUI moveListDisplayMoveInfoPrefab;

        public UFE2FTEMoveListDisplayCharacterInfo[] moveListDisplayCharacterInfos;
    }
}