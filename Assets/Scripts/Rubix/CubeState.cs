using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeState : MonoBehaviour
{
    public List<GameObject> frontTiles = new List<GameObject>();
    public List<GameObject> backTiles = new List<GameObject>();
    public List<GameObject> leftTiles = new List<GameObject>();
    public List<GameObject> rightTiles = new List<GameObject>();
    public List<GameObject> upTiles = new List<GameObject>();
    public List<GameObject> downTiles = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                // Parent of tile = cube
                // Parent of cube = the middle cube now
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
            {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }
    }
}
