using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private float rotationSpeed = 60f;
    
    // The min and max distances that the enemies are allowed to be from the player.
    private float minDistance = 6f;

    // The speed at which the enemy moves.
    private float moveSpeed;
    private float minMoveSpeed = 2f;
    private float maxMoveSpeed = 3.5f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        // Randomizer the enemy moveSpeed
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }
    private void Update()
    {
        // Distance from the enemy to the player.
        float distance = Vector2.Distance(transform.position, player.position);

        // If the enemy is too close to the player, move the enemy away at an increasing speed based on the proximity to the player.
        if (distance < minDistance)
        {
            // Speed that the enemy will use to move away from the player when they are too close that is based on their proximity to the player.
            float moveAwaySpeed = (minDistance - distance) * moveSpeed;

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -moveAwaySpeed * Time.deltaTime);
        }
        
        // If the enemy is too far from the player, get closer to the player.
        else if (distance > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        transform.RotateAround(player.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    //private void LookAtPlayer()
    //{
    //    Vector3 lookDirection = transform.InverseTransformPoint(player.transform.position);
    //}
}
