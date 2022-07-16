using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceNumberController : MonoBehaviour
{
    public static Action<int> TopNumberChanged;
    public List<Material> Materials;
    public List<Texture> Textures;

    public int TopNum = 0;
    
    
    [Button]
    public void ShiftIndexes(int topNum)
    {
        TopNum = topNum;
        for (int i = 0; i < Materials.Count; i++)
        {
            var mat = Materials[i];
            mat.mainTexture = Textures[(i + 1 + topNum) % 6];
        }
        TopNumberChanged?.Invoke(TopNum);
    }

    public void SetRandomTop()
    {
        ShiftIndexes(Random.Range(0,6));
    }
    
}