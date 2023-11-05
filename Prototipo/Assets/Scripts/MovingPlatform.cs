using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 moveDirection = Vector3.right;
    public float waitTime = 2f;

    private bool isMoving = true;
    private bool changingDirection = false;

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ChangeDirectionAfterDelay()
    {
        isMoving = false;
        yield return new WaitForSeconds(waitTime);
        moveDirection = -moveDirection; // Change direction
        isMoving = true;
        changingDirection = false; // Reset the flag
    }

    private void OnCollisionEnter(Collision other)
    {
        // Check if the collision is with the terrain collider but not with the player
        if (other.collider is TerrainCollider && !other.gameObject.CompareTag("Player"))
        {
            if (!changingDirection)
            {
                changingDirection = true;
                StartCoroutine(ChangeDirectionAfterDelay());
            }
        }
        // Check for other collisions that are not with the player
        else if (!other.gameObject.CompareTag("Player") && !changingDirection)
        {
            changingDirection = true;
            StartCoroutine(ChangeDirectionAfterDelay());
        }
    }
}
