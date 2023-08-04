namespace UFE2FTE
{
    public static class UFE2FTETransformShakeEventsManager
    {
        public delegate void TransformShakeScriptableObjectHandler(UFE2FTETransformShakeScriptableObject transformShakeScriptableObject, UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray, ControlsScript player);
        public static event TransformShakeScriptableObjectHandler OnTransformShake;

        public static void CallOnTransformShake(UFE2FTETransformShakeScriptableObject transformShakeScriptableObject, UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray, ControlsScript player)
        {
            if (OnTransformShake == null)
            {
                return;
            }

            OnTransformShake(transformShakeScriptableObject, transformShakeScriptableObjectArray, player);
        }
    }
}
