using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour,ICell
{
    [SerializeField] private FrogColor _Cell_Color;
    public LookType LookRotation;
    [SerializeField] private CellType _Cell_type;

    public FrogColor Cell_Color
    {
        get => _Cell_Color;
        set => _Cell_Color = value;
    }

    
    public CellType Cell_type
    {
        get => _Cell_type;
        set => _Cell_type = value;
    }

    [SerializeField] private int _x;
    public int x
    {
        get => _x;
        set => _x = value;
    }
    [SerializeField] private int _y;
    public int y
    {
        get => _y;
        set => _y = value;
    }
    [SerializeField] private int _z;
    public int z
    {
        get => _z;
        set => _z = value;
    }

    [SerializeField] private Material[] colorMaterial;
    [SerializeField] private GameObject[] GameThing;

    public bool isTwiace;
    public bool isFirstGone;
    void Start()
    {
        switch (_Cell_Color)
        {
            case FrogColor.red:
                this.gameObject.GetComponent<Renderer>().material = colorMaterial[0];
                break;
            case FrogColor.blue:
                this.gameObject.GetComponent<Renderer>().material = colorMaterial[1];
                break;
            case FrogColor.green:
                this.gameObject.GetComponent<Renderer>().material = colorMaterial[2];
                break;
            case FrogColor.yellow:
                this.gameObject.GetComponent<Renderer>().material = colorMaterial[3];
                break;
            case FrogColor.purple:
                this.gameObject.GetComponent<Renderer>().material = colorMaterial[4];
                break;
        }
        GenarateThing();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RestarCell());
    }
    
    public void GenarateThing()
    {

        switch (_Cell_type)
        {
            case CellType.berry:
                
                GameObject berry = Instantiate(GameThing[0], transform.position, Quaternion.identity);
                berry.GetComponent<Berry>().Berry_Color = _Cell_Color;
                berry.transform.parent = transform;
                
                break;
            case CellType.frog:
                GameObject frog = Instantiate(GameThing[1], transform.position, Quaternion.identity);
                frog.GetComponent<Frog>().Look_type = LookRotation;
                frog.GetComponent<Frog>().Frog_Color = _Cell_Color;
                frog.transform.parent = transform;
                GameManager.Instance.FrogCount++;


                break;
            case CellType.rotate:
                GameObject rotate = Instantiate(GameThing[2], transform.position, Quaternion.identity);
                rotate.GetComponent<Arrow>().Arrow_Color = _Cell_Color;
                rotate.GetComponent<Arrow>().turnPos = LookRotation;
                rotate.transform.parent = transform;
                
                break;

        }
    }

    IEnumerator RestarCell()
    {
        yield return null;
        GameManager.Instance.gridManager.RemoveEmptyCells();
    }

}
