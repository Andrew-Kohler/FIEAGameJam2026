using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RotationUI : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;
    [SerializeField] private ReadCube readCube;

    [SerializeField] private PrimaryUI primaryMenu;

    //private List<Transform> transformsOfReadCube;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UICleanupAdmin()
    {
        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);

        if (GameManager.Instance.challengeMode)
        {
            GameManager.Instance.challengeModeRotations--;
        }
    }

    #region TOP AND BOT TURNS

    public void TurnTopToLeft()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        if(GetHighestTransform() == cubeState.backTiles || GetHighestTransform() == cubeState.downTiles || GetHighestTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetHighestTransform());

        UICleanupAdmin();
    }

    public void TurnBotToLeft()
    {
        GameManager.Instance.SetIsTurningLeft(true);
        if (GetLowestTransform() == cubeState.frontTiles || GetLowestTransform() == cubeState.upTiles || GetLowestTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetLowestTransform());

        UICleanupAdmin();
    }

    public void TurnTopToRight()
    {
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetHighestTransform() == cubeState.backTiles || GetHighestTransform() == cubeState.downTiles || GetHighestTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetHighestTransform());

        UICleanupAdmin();
    }

    public void TurnBotToRight()
    {
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetLowestTransform() == cubeState.frontTiles || GetLowestTransform() == cubeState.upTiles || GetLowestTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetLowestTransform());

        UICleanupAdmin();
    }
    #endregion

    #region LEFT AND RIGHT TURNS
    public void TurnLeftToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        if (GetLeftmostTransform() == cubeState.backTiles || GetLeftmostTransform() == cubeState.downTiles || GetLeftmostTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetLeftmostTransform());

        UICleanupAdmin();
    }

    public void TurnLeftToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetLeftmostTransform() == cubeState.backTiles || GetLeftmostTransform() == cubeState.downTiles || GetLeftmostTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetLeftmostTransform());

        UICleanupAdmin();
    }

    public void TurnRightToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        if (GetRightmostTransform() == cubeState.frontTiles || GetRightmostTransform() == cubeState.upTiles || GetRightmostTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetRightmostTransform());

        UICleanupAdmin();
    }

    public void TurnRightToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetRightmostTransform() == cubeState.frontTiles || GetRightmostTransform() == cubeState.upTiles || GetRightmostTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetRightmostTransform());

        UICleanupAdmin();
    }
    #endregion

    #region FRONT AND BACK TURNS
    public void TurnFrontToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetFrontmostTransform() == cubeState.backTiles || GetFrontmostTransform() == cubeState.downTiles || GetFrontmostTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetFrontmostTransform());

        UICleanupAdmin();
    }

    public void TurnFrontToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        if (GetFrontmostTransform() == cubeState.backTiles || GetFrontmostTransform() == cubeState.downTiles || GetFrontmostTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetFrontmostTransform());

        UICleanupAdmin();
    }

    public void TurnBackToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetBackmostTransform() == cubeState.frontTiles || GetBackmostTransform() == cubeState.upTiles || GetBackmostTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetBackmostTransform());

        UICleanupAdmin();
    }

    public void TurnBackToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        if (GetBackmostTransform() == cubeState.frontTiles || GetBackmostTransform() == cubeState.upTiles || GetBackmostTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetBackmostTransform());

        UICleanupAdmin();
    }
    #endregion

    #region HIGH AND LOW GETTERS
    private List<GameObject> GetHighestTransform()
    {
        List<Transform> transformsOfReadCube = new List<Transform>();
        transformsOfReadCube.Add(readCube.tUp);
        transformsOfReadCube.Add(readCube.tDown);
        transformsOfReadCube.Add(readCube.tLeft);
        transformsOfReadCube.Add(readCube.tRight);
        transformsOfReadCube.Add(readCube.tFront);
        transformsOfReadCube.Add(readCube.tBack);

        List<GameObject> side = null;
        Transform notedTransform = transformsOfReadCube[0];
        for (int i = 0; i < transformsOfReadCube.Count; i++)
        {
            if(transformsOfReadCube[i].position.y > notedTransform.position.y)
            {
                notedTransform = transformsOfReadCube[i];
            }
        }

        if(notedTransform == readCube.tUp)
        {
            side = cubeState.upTiles;
        }
        else if (notedTransform == readCube.tDown)
        {
            side = cubeState.downTiles;
        }
        else if (notedTransform == readCube.tFront)
        {
            side = cubeState.frontTiles;
        }
        else if (notedTransform == readCube.tBack)
        {
            side = cubeState.backTiles;
        }
        else if (notedTransform == readCube.tLeft)
        {
            side = cubeState.leftTiles;
        }
        else if (notedTransform == readCube.tRight)
        {
            side = cubeState.rightTiles;
        }

        return side;
    }

    private List<GameObject> GetLowestTransform()
    {
        List<Transform> transformsOfReadCube = new List<Transform>();
        transformsOfReadCube.Add(readCube.tUp);
        transformsOfReadCube.Add(readCube.tDown);
        transformsOfReadCube.Add(readCube.tLeft);
        transformsOfReadCube.Add(readCube.tRight);
        transformsOfReadCube.Add(readCube.tFront);
        transformsOfReadCube.Add(readCube.tBack);

        List<GameObject> side = null;
        Transform notedTransform = transformsOfReadCube[0];
        for (int i = 0; i < transformsOfReadCube.Count; i++)
        {
            if (transformsOfReadCube[i].position.y < notedTransform.position.y)
            {
                notedTransform = transformsOfReadCube[i];
            }
        }

        if (notedTransform == readCube.tUp)
        {
            side = cubeState.upTiles;
        }
        else if (notedTransform == readCube.tDown)
        {
            side = cubeState.downTiles;
        }
        else if (notedTransform == readCube.tFront)
        {
            side = cubeState.frontTiles;
        }
        else if (notedTransform == readCube.tBack)
        {
            side = cubeState.backTiles;
        }
        else if (notedTransform == readCube.tLeft)
        {
            side = cubeState.leftTiles;
        }
        else if (notedTransform == readCube.tRight)
        {
            side = cubeState.rightTiles;
        }

        return side;
    }

    #endregion

    #region LEFT AND RIGHT GETTERS
    private List<GameObject> GetLeftmostTransform()
    {
        List<Transform> transformsOfReadCube = new List<Transform>();
        transformsOfReadCube.Add(readCube.tUp);
        transformsOfReadCube.Add(readCube.tDown);
        transformsOfReadCube.Add(readCube.tLeft);
        transformsOfReadCube.Add(readCube.tRight);
        transformsOfReadCube.Add(readCube.tFront);
        transformsOfReadCube.Add(readCube.tBack);

        List<GameObject> side = null;
        Transform notedTransform = transformsOfReadCube[0];
        for (int i = 0; i < transformsOfReadCube.Count; i++)
        {
            if (transformsOfReadCube[i].position.z > notedTransform.position.z)
            {
                notedTransform = transformsOfReadCube[i];
            }
        }

        if (notedTransform == readCube.tUp)
        {
            side = cubeState.upTiles;
        }
        else if (notedTransform == readCube.tDown)
        {
            side = cubeState.downTiles;
        }
        else if (notedTransform == readCube.tFront)
        {
            side = cubeState.frontTiles;
        }
        else if (notedTransform == readCube.tBack)
        {
            side = cubeState.backTiles;
        }
        else if (notedTransform == readCube.tLeft)
        {
            side = cubeState.leftTiles;
        }
        else if (notedTransform == readCube.tRight)
        {
            side = cubeState.rightTiles;
        }

        return side;
    }

    private List<GameObject> GetRightmostTransform()
    {
        List<Transform> transformsOfReadCube = new List<Transform>();
        transformsOfReadCube.Add(readCube.tUp);
        transformsOfReadCube.Add(readCube.tDown);
        transformsOfReadCube.Add(readCube.tLeft);
        transformsOfReadCube.Add(readCube.tRight);
        transformsOfReadCube.Add(readCube.tFront);
        transformsOfReadCube.Add(readCube.tBack);

        List<GameObject> side = null;
        Transform notedTransform = transformsOfReadCube[0];
        for (int i = 0; i < transformsOfReadCube.Count; i++)
        {
            if (transformsOfReadCube[i].position.z < notedTransform.position.z)
            {
                notedTransform = transformsOfReadCube[i];
            }
        }

        if (notedTransform == readCube.tUp)
        {
            side = cubeState.upTiles;
        }
        else if (notedTransform == readCube.tDown)
        {
            side = cubeState.downTiles;
        }
        else if (notedTransform == readCube.tFront)
        {
            side = cubeState.frontTiles;
        }
        else if (notedTransform == readCube.tBack)
        {
            side = cubeState.backTiles;
        }
        else if (notedTransform == readCube.tLeft)
        {
            side = cubeState.leftTiles;
        }
        else if (notedTransform == readCube.tRight)
        {
            side = cubeState.rightTiles;
        }

        return side;
    }
    #endregion

    #region FRONT AND BACK GETTERS
    private List<GameObject> GetFrontmostTransform()
    {
        List<Transform> transformsOfReadCube = new List<Transform>();
        transformsOfReadCube.Add(readCube.tUp);
        transformsOfReadCube.Add(readCube.tDown);
        transformsOfReadCube.Add(readCube.tLeft);
        transformsOfReadCube.Add(readCube.tRight);
        transformsOfReadCube.Add(readCube.tFront);
        transformsOfReadCube.Add(readCube.tBack);

        List<GameObject> side = null;
        Transform notedTransform = transformsOfReadCube[0];
        for (int i = 0; i < transformsOfReadCube.Count; i++)
        {
            if (transformsOfReadCube[i].position.x > notedTransform.position.x)
            {
                notedTransform = transformsOfReadCube[i];
            }
        }

        if (notedTransform == readCube.tUp)
        {
            side = cubeState.upTiles;
        }
        else if (notedTransform == readCube.tDown)
        {
            side = cubeState.downTiles;
        }
        else if (notedTransform == readCube.tFront)
        {
            side = cubeState.frontTiles;
        }
        else if (notedTransform == readCube.tBack)
        {
            side = cubeState.backTiles;
        }
        else if (notedTransform == readCube.tLeft)
        {
            side = cubeState.leftTiles;
        }
        else if (notedTransform == readCube.tRight)
        {
            side = cubeState.rightTiles;
        }

        return side;
    }

    private List<GameObject> GetBackmostTransform()
    {
        List<Transform> transformsOfReadCube = new List<Transform>();
        transformsOfReadCube.Add(readCube.tUp);
        transformsOfReadCube.Add(readCube.tDown);
        transformsOfReadCube.Add(readCube.tLeft);
        transformsOfReadCube.Add(readCube.tRight);
        transformsOfReadCube.Add(readCube.tFront);
        transformsOfReadCube.Add(readCube.tBack);

        List<GameObject> side = null;
        Transform notedTransform = transformsOfReadCube[0];
        for (int i = 0; i < transformsOfReadCube.Count; i++)
        {
            if (transformsOfReadCube[i].position.x < notedTransform.position.x)
            {
                notedTransform = transformsOfReadCube[i];
            }
        }

        if (notedTransform == readCube.tUp)
        {
            side = cubeState.upTiles;
        }
        else if (notedTransform == readCube.tDown)
        {
            side = cubeState.downTiles;
        }
        else if (notedTransform == readCube.tFront)
        {
            side = cubeState.frontTiles;
        }
        else if (notedTransform == readCube.tBack)
        {
            side = cubeState.backTiles;
        }
        else if (notedTransform == readCube.tLeft)
        {
            side = cubeState.leftTiles;
        }
        else if (notedTransform == readCube.tRight)
        {
            side = cubeState.rightTiles;
        }

        return side;
    }
    #endregion

    public void RandomRotation()
    {
        int random = Random.Range(0, 12);
        switch (random)
        {
            case 0:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(true);
                if (GetHighestTransform() == cubeState.backTiles || GetHighestTransform() == cubeState.downTiles || GetHighestTransform() == cubeState.rightTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(false);
                }
                cubeState.PickUp(GetHighestTransform());
                break;
            case 1:
                GameManager.Instance.SetIsTurningLeft(true);
                if (GetLowestTransform() == cubeState.frontTiles || GetLowestTransform() == cubeState.upTiles || GetLowestTransform() == cubeState.leftTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(false);
                }
                cubeState.PickUp(GetLowestTransform());
                break;
            case 2:
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetHighestTransform() == cubeState.backTiles || GetHighestTransform() == cubeState.downTiles || GetHighestTransform() == cubeState.rightTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetHighestTransform());

                break;
            case 3:
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetLowestTransform() == cubeState.frontTiles || GetLowestTransform() == cubeState.upTiles || GetLowestTransform() == cubeState.leftTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetLowestTransform());

                break;
            case 4:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(true);
                if (GetLeftmostTransform() == cubeState.backTiles || GetLeftmostTransform() == cubeState.downTiles || GetLeftmostTransform() == cubeState.rightTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(false);
                }
                cubeState.PickUp(GetLeftmostTransform());
                break;
            case 5:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetLeftmostTransform() == cubeState.backTiles || GetLeftmostTransform() == cubeState.downTiles || GetLeftmostTransform() == cubeState.rightTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetLeftmostTransform());
                break;
            case 6:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(true);
                if (GetRightmostTransform() == cubeState.frontTiles || GetRightmostTransform() == cubeState.upTiles || GetRightmostTransform() == cubeState.leftTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(false);
                }
                cubeState.PickUp(GetRightmostTransform());
                break;
            case 7:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetRightmostTransform() == cubeState.frontTiles || GetRightmostTransform() == cubeState.upTiles || GetRightmostTransform() == cubeState.leftTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetRightmostTransform());
                break;
            case 8:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetFrontmostTransform() == cubeState.backTiles || GetFrontmostTransform() == cubeState.downTiles || GetFrontmostTransform() == cubeState.rightTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetFrontmostTransform());
                break;
            case 9:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(true);
                if (GetFrontmostTransform() == cubeState.backTiles || GetFrontmostTransform() == cubeState.downTiles || GetFrontmostTransform() == cubeState.rightTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(false);
                }
                cubeState.PickUp(GetFrontmostTransform());
                break;
            case 10:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetBackmostTransform() == cubeState.frontTiles || GetBackmostTransform() == cubeState.upTiles || GetBackmostTransform() == cubeState.leftTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetBackmostTransform());
                break;
            case 11:
                FindFirstObjectByType<ReadCube>().ReadState();
                GameManager.Instance.SetIsTurningLeft(false);
                if (GetBackmostTransform() == cubeState.frontTiles || GetBackmostTransform() == cubeState.upTiles || GetBackmostTransform() == cubeState.leftTiles)
                {
                    GameManager.Instance.SetIsTurningLeft(true);
                }
                cubeState.PickUp(GetBackmostTransform());
                break;
            default:
                break;
        }
    }
}
