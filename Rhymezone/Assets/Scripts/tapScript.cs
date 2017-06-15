using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapScript : GameObjectScript
{
    private int m_TapCount = 0;
    public int m_MaxTaps = 20;

    private void FixedUpdate()
    {
        if(m_ObjectTouched == true)
        {
            m_TapCount += 1;
            m_ObjectTouched = false;
        }

        if (m_TapCount >= m_MaxTaps)
        {
            PerfectHit();
        }
    }
}
