using UnityEngine;

namespace UFE2FTE
{
    public class UFE_2ChallengeModeStageSetter : MonoBehaviour
    {
        [SerializeField]
        private int stageNumber;

        // Start is called before the first frame update
        void Start()
        {
            UFE.SetStage(UFE.config.stages[stageNumber]);
        }
    }
}
