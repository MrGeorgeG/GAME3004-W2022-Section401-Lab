using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("World Properties")]
    [Range(8, 64)]
    public int height = 8;
    [Range(8, 64)]
    public int width = 8;
    [Range(8, 64)]
    public int depth = 8;

    [Header("Scaling Values")]
    [Range(8, 64)]
    public float min = 16.0f;
    [Range(8, 64)]
    public float max = 24.0f;

    [Header("Tile Properties")]
    public Transform tileParent;
    public GameObject threeDTile;

    [Header("Grid")]
    public List<GameObject> grid;

    private int startHeight;
    private int startWidth;
    private int startDepth;
    private float startMin;
    private float startMax;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (height != startHeight || depth != startDepth || width != startWidth || min != startMin || max != startMax)
        {
            Generate();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Generate();
        }
    }

    private void Generate()
    {
        Initialize();
        Reset();
        Regenerate();
    }

    private void Initialize()
    {
        startHeight = height;
        startDepth = depth;
        startWidth = width;
        startMin = min;
        startMax = max;
    }

    private void Regenerate()
    {
        //world generation happens.
        float randomScale = Random.Range(min, max);
        float offsetX = Random.Range(-1024.0f, 1024.0f);
        float offsetZ = Random.Range(-1024.0f, 1024.0f);

        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    var perlinValue = Mathf.PerlinNoise((x + offsetX) / randomScale, (z + offsetZ) / randomScale) * depth * 0.5f;

                    if (y < perlinValue)
                    {
                        var tile = Instantiate(threeDTile, new Vector3(x, y, z), Quaternion.identity);
                        tile.transform.SetParent(tileParent);
                        grid.Add(tile);

                    }
                }
            }
        }
    }

    private void Reset()
    {
        foreach (var tile in grid)
        {
            Destroy(tile);
        }
        grid.Clear();
    }
}
