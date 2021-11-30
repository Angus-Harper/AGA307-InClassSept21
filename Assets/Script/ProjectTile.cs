using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public int damage = 20;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Target"))
        {
            collision.collider.GetComponent<Renderer>().material.color = Color.red;
            Destroy(collision.collider.gameObject, 1f);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
