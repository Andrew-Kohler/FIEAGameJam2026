using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector2 mouseRef;
    private bool dragging = false;

    private float speed = 100f;

    private float sensitivity = 0.3f;
    private Vector3 rotation;

    private bool autoRotate = false;

    private Quaternion targetQuaternion;

    private ReadCube readCube;
    private CubeState cubeState;
    void Start()
    {
        readCube = FindFirstObjectByType<ReadCube>();
        cubeState = FindFirstObjectByType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            SpinSide(activeSide);
            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
        if (autoRotate)
        {
            AutoRotate();
        }
    }

    private void SpinSide(List<GameObject> side)
    {
        rotation = Vector3.zero; // Reset rotation

        Vector3 mouseOffset = Mouse.current.position.ReadValue() - mouseRef;

        if(side == cubeState.frontTiles)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.backTiles)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.upTiles)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.downTiles)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.leftTiles)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.rightTiles)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        transform.Rotate(rotation, Space.Self);
        mouseRef = Mouse.current.position.ReadValue();
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Mouse.current.position.ReadValue();
        dragging = true;

        // Create a vector to rotate around
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;

        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotate = true;
    }

    private void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        // If w/in one degree, set angle to target angle and end the rotation
        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;

            // Unparent the cubes!
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            autoRotate = false;
            dragging = false;
        }
    }

    
}
