using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;

    private Rigidbody rb;
    
    private float mass = 10f;
    private float force = 200f;
    private float acceleration;
    private float gravity = -9.8f; 
    private float speedZ;
    private float speedY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        /*acceleration = force / mass;
        speedZ += acceleration * Time.deltaTime;
        
        speedY += gravity * Time.deltaTime;
        
        transform.Translate(0f, speedY, speedZ);

        force = 0; */

        transform.forward = rb.velocity;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.2f);
            Destroy(this.gameObject);
        }
    }
}
