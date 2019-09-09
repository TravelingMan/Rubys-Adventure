using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float transitionTime = 2.0f;
    private float _elapsed = 0f;
    
    void Update()
    {
        if (_elapsed < transitionTime)
        {
            _elapsed += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
