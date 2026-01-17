using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeMap : MonoBehaviour
{
    CubeState cubeState;

    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {

    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach(Transform map in side)
        {
            if (face[i].GetComponent<Tile>().tileType == Tile.TileType.Wheat)
            {

            }
        }
    }
}
