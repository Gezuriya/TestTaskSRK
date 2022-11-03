using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    GameObject Player;
    MeshController meshCont;
    private void Start()
    {
        Player = GameObject.Find("Player");
        meshCont = GameObject.Find("plane").GetComponent<MeshController>();
    }
    private void Update()
    {
        if(Player.transform.position == transform.position)
        {
            meshCont.SpawnPoint();
            Player.GetComponent<CharacterController>().canGo = false;
            Destroy(gameObject);
        }
    }
}
