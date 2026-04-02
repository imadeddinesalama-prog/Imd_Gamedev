using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPUIManager : MonoBehaviour
{
    [Header("References")]
    public XPManager xpManager;

    [Header("UI Elements")]
    public Image xpBarFill;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    [Header("Visual Effects")]
    public GameObject levelUpEffect; 
    public float levelUpTextScale = 1.5f;
    public float levelUpAnimDuration = 0.5f;

    void Start()
    {
        if (xpManager != null)
        {
            xpManager.OnXPChanged += UpdateXPBar;
            xpManager.OnLevelUp += OnLevelUp;

            UpdateXPBar(xpManager.currentXP, xpManager.xpToNextLevel, xpManager.currentLevel);
        }
        else
        {
            Debug.LogError("XPManager reference is missing!");
        }
    }

    void OnDestroy()
    {
        if (xpManager != null)
        {
            xpManager.OnXPChanged -= UpdateXPBar;
            xpManager.OnLevelUp -= OnLevelUp;
        }
    }

    void UpdateXPBar(int currentXP, int xpToNextLevel, int currentLevel)
    {
        if (xpBarFill != null)
        {
            float fillAmount = (float)currentXP / xpToNextLevel;
            xpBarFill.fillAmount = fillAmount;
        }

        if (levelText != null)
        {
            levelText.text = "Level " + currentLevel;
        }

        if (xpText != null)
        {
            xpText.text = currentXP + " / " + xpToNextLevel + " XP";
        }
    }

    void OnLevelUp(int newLevel)
    {
        if (levelText != null)
        {
            StartCoroutine(LevelUpAnimation());
        }

        if (levelUpEffect != null)
        {
            Instantiate(levelUpEffect, xpManager.transform.position, Quaternion.identity);
        }
    }

    System.Collections.IEnumerator LevelUpAnimation()
    {
        Vector3 originalScale = levelText.transform.localScale;
        Vector3 targetScale = originalScale * levelUpTextScale;

        float elapsed = 0f;
        while (elapsed < levelUpAnimDuration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (levelUpAnimDuration / 2);
            levelText.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < levelUpAnimDuration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (levelUpAnimDuration / 2);
            levelText.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        levelText.transform.localScale = originalScale;
    }
}