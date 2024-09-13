using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private float rotationSpeed = 60f;
    
    // The min distance is the closest the enemies can be to the player before they move away.
    // The minLow and minHigh are ranges used to randomize the closeness of the enemies.
    private float minDistance;
    private float minLow = 6f;
    private float minHigh = 10f;

    // Offset from minDistance that allows for enemies to rotate around the player at different distances from the player.
    private float minDistanceOffset = 6.5f;

    // The speed at which the enemy moves.
    // Min and max provide a range for variability.
    private float moveSpeed;
    private float minMoveSpeed = 2f;
    private float maxMoveSpeed = 3.5f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        // Randomize the enemy minDistance to player and enemy moveSpeed.
        minDistance = Random.Range(minLow, minHigh);
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

            // Move the enemy toward the player, but at a negative speed (since there is no MoveAway method), so the enemy goes away from the player.
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -moveAwaySpeed * Time.deltaTime);
        }
        
        // If the enemy is not too close to the player, get closer to the player.
        else if (distance > minDistance + minDistanceOffset)
        {
            float moveTowardSpeed = ((distance - minDistance) * moveSpeed) / 2.5f;

            // Move the enemy toward the player.
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveTowardSpeed * Time.deltaTime);
        }

        // Rotate the enemies around the player on the Z axis (Vector3.forward).
        transform.RotateAround(player.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        LookAtPlayer();
    }

    // Function that rotates the enemy to look at the player.
    private void LookAtPlayer()
    {
        // Calculate the direction the enemy should look. 
        Vector3 lookDirection = player.position - transform.position;

        // Calculate the angle the enemy will rotate towards with a positive 90 offset to face towards the player.
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90;

        // Set the rotation we want the enemy to make around the Z axis.
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Smoothly rotate the enemy to face the player.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
