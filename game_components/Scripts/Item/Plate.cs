using PW;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item
{
    public GameObject emptyPlate;
    public GameObject haveCake;
    public GameObject haveBread;
    public GameObject haveCakeBread;
    public GameObject haveCoffee;
    public GameObject haveCoffeeCake;
    public GameObject haveCoffeeBread;
    public GameObject haveTwoCakes;
    public GameObject haveTwoBreads;
    public GameObject haveTwoCoffees;

    public List<string> onPlate;

    // Start is called before the first frame update
    void Start()
    {
        // Initially plate is empty
        onPlate = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatus();
    }

    // Update plate status
    void UpdateStatus()
    {
        Boolean containCake = onPlate.Contains("Cake");
        Boolean containBread = onPlate.Contains("Bread");
        Boolean containCoffee = onPlate.Contains("Cup");

        if (containBread && !containCake && !containCoffee && onPlate.Count == 2)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(true);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else if (containCake && !containBread && !containCoffee && onPlate.Count == 2)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(true);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else if (containCoffee && !containCake && !containBread && onPlate.Count == 2)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(true);
            haveCoffeeBread.SetActive(false);
        }
        else if (containCake && containCoffee)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(true);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else if (containBread && containCoffee)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(true);
        }
        else if (containCake && containBread)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(true);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else if (containCake)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(true);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else if (containBread)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(true);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else if (containCoffee)
        {
            emptyPlate.SetActive(false);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(true);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
        else
        {
            emptyPlate.SetActive(true);
            haveCake.SetActive(false);
            haveBread.SetActive(false);
            haveCakeBread.SetActive(false);
            haveCoffee.SetActive(false);
            haveCoffeeCake.SetActive(false);
            haveTwoCakes.SetActive(false);
            haveTwoBreads.SetActive(false);
            haveTwoCoffees.SetActive(false);
            haveCoffeeBread.SetActive(false);
        }
    }

    // Adding food to plate
    public Boolean AddFood(Item foodAdded)
    {
        if (onPlate.Count == 2 || foodAdded.gameObject.CompareTag("Plate")) 
        {
            Debug.Log("Cannot be added 1");
            return false;
        }

        // If this kind of food has been added, it cannot be added again
        if (!foodAdded.gameObject.CompareTag("Cup"))
        {
            onPlate.Add(foodAdded.tag);
            Destroy(foodAdded.gameObject);
            Debug.Log("Successfully Added");
            return true;
        } else
        {
            if (foodAdded.gameObject.GetComponent<Cup>().filledCoffee)
            {
                onPlate.Add(foodAdded.tag);
                Destroy(foodAdded.gameObject);
                Debug.Log("Successfully Added");
                return true;
            }
        }
        Debug.Log("Cannot be Added 2");
        return false;
    }
}
