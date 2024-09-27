using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public float raycastDistance = 100f;

    public static Action OnClickFrog;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameFinish)
        {
            if (Input.GetMouseButtonDown(1) && !GameManager.Instance.isFrogClicked) 
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;    
                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    Debug.Log("Bir �ey t�kland�: " + hit.collider.name);
                    Transform clickedObject = hit.collider.transform;
                    Frog frog = clickedObject.GetComponentInChildren<Frog>();
                    if (frog != null)
                    {
                        Debug.Log("Frog se�ildi: " + frog.name);
                        frog.FrogProcess();
                        OnClickFrog?.Invoke();
                    }
                    else
                    {
                        Debug.Log("T�klanan objede Frog bulunamad�.");
                    }
                }
                else
                {
                    Debug.Log("Hi�bir �ey t�klanmad�.");
                }
            }
        }
        
    }
}

