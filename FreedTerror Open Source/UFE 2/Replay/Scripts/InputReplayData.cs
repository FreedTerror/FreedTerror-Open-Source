using FPLibrary;

namespace FreedTerror.UFE2
{
    [System.Serializable]
    public struct InputReplayData
    {
        public int frame;
        [Fix64Range(-1f, 1f)]
        public Fix64 horizonalAxis;
        [Fix64Range(-1f, 1f)]
        public Fix64 verticalAxis;
        public bool button1Pressed;
        public bool button2Pressed;
        public bool button3Pressed;
        public bool button4Pressed;
        public bool button5Pressed;
        public bool button6Pressed;
        public bool button7Pressed;
        public bool button8Pressed;
        public bool button9Pressed;
        public bool button10Pressed;
        public bool button11Pressed;
        public bool button12Pressed;

        public InputReplayData(
            int frame,
            Fix64 horizonalAxis,
            Fix64 verticalAxis,
            bool button1Pressed,
            bool button2Pressed,
            bool button3Pressed,
            bool button4Pressed,
            bool button5Pressed,
            bool button6Pressed,
            bool button7Pressed,
            bool button8Pressed,
            bool button9Pressed,
            bool button10Pressed,
            bool button11Pressed,
            bool button12Pressed)
        {
            this.frame = frame;
            this.horizonalAxis = horizonalAxis;
            this.verticalAxis = verticalAxis;
            this.button1Pressed = button1Pressed;
            this.button2Pressed = button2Pressed;
            this.button3Pressed = button3Pressed;
            this.button4Pressed = button4Pressed;
            this.button5Pressed = button5Pressed;
            this.button6Pressed = button6Pressed;
            this.button7Pressed = button7Pressed;
            this.button8Pressed = button8Pressed;
            this.button9Pressed = button9Pressed;
            this.button10Pressed = button10Pressed;
            this.button11Pressed = button11Pressed;
            this.button12Pressed = button12Pressed;
        }
    }
}