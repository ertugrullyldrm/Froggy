using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour, IFrog
{
    [SerializeField] private FrogColor _frogColor;
    public List<GameObject> visitedCell = new List<GameObject>();
    
    public bool isFrogClicked = false;

    public GridGenaratorManager gridManager;
    private Cell targetCell;
    public FrogColor Frog_Color
    {
        get => _frogColor;
        set => _frogColor = value;
    }

    [SerializeField] private int _frogToungeSpeed;
    public int frogToungeSpeed
    {
        get => _frogToungeSpeed;
        set => _frogToungeSpeed = value;
    }

    [SerializeField] private GameObject _tonguePrefab;
    public GameObject tonguePrefab
    {
        get => _tonguePrefab;
        set => _tonguePrefab = value;
    }

    [SerializeField] private string _eatableBarryTag;
    public string eatableBarryTag
    {
        get => _eatableBarryTag;
        set => _eatableBarryTag = value;
    }
    [SerializeField] private LookType _Look_type;
    public LookType Look_type
    {
        get => _Look_type;
        set => _Look_type = value;
    }

    [SerializeField] private Material[] colorMaterial;
     private bool isHasTop = false;
    private bool isHasBottom = false;
    public bool isSeeDifferentColor = false;


    void Start()
    {
        gridManager = FindObjectOfType<GridGenaratorManager>();
        Vector3 newPosition = this.gameObject.transform.position;

        newPosition.y = 0.3f;
        this.gameObject.transform.position = newPosition;

        switch (_Look_type)
        {
            case LookType.left:
                this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
              
                break;
            case LookType.right:
                this.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case LookType.bottom:
                this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case LookType.forward:
                this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;

        }

        switch (_frogColor)
        {
            case FrogColor.red:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[0];
                _eatableBarryTag = "red";
                break;
            case FrogColor.blue:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[1];
                _eatableBarryTag = "blue";

                break;
            case FrogColor.green:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[2];
                _eatableBarryTag = "green";

                break;
            case FrogColor.yellow:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[3];
                _eatableBarryTag = "yellow";

                break;
            case FrogColor.purple:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[4];
                _eatableBarryTag = "purple";

                break;
        }

    }

    void Update()
    {

       
       
        
    }

    public void FrogProcess()
    {
        Vector3 ToungePos = new Vector3(0,1f, -.3f);
        GameObject tounge = Instantiate(tonguePrefab, ToungePos, Quaternion.identity);
        tounge.transform.parent = this.transform;
        Debug.Log("Bir kurbaða týklandý");
        GetTargetCell();
        tounge.GetComponent<FrogTounge>().StartDrawing(visitedCell);



    }
    int maxIterations = 1000;
    int iteration = 0;
    void GetTargetCell()
    {
        visitedCell.Clear();
        Vector3 direction = -transform.forward;
        switch (_Look_type)
        {
            case LookType.right:
                direction = Vector3.right;
                break;

            case LookType.left:
                direction = Vector3.left;
                break;
            case LookType.bottom:
                direction = -Vector3.forward;
                break;
            case LookType.forward:
                direction = Vector3.forward;
                break;


        }

        Vector3 position = gameObject.GetComponentInParent<Cell>().transform.position;



        int x = Mathf.RoundToInt(gameObject.GetComponentInParent<Cell>().x);
        int y = Mathf.RoundToInt(gameObject.GetComponentInParent<Cell>().y);
        int z = Mathf.RoundToInt(gameObject.GetComponentInParent<Cell>().z);



        while (iteration < maxIterations)
        {
            iteration++;

            if (iteration >= maxIterations)
            {
                Debug.LogWarning("Maksimum iterasyon sayýsýna ulaþýldý. Döngü sonlandýrýldý.");
            }
            int nextX = x + Mathf.RoundToInt(direction.x);
            int nextY = y;
            int nextZ = z + Mathf.RoundToInt(direction.z);
            if (!isHasTop || !isHasBottom)
            {
                if (nextX < 0 || nextX >= gridManager.gridSize || nextY < 0 || nextY >= gridManager.numberOfLevels || nextZ < 0 || nextZ >= gridManager.gridSize)
                {
                    break;
                }

                var nextCell = gridManager.grid[nextX, nextY, nextZ];

        
                if (nextCell != null)
                {
               
                    if (nextCell.transform.childCount > 0 && nextCell.transform.GetChild(0).gameObject.activeSelf)
                    {
                        if (nextCell.GetComponent<Cell>().Cell_Color != Frog_Color || nextCell.GetComponent<Cell>().Cell_type == CellType.frog)
                        {
                            isSeeDifferentColor = true;
                            break;
                        }
                        else
                        {
                            isSeeDifferentColor = false;
                        }

                        if (nextCell.GetComponent<Cell>().Cell_type == CellType.rotate)
                        {
                            Debug.Log("Rotate hücresi bulundu!");
                            switch (nextCell.GetComponent<Cell>().LookRotation)
                            {
                                case LookType.right:
                                    direction = Vector3.right;
                                    break;
                                case LookType.left:
                                    direction = Vector3.left;
                                    break;
                                case LookType.bottom:
                                    direction = -Vector3.forward; ;
                                    break;
                                case LookType.forward:
                                    direction = Vector3.forward; ;
                                    break;
                            }
                        }
                    }

                    var childObject = nextCell.transform.GetChild(0).gameObject;

                    if (childObject.activeSelf && childObject.activeInHierarchy)
                    {

                        if (!visitedCell.Contains(nextCell))
                        {
                            visitedCell.Add(nextCell);
                            Debug.Log($"Çocuk objesi olmayan hücre listeye eklendi! ({nextX}, {nextY}, {nextZ})");
                        }
                    }

                        

                    
                    x = nextX;
                    y = nextY;
                    z = nextZ;

                    Debug.Log($"Gidilen nokta: ({x}, {y}, {z})");
                }
            }



            for (int level = y + 1; level < gridManager.numberOfLevels; level++)
            {
             
                var upperLevelCell = gridManager.grid[nextX, level, nextZ];
                if (upperLevelCell != null && upperLevelCell.transform.childCount > 0 )
                {

                    var childObject = upperLevelCell.transform.GetChild(0).gameObject;

                    if (childObject.activeSelf && childObject.activeInHierarchy)
                    {
                        isHasTop = true;
                        if (upperLevelCell.GetComponent<Cell>().Cell_Color != _frogColor|| upperLevelCell.GetComponent<Cell>().Cell_type==CellType.frog)
                        {

                            Debug.Log("UpperColor" + upperLevelCell.GetComponent<Cell>().Cell_Color);
                            x = nextX;
                            y = y-1;
                            z = nextZ;
                            isHasTop = false;
                            break;
                        }
                        
                        if (!visitedCell.Contains(upperLevelCell))
                        {


                            visitedCell.Add(upperLevelCell);
                            Debug.Log($"Üst katmanda aktif çocuk bulundu ve listeye eklendi! ({nextX}, {level}, {nextZ})");
                        }
                        x = nextX;
                        y = y;
                        z = nextZ;
                    }
                }




               
            }
            for (int level = y - 1; level >= 0; level--)
            {
                if (level < 0)
                    level = 0;

                var lowerLevelCell = gridManager.grid[nextX, level, nextZ];
                if (this.transform.GetComponentInParent<Cell>().y != level)
                {
                    if (lowerLevelCell != null)
                    {
                        var childObject = lowerLevelCell.transform.GetChild(0).gameObject;

                        if (childObject.activeSelf && childObject.activeInHierarchy)
                        {
                            isHasBottom = true;

                            if (lowerLevelCell.GetComponent<Cell>().Cell_Color != _frogColor || lowerLevelCell.GetComponent<Cell>().Cell_type == CellType.frog)
                            {
                                Debug.Log("LowerColor" + lowerLevelCell.GetComponent<Cell>().Cell_Color);
                                x = nextX;
                                y = y + 1;
                                z = nextZ;
                                isHasBottom = false;
                                break;
                            }

                            if (!visitedCell.Contains(lowerLevelCell))
                            {
                                visitedCell.Add(lowerLevelCell);
                                Debug.Log($"Alt katmanda aktif çocuk bulundu ve listeye eklendi! ({nextX}, {level}, {nextZ})");
                            }

                            x = nextX;
                            y = y;
                            z = nextZ;
                        }

                    }
                    
                }
                    
            }
                
        }
            Debug.Log($"Son nokta: ({x}, {y}, {z})");

            

    }




}

