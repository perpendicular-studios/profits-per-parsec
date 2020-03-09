using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public delegate void LandRocket(GameObject rocket);
    public static event LandRocket OnRocketLand;

    public string targetString, startPositionString;

    [SerializeField] public Transform target;
    [SerializeField] public Transform startPosition;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float rotationalSpeed = 10;               //for collision make sure rotationalSpeed is 10x of movementSpeed
    [SerializeField] float detectionDistance = 10;             //raycast detection distance
    [SerializeField] float rayCastOffset = 0.25f;                 //raycast distance from center of rocket
    [SerializeField] float bufferDuration = 30;                //sets rotateBuffer
    [SerializeField] float emergencyDir = 10;                  //angle of turn when object is detected (20 seems to be good)
    float rotateBuffer = 0;                               //how long emergency direction turning will last for when object is detected

    // Start is called at beginning
    void Start()
    {
        if(targetString != null)
        {
            target = GameObject.FindGameObjectWithTag(targetString).GetComponentInChildren<OrbitMotion>().orbitingObject.transform;
        }

        if(startPositionString != null)
        {
            startPosition = GameObject.FindGameObjectWithTag(startPositionString).GetComponent<OrbitMotion>().orbitingObject.transform;
        }

        transform.position = startPosition.position;
        movementSpeed = 5;
        rotationalSpeed = 10;               //for collision make sure rotationalSpeed is 10x of movementSpeed
        detectionDistance = 15;             //raycast detection distance
        rayCastOffset = 0.25f;                 //raycast distance from center of rocket
        bufferDuration = 30;                //sets rotateBuffer
        emergencyDir = 10;

    }

    // Update is called once per frame
    void Update()
    {
        Pathfinding();
        Move();
    }

    // Turning logic, rotates rocket to face target object location each frame
    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalSpeed * Time.deltaTime);
    }

    // Moves rocket forward by given movementSpeed
    void Move()
    {
        transform.position += transform.forward * movementSpeed* Time.deltaTime;
    }

    // Pathfinding logic
    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 hitOffset = Vector3.zero;

        // Initialize Raycasts positions on rocket
        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        // For debugging, draws out raycasts from rockets
        Debug.DrawRay(left, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.red);
        
        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance) && !isIgnorable(hit))
        {
            hitOffset += emergencyDir * Vector3.right;
            hitOffset += emergencyDir * Vector3.up;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance) && !isIgnorable(hit))
        {
            hitOffset -= emergencyDir * Vector3.right;
            hitOffset += emergencyDir * Vector3.up;
        }
        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance) && !isIgnorable(hit))
        {
            hitOffset -= emergencyDir * Vector3.up;
        }
            
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance) && !isIgnorable(hit))
        {
            hitOffset += emergencyDir * Vector3.up;
        }

        // How many frames the rocket will turn in the emergency direction
        if (rotateBuffer!=0)
        {
            transform.Rotate(hitOffset * emergencyDir * Time.deltaTime);
            rotateBuffer -= 1;
        }
        else if (hitOffset != Vector3.zero )
        {
            rotateBuffer = bufferDuration;
        }
        else
            Turn();
    }

    // Returns true if raycast hits target position
    bool isIgnorable(RaycastHit hit)
    {
        // When in motion
        if (startPosition != null)
        {
            // If raycast hits ignorable object
            return hit.transform.gameObject == target.gameObject || hit.transform.gameObject == startPosition.gameObject 
                || hit.transform.gameObject.tag == "Rocket"; 
        }
        return hit.transform.gameObject == target.gameObject || hit.transform.gameObject.tag == "Rocket";
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject == target.gameObject)
        {
            OnRocketLand?.Invoke(this.gameObject);

            if (RocketController.instance.IsRocketPathQueued(startPositionString, targetString))
            {
                RocketController.instance.activeRockets.Remove(gameObject);
                transform.position = startPosition.position;
            }
        }
    }
}
