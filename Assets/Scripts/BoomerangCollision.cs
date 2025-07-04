using UnityEngine;

public class BoomerangCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Snowman"
        if (collision.gameObject.CompareTag("Snowman"))
        {
            // Deactivate the Snowman object
            collision.gameObject.SetActive(false);
        }
    }
}
