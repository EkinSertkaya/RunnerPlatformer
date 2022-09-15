using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacedDirectionObjBehaviour : MonoBehaviour
{
    PlayerMovement playerMovementScript;

    Transform playerTransform;

    Vector3 playerMovementDirectionIn3D;

    private void Start()
    {
        ComponentGetter();
    }

    private void Update()
    {
        ManagePlayerFacedDirection();
    }

    void ComponentGetter()
    {
        playerMovementScript = GameObject.Find("Boy").GetComponent<PlayerMovement>();
        playerTransform = GameObject.Find("Boy").GetComponent<Transform>();
    }

    void ManagePlayerFacedDirection()
    {
        playerMovementDirectionIn3D = new Vector3(playerMovementScript.movementDirection.x, playerTransform.position.y, playerMovementScript.movementDirection.y);
        transform.position = playerTransform.position + playerMovementDirectionIn3D;
    }
}
