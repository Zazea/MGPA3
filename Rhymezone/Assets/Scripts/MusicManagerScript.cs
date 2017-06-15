using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    private AudioSource m_MyAudiSource;
    private float updateStep = 0.5f;
    private int m_sampleDataLength = 1024;
    private float m_currentUpdateTime;
    private float m_clipLoudness;
    private float[] m_clipSampleData;

    public AudioClip m_Easy;
    public AudioClip m_Medium;
    public AudioClip m_Hard;
    // Use this for initialization
    void Awake()
    {
        m_currentUpdateTime = 0.0f;
        m_clipLoudness = 0.0f;
        m_MyAudiSource = GetComponent<AudioSource>();
        m_clipSampleData = new float[m_sampleDataLength];

        if(PlayerPrefs.GetInt("Difficulty") != 0)
        {
            if(PlayerPrefs.GetInt("Difficulty") == 1)
            {
                m_MyAudiSource.clip = m_Easy;
            }
            else if(PlayerPrefs.GetInt("Difficulty") == 2)
            {
                m_MyAudiSource.clip = m_Medium;
            }
            else
            {
                m_MyAudiSource.clip = m_Hard;
            }
        }
        else
        {
            m_MyAudiSource.clip = m_Medium;
        }
    }

    void FixedUpdate()
    {
        m_currentUpdateTime += Time.deltaTime;
        if (m_currentUpdateTime >= updateStep)
        {
            if (m_MyAudiSource.clip != null)
            {
                UpdateAudioLoudness();
            }        
        }
    }

    private void UpdateAudioLoudness()
    {
        m_currentUpdateTime = 0f;
        m_MyAudiSource.clip.GetData(m_clipSampleData, m_MyAudiSource.timeSamples);
        m_clipLoudness = 0f;
        foreach (var sample in m_clipSampleData)
        {
            m_clipLoudness += Mathf.Abs(sample);
        }
        m_clipLoudness /= m_sampleDataLength;
    }

    public float ReturnClipLoudness()
    {
        return m_clipLoudness;
    }
}
