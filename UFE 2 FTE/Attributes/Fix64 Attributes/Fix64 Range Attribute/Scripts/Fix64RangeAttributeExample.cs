using System.Collections.Generic;
using UnityEngine;
using FPLibrary;

public class Fix64RangeAttributeExample : MonoBehaviour
{
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

    [System.Serializable]
    public class NestedClass
    {
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
    public NestedClass nestedClass;

    [System.Serializable]
    public struct NestedStruct
    {
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
    public NestedStruct nestedStruct;
}
