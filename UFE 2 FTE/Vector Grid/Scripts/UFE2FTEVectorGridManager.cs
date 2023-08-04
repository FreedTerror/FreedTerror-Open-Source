using System.Collections.Generic;
using UnityEngine;

namespace UFE2FTE
{
    public static class UFE2FTEVectorGridManager
    {
        private static List<VectorGrid> vectorGridList;
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

        public static void AddVectorGridToVectorGridsList(VectorGrid vectorGrid)
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

        public static void AddVectorGridToVectorGridsList(VectorGrid[] vectorGridArray)
        {
            if (vectorGridArray == null)
            {
                return;
            }

            int length = vectorGridArray.Length;
            for (int i = 0; i < length; i++)
            {
                AddVectorGridToVectorGridsList(vectorGridArray[i]);
            }
        }

        public static void RemoveVectorGridFromVectorGridsList(VectorGrid vectorGrid)
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

        public static void RemoveVectorGridFromVectorGridsList(VectorGrid[] vectorGridArray)
        {
            if (vectorGridArray == null)
            {
                return;
            }

            int length = vectorGridArray.Length;
            for (int i = 0; i < length; i++)
            {
                RemoveVectorGridFromVectorGridsList(vectorGridArray[i]);
            }
        }

        public static void AddGridForceToVectorGrid(VectorGrid vectorGrid, Transform transform, UFE2FTEVectorGridForceScriptableObject vectorGridForceScriptableObject)
        {
            if (vectorGrid == null
                || transform == null
                || vectorGridForceScriptableObject == null)
            {
                return;
            }

            vectorGrid.AddGridForce(
                new Vector3(
                    transform.position.x + vectorGridForceScriptableObject.forceOffset.x, 
                    transform.position.y + vectorGridForceScriptableObject.forceOffset.y, 
                    transform.position.z), 
                vectorGridForceScriptableObject.forceScale + vectorGridForceScriptableObject.forceOffset.z, 
                vectorGridForceScriptableObject.radius, 
                vectorGridForceScriptableObject.color, 
                vectorGridForceScriptableObject.hasColor);
        }

        public static void AddGridForceToAllVectorGrids(Transform transform, UFE2FTEVectorGridForceScriptableObject vectorGridForceScriptableObject)
        {
            if (transform == null
                || vectorGridForceScriptableObject == null)
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

                vectorGridList[i].AddGridForce(
                    new Vector3(
                        transform.position.x + vectorGridForceScriptableObject.forceOffset.x,
                        transform.position.y + vectorGridForceScriptableObject.forceOffset.y,
                        transform.position.z),
                    vectorGridForceScriptableObject.forceScale + vectorGridForceScriptableObject.forceOffset.z,
                    vectorGridForceScriptableObject.radius,
                    vectorGridForceScriptableObject.color,
                    vectorGridForceScriptableObject.hasColor);
            }
        }

        public static void AddGridForceToAllVectorGrids(Transform transform, UFE2FTEVectorGridForceScriptableObject[] vectorGridForceScriptableObjectArray)
        {
            if (transform == null
                || vectorGridForceScriptableObjectArray == null)
            {
                return;
            }

            int length = vectorGridForceScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                AddGridForceToAllVectorGrids(transform, vectorGridForceScriptableObjectArray[i]);
            }
        }
    }
}