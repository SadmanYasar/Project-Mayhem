using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckUpDropController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer;

    public float pickupRange;
    public float dropForwardForce, dropUpForce;

    public bool equipped;
    public static bool slotfull;

    void Start() {
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotfull = true;
        }


    }

    void Update() {
        //check if inrange and right clicked
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetMouseButtonDown(1) && !slotfull )
        {
            PickUp();
        }
        if ( equipped && Input.GetKeyDown(KeyCode.Q) )
        {
            Drop();
        }
    }

    private void PickUp() {
        
        Player.animator.SetLayerWeight(1,1.0f);
        equipped = true;
        slotfull = true;

        //make weapon child of gunContainer and set position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;

  
        transform.localRotation = Quaternion.Euler(0,3.51f,0);
        transform.localScale = Vector3.one * 2;

        rb.isKinematic = true;
        coll.isTrigger = true;



    }

    private void Drop() {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        Player.animator.SetLayerWeight(1,0f);
        equipped = false;
        slotfull = false;

        //set parent to null
        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        //add force
        rb.AddForce(Player.Direction * dropForwardForce, ForceMode.Impulse);
        float random = Random.Range(-5f,5f);
        rb.AddTorque(new Vector3(random,random,random)*10);

    
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall"))
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        
    }

}
