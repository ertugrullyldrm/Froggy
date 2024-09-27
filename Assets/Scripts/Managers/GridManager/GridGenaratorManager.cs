using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenaratorManager : MonoBehaviour
{
    public int gridSize;
    public int numberOfLevels = 2;  // Kat sayýsý (y ekseni için)
    public float levelHeight = 1.0f;

    public GameObject[,,] grid;
    public GameObject cellPrefab;

    private void Awake()
    {
        // InitializeGrid();
    }

    void Start()
    {
        
        if (grid == null || grid.Length == 0)
        {
            InitializeGrid();
           
        }


        StartCoroutine(HideLowLevel());
    }

    void Update()
    {
        
        
    }

    IEnumerator HideLowLevel()
    {
        GameManager.Instance.FrogCount= GameObject.FindGameObjectsWithTag("Frog").Length;
        yield return new WaitForSeconds(0.1f);
        var newCell = GameObject.FindGameObjectsWithTag("Cell");

        foreach(var a in newCell)
        {
            if(a.GetComponent<Cell>().y< numberOfLevels - 1)
            {
                a.transform.GetChild(0).gameObject.SetActive(false);
                
            }

        }

    }

    public void GenerateGrid()
    {
        grid = new GameObject[gridSize, numberOfLevels, gridSize];

        for (int y = 0; y < numberOfLevels; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 position = new Vector3(x, y * levelHeight, z);
                    GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                    grid[x, y, z] = cell;
                }
            }
        }
        

    }

   
    void InitializeGrid()
    {
        grid = new GameObject[gridSize, numberOfLevels, gridSize];

        Cell[] allCells = FindObjectsOfType<Cell>();

        foreach (Cell cell in allCells)
        {
            Vector3 position = cell.transform.position;

            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y*10);
            Debug.Log(y);
            int z = Mathf.RoundToInt(position.z);

            if (x >= 0 && x <= gridSize && y >= 0 && y <= numberOfLevels && z >= 0 && z <= gridSize)
            {
                grid[x, y, z] = cell.gameObject;
                cell.x = x;
                cell.y = y;
                cell.z = z;
            }
        }
        
        Debug.Log(grid.Length);
    }


    public void RemoveEmptyCells()
    {
        for (int y = 0; y < numberOfLevels; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    GameObject cell = grid[x, y, z];
                    if (cell != null)
                    {
                        if (cell.transform.childCount == 0)
                        {
                            if (y > 0)
                            {
                                Destroy(cell);
                                grid[x, y, z] = null;

                                GameObject bottomCell = grid[x, y - 1, z];
                                if (bottomCell != null)
                                {
                                    foreach (Transform child in bottomCell.transform)
                                    {
                                        if (!child.gameObject.activeSelf)
                                        {
                                            child.gameObject.SetActive(true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
