using System.Data;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    private float timer;
    private const int TIMER_END = 10000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > TIMER_END)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer++;
        }
    }
}