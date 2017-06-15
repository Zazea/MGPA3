using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSeriesHitScript : MonoBehaviour {

    public float m_Killtime = 1;
    private void FixedUpdate()
    {
        m_Killtime -= Time.deltaTime;
        if (m_Killtime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

}
