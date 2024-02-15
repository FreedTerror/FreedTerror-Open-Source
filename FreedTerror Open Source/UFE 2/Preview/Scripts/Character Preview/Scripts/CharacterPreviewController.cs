using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterPreviewController : MonoBehaviour
    {
        // TODO add support for 3D characters.

        private Camera myCamera;

        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text characterNameText;
        private UFE3D.CharacterInfo currentCharacterInfo;
        private UFE3D.CharacterInfo previousCharacterInfo;
        [HideInInspector]
        public GameObject characterGameObject;
        private Transform characterTransform;
        [SerializeField]
        private RectTransform characterRectTransform;
        [SerializeField]
        private Vector3 characterRotation;
        private Animator characterAnimator;
        [SerializeField]
        private bool playCharacterUnselectedAnimation = true;
        [SerializeField]
        private bool playCharacterSelectedAnimation = true;
        private bool playCharacterSelectedAnimationOnce;
        [SerializeField]
        private bool playCharacterSelectedIdleAnimation;

        private void OnEnable()
        {
            previousCharacterInfo = null;
            Destroy(characterGameObject);
            characterGameObject = null;
        }

        private void Start()
        {
            myCamera = Camera.main;
        }

        private void Update()
        {
            if (UFE2Manager.instance.characterInfoReferencesScriptableObject == null
                || characterNameText == null)
            {
                return;
            }

            currentCharacterInfo = UFE2Manager.GetCharacterInfo(characterNameText.text);

            if (previousCharacterInfo != currentCharacterInfo)
            {
                previousCharacterInfo = currentCharacterInfo;

                Destroy(characterGameObject);

                if (currentCharacterInfo.characterPrefabStorage == StorageMode.Prefab)
                {
                    characterGameObject = Instantiate(previousCharacterInfo.characterPrefab);
                }
                else
                {
                    characterGameObject = Instantiate(Resources.Load<GameObject>(currentCharacterInfo.prefabResourcePath));
                }

                if (characterGameObject != null)
                {
                    characterTransform = characterGameObject.transform;
                    characterAnimator = characterGameObject.GetComponent<Animator>();
                    if (characterAnimator != null)
                    {
                        characterAnimator.runtimeAnimatorController = UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterSelectAnimatorController(currentCharacterInfo);
                    }
                }
            }

            if (characterGameObject != null)
            {
                characterTransform.position = Utility.GetWorldPositionOfCanvasElement(characterRectTransform, myCamera);
                characterTransform.eulerAngles = characterRotation;
            }

            if (UFE2Manager.GetCharacterInfo(player) == null)
            {
                if (characterAnimator != null)
                {
                    if (playCharacterUnselectedAnimation == true
                        && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterUnselectedAnimationName(currentCharacterInfo)) == false)
                    {
                        characterAnimator.Play(UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterUnselectedAnimationName(currentCharacterInfo));
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
                        && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterSelectedAnimationName(currentCharacterInfo)) == false)
                    {
                        characterAnimator.Play(UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterSelectedAnimationName(currentCharacterInfo));
                    }
                }

                playCharacterSelectedAnimationOnce = true;
            }

            if (characterAnimator != null)
            {
                if (playCharacterSelectedIdleAnimation == true
                    && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterSelectedIdleAnimationName(currentCharacterInfo)) == false)
                {
                    characterAnimator.Play(UFE2Manager.instance.characterInfoReferencesScriptableObject.GetCharacterSelectedIdleAnimationName(currentCharacterInfo));
                }
            }
        }

        private void OnDisable()
        {
            Destroy(characterGameObject);
        }
    }
}