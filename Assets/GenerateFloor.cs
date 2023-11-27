using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFloor : MonoBehaviour
{
    public GameObject floorTileWhite;
    public GameObject floorTileBlack;
    public GameObject BorderPrefab;
    public int x, y;
    public float tileSize = 1.0f; // Adjust the size of each tile

    private void Awake()
    {
        if (x % 2 == 0 || y % 2 == 0)
        {
            Debug.LogWarning("x and y should be odd for a center-aligned grid.");
        }

        int xOffset = x / 2;
        int yOffset = y / 2;

        for (int i = -xOffset; i <= xOffset; i++)
        {
            for (int j = -yOffset; j <= yOffset; j++)
            {
                GameObject tilePrefab = ((i + j) % 2 == 0) ? floorTileWhite : floorTileBlack;

                Instantiate(tilePrefab, new Vector3(i * tileSize, -1, j * tileSize), Quaternion.identity);
                tilePrefab.transform.parent = this.transform;
            }
        }

        // Spawn border tiles with a height of 3 units
        SpawnBorderTiles(xOffset, yOffset);
    }

    void SpawnBorderTiles(int xOffset, int yOffset)
    {
        // Define the height of the border tiles
        float borderHeight = 0.6f;

        // Create border tiles on the X-axis
        for (int i = -xOffset ; i <= xOffset; i++)
        {
            Instantiate(BorderPrefab, new Vector3(i * tileSize, borderHeight / 2, yOffset * tileSize), Quaternion.identity);
            Instantiate(BorderPrefab, new Vector3(i * tileSize, borderHeight / 2, -yOffset * tileSize), Quaternion.identity);
        }

        // Create border tiles on the Y-axis
        for (int j = -yOffset; j <= yOffset; j++)
        {
            Instantiate(BorderPrefab, new Vector3(xOffset * tileSize, borderHeight / 2, j * tileSize), Quaternion.identity);
            Instantiate(BorderPrefab, new Vector3(-xOffset * tileSize, borderHeight / 2, j * tileSize), Quaternion.identity);
        }
    }
}
