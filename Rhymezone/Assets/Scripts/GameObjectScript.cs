using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectScript : MonoBehaviour
{
    public float m_PrefabDelayTime = 5.0f;
    public int m_PrefabScoreValue = 100;
    public bool m_ObjectTouched = false;
    public bool m_ObjectHeld = false;
    public bool m_PrefabComplete = false;

    public GameObject m_ScoreObject;
    public GameObject m_MissObject;


    private float m_TouchReset = 0.5f;

    GameManagerScript m_GameManager;
    
    public void ObjectHit()
    {
        m_ObjectTouched = true;
        m_ObjectHeld = true;
        m_TouchReset = 0.5f;
    }

    private void FixedUpdate()
    {
        m_TouchReset -= Time.deltaTime;
        if (m_TouchReset <= 0.0f)
        {
             m_ObjectTouched = false;
             m_ObjectHeld = false;
        }
    }

    private void Awake()
    {
        m_GameManager = GameObject.Find("_GameMangerObject").GetComponent<GameManagerScript>();
    }

    public void FailHit()
    {
        
        if (m_PrefabComplete == false)
        {
            m_GameManager.MissHit();
            Instantiate(m_MissObject,transform.position,transform.rotation);
            CleanUpPrefab();
        }
    }

    public void HitSucc()
    {
        if (m_PrefabComplete == false)
        {
            m_GameManager.GoodHit();
            m_ScoreObject.SetActive(true);
            m_GameManager.addScore(m_PrefabScoreValue);
        }
        m_PrefabComplete = true;
    }

    public void PerfectHit()
    {
        if (m_PrefabComplete == false)
        {
            m_GameManager.PerfectHit();
            m_ScoreObject.SetActive(true);
            m_GameManager.addScore(m_PrefabScoreValue);
        }
        m_PrefabComplete = true;
    }

    public void CleanUpPrefab()
    {
        Destroy(gameObject);
    }
}
