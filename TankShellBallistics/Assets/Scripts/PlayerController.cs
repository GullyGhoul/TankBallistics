
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float rotationSpeed;
    public float movingSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            navMeshAgent.Move(transform.forward * movingSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(transform.position, -Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            navMeshAgent.Move(-transform.forward * movingSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}