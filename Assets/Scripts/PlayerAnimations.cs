using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnimator;

    PlayerMovement playerMovementScript;

    private void Start()
    {
        ComponentGetter();
    }

    private void Update()
    {
        ManagePlayerAnimation();
    }

    void ComponentGetter()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
    }

    void ManagePlayerAnimation()
    {
        if (playerMovementScript.isMoving)
        {
            playerAnimator.SetInteger("isRunning", 1);
        }
        else if (!playerMovementScript.isMoving)
        {
            playerAnimator.SetInteger("isRunning", 0);
        }
    }
        
}
