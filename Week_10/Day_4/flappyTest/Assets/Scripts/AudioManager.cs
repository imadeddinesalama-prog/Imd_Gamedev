using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Sources ------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----------- Audio Clips ------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip tubein;

    void Start()
    {
        musicSource.clip = background;  
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
