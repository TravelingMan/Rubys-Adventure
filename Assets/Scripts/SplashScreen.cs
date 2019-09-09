using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float transitionTime = 6.0f;
    private float _elapsed = 0f;
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
        else if (_elapsed < transitionTime)
        {
            _elapsed += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
