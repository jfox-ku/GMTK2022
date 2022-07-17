using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCanvas : MonoBehaviour
{

    public GameObject CompleteText;
    // Start is called before the first frame update

    private void Awake()
    {
        LevelController.LevelComplete += Show;
    }

    void Start()
    {
        CompleteText.SetActive(false);
    }

    public void Show()
    {
        LevelController.LevelComplete -= Show;
        CompleteText.SetActive(true);
    }
}
