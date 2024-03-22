using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : Interactable
{
    // Public Variable
    public Plate plateScript;

    // Private Variable
    private Vector3 storageInitialLocation = new Vector3(0, 0.75f, 0);
    private float spawnPlateTimeInterval = 5.0f;
    private int maxNumOfPlate = 3;
    private int totalNumOfPlates; // Records number of plates in total
    private int platesOnCounter; // Records the number of plates on counter
    private bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        // Initially all plates are on plates counter
        totalNumOfPlates = maxNumOfPlate;
        platesOnCounter = maxNumOfPlate;
    }

    // Update is called once per frame
    void Update()
    {
        // If the total number of plates in game is less than maximum allowed and spawn cool down is over, plate is spawned on plate counter
        if (totalNumOfPlates < maxNumOfPlate && canSpawn)
        {
            totalNumOfPlates++;
            platesOnCounter++;
            canSpawn = false;
            StartCoroutine(spawnPlateTimer());
        }
    }

    public override void Interact(Player player)
    {
        // If the player is holding nothing and there are plates on plate counter, player can take one plate
        if (player.holdingSth == null && platesOnCounter > 0)
        {
            GameObject plateGameObject = Instantiate(plateScript.gameObject, this.transform.position + storageInitialLocation, plateScript.transform.rotation);
            Item plateInstantiated = plateGameObject.GetComponent<Item>();
            player.holdingSth = plateInstantiated;
            platesOnCounter--;
            
            plateInstantiated.transform.SetParent(player.transform, false);
            plateInstantiated.transform.localPosition = new Vector3(0, 1.0f, 0.5f);
        }
    }

    // This function is called when dish is delivered or when plate is put in trash bin
    public void platesDestroyed()
    {
        totalNumOfPlates++;
    }

    // Timer that counts the time that plate counter cannot spawn plate
    IEnumerator spawnPlateTimer()
    {
        yield return new WaitForSeconds(spawnPlateTimeInterval);
        canSpawn = true;
    }
}
