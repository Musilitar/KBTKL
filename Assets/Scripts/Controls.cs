using UnityEngine;
using System.Collections;

public class Controls
{
    private static Controls instance;
    public KeyCode up, right, down, left, inventory, save, load, select, back, interact;

    private Controls() 
    {
        up = KeyCode.UpArrow;
        right = KeyCode.RightArrow;
        down = KeyCode.DownArrow;
        left = KeyCode.LeftArrow;
        inventory = KeyCode.I;
        save = KeyCode.F5;
        load = KeyCode.F9;
        select = KeyCode.Return;
        back = KeyCode.Backspace;
        interact = KeyCode.T;
    }

    public static Controls Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Controls();
            }
            return instance;
        }
    }
}
