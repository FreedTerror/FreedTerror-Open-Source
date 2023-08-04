using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEGCFreeStringNumbersController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void Start()
        {
            UFE2FTEGCFreeStringNumbersScriptableObject.InitializeGCFreeStringNumbers(gCFreeStringNumbersScriptableObject);
        }

        [NaughtyAttributes.Button]
        private void InitializeGCFreeStringNumbers()
        {
            UFE2FTEGCFreeStringNumbersScriptableObject.InitializeGCFreeStringNumbers(gCFreeStringNumbersScriptableObject);
        }
    }
}