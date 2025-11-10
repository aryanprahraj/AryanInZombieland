using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;

    public void AddKey()
    {
        hasKey = true;
        Debug.Log("✅ PlayerInventory: Key added!");
    }
}
