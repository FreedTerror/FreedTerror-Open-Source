using UnityEngine;

namespace UFE2FTE
{
    public class ParticleSystemHelperController : MonoBehaviour
    {
        // Attach this script to the root GameObject to affect every ParticleSystem on the GameObject.
        // Attach this script to a child GameObject to affect every ParticleSystem on the child GameObject.

        private GameObject myGameObject;
        private ParticleSystem[] particleSystemArray;
        //private ParticleSystemRenderer[] particleSystemRendererArray;
        [SerializeField]
        private bool disableGameObjectIfParticleSystemsNotAlive;

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

        private static void SetParticleSystem(ParticleSystem[] particleSystem)
        {
            if (particleSystem == null)
            {
                return;
            }

            int length = particleSystem.Length;
            for (int i = 0; i < length; i++)
            {
                SetParticleSystem(particleSystem[i]);
            }
        }

        private static void DisableGameObjectIfParticleSystemNotAlive(ParticleSystem[] particleSystem, GameObject gameObjectToDisable)
        {
            if (particleSystem == null
                || gameObjectToDisable == null)
            {
                return;
            }

            int length = particleSystem.Length;
            for (int i = 0; i < length; i++)
            {
                if (particleSystem[i] == null)
                {
                    continue;
                }

                if (particleSystem[i].IsAlive() == true)
                {
                    return;
                }
            }

            gameObjectToDisable.SetActive(false);
        }
    }
}