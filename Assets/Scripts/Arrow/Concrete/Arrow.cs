using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour,IArrow
{
    [SerializeField] private FrogColor _Arrow_Color;
    public FrogColor Arrow_Color
    {
        get => _Arrow_Color;
        set => _Arrow_Color = value;
    }

    [SerializeField] private LookType _Look_type;
    public LookType turnPos
    {
        get => _Look_type;
        set => _Look_type = value;
    }
    [SerializeField] private Material[] colorMaterial;
    void Start()
    {
        Vector3 newPosition = this.gameObject.transform.position;

        newPosition.y = 0.3f;
        this.gameObject.transform.position = newPosition;

        switch (_Look_type)
        {
            case LookType.left:
                this.gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case LookType.right:
                this.gameObject.transform.rotation = Quaternion.Euler(-90, 180, 0);
                break;
            case LookType.bottom:
                this.gameObject.transform.rotation = Quaternion.Euler(-90, -90, 0);
                break;
            case LookType.forward:
                this.gameObject.transform.rotation = Quaternion.Euler(-90, 90, 0);
                break;

        }

        switch (_Arrow_Color)
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
    }

    void Update()
    {
        
    }
}
