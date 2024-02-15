using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class VectorGridEventsController : MonoBehaviour
    {
        [System.Serializable]
        private class OnBasicMoveOptions
        {
            public VectorGridForceScriptableObject vectorGridForceScriptableObject;
            public BasicMoveReference[] basicMoveArray;
        }
        [SerializeField]
        private OnBasicMoveOptions[] onBasicMoveOptionsArray;

        [System.Serializable]
        private class OnMoveOptions
        {
            public VectorGridForceScriptableObject vectorGridForceScriptableObject;
            public string[] moveNameArray;
        }
        [SerializeField]
        private OnMoveOptions[] onMoveOptionsArray;

        private void OnEnable()
        {
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
        }

        private void OnDisable()
        {
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
        }

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            int length = onBasicMoveOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = onBasicMoveOptionsArray[i];

                int lengthA = item.basicMoveArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (basicMove != item.basicMoveArray[a])
                    {
                        continue;
                    }

                    VectorGridManager.AddVectorGridForce(item.vectorGridForceScriptableObject, player);
                }
            }
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (move == null)
            {
                return;
            }

            int length = onMoveOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = onMoveOptionsArray[i];

                int lengthA = item.moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != item.moveNameArray[a])
                    {
                        continue;
                    }

                    VectorGridManager.AddVectorGridForce(item.vectorGridForceScriptableObject, player);
                }
            }
        }
    }
}