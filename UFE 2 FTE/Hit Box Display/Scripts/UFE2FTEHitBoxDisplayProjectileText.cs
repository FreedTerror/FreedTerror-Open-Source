using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEHitBoxDisplayProjectileText : MonoBehaviour
    {
        private Transform myTransform;
        private ProjectileMoveScript myProjectileMoveScript;

        [SerializeField]
        UFE2FTEHitBoxDisplayConfigurationScriptableObject hitBoxDisplayConfigurationScriptableObject;
        [SerializeField]
        private Canvas totalHitsCanvas;
        [SerializeField]
        private RectTransform totalHitsTextRectTransform;
        [SerializeField]
        private Text totalHitsText;
        [SerializeField]
        private bool disableTextOnZeroTotalHits = true;
        [SerializeField]
        private bool useCustomOrderInLayer;
        [SerializeField]
        private int customOrderInLayer;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void Awake()
        {
            myTransform = transform;
        }

        private void Start()
        {
            myProjectileMoveScript = GetComponent<ProjectileMoveScript>();
        }

        private void Update()
        {
            SetHitBoxDisplayProjectileText(); 
        }

        private void SetHitBoxDisplayProjectileText()
        {
            if (UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText == false
                || myProjectileMoveScript == null)
            {
                if (totalHitsText != null)
                {
                    totalHitsText.enabled = false;
                }

                return;
            }

            if (totalHitsCanvas != null)
            {
                if (hitBoxDisplayConfigurationScriptableObject != null)
                {
                    totalHitsCanvas.sortingOrder = hitBoxDisplayConfigurationScriptableObject.projectileHitBoxOptions.totalHitsTextOptions.orderInLayer;
                }

                if (useCustomOrderInLayer == true)
                {
                    totalHitsCanvas.sortingOrder = customOrderInLayer;
                }
            }

            if (totalHitsTextRectTransform != null)
            {
                if (myProjectileMoveScript.data.mirrorOn2PSide == true)
                {
                    totalHitsTextRectTransform.localEulerAngles = new Vector3(0, myTransform.localEulerAngles.y, -myTransform.localEulerAngles.z * -myProjectileMoveScript.mirror);
                }
                else
                {
                    totalHitsTextRectTransform.localEulerAngles = new Vector3(0, myTransform.localEulerAngles.y, -myTransform.localEulerAngles.z);
                }
            }

            if (totalHitsText != null)
            {
                totalHitsText.enabled = UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText;

                if (disableTextOnZeroTotalHits == true)
                {
                    if (myProjectileMoveScript.totalHits <= 0)
                    {
                        totalHitsText.enabled = false;
                    }
                }

                totalHitsText.text = UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, myProjectileMoveScript.totalHits);
            }
        }
    }
}
