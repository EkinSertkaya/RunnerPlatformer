using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggerBehaviour : MonoBehaviour
{
    GameObject percentileUICanvas;

    Manager gameManager;

    Vector3 cameraPosAfterPlatform;

    PlayerMovement playerMovementScript;

    CameraBehaviour cameraBehaviourScript;

    [SerializeField] float cameraXPosAfterPlatform = 0.005f;
    [SerializeField] float cameraYPosAfterPlatform = 0.6f;
    [SerializeField] float cameraZPosAfterPlatform = 4.1f;

    private void Start()
    {
        ComponentGetter();
        ManagePercentileUICanvas();
    }

    void ComponentGetter()
    {
        cameraPosAfterPlatform = new Vector3(cameraXPosAfterPlatform, cameraYPosAfterPlatform, cameraZPosAfterPlatform);
        playerMovementScript = GameObject.Find("Boy").GetComponent<PlayerMovement>();
        cameraBehaviourScript = Camera.main.GetComponent<CameraBehaviour>();
        percentileUICanvas = GameObject.Find("Percentage UI");
        gameManager = GameObject.Find("Game Manager").GetComponent<Manager>();
    }

    void ManagePercentileUICanvas()
    {
        percentileUICanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMovementScript.isMoving = false;
            playerMovementScript.isPlatformCompleted = true;
            cameraBehaviourScript.enabled = false;
            percentileUICanvas.SetActive(true);
            gameManager.paintedWallBehaviourScript.gameObject.SetActive(true);

            Camera.main.transform.position = cameraPosAfterPlatform;
        }
        else if (other.gameObject.tag == "Opponent")
        {
            Destroy(other.gameObject);
        }
    }

    
}
