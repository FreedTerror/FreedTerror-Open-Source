using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class MoveListInputButtonUIController : MonoBehaviour
    {
        [SerializeField]
        private Text textToSpawn;
        [SerializeField]
        private Transform spawnParent;
        [SerializeField]
        private bool useDefaultInputs = true;
        [SerializeField]
        private bool useAlternativeInputs;

        private void Awake()
        {
            MoveListPopulateUIController.OnPopulateEvent += OnPopulateEvent;
        }

        private void OnDestroy()
        {
            MoveListPopulateUIController.OnPopulateEvent -= OnPopulateEvent;
        }

        private void OnPopulateEvent(MoveInfo moveInfo)
        {
            if (useDefaultInputs == true)
            {
                PopulateWithText(moveInfo.defaultInputs, UFE2FTE.GetControlsScript(UFE2FTE.Instance.pausedPlayer));
            }

            if (useAlternativeInputs == true)
            {
                PopulateWithText(moveInfo.altInputs, UFE2FTE.GetControlsScript(UFE2FTE.Instance.pausedPlayer));
            }

            MoveListPopulateUIController.OnPopulateEvent -= OnPopulateEvent;
        }

        private void PopulateWithText(MoveInputs moveInputs, ControlsScript player)
        {
            if (textToSpawn == null
                || spawnParent == null
                || moveInputs == null
                || player == null)
            {
                return;
            }

            GameObject textToSpawnGameObject = textToSpawn.gameObject;
            if (textToSpawnGameObject.activeInHierarchy == true)
            {
                textToSpawnGameObject.SetActive(false);
            }

            int length = moveInputs.buttonSequence.Length;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    Text spawnedText = Instantiate(textToSpawn, spawnParent);
                    spawnedText.gameObject.SetActive(true);
                    UFE2FTE.SetInputDisplayRotation(spawnedText.transform, moveInputs.buttonSequence[i], UFE2FTE.GetControlsScript(UFE2FTE.Instance.pausedPlayer));
                    spawnedText.text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(moveInputs.buttonSequence[i]);                
                }
            }

            length = moveInputs.buttonExecution.Length;
            if ((moveInputs.onPressExecution == true
                || moveInputs.onReleaseExecution == true)
                && length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    Text spawnedText = Instantiate(textToSpawn, spawnParent);
                    spawnedText.gameObject.SetActive(true);
                    UFE2FTE.SetInputDisplayRotation(spawnedText.transform, moveInputs.buttonExecution[i], UFE2FTE.GetControlsScript(UFE2FTE.Instance.pausedPlayer));
                    spawnedText.text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(moveInputs.buttonExecution[i]);                   
                }
            }
        }
    }
}