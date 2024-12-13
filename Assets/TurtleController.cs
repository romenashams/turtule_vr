using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f; // Degrees per click

    public void MoveUp()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); // Moves forward
    }

    public void MoveDown()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime); // Moves backward
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // Moves left
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // Moves right
    }

    public void Rotate(float degrees)
    {
        transform.Rotate(Vector3.up, degrees); // Rotates around y-axis
    }

    // Set the initial orientation based on the AR plane
    public void SetInitialOrientation(Quaternion orientation)
    {
        transform.rotation = orientation;
        AdjustToPlane();
    }

    private void AdjustToPlane()
    {
        // This orients the turtle so that it appears to be standing on the plane, not diving into it.
        Vector3 lookPos = Camera.main.transform.position - transform.position;
        lookPos.y = 0; // Keep the turtle upright relative to the camera
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
