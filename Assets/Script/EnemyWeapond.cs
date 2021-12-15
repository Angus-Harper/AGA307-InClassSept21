using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapond : GameBehaviour
{
    public int damage = 20;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _P.Hit(damage);
        }
    }
}
