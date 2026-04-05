using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    private Vector3 buffer;
    private Vector3 placeholder;
    private Vector3 rawDirection;
    private Vector3 destination;
    private Vector3 stepDestination;

    private RaycastHit hit;
    private float nextActionTime;
    private bool isMoving = false;
    private bool isRotating = false;
    private Vector3 centre;
    private float rotateSpeed;
    private float deltaY;

    private bool playerCanMove = false;

    public LayerMask walkableLayer;
    public float playerHeight = 2.0f;
    public float playerSpeed = 2.0f;
    public float timePeriod = 0.015f;

    public void SpawnPlayer()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        playerCanMove = true;

        //Debug.Log("Im ready!");
    }

    public void Motion(Vector3 centre, float deltaY, float rotateSpeed, Vector3 stepDestination)
    {
        this.centre = centre;
        this.deltaY = deltaY;
        this.rotateSpeed = rotateSpeed;
        stepDestination.y += transform.position.y;
        this.stepDestination = stepDestination;
        isRotating = true;
    }
}
