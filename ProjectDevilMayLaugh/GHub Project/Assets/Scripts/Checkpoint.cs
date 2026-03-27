using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static HashSet<string> activatedCheckpoints = new HashSet<string>();
    private Animator anim;
    private AudioSource audioSource;
    private bool isActivated = false;
    private string checkpointID;

    [Header("Blade to arm after activation")]
    public CheckpointBlade bladeToArm;

    [Header("Audio")]
    public AudioClip activationSound;
    [Range(0f, 1f)] public float volume = 1f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        checkpointID = transform.position.x + "_" + transform.position.y;

        if (activatedCheckpoints.Contains(checkpointID))
        {
            isActivated = true;
            if (anim != null)
            {
                anim.enabled = true;
                anim.Play("CheckpointActivate", 0, 1f);
                anim.speed = 0f;
            }
            if (bladeToArm != null)
                bladeToArm.Activate();
        }
        else
        {
            if (anim != null) anim.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated) return;
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            activatedCheckpoints.Add(checkpointID);
            other.GetComponent<DeathHandler>()?.SetSpawnPoint(transform.position);

            if (anim != null)
            {
                anim.enabled = true;
                anim.speed = 1f;
                anim.Play("CheckpointActivate", 0, 0f);
                StartCoroutine(FreezeOnLastFrame());
            }

            PlayActivationSound();

            if (bladeToArm != null)
                bladeToArm.Activate();
        }
    }

    void PlayActivationSound()
    {
        if (activationSound == null) return;

        if (audioSource != null)
        {
            audioSource.PlayOneShot(activationSound, volume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(activationSound, transform.position, volume);
        }
    }

    System.Collections.IEnumerator FreezeOnLastFrame()
    {
        yield return new WaitForSeconds(0.6f);
        if (anim != null) anim.speed = 0f;
    }
}