using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberBlade : MonoBehaviour
{
    // The speed at which the lightsaber blade extends or collapses
    public float extendSpeed = 0.1f;

    public GameObject blade;

    // Boolean to keep track of the lightsaber's state (on/off)
    private bool isOn = true;

    // The minimum scaling value (collapsed state)
    private float minScale = 0f;

    // The maximum scaling value, which we will set based on the initial scale of the blade
    private float maxScale;

    // The interpolation value to calculate scaling each frame
    private float scaleInterpolation;

    // The current scale of the lightsaber blade (for the y-axis)
    private float currentScale;

    // Variables to save the initial x and z scale values of the blade
    private float initialXScale;
    private float initialZScale;

    [SerializeField] private AudioClip ignite;
    [SerializeField] private AudioClip hum;
    public AudioSource audioSource;
    public AudioSource audioSource1;

    void Start()
    {
        // Initialize maxScale based on the current y-scale of the blade (assuming y-axis for blade length)
        maxScale = transform.localScale.y;
        scaleInterpolation = maxScale / extendSpeed;
        // Set current scale to minScale initially (collapsed)
        currentScale = maxScale;

        // Store the initial x and z scales of the blade
        initialXScale = transform.localScale.x;
        initialZScale = transform.localScale.z;
        // audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!isOn) {
                audioSource.clip = ignite;
                audioSource.Play();
                audioSource1.loop = true;
                audioSource1.clip = hum;
                audioSource1.PlayDelayed(0.4f);

            }
            scaleInterpolation = maxScale / extendSpeed;
            scaleInterpolation = isOn ? -Mathf.Abs(scaleInterpolation) : Mathf.Abs(scaleInterpolation);
        }
        currentScale += scaleInterpolation * Time.deltaTime;
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);
        transform.localScale = new Vector3(initialXScale, currentScale, initialZScale);
        isOn = currentScale > 0;
        if (isOn) {
            blade.SetActive(true);
        } else {
            blade.SetActive(false);
            audioSource1.Stop();
        }

    }
}
