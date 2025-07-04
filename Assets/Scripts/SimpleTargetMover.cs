using UnityEngine;
using System.Collections.Generic;

public class SimpleTargetMover : MonoBehaviour
{
    public List<GameObject> targets; // List of target GameObjects to move
    public float moveDistance = 5f;  // Distance to move on the X axis
    public float speed = 2f;         // Speed of movement

    private List<Vector3> initialPositions; // Store the initial positions of the targets
    private bool movingRight = true;        // Direction flag

    void Start()
    {
        // Store the initial positions of the targets
        initialPositions = new List<Vector3>();
        foreach (GameObject target in targets)
        {
            if (target != null)
                initialPositions.Add(target.transform.position);
        }
    }

    void Update()
    {
        MoveTargets();
    }

    void MoveTargets()
    {
        foreach (GameObject target in targets)
        {
            if (target == null) continue;

            // Determine the target position based on the direction
            Vector3 targetPosition = movingRight
                ? initialPositions[targets.IndexOf(target)] + new Vector3(moveDistance, 0, 0)
                : initialPositions[targets.IndexOf(target)] - new Vector3(moveDistance, 0, 0);

            // Move the target towards the target position
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the target has reached the target position
            if (Vector3.Distance(target.transform.position, targetPosition) < 0.01f)
            {
                // Reverse direction when reaching the target position
                movingRight = !movingRight;
            }
        }
    }
}
