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

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GameOver = true;
            other.gameObject.GetComponent<Player>().PlayerDie();
        }

        //Method 4
        gameObject.SetActive(false);
        bulletTrail.Clear();
        
    }

    public override void OnObjectReuse()
    {
        if (GameManager.shotByPlayer == 1)
        {
            bulletRb.AddForce(Player.Direction * 5000);
        } else
        {
            bulletRb.AddForce(Vector3.forward * 3000);
        }
        
    }
}
