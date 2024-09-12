using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float rotationSpeed = 70f;
    
    // The min and max distances that the enemies are allowed to be from the player.
    private float minDistance = 6.5f;
    private float maxDistance = 50f;

    // The speed at which the enemy moves away from the player when they are too close.
    private float moveAwaySpeed = 2f;

    private void Update()
    {
        // Distance from the enemy to the player.
        float distance = Vector2.Distance(transform.position, player.position);

        // If the enemy is too close to the player, move the enemy away at an increasing speed based on the proximity to the player.
        if (distance < minDistance)
        {
            float moveSpeed = (minDistance - distance) * moveAwaySpeed;

            Vector3 directionAway = (transform.position - player.position);
            transform.position += directionAway * moveSpeed * Time.deltaTime;
        }
        
        // If the enemy is too far from the player, get closer to the player.
        else if (distance > maxDistance)
        {
            Vector3 directionToPlayer = (transform.position - player.position);
            transform.position = directionToPlayer * (distance - maxDistance) * Time.deltaTime;
        }

        transform.RotateAround(player.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
