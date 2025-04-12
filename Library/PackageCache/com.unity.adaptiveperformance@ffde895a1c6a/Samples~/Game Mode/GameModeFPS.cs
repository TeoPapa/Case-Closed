using System.Collections;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class GameModeFPS : MonoBehaviour
{
    public SampleFactory objectFactory;

    IAdaptivePerformance m_AP;

    void Start()
    {
        m_AP = Holder.Instance;
        if (m_AP == null)
        {
            Debug.Log("[Performance Mode Control] Warning Adaptive Performance Manager was not found and does not report");
            return;
        }

        // to ensure that we can reach max possible fps
        objectFactory.LimitCount = 64;

        StartCoroutine(TestTimeout());
    }

    IEnumerator TestTimeout()
    {
        while (true)
        {
            yield return new WaitForSeconds(300);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
