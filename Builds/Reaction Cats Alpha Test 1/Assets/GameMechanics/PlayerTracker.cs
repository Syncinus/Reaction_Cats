using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {
    /* Working Thing, Testing Other Thing:  
    public float speed = 2f;
    public static Transform player;

    public void LateUpdate()
    {
      if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.z = -10;
            transform.position = Vector3.Slerp(transform.position, newPosition, speed * Time.deltaTime);
        }
    }
    */

    public float m_DampTime = 10f;
    public static Transform m_Target;
    public float m_XOffset = 0;
    public float m_YOffset = 0;

    private float margin = 0.1f;

    void Start()
    {
       // if (m_Target == null)
       // {
         //   m_Target = GameObject.FindGameObjectWithTag("Player").transform;
       // }
    }

    public void LateUpdate()
    {
        if (m_Target != null)
        {
            float targetX = m_Target.position.x + m_XOffset;
            float targetY = m_Target.position.y + m_YOffset;

            if (Mathf.Abs(transform.position.x - targetX) > margin)
                targetX = Mathf.Lerp(transform.position.x, targetX, m_DampTime * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - targetY) > margin)
                targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}
