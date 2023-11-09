using System.Collections.Generic;
using UnityEngine;
using FPLibrary;

namespace UFE2FTE
{
    public class Fix64AttributeExample : MonoBehaviour
    {
        public Fix64 fix64;
        public Fix64[] fix64Array;    
        public List<Fix64> fix64List;


        public FPVector2 fix64Vector2;
        public FPVector2[] fix64Vector2Array;
        public List<FPVector2> fix64Vector2List;

        public FPVector fix64Vector3;
        public FPVector[] fix64Vector3Array;
        public List<FPVector> fix64Vector3List;

        public FPRect fix64Rect;
        public FPRect[] fix64RectArray;
        public List<FPRect> fix64RectList;

        [Fix64Range(-100f, 100f)]
        public Fix64 fix64RangeFloat;
        [Fix64Range(-100f, 100f)]
        public Fix64[] fix64RangeFloatArray;
        [Fix64Range(-100f, 100f)]
        public List<Fix64> fix64RangeFloatList;

        [Fix64Range(-100, 100)]
        public Fix64 fix64RangeInt;
        [Fix64Range(-100, 100)]
        public Fix64[] fix64RangeIntArray;
        [Fix64Range(-100, 100)]
        public List<Fix64> fix64RangeIntList;
    }
}