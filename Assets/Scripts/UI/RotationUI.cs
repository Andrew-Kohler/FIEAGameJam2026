using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RotationUI : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;
    [SerializeField] private ReadCube readCube;

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
    }

    public void TurnBotToLeft()
    {
        GameManager.Instance.SetIsTurningLeft(true);
        cubeState.PickUp(GetLowestTransform());
    }

    public void TurnTopToRight()
    {
        GameManager.Instance.SetIsTurningLeft(false);
        if (GetHighestTransform() == cubeState.backTiles || GetHighestTransform() == cubeState.downTiles || GetHighestTransform() == cubeState.rightTiles)
        {
            GameManager.Instance.SetIsTurningLeft(false);
        }
        cubeState.PickUp(GetHighestTransform());
    }

    public void TurnBotToRight()
    {
        GameManager.Instance.SetIsTurningLeft(false);
        cubeState.PickUp(GetLowestTransform());
    }
    #endregion

    #region LEFT AND RIGHT TURNS
    public void TurnLeftToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        cubeState.PickUp(GetLeftmostTransform());
    }

    public void TurnLeftToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        cubeState.PickUp(GetLeftmostTransform());
    }

    public void TurnRightToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        cubeState.PickUp(GetRightmostTransform());
    }

    public void TurnRightToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        cubeState.PickUp(GetRightmostTransform());
    }
    #endregion

    #region FRONT AND BACK TURNS
    public void TurnFrontToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        cubeState.PickUp(GetFrontmostTransform());
    }

    public void TurnFrontToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        cubeState.PickUp(GetFrontmostTransform());
    }

    public void TurnBackToUp()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(false);
        cubeState.PickUp(GetBackmostTransform());
    }

    public void TurnBackToDown()
    {
        FindFirstObjectByType<ReadCube>().ReadState();
        GameManager.Instance.SetIsTurningLeft(true);
        cubeState.PickUp(GetBackmostTransform());
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
