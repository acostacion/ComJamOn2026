using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public enum MusicState
    {
        Idle,
        FadingOut,
        FadingIn
    }

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    public AudioClip currentClip;
    private AudioClip nextClip;

    private AudioSource audioSource;

    public float fadeSpeed = 1.5f;

    private MusicState state = MusicState.Idle;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1f;
    }

    void Start()
    {
        if (audioSource.clip != null)
            audioSource.Play();
    }
    void Update()
    {
        switch (state)
        {
            case MusicState.FadingOut:
                FadeOut();
                break;

            case MusicState.FadingIn:
                FadeIn();
                break;
        }
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip)
            return;

        nextClip = newClip;
        state = MusicState.FadingOut;
    }

    public void PlayMusicForScene(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                PlayMusic(menuMusic);
                break;

            default:
                PlayMusic(gameplayMusic);
                break;
        }
    }

    void FadeOut()
    {
        audioSource.volume -= fadeSpeed * Time.deltaTime;

        if (audioSource.volume <= 0)
        {
            audioSource.volume = 0;

            audioSource.clip = nextClip;
            audioSource.Play();

            state = MusicState.FadingIn;
        }
    }

    void FadeIn()
    {
        audioSource.volume += fadeSpeed * Time.deltaTime;

        if (audioSource.volume >= 1)
        {
            audioSource.volume = 1;
            state = MusicState.Idle;
        }
    }
}
