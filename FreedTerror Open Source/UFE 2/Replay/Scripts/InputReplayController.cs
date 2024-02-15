using FPLibrary;
using System.Collections.Generic;
using UFE3D;

namespace FreedTerror.UFE2
{
    [System.Serializable]
    public class InputReplayController
    {
        private enum Mode
        {
            Paused,
            Playback,
            Recording
        }
        private Mode currentMode = Mode.Paused;
        public int maxRecordingFrames = 0;
        private int currentFrame = 0;
        public List<InputReplayData> inputReplayDataList = new List<InputReplayData>();

        public void DoFixedUpdate(ControlsScript player, UFEController controller)
        {
            if (UFE.IsPaused() == true)
            {
                return;
            }

            switch (currentMode)
            {
                case Mode.Playback:
                    Playback(player, controller);
                    break;

                case Mode.Recording:
                    Recording(controller.inputs);
                    break;
            }
        }

        public void DoFixedUpdate(ControlsScript player, UFEController controller, IDictionary<InputReferences, InputEvents> inputs)
        {
            if (UFE.IsPaused() == true)
            {
                return;
            }

            switch (currentMode)
            {
                case Mode.Playback:
                    Playback(player, controller);
                    break;

                case Mode.Recording:
                    Recording(inputs);
                    break;
            }
        }

        public void StartPlayback()
        {
            currentMode = Mode.Playback;
            currentFrame = 0;
        }

        public void StopPlayBack()
        {
            currentMode = Mode.Paused;
        }

        private void Playback(ControlsScript player, UFEController controller)
        {
            if (player == null
                || controller == null)
            {
                return;
            }

            int count = inputReplayDataList.Count;
            int highestFrame = 0;
            for (int i = 0; i < count; i++)
            {
                var inputReplayData = inputReplayDataList[i];

                if (highestFrame < inputReplayData.frame)
                {
                    highestFrame = inputReplayData.frame;
                }
            }

            for (int i = 0; i < count; i++)
            {
                var inputReplayData = inputReplayDataList[i];

                if (currentFrame != inputReplayData.frame)
                {
                    continue;
                }

                UFE2Manager.PressAxis(controller, InputType.HorizontalAxis, inputReplayData.horizonalAxis);

                UFE2Manager.PressAxis(controller, InputType.VerticalAxis, inputReplayData.verticalAxis);

                if (inputReplayData.button1Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button1);
                }

                if (inputReplayData.button2Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button2);
                }

                if (inputReplayData.button3Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button3);
                }

                if (inputReplayData.button4Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button4);
                }

                if (inputReplayData.button5Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button5);
                }

                if (inputReplayData.button6Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button6);
                }

                if (inputReplayData.button7Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button7);
                }

                if (inputReplayData.button8Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button8);
                }

                if (inputReplayData.button9Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button9);
                }

                if (inputReplayData.button10Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button10);
                }

                if (inputReplayData.button11Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button11);
                }

                if (inputReplayData.button12Pressed == true)
                {
                    UFE2Manager.PressButton(controller, ButtonPress.Button12);
                }

                break;
            }

            currentFrame += 1;

            if (currentFrame > highestFrame
                || currentFrame > maxRecordingFrames)
            {
                StartPlayback();
            }
        }

        public void StartRecording()
        {
            currentMode = Mode.Recording;
            currentFrame = 0;
            inputReplayDataList.Clear();
        }

        public void StopRecording()
        {
            currentMode = Mode.Paused;
        }

        private void Recording(IDictionary<InputReferences, InputEvents> inputs)
        {
            Fix64 horizonalAxis = 0;
            Fix64 verticalAxis = 0;
            bool button1Pressed = false;
            bool button2Pressed = false;
            bool button3Pressed = false;
            bool button4Pressed = false;
            bool button5Pressed = false;
            bool button6Pressed = false;
            bool button7Pressed = false;
            bool button8Pressed = false;
            bool button9Pressed = false;
            bool button10Pressed = false;
            bool button11Pressed = false;
            bool button12Pressed = false;

            foreach (KeyValuePair<InputReferences, InputEvents> pair in inputs)
            {
                if (pair.Key.inputType == InputType.HorizontalAxis)
                {
                    horizonalAxis = pair.Value.axisRaw;
                }

                if (pair.Key.inputType == InputType.VerticalAxis)
                {
                    verticalAxis = pair.Value.axisRaw;
                }

                if (pair.Key.inputType == InputType.Button)
                {
                    switch (pair.Key.engineRelatedButton)
                    {
                        case ButtonPress.Button1:
                            button1Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button2:
                            button2Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button3:
                            button3Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button4:
                            button4Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button5:
                            button5Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button6:
                            button6Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button7:
                            button7Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button8:
                            button8Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button9:
                            button9Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button10:
                            button10Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button11:
                            button11Pressed = pair.Value.button;
                            break;

                        case ButtonPress.Button12:
                            button12Pressed = pair.Value.button;
                            break;
                    }
                }
            }

            if (horizonalAxis != (Fix64)0
                || verticalAxis != (Fix64)0
                || button1Pressed == true
                || button2Pressed == true
                || button3Pressed == true
                || button4Pressed == true
                || button5Pressed == true
                || button6Pressed == true
                || button7Pressed == true
                || button8Pressed == true
                || button9Pressed == true
                || button10Pressed == true
                || button11Pressed == true
                || button12Pressed == true)
            {
                InputReplayData newInputReplayData = new InputReplayData(
                    currentFrame,
                    horizonalAxis,
                    verticalAxis,
                    button1Pressed,
                    button2Pressed,
                    button3Pressed,
                    button4Pressed,
                    button5Pressed,
                    button6Pressed,
                    button7Pressed,
                    button8Pressed,
                    button9Pressed,
                    button10Pressed,
                    button11Pressed,
                    button12Pressed);

                inputReplayDataList.Add(newInputReplayData);
            }

            currentFrame += 1;

            if (currentFrame > maxRecordingFrames)
            {
                StopRecording();
            }
        }       
    }
}