using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyController : MonoBehaviour
{
 
    public Enemy_Muscle[] muscles;
    public bool Right;
    public bool Left;
    public bool Jump = false;
    public bool onGround = true;

    public Rigidbody2D rbRight;
    public Rigidbody2D rbLeft;
    public GameObject player;

    public Vector2 walkRightVector;
    public Vector2 walkLeftVector;
    public Vector2 jumpVector;

    private float MoveDelayPointer;
    public float MoveDelay;
 
    private void Start()
    {
        GetWallkDirection();
    }
    private void Update()
    {   
        foreach (Enemy_Muscle muscle in muscles)
        {
            muscle.ActivateMuscle();    
        }
           // Jump = true;

        if(Jump == true && onGround == true && Time.time > MoveDelayPointer)
        {
            Invoke("PlayerJump", 0f);
        }

        while(Right == true && Left == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Right", 0f);
            Invoke("Step2Right", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
            if (EnemyCanChangeDirection())
            {
                GetWallkDirection();
            }
        }
        while(Left == true && Right == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Left", 0f);
            Invoke("Step2Left", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
            if (EnemyCanChangeDirection())
            {
                GetWallkDirection();
            }
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
    
    public void CollisionDetected(EnemyLegColl childScript)
    {
        onGround = true;
    }

    public bool EnemyCanChangeDirection()
    {
        float playerPosY = player.transform.GetChild(9).gameObject.transform.position.y;
        float enemyPosY = transform.GetChild(9).gameObject.transform.position.y;

        if (Mathf.Abs(playerPosY - enemyPosY) > 3)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void GetWallkDirection()
    {
        float playerPosX = player.transform.GetChild(9).gameObject.transform.position.x;
        float enemyPosX = transform.GetChild(9).gameObject.transform.position.x;
        if(enemyPosX < playerPosX)
        {
            Left = false;
            Right = true;
        }
        if(enemyPosX > playerPosX)
        {
            Right = false;
            Left = true;
        }
    } 
}
    [System.Serializable]
public class Enemy_Muscle 
{ 
    public Rigidbody2D bone;
    public float restRotation;
    public float force;
 
 
    public void ActivateMuscle()
    {   
        bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime)); 
    }
}
