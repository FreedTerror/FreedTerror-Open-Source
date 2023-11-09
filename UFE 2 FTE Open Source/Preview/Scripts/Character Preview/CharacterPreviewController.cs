using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterPreviewController : MonoBehaviour
    {
        // TODO add support for 3D characters.

        private Camera myCamera;

        [SerializeField]
        private CharacterInfoReferencesScriptableObject characterInfoReferencesScriptableObject;
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text characterNameText;
        private UFE3D.CharacterInfo characterInfo;
        [HideInInspector]
        public GameObject characterGameObject;
        private Transform characterTransform;
        [SerializeField]
        private RectTransform characterRectTransform;
        [SerializeField]
        private Vector3 characterPositionOffset;
        [SerializeField]
        private Vector3 characterRotation;
        [SerializeField]
        private Vector3 characterScale = Vector3.one;
        private Animator characterAnimator;
        [SerializeField]
        private bool playCharacterUnselectedAnimation = true;
        [SerializeField]
        private bool playCharacterSelectedAnimation = true;
        private bool playCharacterSelectedAnimationOnce;
        [SerializeField]
        private bool playCharacterSelectedIdleAnimation;

        private void Start()
        {
            myCamera = Camera.main;
        }

        private void Update()
        {
            if (characterInfoReferencesScriptableObject == null)
            {
                return;
            }

            UFE3D.CharacterInfo playerSpecificCharacterInfo = UFE2FTE.GetCharacterInfo(player);
            UFE3D.CharacterInfo foundCharacterInfo = null;       
            if (characterNameText != null)
            {
                if (playerSpecificCharacterInfo != null)
                {
                    characterNameText.text = playerSpecificCharacterInfo.characterName;
                }

                foundCharacterInfo = UFE2FTE.GetCharacterInfo(characterNameText.text);
            }

            if (foundCharacterInfo != characterInfo)
            {
                Destroy(characterGameObject);
                characterGameObject = null;
            }

            characterInfo = foundCharacterInfo;
            if (characterInfo == null)
            {
                Destroy(characterGameObject);
                characterGameObject = null;
                return;
            }

            if (characterGameObject == null)
            {
                if (characterInfo.characterPrefabStorage == StorageMode.Prefab)
                {
                    characterGameObject = Instantiate(characterInfo.characterPrefab);
                }
                else
                {
                    characterGameObject = Instantiate(Resources.Load<GameObject>(characterInfo.prefabResourcePath));
                }

                if (characterGameObject != null)
                {
                    characterTransform = characterGameObject.transform;
                    characterAnimator = characterGameObject.GetComponent<Animator>();
                    if (characterAnimator != null)
                    {
                        characterAnimator.runtimeAnimatorController = characterInfoReferencesScriptableObject.GetCharacterSelectAnimatorController(characterInfo);
                    }
                }
            }

            if (characterGameObject != null)
            {
                characterTransform.position = UFE2FTE.GetWorldPositionOfCanvasElement(characterRectTransform, myCamera) + characterPositionOffset;
                characterTransform.eulerAngles = characterRotation;
                characterTransform.localScale = characterScale;
            }
         
            if (playerSpecificCharacterInfo == null)
            {
                if (characterAnimator != null)
                {
                    if (playCharacterUnselectedAnimation == true
                        && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(characterInfoReferencesScriptableObject.GetCharacterUnselectedAnimationName(characterInfo)) == false)
                    {
                        characterAnimator.Play(characterInfoReferencesScriptableObject.GetCharacterUnselectedAnimationName(characterInfo));
                    }
                }

                playCharacterSelectedAnimationOnce = false;
            }
            else
            {
                if (characterAnimator != null)
                {
                    if (playCharacterSelectedAnimation == true
                        && playCharacterSelectedAnimationOnce == false
                        && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(characterInfoReferencesScriptableObject.GetCharacterSelectedAnimationName(characterInfo)) == false)
                    {
                        characterAnimator.Play(characterInfoReferencesScriptableObject.GetCharacterSelectedAnimationName(characterInfo));
                    }
                }

                playCharacterSelectedAnimationOnce = true;
            }

            if (characterAnimator != null)
            {
                if (playCharacterSelectedIdleAnimation == true
                    && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(characterInfoReferencesScriptableObject.GetCharacterSelectedIdleAnimationName(characterInfo)) == false)
                {
                    characterAnimator.Play(characterInfoReferencesScriptableObject.GetCharacterSelectedIdleAnimationName(characterInfo));
                }
            }
        }

        private void OnDisable()
        {
            Destroy(characterGameObject);
        }
    }
}