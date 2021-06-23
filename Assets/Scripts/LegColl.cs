using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegColl : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Box")
        {
            transform.parent.GetComponent<PlayerController>().CollisionDetected(this);
        }
    }
}
