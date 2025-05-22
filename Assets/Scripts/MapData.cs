using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public int[,] mData = new int[,] {

        { 0, 0, 0, 1, 1, 0, 0, 0, 1 },
        { 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 1, 1, 1, 0, 1 }



    };

    //public TextAsset textMap;

    /*private void Start()
    {
        Debug.Log(textMap.text);
    }
    */
    
}
