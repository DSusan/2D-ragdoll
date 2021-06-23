using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool hold;
    public KeyCode mouseButton;
    public Rigidbody2D rbHand;
    public Vector2 grabVector;
    public Vector2 punchForce;

    void Update()
    {
        if (Input.GetKey(mouseButton))
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }

        if (hold)
        {
            rbHand.AddForce(grabVector, ForceMode2D.Force);
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        StartCoroutine(grabFunc(coll));
    }

    IEnumerator grabFunc(Collision2D coll)
    {
        if (hold)
        {
            Rigidbody2D rb = coll.transform.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                if (rb.tag == "Enemy")
                {
                    Debug.Log("ENEMY HIT");
                    rb.AddForce(punchForce, ForceMode2D.Impulse);
                    hold = false;
                    yield return 0;
                }
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                fj.connectedBody = rb;
                yield return new WaitForSeconds(2);
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            }
            Destroy(GetComponent<FixedJoint2D>());
            hold = false;
        }
        yield return 0;
    }
}
