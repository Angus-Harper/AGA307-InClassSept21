using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 1000f;
    public Transform firingPoint;

    [Header("Raycast Stuff")]
    public float maxDistance = 100f;
    public LayerMask layerMask;
    public GameObject RParticle;
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            GameObject projectileInstance;
            projectileInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * projectileSpeed);
            Destroy(projectileInstance, 3f);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Ray ray = new Ray(transform.position, transform.forward); // start at player and end where the players is looking
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
            {
                GameObject AParticle; 
                AParticle = Instantiate(RParticle, hitInfo.collider.gameObject.transform);
                hitInfo.collider.GetComponent<Renderer>().material.color = Color.red;
                Destroy(hitInfo.collider.gameObject, 3f);
            }
        }
    }
}
