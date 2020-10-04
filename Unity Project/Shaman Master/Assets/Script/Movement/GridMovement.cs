using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private bool isRotating;
    private bool forward = true;
    private bool back = true;

    private Vector3 currPos;
    public Transform nextPos;
    public Transform prevPos;
    public float timeToMove;
    public float gridMoveSize;

    public float timeToRotate;
    private Quaternion targetRot;
    private float currAngle;

    public LayerMask encounterLayer;
    public float chance;

    public event Action onEncounter; // observer pattern

    private void Awake()
    {
        targetRot = transform.rotation;
        currAngle = transform.rotation.z;
    }

    public void HandleUpdate()
    {
        if (!isMoving && !isRotating)
        {
            CheckWall();
            if (Input.GetKey(KeyCode.W) && forward)
            {
                StartCoroutine(MovePlayer(new Vector3(gridMoveSize, 0, 0), nextPos));
            }
            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(RotatePlayer(new Vector3(0,0,90)));
            }
            if (Input.GetKey(KeyCode.S) && back)
            {
                StartCoroutine(MovePlayer(new Vector3(gridMoveSize, 0, 0), prevPos));
            }
            if (Input.GetKey(KeyCode.D))
            {     
                StartCoroutine(RotatePlayer(new Vector3(0, 0, -90)));
            }
        }
    }

    IEnumerator MovePlayer(Vector3 dir, Transform pos)
    {
        isMoving = true;
        float elapsedTime = 0;

        currPos = transform.position;
        Vector3 targetPos = pos.position;
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(currPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        CheckEncounter(transform.position);
        isMoving = false;
    }

    IEnumerator RotatePlayer(Vector3 rotation)
    {
        isRotating = true;

        float elapsedTime = 0;
        Quaternion currAngle = transform.rotation;
        Quaternion nextAngle = Quaternion.Euler(transform.eulerAngles + rotation);

        while(elapsedTime < timeToRotate)
        {
            transform.rotation = Quaternion.Lerp(currAngle, nextAngle, elapsedTime/timeToRotate);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = nextAngle;
        
        isRotating = false;
    }

    private void CheckWall()
    {
        if (nextPos.GetComponent<CollisionDetector>().getWall())
        {
            forward = false;
        }
        else
        {
            forward = true;
        }

        if (prevPos.GetComponent<CollisionDetector>().getWall())
        {
            back = false;
        }
        else
        {
            back = true;
        }
    }

    public void CheckEncounter(Vector3 pos)
    {
        if (Physics2D.OverlapCircle(pos, 0.2f, encounterLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= chance)
            {
                onEncounter();
            }
        }
    }
}
