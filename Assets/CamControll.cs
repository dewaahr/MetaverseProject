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

    public UnityEngine.UI.Image blinkImage; // Drag image dari canvas
    public float blinkSpeed = 2f;
    private bool isBlinking = true;
    public float lookAroundStrength = 0.5f;
    private Quaternion initialRotation;

    public float alignDuration = 2f; // Duration to align wakeUpCam to mainCam


    MonoBehaviour playerController; // Reference to the player controller script
    GameObject player; // Reference to the player GameObject

    public UiControllerIngame uiControllerIG; // Reference to the UI controller

    public GameObject cursor;

    public GameObject tolongPlay; // Reference to the AudioManager object
    AudioSource audioSource;

    void Awake()
    {
        // Find the player object and its controller script
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<MonoBehaviour>();
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure it has the tag 'Player'.");
        }

        if (wakeUpCam == null || mainCam == null || postProcessVolume == null || blinkImage == null)
        {
            Debug.LogError("Please assign all camera and UI references in the inspector.");
        }
    }
    void Start()
    {
        playerController.enabled = false; // Disable player controls during wake-up
        cursor.SetActive(false); // Disable cursor visibility
        mainCam.enabled = false;
        wakeUpCam.enabled = true;
        uiControllerIG.disablePanel("IngamePanel"); // Disable rules panel if it's active


        postProcessVolume.profile.TryGet(out dof);
        postProcessVolume.profile.TryGet(out colorAdjust);

        // Set efek awal (blur dan gelap)
        dof.focusDistance.value = 0.1f;
        colorAdjust.postExposure.value = -2f;

        initialRotation = wakeUpCam.transform.localRotation;

        audioSource = tolongPlay.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer < wakeUpDuration)
        {
            timer += Time.deltaTime;
            dof.focusDistance.value = Mathf.Lerp(0.1f, 10f, timer / wakeUpDuration);
            colorAdjust.postExposure.value = Mathf.Lerp(-2f, 0f, timer / wakeUpDuration);

            // Look-around effect
            float rotX = Mathf.Sin(Time.time * 0.5f) * lookAroundStrength;
            float rotY = Mathf.Cos(Time.time * 0.3f) * lookAroundStrength;
            wakeUpCam.transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
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
            }
        }
    }

    IEnumerator AlignCameras()
    {
        audioSource.Play(); // Play wake-up sound

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
        playerController.enabled = true; // Re-enable player controls
        cursor.SetActive(true); // Enable cursor visibility
        uiControllerIG.enablePanel("IngamePanel"); // Enable rules panel
                                                   // tolongPlay.SetActive(false); // Stop wake-up sound
        audioSource.Stop(); // Stop wake-up sound
        tolongPlay.SetActive(false); // Disable the AudioManager object
        // Disable this script after alignment
        this.enabled = false;
    }
}
