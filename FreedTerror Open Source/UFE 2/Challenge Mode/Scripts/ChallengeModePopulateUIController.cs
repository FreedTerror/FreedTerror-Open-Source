using UnityEngine;

namespace FreedTerror.UFE2
{
    public class ChallengeModePopulateUIController : MonoBehaviour
    {
        [SerializeField]
        private ChallengeModeStartChallengeUIController gameObjectToSpawn;
        [SerializeField]
        private Transform spawnParent;
        //public UFE3D.CharacterInfo characterInfo;
        [SerializeField]
        private string challengeModeScriptableObjectPath = "Tutorial Challenges";

        public void Start()
        {
            Populate();      
        }

        private void Populate()
        {
            if (gameObjectToSpawn == null
                || spawnParent == null)
            {
                return;
            }

            gameObjectToSpawn.gameObject.SetActive(false);

            ChallengeModeController.instance.LoadChallenges(challengeModeScriptableObjectPath);
 
            int count = ChallengeModeController.instance.currentChallengeOptionsList.Count;
            for (int i = 0; i < count; i++)
            {
                var newGameObject = Instantiate(gameObjectToSpawn, spawnParent);            
                newGameObject.challengeOptions = ChallengeModeController.instance.currentChallengeOptionsList[i];
                //newGameObject.gameObject.name = 
                newGameObject.gameObject.SetActive(true);
            }
        }
    }
}