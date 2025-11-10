using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ZombieController : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    private AudioSource audioSource;

    [Header("Zombie Sound")]
    public AudioClip biteLoop; // single looping bite/snarl sound

    [Header("Behavior Settings")]
    public float attackRange = 2.5f;
    public float rotationSpeed = 5f;
    private bool hasKilledPlayer = false;

    void Start()
    {
        // Get references
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player")?.transform;

        // Setup audio
        if (audioSource != null && biteLoop != null)
        {
            audioSource.clip = biteLoop;
            audioSource.loop = true;
            audioSource.spatialBlend = 1f; // 3D sound
            audioSource.volume = 0.9f;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("ZombieController: Missing AudioSource or biteLoop clip!");
        }
    }

    void Update()
    {
        if (player == null || hasKilledPlayer) return;

        float distance = Vector3.Distance(player.position, transform.position);
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (distance <= attackRange)
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isRunning", false);
            agent.isStopped = true;

            if (!hasKilledPlayer)
            {
                hasKilledPlayer = true;
                Invoke(nameof(LoadLoseScene), 1.0f);
            }
        }
        else
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }

        // Smooth rotation toward player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void LoadLoseScene()
    {
        SceneManager.LoadScene("Loose Screen"); // make sure this matches the exact name
    }
}
