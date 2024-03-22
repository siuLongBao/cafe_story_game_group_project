using System.Diagnostics;
using UnityEngine;

public class StorageCounter : BasicCounter
{
    // Public Variables
    public Item stored;

    // Private
    private Vector3 storageInitialLocation = new Vector3(0, 0.75f, 0);
    private Vector3 spawnPlayerLocalPosition = new Vector3(0f, 1.7f, 0.8f);


    // Give the user 1 stored item
    public override void Interact(Player player)
    {
        //Debug.log("Interact method in StorageCounter was called.");
        if (stored != null)
        {
            if (player.holdingSth == null)
            {
                //Debug.log("Instantiating stored object at position: " + (transform.position + storageInitialLocation));
                GameObject instantiatedGameObject = Instantiate(stored.gameObject, transform.position + storageInitialLocation, stored.transform.rotation);
                Item instantiatedItem = instantiatedGameObject.GetComponent<Item>();
                player.holdingSth = instantiatedItem;

                // Set the parent of the instantiated item to the player
                instantiatedItem.transform.SetParent(player.transform, false);

                // Set the local position of the instantiated item
                instantiatedItem.transform.localPosition = spawnPlayerLocalPosition;
            } else if (player.holdingSth.gameObject.CompareTag("Plate"))
            {
                player.holdingSth.gameObject.GetComponent<Plate>().AddFood(stored);
            }
        }
        else
        {
            //Debug.LogError("Stored item not assigned!");
        }
    }

}
