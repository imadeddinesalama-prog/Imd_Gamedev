using UnityEngine;
using System;

public class XPManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    [Header("Level Scaling")]
    public float xpMultiplier = 1.5f; 

    [Header("Audio")]
    public AudioClip levelUpSound;

    private AudioSource audioSource;

    public event Action<int, int, int> OnXPChanged; 
    public event Action<int> OnLevelUp; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        OnXPChanged?.Invoke(currentXP, xpToNextLevel, currentLevel);
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        OnXPChanged?.Invoke(currentXP, xpToNextLevel, currentLevel);
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;

        currentLevel++;

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);

        if (levelUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(levelUpSound);
        }

        OnLevelUp?.Invoke(currentLevel);

        Debug.Log($"Level Up! Now Level {currentLevel}. XP needed for next level: {xpToNextLevel}");
    }

    public float GetXPProgress()
    {
        return (float)currentXP / xpToNextLevel;
    }
}