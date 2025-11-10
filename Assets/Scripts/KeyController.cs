using UnityEngine;

public class KeyController : MonoBehaviour
{
    [Header("Floating & Rotation Settings")]
    public float rotationSpeed = 50f;
    public float floatSpeed = 1f;
    public float floatHeight = 0.25f;

    [Header("Audio")]
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        // Create audio source dynamically if needed
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Float + rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Key collided with: {other.name} (tag: {other.tag})");

        // look for PlayerInventory anywhere on the player or its children
        if (other.CompareTag("Player"))
        {
            var inv = other.GetComponentInChildren<PlayerInventory>();

            if (inv != null)
            {
                inv.AddKey();
                Debug.Log("🔑 Key collected! Player now has the key.");

                // play pickup sound if assigned
                if (pickupSound != null)
                    audioSource.PlayOneShot(pickupSound);

                // destroy key after short delay (so sound finishes)
                Destroy(gameObject, 0.3f);
            }
            else
            {
                Debug.LogError("❌ PlayerInventory script not found on player or children!");
            }
        }
    }
}
