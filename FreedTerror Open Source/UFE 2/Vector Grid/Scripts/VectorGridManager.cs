using System.Collections.Generic;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public static class VectorGridManager
    {
        private static List<VectorGrid> vectorGridList = null;
        private static List<VectorGrid> GetVectorGridList()
        {
            if (vectorGridList == null)
            {
                vectorGridList = new List<VectorGrid>();
            }

            int count = vectorGridList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (vectorGridList[i] != null)
                {
                    continue;
                }

                vectorGridList.RemoveAt(i);
            }

            return vectorGridList;
        }

        public static void AddVectorGrid(VectorGrid vectorGrid)
        {
            if (vectorGrid == null)
            {
                return;
            }

            int count = GetVectorGridList().Count;
            for (int i = 0; i < count; i++)
            {
                if (vectorGrid != vectorGridList[i])
                {
                    continue;
                }

                return;
            }

            GetVectorGridList().Add(vectorGrid);
        }

        public static void AddVectorGrid(VectorGrid[] vectorGrid)
        {
            if (vectorGrid == null)
            {
                return;
            }

            int length = vectorGrid.Length;
            for (int i = 0; i < length; i++)
            {
                AddVectorGrid(vectorGrid[i]);
            }
        }

        public static void RemoveVectorGrid(VectorGrid vectorGrid)
        {
            if (vectorGrid == null)
            {
                return;         
            }

            int count = GetVectorGridList().Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (vectorGrid != vectorGridList[i])
                {
                    continue;
                }

                vectorGridList.RemoveAt(i);
            }
        }

        public static void RemoveVectorGrid(VectorGrid[] vectorGrid)
        {
            if (vectorGrid == null)
            {
                return;
            }

            int length = vectorGrid.Length;
            for (int i = 0; i < length; i++)
            {
                RemoveVectorGrid(vectorGrid[i]);
            }
        }

        public static void AddVectorGridForce(VectorGridForceScriptableObject vectorGridForceScriptableObject, Vector3 position)
        {
            if (vectorGridForceScriptableObject == null)
            {
                return;
            }

            int count = GetVectorGridList().Count;
            for (int i = 0; i < count; i++)
            {
                var item = vectorGridList[i];
                if (item == null)
                {
                    continue;
                }

                item.AddGridForce(
                    new Vector3(
                        position.x,
                        position.y + vectorGridForceScriptableObject.forceYOffset,
                        position.z),
                    vectorGridForceScriptableObject.forceScale,
                    vectorGridForceScriptableObject.radius,
                    vectorGridForceScriptableObject.color,
                    vectorGridForceScriptableObject.hasColor);
            }
        }

        public static void AddVectorGridForce(VectorGridForceScriptableObject[] vectorGridForceScriptableObject, Vector3 position)
        {
            if (vectorGridForceScriptableObject == null)
            {
                return;
            }

            int length = vectorGridForceScriptableObject.Length;
            for (int i = 0; i < length; i++)
            {
                AddVectorGridForce(vectorGridForceScriptableObject[i], position);
            }
        }

        public static void AddVectorGridForce(VectorGridForceScriptableObject vectorGridForceScriptableObject, ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            AddVectorGridForce(vectorGridForceScriptableObject, player.transform.position);
        }

        public static void ChangeVectorGridColor(VectorGridForceScriptableObject vectorGridForceScriptableObject)
        {
            if (vectorGridForceScriptableObject == null)
            {
                return;
            }

            int count = GetVectorGridList().Count;
            for (int i = 0; i < count; i++)
            {
                if (vectorGridList[i] == null)
                {
                    continue;
                }

                for (int x = 0; x < vectorGridList[i].m_GridWidth; x++)
                {
                    for (int y = 0; y < vectorGridList[i].m_GridHeight; y++)
                    {
                        if (vectorGridList[i].GridPoints[x, y] != null)
                        {
                            vectorGridList[i].GridPoints[x, y].m_Color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);

                            //vectorGridList[i].GridPoints[x, y].UpdatePositionAndColor(m_ColorRevertEnabled, m_ColorRevertDelay, m_ColorRevertSpeed);
                        }
                    }
                }

                vectorGridList[i].m_ThinLineSpawnColor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
            }
        }

        public static void ResetAllVectorGrids()
        {
            int count = GetVectorGridList().Count;
            for (int i = 0; i < count; i++)
            {
                var item = vectorGridList[i];
                if (item == null)
                {
                    continue;
                }

                item.InitGrid();
            }
        }

        public static void PauseAllVectorGrids()
        {
            int count = GetVectorGridList().Count;
            for (int i = 0; i < count; i++)
            {
                var item = vectorGridList[i];
                if (item == null)
                {
                    continue;
                }

                item.enabled = false;
            }
        }

        public static void UnpauseAllVectorGrids()
        {
            int count = GetVectorGridList().Count;
            for (int i = 0; i < count; i++)
            {
                var item = vectorGridList[i];
                if (item == null)
                {
                    continue;
                }

                item.enabled = true;
            }
        }
    }
}