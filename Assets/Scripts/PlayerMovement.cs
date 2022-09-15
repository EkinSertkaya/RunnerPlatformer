using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject playerLookingDirection;

    Manager gameManagerScript;

    Rigidbody playerRB;

    Vector3[] cheatPositions = { new Vector3(0.01f, 0.01f, 1.3f), new Vector3(0.01f, 0.01f, 2.9f), new Vector3(0.01f, 0.01f, 4.075f), new Vector3(0.01f, 0.01f, 5.25f) };
    public Vector3 movementDirection;
    Vector3 initialMousePosInScreenUnits;
    Vector3 primaryMousePosInScreenUnits;
    Vector3 currentPlayerVelocity;

    public Touch primaryTouch;

    public bool isPlatformCompleted = false;
    public bool isMoving = false;

    [SerializeField] float maxVelocity = 0.1f;
    [SerializeField] float playerSpeed = 100f;

    int currentCheatPos = 0;

    private void Start()
    {
        ComponentGetter();
    }

    private void Update()
    {
        PlayerControls();
        PlayerVelocityManager();
    }

    void ComponentGetter()
    {
        playerLookingDirection = GameObject.Find("Faced Direction");
        playerRB = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<Manager>();
    }

    void PlayerControls()
    {
        if (movementDirection != Vector3.zero && !isPlatformCompleted)
        {
            transform.LookAt(playerLookingDirection.transform);
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosInScreenUnits = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }

            primaryMousePosInScreenUnits = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            movementDirection = (primaryMousePosInScreenUnits - initialMousePosInScreenUnits).normalized;

            if (!isPlatformCompleted && movementDirection != Vector3.zero)
            {
                isMoving = true;
                playerRB.AddForce(new Vector3(movementDirection.x, 0f, movementDirection.y) * playerSpeed * Time.deltaTime, ForceMode.Force);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameManagerScript.endGameUI.activeInHierarchy)
            {
                gameManagerScript.endGameUI.SetActive(true);
            }
            else if (gameManagerScript.endGameUI.activeInHierarchy)
            {
                gameManagerScript.endGameUI.SetActive(false);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.position = cheatPositions[currentCheatPos];
            ++currentCheatPos;
            
            if(currentCheatPos > 3)
            {
                currentCheatPos = 0;
            }
        }
    }
    void PlayerVelocityManager()
    {
        currentPlayerVelocity = playerRB.velocity;

        if (currentPlayerVelocity.x > maxVelocity)
        {
            playerRB.velocity = new Vector3(maxVelocity, playerRB.velocity.y, playerRB.velocity.z);
        }
        if (currentPlayerVelocity.x < -maxVelocity)
        {
            playerRB.velocity = new Vector3(-maxVelocity, playerRB.velocity.y, playerRB.velocity.z);
        }
        if (currentPlayerVelocity.z > maxVelocity)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, maxVelocity);
        }
        if (currentPlayerVelocity.z < -maxVelocity)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, -maxVelocity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            gameManagerScript.RespawnPlayer();
        }
    }

    private void OnDisable()
    {
        isMoving = false;
    }


}
