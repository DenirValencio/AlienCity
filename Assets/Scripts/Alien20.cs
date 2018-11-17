using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien20 : MonoBehaviour
{
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }             
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
