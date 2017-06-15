using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
    private string m_label = "";
    private float m_count;
    public Text m_FPSTXT;

    IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            if (Time.timeScale == 1)
            {
                yield return new WaitForSeconds(0.1f);
                m_count = (1 / Time.deltaTime);
                m_label = "FPS :" + (Mathf.Round(m_count));
            }
            else
            {
                m_label = "Pause";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnGUI()
    {
        m_FPSTXT.text = m_label;
    }
}