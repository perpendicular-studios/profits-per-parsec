using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform startPosition;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationalSpeed; //for collision make sure rotationalSpeed is 10x of movementSpeed

    // Start is called at beginning
    void Start()
    {
        transform.position = startPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Move();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalSpeed * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed* Time.deltaTime;
    }
}
