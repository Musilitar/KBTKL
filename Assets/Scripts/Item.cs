using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{
    private int id, price;
    private string description;

    public Item(int id, int price, string description)
    {
        this.id = id;
        this.price = price;
        this.description = description;
    }

    public int Id
    {
        get { return id; }
        set { id = value;}
    }

    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public static Item GenerateItem()
    {
        return new Item(Random.Range(0, 4), 1, "An item!");
    }
}
