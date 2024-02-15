using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Replay/Input Replay Controller", fileName = "New Input Replay Controller")]
    public class InputReplayControllerScriptableObject : ScriptableObject
    {
        [SerializeField]
        private int maxRecordingFrames;
        [Range(1, 10)]
        [SerializeField]
        private int inputReplayControllerSlots = 1;
        public InputReplayController[] inputReplayControllerArray;
        private int currentInputReplayControllerArrayIndex = 0;

        private void OnEnable()
        {
            inputReplayControllerArray = new InputReplayController[inputReplayControllerSlots];
            for (int i = 0; i < inputReplayControllerSlots; i++)
            {
                InputReplayController newInputReplayController = new InputReplayController();
                newInputReplayController.maxRecordingFrames = maxRecordingFrames;

                inputReplayControllerArray[i] = newInputReplayController;
            }
        }

        public void Clear()
        {
            int length = inputReplayControllerArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = inputReplayControllerArray[i];

                if (item == null)
                {
                    continue;
                }

                item.inputReplayDataList.Clear();
            }
        }

        public InputReplayController GetCurrentInputReplayController()
        {
            return inputReplayControllerArray[currentInputReplayControllerArrayIndex];
        }
    }
}