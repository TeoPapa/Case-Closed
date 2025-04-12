namespace UnityEngine.AdaptivePerformance
{
    internal class AutoPerformanceModeController
    {
        string m_FeatureName = "Auto Performance Mode Control";

        public AutoPerformanceModeController(IPerformanceModeStatus perfModeStat)
        {
            perfModeStat.PerformanceModeEvent += (PerformanceMode mode) => OnPerformanceModeChange(mode);
            AdaptivePerformanceAnalytics.RegisterFeature(m_FeatureName, true);
        }

        private void OnPerformanceModeChange(PerformanceMode performanceMode)
        {
            switch (performanceMode)
            {
                case PerformanceMode.Battery:
                    Application.targetFrameRate = 30;
                    break;
                case PerformanceMode.Optimize:
#if UNITY_2022_2_OR_NEWER
                    Application.targetFrameRate = (int)(Screen.currentResolution.refreshRateRatio.value);
#else
                    Application.targetFrameRate = Screen.currentResolution.refreshRate;
#endif
                    break;
                default:
                    Application.targetFrameRate = -1;
                    break;
            }
            APLog.Debug($"[AutoPerformanceModeController] Performance Mode: {performanceMode}, fps: {Application.targetFrameRate}");
        }
    }
}
