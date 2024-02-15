using UnityEngine;

namespace FreedTerror.UFE2
{
    public class FontController : MonoBehaviour
    {
        [SerializeField]
        private Font font;
        [SerializeField]
        private FilterMode filterMode;

        private void Start()
        {
            if (font != null)
            {
                font.material.mainTexture.filterMode = filterMode;
            }
        }
    }
}