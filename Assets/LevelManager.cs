using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<LevelController> Levels;
    public IntVariable CurrentLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Instantiate(Levels[CurrentLevel.Value]);
    }

    public void IncreaseLevel()
    {
        CurrentLevel.Value++;
    }
    
    
}
