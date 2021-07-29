using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    float velocityX;
    float velocityZ;

    int velocityXHash;
    int velocityZHash;
    public float acceleration;
    public float deceleration;

    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");

    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        changeVelocity(forwardPressed,leftPressed,rightPressed,currentMaxVelocity);
        LockOrResetVelocity(forwardPressed,leftPressed,rightPressed,runPressed,currentMaxVelocity);

        //set velocity to animation parameters
        animator.SetFloat(velocityZHash, velocityZ);
        animator.SetFloat(velocityXHash, velocityX);

    }
    //handles acceleration and deceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity) {
         //forward pressed
        if ( forwardPressed && velocityZ <currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //forward released
        if ( !forwardPressed && velocityZ > 0.0f )
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //left pressed
        if ( leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        //left released
        if ( !leftPressed && velocityX < 0.0f )
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //right pressed
        if ( rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //right released
        if ( !rightPressed && velocityX > 0.0f )
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    //handles reset and locking of velocity
    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
        //keeps velocity exactly zero
        if ( !forwardPressed && velocityZ < 0.0f )
        {
            velocityZ = 0.0f;
        }

        

        //reset velocityX
        if ( !leftPressed && !rightPressed && (velocityX > -0.05f && velocityX < 0.05f) )
        {
            velocityX = 0.0f;
        }

        //sets max running speed
        if ( forwardPressed && runPressed && velocityZ > currentMaxVelocity )
        {
            velocityZ = currentMaxVelocity;
          //decelerate to max walk speed  
        } else if( forwardPressed && velocityZ > currentMaxVelocity )
        {
            velocityZ -= Time.deltaTime * deceleration;
            if ( velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f) )
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if( forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f) )
        {
            velocityZ = currentMaxVelocity;
        }

        //locks left
        if ( leftPressed && runPressed && velocityX < -currentMaxVelocity )
        {
            velocityX = -currentMaxVelocity;
        }
        //decelerate to maxwalk velocity
        else if ( leftPressed && velocityX < -currentMaxVelocity )
        {
            velocityX += Time.deltaTime * deceleration;
            //round to currentmaxvelocity if within offset
            if ( velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity-0.05f) )
            {
                velocityX = -currentMaxVelocity;
            }
        }
        //round to the currentmaxvelocity if within offset
        else if ( leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f) )
        {
                velocityX = -currentMaxVelocity;
            
        }

        //locks right
        if ( rightPressed && runPressed && velocityX > currentMaxVelocity )
        {
            velocityX = currentMaxVelocity;
        }
        //decelerate to maxwalk velocity
        else if ( rightPressed && velocityX >currentMaxVelocity )
        {
            velocityX -= Time.deltaTime * deceleration;
            //round to currentmaxvelocity if within offset
            if ( velocityX >currentMaxVelocity && velocityX < (currentMaxVelocity+0.05f) )
            {
                velocityX = currentMaxVelocity;
            }
        }
        //round to the currentmaxvelocity if within offset
        else if ( rightPressed && velocityX <currentMaxVelocity && velocityX > (currentMaxVelocity -0.05f) )
        {
                velocityX = currentMaxVelocity;
            
        }

    }


}

    
