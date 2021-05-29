using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public Transform shellSpawnPos;
    public GameObject target;
    public GameObject parent;
    
    private float speed = 15;
    private float turnSpeed = 2;
    private bool canShoot = true;

    private void Update()
    {
        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        
        parent.transform.rotation =
            Quaternion.Slerp(parent.transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        float? angle = RotateTurret();

        if (angle != null && Vector3.Angle(direction, parent.transform.forward) < 10)
        {
            Fire();
        }
    }

    private float? RotateTurret()
    {
        float? angle = CalculateAngle(false);

        if (angle != null)
        {
            transform.localEulerAngles = new Vector3(360f - (float) angle, 0f, 0f);
        }

        return angle;
    }

    private float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float speedSqrt = speed * speed;
        float underTheSqrtRoot = (speedSqrt * speedSqrt) - gravity * (gravity * x * x + 2 * y * speedSqrt);

        if (underTheSqrtRoot >= 0)
        {
            float root = Mathf.Sqrt(underTheSqrtRoot);
            float highAngle = speedSqrt + root;
            float lowAngle = speedSqrt - root;

            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }
    }

    private void Fire()
    {
        if (canShoot)
        {
            GameObject shell = Instantiate(shellPrefab, shellSpawnPos.position, shellSpawnPos.rotation);
            shell.GetComponent<Rigidbody>().velocity = speed * transform.forward;
            canShoot = false;
            Invoke("CanShootAgain", 0.5f);
        }
    }

    private void CanShootAgain()
    {
        canShoot = true;
    }
}
