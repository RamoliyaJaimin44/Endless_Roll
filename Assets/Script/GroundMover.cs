using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public Transform player;        // assign player
    public float groundLength = 20; // size of the ground mesh
    public float speed = 5f;        // same as player's forwardSpeed

    void Update()
    {
        // Move opposite of player's forward direction
        Vector3 moveDirection = -player.forward * speed * Time.deltaTime;
        transform.position += moveDirection;

        // Reset ground when far behind the player
        float distance = Vector3.Dot(player.forward, player.position - transform.position);

        if (distance > groundLength)
        {
            transform.position += player.forward * groundLength * 2f;
        }
    }
}

