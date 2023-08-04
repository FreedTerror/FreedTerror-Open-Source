namespace UFE2FTE
{
    public static class UFE2FTEObjectPoolEventsManager
    {
        public delegate void PooledGameObjectDataHandler(UFE2FTEObjectPoolOptionsManager.PooledGameObjectData pooledGameObjectData);
        public static event PooledGameObjectDataHandler OnNewPooledGameObjectData;

        public static void CallOnNewPooledGameObjectData(UFE2FTEObjectPoolOptionsManager.PooledGameObjectData pooledGameObjectData)
        {
            if (OnNewPooledGameObjectData == null)
            {
                return;
            }

            OnNewPooledGameObjectData(pooledGameObjectData);
        }
    }
}
