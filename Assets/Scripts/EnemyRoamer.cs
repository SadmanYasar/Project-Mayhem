using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamer : Enemy
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSeePlayer ) {
            ChasePlayer();
        } else Patrolling();
    }
}
