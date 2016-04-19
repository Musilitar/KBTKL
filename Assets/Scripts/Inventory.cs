using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    private List<Item> items;
    private List<GameObject> availableInventoryItems, usedInventoryItems, sideMenuOptions;
    public Text itemDescription, sideMenuUse, sideMenuEquip, sideMenuDelete;
    public GameObject selector, sideMenu, sideMenuSelector;
    private int selectedItem, selectedOption;
    private bool managing;

    public List<Item> Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }

	// Use this for initialization
	void Start() {
        InitialiseInventory();
	}
	
	// Update is called once per frame
	void Update() {
        CheckForMovement();
        CheckForManagement();
	}

    public void InitialiseInventory()
    {
        gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(false);
        sideMenuUse.gameObject.SetActive(false);
        sideMenuEquip.gameObject.SetActive(false);
        sideMenuDelete.gameObject.SetActive(false);
        selector.SetActive(false);
        sideMenu.SetActive(false);
        sideMenuSelector.SetActive(false);
        selectedItem = 0;
        selectedOption = 0;
        managing = false;
        items = new List<Item>();
        availableInventoryItems = new List<GameObject>();
        usedInventoryItems = new List<GameObject>();
        sideMenuOptions = new List<GameObject>();
        foreach (Transform item in transform)
        {
            if (item.tag == "InventoryItem")
            {
                item.gameObject.SetActive(false);
                availableInventoryItems.Add(item.gameObject);
            }
        }
        foreach (Transform item in sideMenu.transform)
        {
            if (item.tag == "SideMenuOption")
            {
                item.gameObject.SetActive(false);
                sideMenuOptions.Add(item.gameObject);
            }
        }
    }

    public void UpdateInventory(Item item)
    {
        if (availableInventoryItems.Count > 0)
        {
            GameObject inventoryItem = availableInventoryItems[0];
            inventoryItem.GetComponent<SpriteRenderer>().sprite = Sprites.Instance.items[item.Id];
            inventoryItem.SetActive(true);
            usedInventoryItems.Add(inventoryItem);
            availableInventoryItems.RemoveAt(0);
            selector.SetActive(true);
        }
    }

    public void LoadInventory()
    {
        foreach (Item item in items)
        {
            UpdateInventory(item);
        }
    }

    public void CheckForMovement()
    {
        if (!managing)
        {
            if (Input.GetKeyDown(Controls.Instance.right))
            {
                if (selectedItem + 1 < usedInventoryItems.Count)
                {
                    selector.transform.position = usedInventoryItems[selectedItem + 1].transform.position;
                    selectedItem++;
                }
                else
                {
                    selector.transform.position = usedInventoryItems[0].transform.position;
                    selectedItem = 0;
                }
                itemDescription.text = items[selectedItem].Description;
            }
            if (Input.GetKeyDown(Controls.Instance.left))
            {
                if (selectedItem - 1 >= 0)
                {
                    selector.transform.position = usedInventoryItems[selectedItem - 1].transform.position;
                    selectedItem--;
                }
                else
                {
                    selector.transform.position = usedInventoryItems[usedInventoryItems.Count - 1].transform.position;
                    selectedItem = usedInventoryItems.Count - 1;
                }
                itemDescription.text = items[selectedItem].Description;
            }
        }
        else
        {
            if (Input.GetKeyDown(Controls.Instance.up))
            {
                if (selectedOption - 1 >= 0)
                {
                    sideMenuSelector.transform.position = sideMenuOptions[selectedOption - 1].transform.position;
                    selectedOption--;
                }
                else
                {
                    sideMenuSelector.transform.position = sideMenuOptions[sideMenuOptions.Count - 1].transform.position;
                    selectedOption = sideMenuOptions.Count - 1;
                }
            }
            if (Input.GetKeyDown(Controls.Instance.down))
            {
                if (selectedOption + 1 < sideMenuOptions.Count)
                {
                    sideMenuSelector.transform.position = sideMenuOptions[selectedOption + 1].transform.position;
                    selectedOption++;
                }
                else
                {
                    sideMenuSelector.transform.position = sideMenuOptions[0].transform.position;
                    selectedOption = 0;
                }
            }
        }
    }

    public void CheckForManagement()
    {
        if (Input.GetKeyDown(Controls.Instance.select))
        {
            if (!managing)
            {
                managing = !managing;
                ToggleSideMenu();
            }
            else
            {
                managing = !managing;
                DoSelectedOption();
            }
        }
        if (Input.GetKeyDown(Controls.Instance.back))
        {
            managing = !managing;
            ToggleSideMenu();
        }
    }

    public void ToggleAt(Vector3 position)
    {
        transform.position = position;
        itemDescription.rectTransform.position = new Vector3(position.x, position.y - 3, 0);
        gameObject.SetActive(!gameObject.activeSelf);
        itemDescription.gameObject.SetActive(!itemDescription.gameObject.activeSelf);
        if(items.Count > 0)
        {
            itemDescription.text = items[0].Description;
        }
    }

    public void ToggleSideMenu()
    {
        sideMenu.transform.position = new Vector3(selector.transform.position.x + 0.925f, selector.transform.position.y, 0);
        sideMenu.gameObject.SetActive(!sideMenu.gameObject.activeSelf);
        sideMenuSelector.transform.position = new Vector3(sideMenu.transform.position.x, sideMenu.transform.position.y + 0.435f, -1);
        sideMenuSelector.gameObject.SetActive(!sideMenuSelector.gameObject.activeSelf);
        sideMenuUse.rectTransform.position = sideMenuOptions[0].transform.position;
        sideMenuUse.gameObject.SetActive(!sideMenuUse.gameObject.activeSelf);
        sideMenuEquip.rectTransform.position = sideMenuOptions[1].transform.position;
        sideMenuEquip.gameObject.SetActive(!sideMenuEquip.gameObject.activeSelf);
        sideMenuDelete.rectTransform.position = sideMenuOptions[2].transform.position;
        sideMenuDelete.gameObject.SetActive(!sideMenuDelete.gameObject.activeSelf);
    }

    public void DoSelectedOption()
    {
        if (selectedOption == 2)
        {
            ToggleSideMenu();
            items.RemoveAt(selectedItem);
            usedInventoryItems[selectedItem].SetActive(false);
            List<GameObject> newAvailableInventoryItems = new List<GameObject>();
            newAvailableInventoryItems.Add(usedInventoryItems[selectedItem]);
            newAvailableInventoryItems.AddRange(availableInventoryItems);
            availableInventoryItems = newAvailableInventoryItems;
            usedInventoryItems.RemoveAt(selectedItem);
            if (items.Count == 0)
            {
                selector.transform.position = usedInventoryItems[0].transform.position;
                selector.SetActive(false);
                itemDescription.text = "";
            }
            else if (items.Count == 1)
            {
                selector.transform.position = usedInventoryItems[0].transform.position;
                selectedItem = 0;
                itemDescription.text = items[0].Description;
            }
            else if (selectedItem - 1 >= 0)
            {
                selector.transform.position = usedInventoryItems[selectedItem - 1].transform.position;
                selectedItem--;
            }
            else
            {
                selector.transform.position = usedInventoryItems[0].transform.position;
                selectedItem = 0;
                itemDescription.text = items[0].Description;
            }
        }
    }
}
