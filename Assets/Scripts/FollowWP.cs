using UnityEngine;

public class FollowWP : MonoBehaviour 
{
    public GameObject[] waypoints;
    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    public float lookAhead = 10.0f;

    private int currentWP = 0; 
    private GameObject tracker;

    private void Start() 
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        //tracker.GetComponent<MeshRenderer>().enabled = false;

        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }

    private void ProcessTracker() 
    {
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead) 
            return;

        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3.0f)
            currentWP++;

        if (currentWP >= waypoints.Length)
            currentWP = 0;

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0.0f, 0.0f, (speed + 20.0f) * Time.deltaTime);
    }

    private void Update() 
    {
        ProcessTracker();

        Quaternion lookAtWP = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }
}