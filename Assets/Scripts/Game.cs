using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game
{
    public static Game current = new Game();
    public List<Item> items;
    public float x, y;

    public Game() 
    {
    }
}
