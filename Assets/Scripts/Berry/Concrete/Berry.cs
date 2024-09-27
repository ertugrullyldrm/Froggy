using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour,IBerry
{

    [SerializeField] private FrogColor _Berry_Color;
    public FrogColor Berry_Color
    {
        get => _Berry_Color;
        set => _Berry_Color = value;
    }
    [SerializeField] private string _BarryTag;
    public string BarryTag
    {
        get => _BarryTag;
        set => _BarryTag = value;
    }
    [SerializeField] private Material[] colorMaterial;
    void Start()
    {
        Vector3 newPosition = this.gameObject.transform.position;

        newPosition.y = 0.3f;
        this.gameObject.transform.position = newPosition;
        switch (_Berry_Color)
        {
            case FrogColor.red:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[0];
                _BarryTag = "red";
                break;
            case FrogColor.blue:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[1];
                _BarryTag = "blue";
                break;
            case FrogColor.green:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[2];
                _BarryTag = "green";
                break;
            case FrogColor.yellow:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[3];
                _BarryTag = "yellow";
                break;
            case FrogColor.purple:
                this.gameObject.GetComponentInChildren<Renderer>().material = colorMaterial[4];
                _BarryTag = "purple";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
