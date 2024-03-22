using System.Collections;
using UnityEngine;

public class CoffeeCounter : BasicCounter
{

    public override void Interact(Player player)
    {
        // Check if the player is holding a Cup
        Cup cup = player.holdingSth?.GetComponent<Cup>();
        if (cup != null && !cup.filledCoffee)
        {
            // Disable player movement
            player.enabled = false;

            // Start the coffee making coroutine
            StartCoroutine(MakeCoffee(player, cup));
        }
    }

    private IEnumerator MakeCoffee(Player player, Cup cup)
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Change the color of the cup
        cup.ChangeColor();

        // Re-enable player movement
        player.enabled = true;
    }
}
