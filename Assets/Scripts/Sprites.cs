using UnityEngine;
using System.Collections;

public class Sprites
{
    private static Sprites instance;
    public Sprite[] items;
    public Sprite[] placeholders;

    private Sprites() 
    {
        items = Resources.LoadAll<Sprite>("Items");
        placeholders = Resources.LoadAll<Sprite>("Placeholders");
    }

    public static Sprites Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Sprites();
            }
            return instance;
        }
    }
}
