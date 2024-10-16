using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
public class UIDisplay : MonoBehaviour
{
    public static UIDisplay Instance { get; private set; }
    [Header("Health")]
    [SerializeField] Image healthImage;
    [SerializeField] Health health;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Scorekeeper scorekeeper;
    [Header("Title Level")]
    [SerializeField] TextMeshProUGUI titleLevelText;
    public float displayDuration = 2f;
    private Tween tween;


    void Awake()
    {
        Instance = this;
        DOTween.SetTweensCapacity(500, 50);
        scorekeeper = FindAnyObjectByType<Scorekeeper>();
        health = FindAnyObjectByType<Health>();

    }

    void Start()
    {
        if (healthImage != null)
        {
            tween = healthImage.DOFillAmount(1f, 2f);
        }
        ScoreUI();
        HealthUI();
        StartCoroutine(DisplayTitle());
    }

    public void HealthUI()
    {
        float taget = (float)health.GetHealth() / 50;
        healthImage.DOFillAmount(taget, 0.5f);
    }

    public void ScoreUI()
    {
        scoreText.text = scorekeeper.GetScore().ToString("000000000");
    }
    IEnumerator DisplayTitle()
    {
        titleLevelText.text = SceneManager.GetActiveScene().name;
        titleLevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        titleLevelText.gameObject.SetActive(false);

    }
}
