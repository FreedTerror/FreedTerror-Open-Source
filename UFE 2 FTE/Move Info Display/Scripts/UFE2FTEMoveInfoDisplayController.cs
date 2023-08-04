using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEMoveInfoDisplayController : MonoBehaviour
    {
        [SerializeField]
        private GameObject moveInfoDisplayGameObject;

        private void Awake()
        {
            UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay = false;
            
            SetGameObjectActive(moveInfoDisplayGameObject, UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay);
        }

        private void Update()
        {
            SetGameObjectActive(moveInfoDisplayGameObject, UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay);
        }

        private void OnDestroy()
        {
            UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay = false;
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }

        [NaughtyAttributes.Button]
        private void DisableMoveInfoDisplay()
        {
            UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay = false;
        }

        [NaughtyAttributes.Button]
        private void EnableMoveInfoDisplay()
        {
            UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay = true;
        }
    }
}