using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowMouse : MonoBehaviour
{
     [SerializeField]private GameObject ObjectToMove;

    // Update is called once per frame
    void Update()
    {
       Vector3 MousePosition = Input.mousePosition;
       MousePosition.z = Camera.main.nearClipPlane;
       ObjectToMove.transform.position = Camera.main.ScreenToWorldPoint(MousePosition);
      
       
    }
}
