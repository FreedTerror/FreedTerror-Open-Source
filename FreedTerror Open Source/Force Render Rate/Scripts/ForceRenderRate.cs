using System.Threading;
using UnityEngine;

namespace FreedTerror
{
    [DefaultExecutionOrder(10000)]
    public class ForceRenderRate : MonoBehaviour
    {
        public int frameRate = 60;
        double currentFrameTime;
        private Camera cam;

        private bool RespectVSync => !Application.isEditor && QualitySettings.vSyncCount != 0 && Mathf.Abs((float)(Screen.currentResolution.refreshRate / QualitySettings.vSyncCount) - frameRate) <= 1;

        void Awake()
        {
            // Force stable delta times
            Time.captureFramerate = frameRate;

#if UNITY_WEBGL
		Application.targetFrameRate = frameRate;
#else
            Application.targetFrameRate = -1;

            currentFrameTime = Time.realtimeSinceStartup;

            this.cam = GetComponent<Camera>();
            Camera.onPreRender += Camera_OnPreRender;
#endif
        }

        private void Camera_OnPreRender(Camera camera)
        {
            if (camera == cam)
            {
                if (QualitySettings.vSyncCount == 0)
                {
                    Application.targetFrameRate = 60;
                    currentFrameTime = Time.realtimeSinceStartup;
                }
                else
                {
                    Application.targetFrameRate = -1;
                    if (RespectVSync)
                    {
                        currentFrameTime = Time.realtimeSinceStartup;
                        // Current VSync settings should be good enough to get us near 60fps
                    }
                    else
                    {
                        currentFrameTime += 1.0 / frameRate;

                        var t = Time.realtimeSinceStartup;

                        if (t < currentFrameTime)
                        {
                            // We're ahead of schedule, so need to wait
                            float waitTime = (float)currentFrameTime - t;

                            // Sleep the thread for a while to lower CPU usage
                            float sleepTime = waitTime - 0.005f;
                            if (sleepTime > 0)
                            {
                                Thread.Sleep(Mathf.FloorToInt(sleepTime * 1000));
                            }

                            // Burn the reset of the time on the CPU
                            while (t < currentFrameTime)
                            {
                                //Thread.Sleep (0);
                                t = Time.realtimeSinceStartup;
                            }
                        }
                        else
                        {
                            // We're behind schedule for whatever reason
                            // Reset schedule so we're not playing catch up
                            Reset();
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            Camera.onPreRender -= Camera_OnPreRender;
        }

        internal void Reset()
        {
            currentFrameTime = Time.realtimeSinceStartup;
        }

#if !UNITY_WEBGL
        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                Reset();
            }
        }
#endif

    }
}