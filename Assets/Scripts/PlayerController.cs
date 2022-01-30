using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Used this video as foundation: https://www.youtube.com/watch?v=PmIPqGqp8UY&ab_channel=AcaciaDeveloper
    //--- SETUP ---//
    // Create empty GameObject.
    // Add Character controller component and set CenterY to half of the character height.
    // Add camera as child then offset Y to the character height.
    //--- SETUP END ---//

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip footStepSound;

    [SerializeField] Transform player;
    [SerializeField] Transform playerCameraBobContainer;
    [SerializeField] Transform playerCamera;
    [SerializeField] float mouseSensitivity = 0.4f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float sprintSpeed = 8.0f;
    [SerializeField] float gravity = -9.81f; // 13 in tutorial but I set it as the actual value for gravity
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0; //0.1f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0; //0.03f;



    float cameraAngle = 0.0f;
    float velocityY = 0.0f;
    [SerializeField] CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    [Header("Headbob")]
    [SerializeField] bool bobUp = false;
    [SerializeField] float defaultYPosition; //camera container starting point
    [SerializeField] float walkHeadbobSpeed = 14f;
    [SerializeField] float walkHeadBobAmount = 0.06f;
    [SerializeField] float sprintHeadbobSpeed = 18f;
    [SerializeField] float sprintHeadBobAmount = 0.1f;

    [SerializeField] bool canLook = true;
    [SerializeField] bool canMove = true;


    void Awake()
    {
        GiveControlToPlayer();
    }

    void Start()
    {
      

        // Subscribe to death event
        PlayerDeathManager.Instance.OnPlayerDied += delegate (object sender, EventArgs e)
        {
            RemoveControlFromPlayer();
        };

        PlayerDeathManager.Instance.OnPlayerRespawned += delegate (object sender, EventArgs e)
        {
            GiveControlToPlayer();
        };

        bobUp = false;
        defaultYPosition = playerCameraBobContainer.localPosition.y;

        GiveControlToPlayer();
    }


    void GiveControlToPlayer()
    {
        canMove = true;
        canLook = true;
    }

    void RemoveControlFromPlayer()
    {
        //controller.Move(Vector3.zero);
        canMove = false;
        canLook = false;
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        HandleHeadbob();
    }

    void UpdateMouseLook()
    {
        if (canLook)
        {
            Vector2 targetMouseDelta = new Vector2(Input.Instance.mousePosition.x, Input.Instance.mousePosition.y);

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            cameraAngle -= currentMouseDelta.y * mouseSensitivity;
            cameraAngle = Mathf.Clamp(cameraAngle, -90.0f, 90.0f);

            playerCamera.localEulerAngles = Vector3.right * cameraAngle;
            player.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }

    }

    void UpdateMovement()
    {
        if (canMove)
        {
            Vector2 targetDir = new Vector2(Input.Instance.moveDirictionInput.x, Input.Instance.moveDirictionInput.y);
            targetDir.Normalize();

            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

            if (controller.isGrounded)
                velocityY = 0.0f;

            velocityY += gravity * Time.deltaTime;

            Vector3 velocity = Vector3.zero;

            // Sprinting
            if (Input.Instance.holdingSprint)
            {
                velocity = (player.forward * currentDir.y + player.right * currentDir.x) * sprintSpeed + Vector3.up * velocityY;

            }
            // Walking
            else
            {
                velocity = (player.forward * currentDir.y + player.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

            }

            controller.Move(velocity * Time.deltaTime);
        }


    }





    void HandleHeadbob()
    {
        if (canMove)
        {
            // Check if moving
            if (Mathf.Abs(Input.Instance.moveDirictionInput.x) > 0.1f || Mathf.Abs(Input.Instance.moveDirictionInput.y) > 0.1f)
            {

                // running
                if (Input.Instance.holdingSprint)
                {
                    if (bobUp)
                    {
                        playerCameraBobContainer.localPosition = Vector3.MoveTowards(playerCameraBobContainer.localPosition, (new Vector3(0, defaultYPosition + sprintHeadBobAmount, 0)), sprintHeadbobSpeed * Time.deltaTime);
                        if (playerCameraBobContainer.localPosition.y >= defaultYPosition + sprintHeadBobAmount)
                        {
                            bobUp = false;
                        }
                    }
                    else
                    {
                        playerCameraBobContainer.localPosition = Vector3.MoveTowards(playerCameraBobContainer.localPosition, (new Vector3(0, defaultYPosition - sprintHeadBobAmount, 0)), sprintHeadbobSpeed * Time.deltaTime);
                        if (playerCameraBobContainer.localPosition.y <= defaultYPosition - sprintHeadBobAmount)
                        {
                            PlayFootStepSound();
                            bobUp = true;
                        }
                    }
                }
                // walking
                else
                {
                    if (bobUp)
                    {
                        playerCameraBobContainer.localPosition = Vector3.MoveTowards(playerCameraBobContainer.localPosition, (new Vector3(0, defaultYPosition + walkHeadBobAmount, 0)), walkHeadbobSpeed * Time.deltaTime);
                        if (playerCameraBobContainer.localPosition.y >= defaultYPosition + walkHeadBobAmount)
                        {
                            bobUp = false;
                        }
                    }
                    else
                    {
                        playerCameraBobContainer.localPosition = Vector3.MoveTowards(playerCameraBobContainer.localPosition, (new Vector3(0, defaultYPosition - walkHeadBobAmount, 0)), walkHeadbobSpeed * Time.deltaTime);
                        if (playerCameraBobContainer.localPosition.y <= defaultYPosition - walkHeadBobAmount)
                        {
                            PlayFootStepSound();
                            bobUp = true;
                        }
                    }
                }
            }
        }

    }

    void PlayFootStepSound()
    {
        AudioManager.Instance.SetRandomPitch(audioSource, 0.9f, 1.1f);
        AudioManager.Instance.PlayOneShot(audioSource, footStepSound);
    }
}
