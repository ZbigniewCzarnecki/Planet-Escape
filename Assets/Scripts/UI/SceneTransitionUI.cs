using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    public static SceneTransitionUI Instance { get; private set; }

    CanvasGroup _canvasGroup;

    private bool _fadeIn;
    private bool _fadeOut;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one SceneTransitionUI: " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 1;
        FadeOut();
    }

    private void Update()
    {
        if (_fadeIn)
        {
            _canvasGroup.alpha += Time.deltaTime;

            if (_canvasGroup.alpha >= 1)
            {
                _fadeIn = false;
            }
        }

        if (_fadeOut)
        {
            _canvasGroup.alpha -= Time.deltaTime;

            if (_canvasGroup.alpha <= 0)
            {

                _fadeOut = false;
            }
        }
    }

    public void FadeIn()
    {
        _fadeIn = true;
    }

    public void FadeOut()
    {
        _fadeOut = true;
    }
}
