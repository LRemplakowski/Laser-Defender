using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyBehaviour enemy = collider.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            EnemyFormationScript.movingRight = !EnemyFormationScript.movingRight;
        }
    }
}
