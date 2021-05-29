using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    private void Update()
    {
        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        transform.Translate(0, 0, translation * speed * Time.deltaTime);
        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);
    }
}
