using System.Collections.Generic;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public static class PaletteSwapSpriteSwapManager
    {
        private class Data
        {
            public string name;
            public PaletteSwapSpriteController paletteSwapSpriteController;
            public Texture2D swapTexture;
        }
        private static List<Data> dataList = new List<Data>();

        public static void AddSwapTexture(Texture2D swapTexture, string name, PaletteSwapSpriteController paletteSwapSpriteController)
        {
            if (swapTexture == null
                || paletteSwapSpriteController == null
                || IsSwapTextureInPool(swapTexture) == true)
            {
                return;
            }

            Data newData = new Data();
            newData.name = name;
            newData.paletteSwapSpriteController = paletteSwapSpriteController;
            newData.swapTexture = swapTexture;        
            dataList.Add(newData);
        }

        public static Texture2D GetSwapTexture(string name, PaletteSwapSpriteController paletteSwapSpriteController)
        {
            if (paletteSwapSpriteController == null)
            {
                return null;
            }

            int count = dataList.Count;
            for (int i = 0; i < count; i++)
            {
                if (dataList[i].swapTexture != null
                    && name == dataList[i].name
                    && dataList[i].paletteSwapSpriteController == null)
                {
                    dataList[i].paletteSwapSpriteController = paletteSwapSpriteController;
                    return dataList[i].swapTexture;
                }
            }

            return null;
        }

        public static void Clear()
        {
            int count = dataList.Count;
            for (int i = 0; i < count; i++)
            {
                Object.Destroy(dataList[i].swapTexture);
            }

            dataList.Clear();
        }

        public static bool IsSwapTextureInPool(Texture2D swapTexture)
        {
            if (swapTexture == null)
            {
                return false;
            }

            int count = dataList.Count;
            for (int i = 0; i < count; i++)
            {
                if (swapTexture != dataList[i].swapTexture)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
    }
}