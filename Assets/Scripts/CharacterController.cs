using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool canGo = false;
    GameObject destination;
    public float speed;
   public void StartMove()
    {
        destination = GameObject.FindGameObjectWithTag("SpawnPoint");
        canGo = true;
    }

    private void Update()
    {
        if (canGo)
        {
            transform.LookAt(destination.transform);
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);
        }
    }
}
