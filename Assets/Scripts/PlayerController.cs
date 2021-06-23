using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
 
    public _Muscle[] muscles;
    public bool Right;
    public bool Left;
    public bool Jump = false;
    public bool onGround = true;

    public Rigidbody2D rbRight;
    public Rigidbody2D rbLeft;
    

    public Vector2 walkRightVector;
    public Vector2 walkLeftVector;
    public Vector2 jumpVector;

    private float MoveDelayPointer;
    public float MoveDelay;
 
    private void Update()
    {   
        foreach (_Muscle muscle in muscles)
        {
            muscle.ActivateMuscle();    
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Right = true;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Left = true;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Jump = true;
        }
        if(Input.GetButtonUp("Horizontal"))
        {
            Right = false;
            Left = false;
        }
        if(Input.GetButtonUp("Vertical"))
        {
            Jump = false;
        }
        if(Jump == true && onGround == true && Time.time > MoveDelayPointer)
        {
            Invoke("PlayerJump", 0f);
        }

        while(Right == true && Left == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Right", 0f);
            Invoke("Step2Right", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }
        while(Left == true && Right == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Left", 0f);
            Invoke("Step2Left", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }
    }

    public void Step1Right()
    {
        rbRight.AddForce(walkRightVector, ForceMode2D.Impulse);
        rbLeft.AddForce(walkRightVector * -0.5f, ForceMode2D.Impulse);
    }
    public void Step2Right()
    {
        rbLeft.AddForce(walkRightVector, ForceMode2D.Impulse);
        rbRight.AddForce(walkRightVector * -0.5f, ForceMode2D.Impulse);
    }
    public void Step1Left()
    {
        rbRight.AddForce(walkLeftVector, ForceMode2D.Impulse);
        rbLeft.AddForce(walkLeftVector * -0.5f, ForceMode2D.Impulse);
    }
    public void Step2Left()
    {
        rbLeft.AddForce(walkLeftVector, ForceMode2D.Impulse);
        rbRight.AddForce(walkLeftVector * -0.5f, ForceMode2D.Impulse);
    }
    public void PlayerJump()
    {
        onGround = false;
        rbRight.AddForce(jumpVector, ForceMode2D.Impulse);
        rbLeft.AddForce(jumpVector, ForceMode2D.Impulse);
    }
    
    public void CollisionDetected(LegColl childScript)
    {
        onGround = true;
    } 
}
    [System.Serializable]
public class _Muscle 
{ 
    public Rigidbody2D bone;
    public float restRotation;
    public float force;
 
 
    public void ActivateMuscle()
    {   
        bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime)); 
    }
}
