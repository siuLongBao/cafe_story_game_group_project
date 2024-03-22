using UnityEngine;

public class BasicCounter : Interactable
{
    private Item currentItem = null;  // Reference to the food item currently on the counter

    public virtual void PlaceItem(Item item)
    {
        item.gameObject.transform.SetParent(transform, false);
        item.gameObject.transform.localPosition = Vector3.up * 1.5f;
        currentItem = item; // Set the item on table to be the item on the item
    }

    public override void Interact(Player player)
    {
        Debug.Log("touched basic counter");

        if (player.holdingSth == null && currentItem != null)
        {
            // Player is not holding anything and there is food on the counter, so give the food to the player
            player.holdingSth = currentItem;
            currentItem.transform.SetParent(player.transform, false);
            currentItem.transform.localPosition = new Vector3(0f, 1.7f, 0.8f);  // Adjust these values to place the item where you want it relative to the player
            currentItem = null;  // Clear the reference to the current food item
        }
        else if (player.holdingSth != null && currentItem == null)
        {
            // Player is holding something, so place it on the counter
            PlaceItem(player.holdingSth.GetComponent<Item>());
            player.holdingSth = null;
        }
        // Player is holding food and add food onto plate on counter
        else if (player.holdingSth != null && currentItem.CompareTag("Plate"))
        {
            bool addedToPlate = currentItem.GetComponent<Plate>().AddFood(player.holdingSth);
            // If food is added to plate, player is holding nothing
            if (addedToPlate)
            {
                player.holdingSth = null;
            }
        }
        // Player is holding plate and add food on counter to plate
        else if (player.holdingSth.CompareTag("Plate") && currentItem != null)
        {
            bool addedToPlate = player.holdingSth.GetComponent<Plate>().AddFood(currentItem);
            // If food is added to plate, counter becomes empty
            if (addedToPlate)
            {
                currentItem = null;
            }
        }
    }
}
