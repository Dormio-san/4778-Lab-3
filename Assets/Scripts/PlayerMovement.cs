using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed at which the player moves.
    private float moveSpeed = 9f;

    // Floats for the horizontal and vertical movement axes.
    private float horizontalMovement;
    private float verticalMovement;

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // Move the player based on input and speed variable.
        transform.Translate(new Vector2(horizontalMovement, verticalMovement).normalized * moveSpeed * Time.deltaTime);
    }
}
