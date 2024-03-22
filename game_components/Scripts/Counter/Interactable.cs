using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Color highlightColor = Color.green;
    private Color originalColor;
    private Renderer rend;
    protected bool playerInRange = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (transform.parent != null)
        {
            rend = transform.GetComponent<Renderer>();
            if (rend != null)
            {
                originalColor = rend.material.color;
            }
            else
            {
                Debug.LogError("Renderer component not found on parent!");
            }
        }
        else
        {
            Debug.LogError("No parent object found!");
        }
    }

    public virtual void Interact(Player player)
    {
        // Override this method in subclasses to define specific interaction behavior
    }

    public void Highlight(bool highlight)
    {
        if (rend != null)
        {
            rend.material.color = highlight ? highlightColor : originalColor;
        }
    }
}
