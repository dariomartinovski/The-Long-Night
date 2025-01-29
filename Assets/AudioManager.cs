using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio Clip --------")]
    public AudioClip background;
    public AudioClip shoot;
    public AudioClip heroHit;
    public AudioClip enemyHit;
    public AudioClip bossHit;
    public AudioClip coinPickup;
    public AudioClip coinBagPickup;

    public LogicScript Logic;

    private void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void Update()
    {
        if (Logic.PausedGame())
        {
            musicSource.Pause();
        }
        if (Logic.IsGameActive() && !Logic.PausedGame())
        {
            musicSource.UnPause();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
