
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int points = 1;
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCollector collector = other.GetComponent<PlayerCollector>();
            if (collector != null)
            {
                collector.Collect(points);

                if (collectSound != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);
                }

                Destroy(gameObject);
            }
        }
    }
}
