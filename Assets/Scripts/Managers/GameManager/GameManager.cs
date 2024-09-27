using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int ClickCount;
    public bool isFrogClicked = false;
    public int moves = 5;
    private int MovesData;
    public bool isGameFinish = false;
    public int FrogCount;
    public static GameManager Instance { get; private set; }

    public GridGenaratorManager gridManager;
    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            
            Destroy(gameObject);
        }
    }

    void Start()
    {
        MovesData = moves;
        

       
    }

    
    void Update()
    {
        gridManager = FindObjectOfType<GridGenaratorManager>();
    }

    public void RestartGame()
    {
        moves = MovesData;
        GameManager.Instance.FrogCount = 1;
        GameManager.Instance.isGameFinish = false;
    }
}
