using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private bool isMovingSnail = true; // Controls whether left mose rotates the cube or moves the snail
    private bool isTurningLeft = true;  // Control variable for planetary rotations going in one of two directions

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerHolder = new GameObject("[Game Manager]");
                managerHolder.AddComponent<GameManager>();
                DontDestroyOnLoad(managerHolder);
                _instance = managerHolder.GetComponent<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        //_instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            isMovingSnail = !isMovingSnail;
        }
    }

    public bool GetIsMovingSnail()
    {
        return isMovingSnail;
    }

    public bool GetIsTurningLeft()
    {
        return isTurningLeft;
    }

    public void SetIsTurningLeft(bool left)
    {
        isTurningLeft=left;
    }
}