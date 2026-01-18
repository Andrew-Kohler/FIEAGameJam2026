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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
    }

    public void TurnBotToLeft()
    {
        GameManager.Instance.SetIsTurningLeft(true);
        if (GetLowestTransform() == cubeState.frontTiles || GetLowestTransform() == cubeState.upTiles || GetLowestTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetLowestTransform());

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
    }

    public void TurnTopToRight()
    {
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetHighestTransform() == cubeState.backTiles || GetHighestTransform() == cubeState.downTiles || GetHighestTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetHighestTransform());

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
    }

    public void TurnBotToRight()
    {
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetLowestTransform() == cubeState.frontTiles || GetLowestTransform() == cubeState.upTiles || GetLowestTransform() == cubeState.leftTiles)
        {
            GameManager.Instance.SetIsTurningLeft(true);
        }
        cubeState.PickUp(GetLowestTransform());

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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

        primaryMenu.ToggleRotationMode();
        this.gameObject.SetActive(false);
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
}
