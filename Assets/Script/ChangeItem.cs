using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItem : MonoBehaviour
{
    public GameObject LParticle;
    public Transform ParticlePoint;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projecttile"))
        {
            LParticle = Instantiate(LParticle, ParticlePoint.position, ParticlePoint.rotation);
        }
    }
}
