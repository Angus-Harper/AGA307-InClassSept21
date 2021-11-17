using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;
    Vector3 startScale;
    private void Start()
    {
        startScale = sphere.transform.localScale;
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sphere.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sphere.transform.localScale = Vector3.one * 0.01f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sphere.GetComponent<Renderer>().material.color = Color.yellow;
            sphere.transform.localScale = startScale;
        }
    }
}
