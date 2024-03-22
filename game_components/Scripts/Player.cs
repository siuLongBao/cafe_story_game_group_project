using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Declare new variable
    private float speed = 8.0f;
    private float rotateSpeed = 5.0f;
    private bool isDashing = false;
    public float desiredOffset;

    public Item holdingSth = null;
    private Interactable lastHighlightedCounter = null;
    private Vector3 offsetLocation = new Vector3(0, 0, 1.2f);

    private float playerRadius = 0.7f;
    private float playerHeight = 2.0f;

    private Vector3 movementDirection = new Vector3(0, 0, 0);
    private Vector3 lastInteractDir;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.timeIsUp){
            Movement();
            CheckForFacingInteractable();
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckForInteractions();
            }
        }

    }

    // Move the character in the player's desired direction
    void Movement()
    {
        // Assign horizontalInput and forwardInput a value
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        // Moves based on vertical or horizontal input
        movementDirection = new Vector3(horizontalInput, 0, forwardInput);

        // If there is an obstacle, enable the player to try to move solely in the x-axis direction or z-axis
        if (HasObstacle(movementDirection))
        {
            Vector3 movementDirectionX = new Vector3(horizontalInput, 0, 0);
            if (!HasObstacle(movementDirectionX))
            {
                transform.Translate(movementDirectionX * Time.deltaTime * speed, Space.World);
                movementDirection = movementDirectionX;
            }
            else
            {
                Vector3 movementDirectionZ = new Vector3(0, 0, forwardInput);
                if (!HasObstacle(movementDirectionZ))
                {
                    transform.Translate(movementDirectionZ * Time.deltaTime * speed, Space.World);
                    movementDirection = movementDirectionZ;
                }
            }
        }
        // If not, enable the player to move in the input direction
        else if (!HasObstacle(movementDirection))
        {
            transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        }

        // Rotate when moving around
        if (movementDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        // Dash when press the left shift key
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopDash();
        }
    }

    // Enable the player dash
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            speed = 20.0f;
        }
    }

    // Enable the player stop dashing
    private void StopDash()
    {
        if (isDashing)
        {
            isDashing = false;
            speed = 8.0f;
        }
    }

    // Enable the player to have a simple collision detection using Physics.CapsuleCast()
    private bool HasObstacle(Vector3 direction)
    {
        float rayDistance = speed * Time.deltaTime;
        Vector3 point1 = transform.position + playerHeight/2 * Vector3.up; // Top of the capsule
        Vector3 point2 = transform.position - playerHeight/2 * Vector3.up; // Bottom of the capsule
        RaycastHit hit;

        return Physics.CapsuleCast(point1, point2, playerRadius, direction.normalized, out hit, rayDistance);
    }

    void CheckForInteractions()
    {
        Debug.Log("CheckForInteractions was called.");
        float interactionDistance = 3.0f;
        Vector3 point1 = transform.position + playerHeight/2 * Vector3.up; // Top of the capsule
        Vector3 point2 = transform.position - playerHeight/2 * Vector3.up; // Bottom of the capsule

        RaycastHit hit;
        if (CapsuleCastForInteractable(point1, point2, playerRadius, transform.forward, interactionDistance, out hit))
        {
            Debug.Log("Interact.");
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(this);  // Pass the current Player object to the Interact method
            }
        }
    }

    void CheckForFacingInteractable()
    {
        float checkDistance = 0.5f;
        Vector3 point1 = transform.position + playerHeight/2 * Vector3.up; // Top of the capsule
        Vector3 point2 = transform.position - playerHeight / 2 * Vector3.up; // Bottom of the capsule

        RaycastHit hit;
        if (CapsuleCastForInteractable(point1, point2, playerRadius, transform.forward, checkDistance, out hit))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Highlight(true);  // Highlight the counter

                // If there was a previously highlighted counter and it's not the current one, unhighlight it
                if (lastHighlightedCounter != null && lastHighlightedCounter != interactable)
                {
                    lastHighlightedCounter.Highlight(false);
                }

                lastHighlightedCounter = interactable;  // Update the reference to the last highlighted counter
            }
        }
        else if (lastHighlightedCounter != null)
        {
            lastHighlightedCounter.Highlight(false);  // Unhighlight the last highlighted counter
            lastHighlightedCounter = null;  // Clear the reference to the last highlighted counter
        }
    }

    bool CapsuleCastForInteractable(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance, out RaycastHit hitInfo)
    {
        if (Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, distance))
        {
            Debug.Log(hitInfo.transform.name);
            Interactable interactable = hitInfo.transform.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                return true;
            }
        }
        return false;
    }

    public Item GetHoldItem(){
        return this.holdingSth;
    }

    public void RemoveHoldingItem()
    {
        if(holdingSth != null)
        {
            Destroy(holdingSth.gameObject);
            holdingSth = null;
        }
    }
}
