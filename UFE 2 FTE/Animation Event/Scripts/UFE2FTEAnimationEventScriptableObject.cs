using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Animation Event", menuName = "U.F.E. 2 F.T.E./Animation Event/Animation Event")]
    public class UFE2FTEAnimationEventScriptableObject : ScriptableObject
    {
        [SerializeField]
        private UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;

        [SerializeField]
        private UFE2FTEObjectPoolOptionsManager.ObjectPoolScriptableObjectOptions[] objectPoolScriptableObjectOptionsArray;

        [SerializeField]
        private UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;

        [SerializeField]
        private UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray;

        public void AnimationEventScriptableObject(Transform transform, ControlsScript player)
        {
            UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(audioClipGroupScriptableObjectArray);

            UFE2FTEObjectPoolOptionsManager.SpawnPooledGameObject(objectPoolScriptableObjectOptionsArray, transform, player, null, null);

            UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, transformShakeScriptableObjectArray, player);

            UFE2FTEVectorGridManager.AddGridForceToAllVectorGrids(transform, vectorGridForceScriptableObjectArray);
        }
    }
}