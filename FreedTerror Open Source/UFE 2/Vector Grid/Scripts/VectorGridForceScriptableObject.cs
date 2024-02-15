using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Vector Grid/Vector Grid Force", fileName = "New Vector Grid Force")]
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
            VectorGridManager.AddVectorGridForce(this, UFE.p1ControlsScript);
        }

        [NaughtyAttributes.Button]
        private void AddVectorGridForcePlayer2()
        {
            VectorGridManager.AddVectorGridForce(this, UFE.p2ControlsScript);
        }

        [NaughtyAttributes.Button]
        private void ResetAllVectorGrids()
        {
            VectorGridManager.ResetAllVectorGrids();
        }
    }
}
