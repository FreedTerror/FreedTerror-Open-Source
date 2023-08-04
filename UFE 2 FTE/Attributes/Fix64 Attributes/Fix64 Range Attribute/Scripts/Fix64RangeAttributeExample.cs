using System.Collections.Generic;
using UnityEngine;
using FPLibrary;

public class Fix64RangeAttributeExample : MonoBehaviour
{
    [Fix64Range(-100, 100)]
    public Fix64 fix64Range;
    [Fix64Range(-100, 100)]
    public Fix64[] fix64RangeArray;
    [Fix64Range(-100, 100)]
    public List<Fix64> fix64RangeList;

    [System.Serializable]
    public class NestedClass
    {
        [Fix64Range(-100, 100)]
        public Fix64 fix64Range;
        [Fix64Range(-100, 100)]
        public Fix64[] fix64RangeArray;
        [Fix64Range(-100, 100)]
        public List<Fix64> fix64RangeList;
    }
    public NestedClass nestedClass;

    [System.Serializable]
    public struct NestedStruct
    {
        [Fix64Range(-100, 100)]
        public Fix64 fix64Range;
        [Fix64Range(-100, 100)]
        public Fix64[] fix64RangeArray;
        [Fix64Range(-100, 100)]
        public List<Fix64> fix64RangeList;
    }
    public NestedStruct nestedStruct;
}
