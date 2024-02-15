using System;

namespace UFE3D
{
    [System.Serializable]
    public class IgnoreGravityOptions : ICloneable
    {
        public int activeFramesBegin;
        public int activeFramesEnd;

        public object Clone()
        {
            return CloneObject.Clone(this);
        }
    }
}