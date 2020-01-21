using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    private Vector3 position;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = new Vector3(player.transform.position.x,
            player.transform.position.y, player.transform.position.z - 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Vector3 player_position= player.transform.localPosition;
            float x, z;
            x = gameObject.transform.localPosition.x - player_position.x;
            z = gameObject.transform.localPosition.z - player_position.z;
            gameObject.transform.Rotate(new Vector3(0,gameObject.transform.rotation.y + 90,0));
            gameObject.transform.position = new Vector3(-15,player.transform.position.y, player.transform.position.z);
        } else if (Input.GetKeyDown("q"))
        {
            gameObject.transform.Rotate(new Vector3(0,gameObject.transform.rotation.y - 90,0));
            gameObject.transform.position = new Vector3(15,player.transform.position.y, player.transform.position.z);
        }
    }
}
