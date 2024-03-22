using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverCounter : Interactable
{

    // Interaction method called from Player.cs
    public override void Interact(Player player)
    {
        // If player has a holding item and the item is a plate, it will check the dishes with active orders
        Item heldItem = player.GetHoldItem();
        if(heldItem!=null && heldItem.CompareTag("Plate")){
            Plate plate = heldItem as Plate;
            if (plate.onPlate != null){
                 HandlePlateDelivery(plate.onPlate,player);
            }
        }
        else
        {
            // Handle scenarios where the player doesn't hold a plate.
            // Todo:You can display a UI message, play a sound, or any other feedback mechanism.
        }
    }

    // Placeholder function to handle plate delivery.
    private void HandlePlateDelivery(List<string> deliveredDishes, Player player)
    {
        bool isDeliveryValid = DeliveryManager.Instance.ValidateDelivery(deliveredDishes);
        if(isDeliveryValid){
            player.RemoveHoldingItem();
        }
        else{
            // Handle incorrect delivery feedback.
            Debug.Log("Deliver failed.");
            // Todo: you might play an error sound, show a UI message, etc.
        }
    }

}
