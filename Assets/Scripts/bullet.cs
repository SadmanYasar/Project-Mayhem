using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : PoolObject
{
    [SerializeField] Rigidbody bulletRb;
    [SerializeField] TrailRenderer bulletTrail;

    
        

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.name)
        {
            case "Enemy": 
                other.gameObject.GetComponent<Enemy>().Die();
                break;

            case "Player":
                GameManager.GameOver = true;
                other.gameObject.GetComponent<Player>().PlayerDie();
                break;    

        }
        /* if ( other.gameObject.CompareTag("Enemy") )
        {
            other.gameObject.GetComponent<Enemy>().Die();
            GameManager.enemyCount--;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GameOver = true;
            other.gameObject.GetComponent<Player>().PlayerDie();
        } */

        //Method 4
        gameObject.SetActive(false);
        bulletTrail.Clear();
        
    }

    public override void OnObjectReuse()
    {
        switch (GameManager.shotByPlayer)
        {
            
            case 1:
                bulletRb.AddForce(Player.Direction * 4000);
                break;

            case 0:
                Vector3 playerPos = Player.playerInstance.gameObject.transform.position;
                bulletRb.AddForce((playerPos - transform.position).normalized * 3000);
                break;    
        }


        /* if (GameManager.shotByPlayer == 1)
        {
            bulletRb.AddForce(Player.Direction * 5000);
        } else
        {
            Vector3 playerPos = Player.playerInstance.gameObject.transform.position;
            bulletRb.AddForce((playerPos - transform.position).normalized * 3000);
        } */
        
    }
}
