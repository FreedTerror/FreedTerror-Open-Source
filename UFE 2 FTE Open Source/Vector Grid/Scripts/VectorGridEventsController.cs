using UFE3D;
using UnityEngine;

namespace UFE2FTE
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
                int lengthA = onBasicMoveOptionsArray[i].basicMoveArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (basicMove != onBasicMoveOptionsArray[i].basicMoveArray[a])
                    {
                        continue;
                    }

                    VectorGridManager.AddVectorGridForce(onBasicMoveOptionsArray[i].vectorGridForceScriptableObject, player);
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
                int lengthA = onMoveOptionsArray[i].moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != onMoveOptionsArray[i].moveNameArray[a])
                    {
                        continue;
                    }

                    VectorGridManager.AddVectorGridForce(onMoveOptionsArray[i].vectorGridForceScriptableObject, player);
                }
            }
        }
    }
}