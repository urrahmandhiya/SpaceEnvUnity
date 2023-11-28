// ShipScript.cs
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    private Animator shipAnimator;

    void Start()
    {
        // Attempt to get the Animator component from a child object
        shipAnimator = GetComponentInChildren<Animator>();

        if (shipAnimator == null)
        {
            Debug.LogError("Animator component not found in children of the Ship object.");
        }
    }

    public void PlayInteractionAnimation()
    {
        if (shipAnimator != null)
        {
            // Trigger the animation or any other interaction logic
            shipAnimator.SetTrigger("Activate");
        }
        else
        {
            Debug.LogError("Animator component is null. Make sure it is attached to a child object.");
        }
    }
}
