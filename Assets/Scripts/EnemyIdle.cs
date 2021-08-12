using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : Enemy
{
    // Update is called once per frame
    void Update()
    {
        if ( canSeePlayer )
        {
            ChasePlayer();
        }
    }
}
