using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : Enemy
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if ( canSeePlayer && !isDead)
        {
            ChasePlayer();
        }
    }
}
