using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : GameObjectScript
{
    public float m_shakeAmount = 2.0f;
    private float m_ShakeFill = 0.0f;

    private void FixedUpdate()
    {
        if(Input.acceleration.magnitude > 0.5f)
        {
            m_ShakeFill += Time.deltaTime;
        }

        if(m_ShakeFill >= m_shakeAmount)
        {
            PerfectHit();
        }
    }
}
