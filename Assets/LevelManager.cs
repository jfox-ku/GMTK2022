using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<LevelController> Levels;
    public IntVariable CurrentLevel;

    public static bool Block = false;

    private void Awake()
    {
        Block = true;
        Instance = this;
    }

    private void Start()
    {
        if(Instance.CurrentLevel.Value!=0) StartLevel();
    }

    public static void StartLevel()
    {
        Block = false;
        Instantiate(Instance.Levels[Instance.CurrentLevel.Value%Instance.Levels.Count]);
    }

    public void IncreaseLevel()
    {
        CurrentLevel.Value++;
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
}
