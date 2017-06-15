using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCircleHitScript : GameObjectScript {

    public bool m_IsPerfectHit = false;
    public bool m_IsHit = false;

    private void FixedUpdate()
    {
        if(m_ObjectTouched == true)
        {
            if(m_IsPerfectHit == true)
            {
                PerfectHit();
            }
            else if(m_IsHit == true)
            {
                HitSucc();
            }
            else
            {
                FailHit();
            }
        }
    }

 
}
