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
    }
}
