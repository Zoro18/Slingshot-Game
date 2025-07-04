using UnityEngine;
using DG.Tweening; // DOTween namespace

public class SnowmanManager : MonoBehaviour
{
    [SerializeField] private GameObject[] snowmen; // Array of Snowman objects
    [SerializeField] private GameObject confettiPrefab; // Confetti particle effect prefab
    [SerializeField] private AudioClip winSound; // Win sound effect
    [SerializeField] private Transform confettiSpawnPoint; // Point where confetti should appear
    [SerializeField] private GameObject winPanel; // Reference to the win panel

    private AudioSource audioSource;
    private bool winEffectsPlayed = false; // Flag to check if win effects have already been played

    private void Start()
    {
        // Ensure an AudioSource is attached to this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Make sure the win panel is initially inactive
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    private void Update()
    {
        CheckForWin();
    }

    private void CheckForWin()
    {
        // Check if all snowmen in the array are inactive
        bool allSnowmenInactive = true;
        foreach (GameObject snowman in snowmen)
        {
            if (snowman.activeSelf)
            {
                allSnowmenInactive = false;
                break;
            }
        }

        // If all snowmen are inactive and win effects haven't been played, display the win message, play confetti, sound, and activate the win panel
        if (allSnowmenInactive && !winEffectsPlayed)
        {
            Debug.Log("You Win!");
            PlayWinEffects();
            winEffectsPlayed = true; // Set the flag to true to prevent playing effects again

            // Delay the activation of the win panel by 2 seconds
            DOVirtual.DelayedCall(2f, () =>
            {
                if (winPanel != null)
                {
                    winPanel.SetActive(true);
                    AnimateWinPanel();
                }
            });
        }
    }

    private void PlayWinEffects()
    {
        // Play confetti effect
        if (confettiPrefab != null)
        {
            Instantiate(confettiPrefab, confettiSpawnPoint.position, Quaternion.identity);
        }

        // Play win sound effect
        if (winSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(winSound);
        }
    }

    private void AnimateWinPanel()
    {
        // Example DOTween animations for the win panel
        RectTransform panelRectTransform = winPanel.GetComponent<RectTransform>();

        // Scale the panel up and fade in
        if (panelRectTransform != null)
        {
            panelRectTransform.localScale = Vector3.zero; // Start with scale of 0
            panelRectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // Scale up

            CanvasGroup canvasGroup = winPanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0; // Start with alpha of 0
                canvasGroup.DOFade(1, 0.5f); // Fade in
            }
        }
    }
}
