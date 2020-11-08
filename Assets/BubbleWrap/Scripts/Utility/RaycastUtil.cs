using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUtil : MonoBehaviour
{
    

    public static RaycastHit2D checkRay(Vector2 direction, Animator m_Anim, float originOffset = 0,
        float raycastDistance = 1)
    {
        float directionOriginoffset = originOffset * (direction.x > 0 ? 1 : -1);

        Vector2 startPos = new Vector2(m_Anim.transform.position.x + directionOriginoffset, m_Anim.transform.position.y);
        Debug.DrawRay(startPos, direction * raycastDistance, Color.red);
        return Physics2D.Raycast(startPos, direction, raycastDistance, 1 << 9);
    }
}
