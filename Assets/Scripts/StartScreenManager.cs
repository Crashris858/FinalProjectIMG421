using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Image backgroundOverlay;
    public TextMeshProUGUI titleText;
    public Button startButton;

    [Header("Animation Settings")]
    public float titleFadeInDuration = 1.5f;
    public float menuFadeInDelay = 0.5f;
    public float menuFadeInDuration = 1f;

    private Color titleOriginalColor;
    private Color buttonOriginalColor;
    private Image startButtonImage;

    void Start()
    {
        InitializeStartScreen();
    }

    void InitializeStartScreen()
    {
        if (titleText != null)
        {
            titleOriginalColor = titleText.color;
            titleText.color = new Color(titleOriginalColor.r, titleOriginalColor.g, titleOriginalColor.b, 0);
        }

        if (startButton != null)
        {
            startButton.interactable = false;
            startButtonImage = startButton.GetComponent<Image>();
            if (startButtonImage != null)
            {
                buttonOriginalColor = startButtonImage.color;
                startButtonImage.color = new Color(buttonOriginalColor.r, buttonOriginalColor.g, buttonOriginalColor.b, 0);
            }
        }

        StartCoroutine(AnimateTitleSequence());
    }

    IEnumerator AnimateTitleSequence()
    {
        if (titleText != null)
        {
            float elapsed = 0;
            while (elapsed < titleFadeInDuration)
            {
                float alpha = Mathf.Lerp(0, 1, elapsed / titleFadeInDuration);
                titleText.color = new Color(titleOriginalColor.r, titleOriginalColor.g, titleOriginalColor.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }
            titleText.color = titleOriginalColor;
        }

        yield return new WaitForSeconds(menuFadeInDelay);

        if (startButton != null && startButtonImage != null)
        {
            float elapsed = 0;
            while (elapsed < menuFadeInDuration)
            {
                float alpha = Mathf.Lerp(0, 1, elapsed / menuFadeInDuration);
                startButtonImage.color = new Color(buttonOriginalColor.r, buttonOriginalColor.g, buttonOriginalColor.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            startButtonImage.color = buttonOriginalColor;
            startButton.interactable = true;
        }
    }

    public void OnStartButtonClicked()
    {
        StartCoroutine(TransitionToGame());
    }

    IEnumerator TransitionToGame()
    {
        if (backgroundOverlay != null)
        {
            float elapsed = 0;
            Color startColor = backgroundOverlay.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1);

            while (elapsed < 1f)
            {
                backgroundOverlay.color = Color.Lerp(startColor, endColor, elapsed / 1f);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        SceneManager.LoadScene("_Main");
    }
}