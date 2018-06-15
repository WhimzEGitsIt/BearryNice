using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log(this.gameObject.name);
            Inventory playerInventory = other.gameObject.GetComponent<Inventory>();
            if (playerInventory.AddItem(this))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
