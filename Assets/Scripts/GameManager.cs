using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private bool isMovingSnail = true; // Controls whether left mose rotates the cube or moves the snail
    private bool isTurningLeft = true;  // Control variable for planetary rotations going in one of two directions
    private bool isRotatingPiece = false;

    private int health = 3;
    private int maxHealth = 3;

    private int turnCount = 0;
    private int piecesRetrieved = 0;
    private int piecesHeld = 0;

    private int activeDustDevils = 0;

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
        if (health == 0)
        {
            SceneManager.LoadScene(1);
        }
        if (piecesRetrieved == 3)
        {
            SceneManager.LoadScene(2);
        }
    }

    public bool GetIsMovingSnail()
    {
        return isMovingSnail;
    }

    public void SetIsMovingSnail(bool set)
    {
        isMovingSnail = set;
    }

    public bool GetIsTurningLeft()
    {
        return isTurningLeft;
    }

    public void SetIsTurningLeft(bool left)
    {
        isTurningLeft=left;
    }

    /// <summary>
    /// Setter for health
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(int health)
    {
        this.health = health;
    }

    /// <summary>
    /// Gettter for health
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return health;
    }

    public void IncrementTurnCount()
    {
        turnCount++;
    }

    public int GetTurnCount()
    {
        return turnCount;
    }

    public int GetDustDevilCount()
    {
        return activeDustDevils;
    }

    public void SetDustDevilCount(int count)
    {
        activeDustDevils = count;
    }

    public bool GetIsRotatingPiece()
    {
        return isRotatingPiece;
    }

    public void SetRotatingPiece(bool val)
    {
        isRotatingPiece = val;
    }

    public int GetPiecesHeld()
    {
        return piecesHeld;
    }

    public int GetPiecesRetrieved()
    {
        return piecesRetrieved;
    }

    public void AddToPiecesRetrieved()
    {
        piecesRetrieved++;
    }

    public void AddToPiecesHeld()
    {
        piecesHeld++;
    }

    public void SubtractFromPiecesHeld()
    {
        piecesHeld--;
    }

    public void ResetAllValues()
    {
        health = maxHealth;
        turnCount = 0;
        piecesRetrieved = 0;
        piecesHeld = 0;
        activeDustDevils = 0;
    }
}