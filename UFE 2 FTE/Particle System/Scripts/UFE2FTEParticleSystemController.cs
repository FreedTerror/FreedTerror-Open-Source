using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEParticleSystemController : MonoBehaviour
    {
        private GameObject myGameObject;
        private ParticleSystem[] particleSystemArray;
        //private ParticleSystemRenderer[] particleSystemRendererArray;
        [SerializeField]
        private bool disableGameObjectIfParticleSystemsNotAlive = true;

        private void Awake()
        {
            myGameObject = gameObject;
        }

        private void Start()
        {
            particleSystemArray = GetComponentsInChildren<ParticleSystem>();
        }

        private void Update()
        {
            SetParticleSystem(particleSystemArray);

            if (disableGameObjectIfParticleSystemsNotAlive == true)
            {
                DisableGameObjectIfParticleSystemNotAlive(particleSystemArray, myGameObject);
            }
        }

        private static void SetParticleSystem(ParticleSystem particleSystem)
        {
            if (particleSystem == null)
            {
                return;
            }

            var mainModule = particleSystem.main;

            mainModule.simulationSpeed = (float)UFE.timeScale;
        }

        private static void SetParticleSystem(ParticleSystem[] particleSystemArray)
        {
            if (particleSystemArray == null)
            {
                return;
            }

            int length = particleSystemArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetParticleSystem(particleSystemArray[i]);
            }
        }

        private static void DisableGameObjectIfParticleSystemNotAlive(ParticleSystem[] particleSystemArray, GameObject gameObjectToDisable)
        {
            if (particleSystemArray == null
                || gameObjectToDisable == null)
            {
                return;
            }

            int length = particleSystemArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (particleSystemArray[i] == null)
                {
                    continue;
                }

                if (particleSystemArray[i].IsAlive() == true)
                {
                    return;
                }
            }

            gameObjectToDisable.SetActive(false);
        }
    }
}