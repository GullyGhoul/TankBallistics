using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletPrefabBehaviour : MonoBehaviour
{
    public GameObject ShootEffect;
    public GameObject DestroyEffect;

    public void Init(Vector3 startPoint, Vector3 velocity, float time, float gravity)
    {
        transform.position = startPoint;
        StartCoroutine(Path(velocity, time, gravity));
        Instantiate(ShootEffect, transform.position, new Quaternion(velocity.x, velocity.y, velocity.z, 1));
    }

    private IEnumerator Path(Vector3 velocity, float flightDuration, float gravity)
    {
        float elapse_time = 0;
        while (elapse_time <= flightDuration)
        {
            transform.position += new Vector3(velocity.x * Time.deltaTime,
                (velocity.y - (gravity * elapse_time)) * Time.deltaTime
                , velocity.z * Time.deltaTime);
            Vector3 vector3 = (new Vector3(velocity.x, velocity.y - (gravity * elapse_time), velocity.z)).normalized;
            Quaternion rotation = Quaternion.LookRotation(vector3, Vector3.up);
            transform.rotation = rotation;
            elapse_time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject, 0.01f);
        Instantiate(DestroyEffect, transform.position, Quaternion.identity);
    }
}