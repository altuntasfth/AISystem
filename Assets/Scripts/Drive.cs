using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public Transform fuel;

    private void Update()
    {
        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        transform.Translate(0, translation * speed * Time.deltaTime, 0);
        transform.Rotate(0, 0, -rotation * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
        }
    }

    private void CalculateDistance()
    {
        Vector3 tankPos = transform.position;
        Vector3 fuelPos = fuel.position;

        float distance = Mathf.Sqrt(Mathf.Pow(tankPos.x - fuelPos.x, 2) +
                                    Mathf.Pow(tankPos.y - fuelPos.y, 2) +
                                    Mathf.Pow(tankPos.z + fuelPos.z, 2));
        float unityDistance = Vector3.Distance(tankPos, fuelPos);
        
        Debug.Log("Calculated distance : " + distance);
        Debug.Log("Unity distance: " + unityDistance);
    }
}