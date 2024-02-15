using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
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
                PopulateWithText(moveInfo.defaultInputs, UFE2Manager.GetControlsScript(UFE2Manager.instance.pausedPlayer));
            }

            if (useAlternativeInputs == true)
            {
                PopulateWithText(moveInfo.altInputs, UFE2Manager.GetControlsScript(UFE2Manager.instance.pausedPlayer));
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
                    UFE2Manager.SetInputDisplayRotation(spawnedText.transform, moveInputs.buttonSequence[i], UFE2Manager.GetControlsScript(UFE2Manager.instance.pausedPlayer));
                    spawnedText.text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(moveInputs.buttonSequence[i]);                
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
                    UFE2Manager.SetInputDisplayRotation(spawnedText.transform, moveInputs.buttonExecution[i], UFE2Manager.GetControlsScript(UFE2Manager.instance.pausedPlayer));
                    spawnedText.text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(moveInputs.buttonExecution[i]);                   
                }
            }
        }
    }
}