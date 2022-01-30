using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// Delete
using UnityEngine.SceneManagement;

public class BlinkEnemyMovementAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] Transform player;

    [SerializeField] float maxMoveDistance = 6f;
    [SerializeField] float maxMoveDistanceLookingAway = 2f;

    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    public bool seenByCamera = false;
    public bool seenByPlayer = false;
    public bool lookingAtPlayer = false;

    public LayerMask layersForSight;







    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip moveClip;

    void Start()
    {
        // Subscribe to blink event
        PlayerBlink.Instance.OnBlink += delegate (object sender, EventArgs e)
        {
            MoveToPlayer();
        };


    }


    private void Update()
    {
        CheckIfPlayerIsFacing();

    }


    // if can see player and player looks away, kill

    public void MoveToSpawnPoint(Transform spawnPoint)
    {
        navAgent.Warp(spawnPoint.position);
    }

    void CheckIfPlayerIsFacing()
    {
        // Check if within camera
        if (skinnedMeshRenderer.isVisible)
        {
            seenByCamera = true;
        }
        else
        {
            seenByCamera = false;
        }

        // check if can see player
        if (Physics.Linecast(transform.position, player.position, layersForSight))
        {
            lookingAtPlayer = false;
        }
        else
        {
            lookingAtPlayer = true;
        }



        // If within camera check if objects block line of sight
        if (seenByCamera && lookingAtPlayer)
        {
            seenByPlayer = true;
        }
        else
        {
            seenByPlayer = false;
        }

        // player looked away
        if (lookingAtPlayer && !seenByPlayer && BlinkMonsterManager.Instance.blinkMonsterIsActive && !PlayerDeathManager.Instance.isDead)
        {
            Debug.Log("player looked away, big mistake lol");
            // Kill player          
            //KillPlayer(PlayerDeathType.LookedAwayFromBlinkMonster);
            if(lookingAwayMoveTimer < maxTimeBetweenLookAwaySteps)
            {
                lookingAwayMoveTimer += Time.deltaTime;
            }
            else
            {
                MoveToPlayerWhenLookingAway();
                lookingAwayMoveTimer = 0;
            }
        }
    }


    [SerializeField] float lookingAwayMoveTimer = 0f;
   [SerializeField] float maxTimeBetweenLookAwaySteps = 1f;


    void KillPlayer(PlayerDeathType playerDeathType)
    {
        Debug.Log("PlayerDied");
        PlayerDeathManager.Instance.SetPlayerDead(playerDeathType);
    }


    void MoveToPlayerWhenLookingAway()
    {
        if (BlinkMonsterManager.Instance.blinkMonsterIsActive && !PlayerDeathManager.Instance.isDead)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, path);

            if (path != null && path.status != NavMeshPathStatus.PathInvalid)
            {
                //Debug.Log("corner: " + path.corners.Length);

                // if target is not the current position
                if (path.corners.Length > 1)
                {
                    for (int i = 0; i < path.corners.Length - 1; i++)
                    {
                        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 2f);

                    }

                    // if it isn't a straight line to the player
                    if (path.corners.Length > 2)
                    {
                        int targetCorner = 0;
                        float totalDistance = 0f;
                        Vector3 targetPoint = transform.position;

                        // cycle through each point of the path to find out which point is futher than the max move distance
                        for (int i = 1; i < path.corners.Length - 1; i++)
                        {
                            float nextDistance = totalDistance + Vector3.Distance(path.corners[targetCorner], path.corners[i]);

                            if (nextDistance < maxMoveDistanceLookingAway)
                            {
                                totalDistance += Vector3.Distance(path.corners[targetCorner], path.corners[i]);
                                targetCorner = i;
                                targetPoint = path.corners[i];
                            }
                            else
                            {
                                //targetCorner = i;
                                targetPoint = ReturnPointByDistance(path.corners[targetCorner], path.corners[i], maxMoveDistanceLookingAway);
                                break;
                            }
                        }

                        if (targetCorner == 0)
                        {
                            // Debug.Log("what happened lol");
                        }

                        navAgent.Warp(targetPoint);
                        audioSource.PlayOneShot(moveClip);
                        RotateToPalyer();
                    }
                    // if it is a straight line to the player, calculate the distance to them and move the max amount of distance allowed along the line
                    else
                    {
                        //Debug.Log("only 1 point");
                        float pathDistance = Vector3.Distance(path.corners[0], path.corners[1]);
                        // if path to player only has 1 point, check distance to point
                        if (pathDistance <= maxMoveDistanceLookingAway)
                        {
                            // teleport to player if point is in range
                            navAgent.Warp(path.corners[1]);
                            audioSource.PlayOneShot(moveClip);
                            RotateToPalyer();

                            // This is the player position

                            KillPlayer(PlayerDeathType.LookedAwayFromBlinkMonster);
                        }
                        else
                        {
                            // calculate max move distance between position and point
                            Vector3 targetPoint = ReturnPointByDistance(path.corners[0], path.corners[1], maxMoveDistanceLookingAway);
                            navAgent.Warp(targetPoint);
                            audioSource.PlayOneShot(moveClip);
                            RotateToPalyer();
                        }
                    }
                }
                else
                {
                    Debug.Log("target location is the current location");
                }
            }
        }
    }

    void MoveToPlayer()
    {
        if (BlinkMonsterManager.Instance.blinkMonsterIsActive && !PlayerDeathManager.Instance.isDead)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, path);

            if (path != null && path.status != NavMeshPathStatus.PathInvalid)
            {
                //Debug.Log("corner: " + path.corners.Length);

                // if target is not the current position
                if (path.corners.Length > 1)
                {
                    for (int i = 0; i < path.corners.Length - 1; i++)
                    {
                        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 2f);

                    }

                    // if it isn't a straight line to the player
                    if (path.corners.Length > 2)
                    {
                        int targetCorner = 0;
                        float totalDistance = 0f;
                        Vector3 targetPoint = transform.position;

                        // cycle through each point of the path to find out which point is futher than the max move distance
                        for (int i = 1; i < path.corners.Length - 1; i++)
                        {
                            float nextDistance = totalDistance + Vector3.Distance(path.corners[targetCorner], path.corners[i]);

                            if (nextDistance < maxMoveDistance)
                            {
                                totalDistance += Vector3.Distance(path.corners[targetCorner], path.corners[i]);
                                targetCorner = i;
                                targetPoint = path.corners[i];
                            }
                            else
                            {
                                //targetCorner = i;
                                targetPoint = ReturnPointByDistance(path.corners[targetCorner], path.corners[i], maxMoveDistance);
                                break;
                            }
                        }

                        if (targetCorner == 0)
                        {
                            // Debug.Log("what happened lol");
                        }

                        navAgent.Warp(targetPoint);
                        audioSource.PlayOneShot(moveClip);
                        RotateToPalyer();
                    }
                    // if it is a straight line to the player, calculate the distance to them and move the max amount of distance allowed along the line
                    else
                    {
                        //Debug.Log("only 1 point");
                        float pathDistance = Vector3.Distance(path.corners[0], path.corners[1]);
                        // if path to player only has 1 point, check distance to point
                        if (pathDistance <= maxMoveDistance)
                        {
                            // teleport to player if point is in range
                            navAgent.Warp(path.corners[1]);
                            audioSource.PlayOneShot(moveClip);
                            RotateToPalyer();

                            // This is the player position

                            KillPlayer(PlayerDeathType.BlinkedWhenBlinkMonsterIsTooClose);
                        }
                        else
                        {
                            // calculate max move distance between position and point
                            Vector3 targetPoint = ReturnPointByDistance(path.corners[0], path.corners[1], maxMoveDistance);
                            navAgent.Warp(targetPoint);
                            audioSource.PlayOneShot(moveClip);
                            RotateToPalyer();
                        }
                    }
                }
                else
                {
                    Debug.Log("target location is the current location");
                }
            }
        }

    }

    void RotateToPalyer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0; //This allows the object to only rotate on its y axis
        transform.LookAt(player);
    }


    Vector3 ReturnPointByDistance(Vector3 pointA, Vector3 pointB, float distance)
    {
        Vector3 targetPoint = distance * Vector3.Normalize(pointB - pointA) + pointA;
        return targetPoint;
    }
}
