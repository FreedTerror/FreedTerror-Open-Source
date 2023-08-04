using System.Collections.Generic;
using UnityEngine;
using FPLibrary;

public class Fix64InspectorAttributeExample : MonoBehaviour
{
    [Fix64Inspector]
    public Fix64 fix64;
    [Fix64Inspector]
    public Fix64[] fix64Array;
    [Fix64Inspector]
    public List<Fix64> fix64List;

    [Fix64Inspector]
    public FPVector2 fPVector2;
    [Fix64Inspector]
    public FPVector2[] fPVector2Array;
    [Fix64Inspector]
    public List<FPVector2> fPVector2List;

    [Fix64Inspector]
    public FPVector fPVector;
    [Fix64Inspector]
    public FPVector[] fPVectorArray;
    [Fix64Inspector]
    public List<FPVector> fPVectorList;

    [Fix64Inspector]
    public FPRect fPRect;
    [Fix64Inspector]
    public FPRect[] fPRectArray;
    [Fix64Inspector]
    public List<FPRect> fPRectList;

    [System.Serializable]
    public class NestedClass
    {
        [Fix64Inspector]
        public Fix64 fix64;
        [Fix64Inspector]
        public Fix64[] fix64Array;
        [Fix64Inspector]
        public List<Fix64> fix64List;

        [Fix64Inspector]
        public FPVector2 fPVector2;
        [Fix64Inspector]
        public FPVector2[] fPVector2Array;
        [Fix64Inspector]
        public List<FPVector2> fPVector2List;

        [Fix64Inspector]
        public FPVector fPVector;
        [Fix64Inspector]
        public FPVector[] fPVectorArray;
        [Fix64Inspector]
        public List<FPVector> fPVectorList;

        [Fix64Inspector]
        public FPRect fPRect;
        [Fix64Inspector]
        public FPRect[] fPRectArray;
        [Fix64Inspector]
        public FPRect[] fPRectList;
    }
    public NestedClass nestedClass;

    [System.Serializable]
    public struct NestedStruct
    {
        [Fix64Inspector]
        public Fix64 fix64;
        [Fix64Inspector]
        public Fix64[] fix64Array;
        [Fix64Inspector]
        public List<Fix64> fix64List;

        [Fix64Inspector]
        public FPVector2 fPVector2;
        [Fix64Inspector]
        public FPVector2[] fPVector2Array;
        [Fix64Inspector]
        public List<FPVector2> fPVector2List;

        [Fix64Inspector]
        public FPVector fPVector;
        [Fix64Inspector]
        public FPVector[] fPVectorArray;
        [Fix64Inspector]
        public List<FPVector> fPVectorList;

        [Fix64Inspector]
        public FPRect fPRect;
        [Fix64Inspector]
        public FPRect[] fPRectArray;
        [Fix64Inspector]
        public FPRect[] fPRectList;
    }
    public NestedStruct nestedStruct;
}
