using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
     public Transform target;
     public float smoothTime = 0.3f;
 
     private Vector3 velocity = Vector3.zero;

     void FixedUpdate() {
         Vector3 goalPos = target.position;
         goalPos.y = transform.position.y;
         transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);

          
     }


}
      
