// PlayerScript.cs
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float interactRange = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            // Check if the object hit has the tag "Ship" or add a specific layer for interactive objects
            if (hit.collider.CompareTag("Ship"))
            {
                // Notify the ship to play its animation
                hit.collider.GetComponent<ShipScript>().PlayInteractionAnimation();
            }
        }
    }
}
