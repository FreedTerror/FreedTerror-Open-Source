using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Vector Grid Force", menuName = "U.F.E. 2 F.T.E./Vector Grid/Vector Grid Force")]
    public class VectorGridForceScriptableObject : ScriptableObject
    {
        public float forceYOffset;
        public float forceScale;
        public float radius;
        public bool hasColor;
        public Color color;

        public Color thickLineColor;
        public Color thinLineColor;

        [NaughtyAttributes.Button]
        private void ChangeVectorGridColor()
        {
            VectorGridManager.ChangeVectorGridColor(this);
        }

        [NaughtyAttributes.Button]
        private void AddVectorGridForce()
        {
            VectorGridManager.AddVectorGridForce(this, new Vector3(0,0,0));
        }

        [NaughtyAttributes.Button]
        private void AddVectorGridForcePlayer1()
        {
            VectorGridManager.AddVectorGridForce(this, UFE.GetPlayer1ControlsScript());
        }

        [NaughtyAttributes.Button]
        private void AddVectorGridForcePlayer2()
        {
            VectorGridManager.AddVectorGridForce(this, UFE.GetPlayer2ControlsScript());
        }

        [NaughtyAttributes.Button]
        private void ResetAllVectorGrids()
        {
            VectorGridManager.ResetAllVectorGrids();
        }
    }
}
