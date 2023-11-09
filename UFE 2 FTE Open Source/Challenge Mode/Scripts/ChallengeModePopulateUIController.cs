using UnityEngine;

namespace UFE2FTE
{
    public class ChallengeModePopulateUIController : MonoBehaviour
    {
        [SerializeField]
        private ChallengeModeStartChallengeUIController gameObjectToSpawn;
        [SerializeField]
        private Transform spawnParent;
        private enum LoadingMethod
        {
            Path,
            //Player1CharacterName
        }
        [SerializeField]
        private LoadingMethod loadingMethod;
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

            GameObject gameObjectToSpawnGameObject = gameObjectToSpawn.gameObject;
            if (gameObjectToSpawnGameObject.activeInHierarchy == true)
            {
                gameObjectToSpawnGameObject.SetActive(false);
            }

            switch(loadingMethod)
            {
                case LoadingMethod.Path:
                    ChallengeModeController.Instance.LoadChallenges(challengeModeScriptableObjectPath);
                    break;
            }

            int count = ChallengeModeController.Instance.currentChallengesList.Count;
            for (int i = 0; i < count; i++)
            {
                ChallengeModeStartChallengeUIController spawnedGameObject = Instantiate(gameObjectToSpawn, spawnParent);
                spawnedGameObject.gameObject.SetActive(true);
                spawnedGameObject.challengeOptions = ChallengeModeController.Instance.currentChallengesList[i];
            }
        }
    }
}