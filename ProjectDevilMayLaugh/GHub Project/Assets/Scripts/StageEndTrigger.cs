using UnityEngine;

public class StageEndTrigger : MonoBehaviour
{
    [Header("Win UI")]
    public GameObject winPopup;

    [Header("Audio")]
    public AudioClip winSound;
    [Range(0f, 1f)] public float winVolume = 1f;

    [Header("Options")]
    public bool freezePlayerOnWin = true;

    private bool hasWon = false;

    void Start()
    {
        if (winPopup != null)
            winPopup.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;
        if (!other.CompareTag("Player")) return;

        hasWon = true;

        AudioListener.pause = true;

        if (winSound != null)
        {
            GameObject tempAudio = new GameObject("WinSound");
            AudioSource source = tempAudio.AddComponent<AudioSource>();

            source.clip = winSound;
            source.volume = winVolume;
            source.ignoreListenerPause = true; 
            source.Play();

            Destroy(tempAudio, winSound.length);
        }

        if (winPopup != null)
            winPopup.SetActive(true);

        if (freezePlayerOnWin)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                player.SetDead(true);
        }

    }
}