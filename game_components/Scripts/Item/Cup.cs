using UnityEngine;

public class Cup : Item
{
    public GameObject haveCoffee;
    public GameObject noCoffee;
    public bool filledCoffee;

    void Start()
    {
        haveCoffee.SetActive(false);
        noCoffee.SetActive(true);
        filledCoffee = false;
    }

    public void ChangeColor()
    {
        haveCoffee.SetActive(true);
        noCoffee.SetActive(false);
        filledCoffee = true;
    }

    private void ChangeColorRecursive(Transform targetTransform, Color color)
    {
        Renderer renderer = targetTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }

        for (int i = 0; i < targetTransform.childCount; i++)
        {
            ChangeColorRecursive(targetTransform.GetChild(i), color);
        }
    }
}
