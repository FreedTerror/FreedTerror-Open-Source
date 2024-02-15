using UnityEngine;

namespace FreedTerror
{
    public class URLPopulateUIController : MonoBehaviour
    {
        [SerializeField]
        private URLDataScriptableObjectManager urlDataScriptableObjectManager;
        [SerializeField]
        private URLUIController gameObjectToSpawn;
        [SerializeField]
        private Transform spawnParent;

        private void Start()
        {
            Populate();
        }

        private void Populate()
        {
            if (urlDataScriptableObjectManager == null
                || gameObjectToSpawn == null
                || spawnParent == null)
            {
                return;
            }

            gameObjectToSpawn.gameObject.SetActive(false);

            int length = urlDataScriptableObjectManager.urlDataScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = urlDataScriptableObjectManager.urlDataScriptableObjectArray[i];

                if (item == null)
                {
                    continue;
                }

                var newGameObject = Instantiate(gameObjectToSpawn, spawnParent);
                newGameObject.urlDataScriptableObject = item;
                newGameObject.gameObject.name = newGameObject.urlDataScriptableObject.urlName;
                newGameObject.gameObject.SetActive(true);
            }
        }
    }
}
