using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Order
{
    public List<string> requiredItems;    // Items required for the order.
    public float timeToExpire = 40f;      // Duration the player has to complete this order before it expires.
    public bool hasExpired = false;       // Track if the order has expired.
    public TextMeshProUGUI timerText;
    public Image image;
    public GameObject orderUIObject;

    // Default constructor
    public Order()
    {
        requiredItems = new List<string>();
    }

    // Parameterized constructor
    public Order(List<string> items)
    {
        requiredItems = items;
    }
}
