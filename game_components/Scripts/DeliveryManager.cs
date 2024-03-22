using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    // Declare new variables
    public static DeliveryManager Instance;
    public List<Order> activeOrders = new List<Order>();
    public float generateOrderInterval = 15f;

    public GameObject orderUIPrefab;
    public GameObject itemTextPrefab;
    public Transform ordersContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Generate initial three orders
        for (int i = 0; i < 2; i++)
        {
            GenerateOrder();
        }

        // Start generating new orders every 15 seconds
        StartCoroutine(GenerateOrdersRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Match the dishes on the plate with active orders
    public bool ValidateDelivery(List<string> deliveredItems)
    {
        foreach (Order order in activeOrders)
        {
            if (AreListsEqual(order.requiredItems, deliveredItems))
            {
                int baseScore = deliveredItems.Count * 15;
                int bonusScore = order.timeToExpire > 15f ? 10 : 0;
                GameManager.Instance.AddScore(baseScore + bonusScore);

                // Todo: Update UI, sound, etc. to show order completion

                activeOrders.Remove(order);
                Destroy(order.orderUIObject);

                Debug.Log("Deliver Successfully.");
                return true;
            }
        }
        return false;
    }

    // Simplified check if both lists have the same elements in the same order
    private bool AreListsEqual(List<string> list1, List<string> list2)
    {
        list1 = list1.OrderBy(item => item).ToList();
        list2 = list2.OrderBy(item => item).ToList();
        for (int i = 0; i < list2.Count; i++)
        {
            if (list2[i] == "Cup")
            {
                list2[i] = "Coffee";
            }
        }
        //Debug.Log(string.Join(", ", list1));
        //Debug.Log(string.Join(", ", list2));
        return list1.SequenceEqual(list2);
    }

    // Generate a new order with different combinations of drinks and pastries
    private void GenerateOrder()
    {
        List<string> items = new List<string>();

        string[] dishes = {"", "Coffee", "Cake", "Bread"};

        // Generate the content of new orders randomly
        string dish1 = "";
        string dish2 = "";

        // Avoid to have an empty order
        while(dish1 == "" || dish2 == ""){
            dish1 = dishes[Random.Range(0, dishes.Length)];
            dish2 = dishes[Random.Range(0, dishes.Length)];
            if(dish1 != "" || dish2 != ""){
                break;
            }
        }

        if (dish1 != "")
            items.Add(dish1);
        if (dish2 != "")
            items.Add(dish2);

        // Create a new order instance
        if (items.Count > 0)
        {
            Order newOrder = new Order(items);
            activeOrders.Add(newOrder);
            StartCoroutine(TrackOrderExpiry(newOrder));

            // Get the number of each item
            Dictionary<string, int> itemCounts = new Dictionary<string, int>();
            foreach (var item in items){
                if (itemCounts.ContainsKey(item))
                {
                    itemCounts[item]++;
                }
                else
                {
                    itemCounts[item] = 1;
                }
            }

            // Instantiate and display the order in the UI
            GameObject newOrders = Instantiate(orderUIPrefab, ordersContainer);
            newOrder.orderUIObject = newOrders;
            TextMeshProUGUI timerTextComponent = newOrders.transform.Find("OrderTimer").GetComponent<TextMeshProUGUI>();
            if (timerTextComponent != null)
            {
                newOrder.timerText = timerTextComponent;
            }

            Image orderImageComponent = newOrders.transform.Find("Image").GetComponent<Image>();
            if (orderImageComponent != null)
            {
                newOrder.image = orderImageComponent;
            }

            foreach (var entry in itemCounts){
                GameObject item = Instantiate(itemTextPrefab, newOrders.transform.Find("ItemContainer").transform);
                TextMeshProUGUI itemText = item.GetComponent<TextMeshProUGUI>();
                if (itemText != null){
                    itemText.text = $"{entry.Key}×{entry.Value}";
                }
            }
        }
    }

    // Enable to track if the order is expiry
    private IEnumerator TrackOrderExpiry(Order order)
    {
        float remainingTime = order.timeToExpire;
        // Order timer
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1);
            remainingTime -= 1;
            order.timerText.text = Mathf.RoundToInt(remainingTime).ToString();
        }

        if(remainingTime == 0){
            order.hasExpired = true;
        }

        // While the order is expiry
        if(order.hasExpired){
            GameManager.Instance.SubScore();
            Debug.Log("Order is expiry.");
            Destroy(order.orderUIObject);
        }
        // Todo: When the time only left 5 seconds, give a sound effect
    }

    // Generate a new order every 25 seconds if we have less than 6 orders
    private IEnumerator GenerateOrdersRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(generateOrderInterval);

            GenerateOrder();
            Debug.Log("New order is coming. Hurry up!!!");
        }
    }
}
