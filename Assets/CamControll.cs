using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class CamControll : MonoBehaviour
{
    public Camera wakeUpCam;
    public Camera mainCam;
    public Volume postProcessVolume;
    private DepthOfField dof;
    private ColorAdjustments colorAdjust;
    private float timer = 0f;
    public float wakeUpDuration = 5f;

    public UnityEngine.UI.Image blinkImage; // Drag image from canvas
    public float blinkSpeed = 2f;
    private bool isBlinking = true;
    public float lookAroundStrength = 0.5f;
    private Quaternion initialRotation;

    public float alignDuration = 2f; // Duration to align wakeUpCam to mainCam

    public FirstPersonController playerController; // Reference to the player controller script
    public GameObject cursor; // Reference to the cursor GameObject

    public GameObject tolongPlay; // Reference to the AudioManager object
    private AudioSource audioSource;

    private bool hasSoundPlayed = false; // To ensure the sound plays only once

    public UiControllerIngame uiControllerIG; // Reference to the in-game UI controller

    void Awake()
    {
        if (wakeUpCam == null || mainCam == null || postProcessVolume == null || blinkImage == null || uiControllerIG == null)
        {
            Debug.LogError("Missing references in CamControll script!");
        }
        uiControllerIG.disablePanel("IngamePanel");
        uiControllerIG.disablePanel("Rules");
        playerController.enabled = false; // Disable player controls initially
        cursor.SetActive(false); // Disable cursor visibility initially
    }

    void Start()
    {
        // Set up cameras
        mainCam.enabled = false;
        wakeUpCam.enabled = true;

        // Set up post-processing effects
        postProcessVolume.profile.TryGet(out dof);
        postProcessVolume.profile.TryGet(out colorAdjust);

        // Set initial effects (blur and dark screen)
        dof.focusDistance.value = 0.1f;
        colorAdjust.postExposure.value = -2f;

        // Store the initial rotation of the wakeUpCam
        initialRotation = wakeUpCam.transform.localRotation;

        // Get the audio source
        audioSource = tolongPlay.GetComponent<AudioSource>();

        // Keep the screen black initially
        blinkImage.color = new Color(0, 0, 0, 1); // Fully black
    }

    void Update()
    {
        if (!hasSoundPlayed)
        {
            // Play the sound once
            audioSource.Play();
            hasSoundPlayed = true;
        }

        // Wait for the sound to finish before starting the wake-up effect
        if (!audioSource.isPlaying)
        {
            if (timer < wakeUpDuration)
            {
                timer += Time.deltaTime;

                // Gradually reduce blur and brighten the screen
                dof.focusDistance.value = Mathf.Lerp(0.1f, 10f, timer / wakeUpDuration);
                colorAdjust.postExposure.value = Mathf.Lerp(-2f, 0f, timer / wakeUpDuration);

                // Look-around effect: Add slight left-right shake to the camera
                float rotX = initialRotation.eulerAngles.x; // Keep the initial X rotation
                float rotY = initialRotation.eulerAngles.y + Mathf.Sin(Time.time * 2f) * lookAroundStrength; // Add left-right shake
                float rotZ = initialRotation.eulerAngles.z; // Keep the initial Z rotation

                wakeUpCam.transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
            }

            if (isBlinking)
            {
                float blinkAlpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
                Color c = blinkImage.color;
                c.a = blinkAlpha;
                blinkImage.color = c;

                if (timer > wakeUpDuration * 0.5f)
                {
                    // Stop blinking halfway through
                    isBlinking = false;
                    c.a = 0f;
                    blinkImage.color = c;

                    // Start aligning wakeUpCam to mainCam
                    StartCoroutine(AlignCameras());
                    blinkImage.gameObject.SetActive(false); // Hide the blink image
                }
            }
        }
    }

    IEnumerator AlignCameras()
    {
        float elapsed = 0f;

        // Store the starting position and rotation of the wakeUpCam
        Vector3 startPosition = wakeUpCam.transform.position;
        Quaternion startRotation = wakeUpCam.transform.rotation;

        // Store the target position and rotation of the mainCam
        Vector3 targetPosition = mainCam.transform.position;
        Quaternion targetRotation = mainCam.transform.rotation;

        while (elapsed < alignDuration)
        {
            elapsed += Time.deltaTime;

            // Smoothly interpolate the position and rotation
            wakeUpCam.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / alignDuration);
            wakeUpCam.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / alignDuration);

            yield return null;
        }

        // Ensure the final position and rotation match exactly
        wakeUpCam.transform.position = targetPosition;
        wakeUpCam.transform.rotation = targetRotation;

        // Switch to the mainCam
        wakeUpCam.enabled = false;
        mainCam.enabled = true;

        // Enable the Rules Panel after the camera alignment
        uiControllerIG.enablePanel("Rules");
    }

    // Called by the button in the Rules Panel
    public void StartGame()
    {
        // Hide the Rules Panel and start the game
        uiControllerIG.disablePanel("Rules");

        // Enable player controls and cursor
        playerController.enabled = true;
        cursor.SetActive(true);

        // Enable the Ingame Panel
        uiControllerIG.enablePanel("IngamePanel");

        // Disable this script after starting the game
        this.enabled = false;
    }
}