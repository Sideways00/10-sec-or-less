using UnityEngine;
using UnityEngine.InputSystem;

public class BBLogic : MonoBehaviour
{
    public Rigidbody ballRb;
    public Camera playerCamera;

    public float throwForce = 10f;
    public float upwardForce = 5f;

    private bool hasBeenThrown = false;

    void Start()
    {
        ballRb.isKinematic = true;
        ballRb.useGravity = false;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        // Left-click throws the ball
        if (Mouse.current.leftButton.wasPressedThisFrame
            && !hasBeenThrown)
        {
            ThrowBall();
        }

        // Right-click returns the ball
        if (Mouse.current.rightButton.wasPressedThisFrame
            && hasBeenThrown)
        {
            ReturnBall();
        }
    }

    void ThrowBall()
    {
        hasBeenThrown = true;

        Vector3 direction =
            playerCamera.transform.forward * throwForce +
            playerCamera.transform.up * upwardForce;

        transform.SetParent(null);

        ballRb.isKinematic = false;
        ballRb.useGravity = true;
        ballRb.AddForce(direction, ForceMode.Impulse);
    }

    void ReturnBall()
    {
        // Stop the ball's movement
        ballRb.linearVelocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        // Turn physics off while the ball is held
        ballRb.isKinematic = true;
        ballRb.useGravity = false;

        // Attach it back to the camera
        transform.SetParent(playerCamera.transform);

        // Position it in front of the camera
        transform.localPosition = new Vector3(0.3f, -0.3f, 0.8f);
        transform.localRotation = Quaternion.identity;

        hasBeenThrown = false;
    }
}