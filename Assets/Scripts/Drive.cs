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

        if (Input.GetKeyDown(KeyCode.X))
        {
            CalculateAngle();
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

    private void CalculateAngle()
    {
        Vector3 tankForward = transform.up;
        Vector3 directionToFuel = fuel.transform.position - transform.position;
        
        Debug.DrawRay(transform.position, tankForward * 30, Color.green, 3f);
        Debug.DrawRay(transform.position, directionToFuel, Color.red, 3f);

        float dot = tankForward.x * directionToFuel.x + tankForward.y * directionToFuel.y;
        float angle = Mathf.Acos(dot / (tankForward.magnitude * directionToFuel.magnitude)) * Mathf.Rad2Deg;

        float unityAngle = Vector3.Angle(tankForward, directionToFuel);
        
        Debug.Log("Calculated angle : " + angle);
        Debug.Log("Unity angle: " + unityAngle);

        int clockwise = 1;
        if (Cross(tankForward, directionToFuel).z < 0)
            clockwise = -1;

        float unitySignedAngle = Vector3.SignedAngle(tankForward, directionToFuel, transform.forward);
        
        // manuel return face
        //transform.Rotate(0f, 0f, angle * clockwise);
        
        // unity auto return face
        transform.Rotate(0f, 0f, unitySignedAngle);
    }

    private Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMultiplier = v.y * w.z - v.z * w.y;
        float yMultiplier = v.z * w.x - v.x * w.z;
        float zMultiplier = v.x * w.y - v.y * w.x;

        Vector3 crossProduct = new Vector3(xMultiplier, yMultiplier, zMultiplier);

        return crossProduct;
    }
}