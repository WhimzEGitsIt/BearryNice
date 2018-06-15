using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int SIZE = 10;
    protected Collectable[] _inventory;
    protected int _currentSize;
    

	// Use this for initialization
	void Start () {
        _inventory = new Collectable[SIZE];
        _currentSize = 0;
	}

    public bool AddItem(Collectable item)
    {
        if(_currentSize == SIZE)
        {
            Debug.Log("Inventory full. Cannot carry " + item);
            return false;
        }
        _inventory[_currentSize] = item;
        _currentSize++;
        Debug.Log("Adding item to inventory. Current size: " + _currentSize);
        Debug.Log("name test: " + _inventory[_currentSize - 1].name);
        return true;
    }

    public bool RemoveItem(Collectable item)
    {
        for(int i = 0; i < SIZE; i++)
        {
            if(_inventory[i].Equals(item))
            {
                // remove item
            }
        }
        return true;
    }

    public Collectable[] getInventory()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for(int i = 0; i < SIZE; i++)
        {
            sb.Append(_inventory[i].name);
        }
        Debug.Log(sb.ToString());
        return _inventory;
    }
}
