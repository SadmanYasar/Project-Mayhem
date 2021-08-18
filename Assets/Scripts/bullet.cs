using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : PoolObject
{
    //[SerializeField] ParticleSystem spark;
    [SerializeField] Rigidbody bulletRb;
    [SerializeField] TrailRenderer bulletTrail;

    private void OnCollisionEnter(Collision other) {
        if ( other.gameObject.CompareTag("Enemy") )
        {
            other.gameObject.GetComponent<Enemy>().Die();
        }
        //spark.Play();
        //Method 3
        //QFSW.MOP2.MasterObjectPooler.Instance.Release(gameObject, "bulletPool");

        //Method 4
        gameObject.SetActive(false);
        bulletTrail.Clear();
        
    }

    public override void OnObjectReuse()
    {
        bulletRb.AddForce(Player.Direction * 5000);
    }
}
