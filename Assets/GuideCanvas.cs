using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class GuideCanvas : MonoBehaviour
{
    public IntVariable Level;

    public GameObject Guide;
    // Start is called before the first frame update
    void Start()
    {
        Guide.SetActive(Level.Value == 0);
    }

    public void Continue()
    {
        Guide.SetActive(false);
        LevelManager.StartLevel();
    }
    
}
