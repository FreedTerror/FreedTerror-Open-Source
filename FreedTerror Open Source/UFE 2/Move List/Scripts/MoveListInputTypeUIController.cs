using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class MoveListInputTypeUIController : MonoBehaviour
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
                PopulateWithText(moveInfo.defaultInputs);
            }

            if (useAlternativeInputs == true)
            {
                PopulateWithText(moveInfo.altInputs);
            }

            MoveListPopulateUIController.OnPopulateEvent -= OnPopulateEvent;
        }

        private void PopulateWithText(MoveInputs moveInputs)
        {
            if (textToSpawn == null
                || spawnParent == null
                || moveInputs == null)
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
                    spawnedText.text = "Sequence";
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
                    spawnedText.text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromMoveInputs(moveInputs);
                }
            }
        }
    }
}