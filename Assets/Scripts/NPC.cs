using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox = null;
    
    [SerializeField] private float displayTime = 4.0f;
    private float _timerDisplay;
    
    void Start()
    {
        dialogBox.SetActive(false);
        _timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerDisplay >= 0)
        {
            _timerDisplay -= Time.deltaTime;
            if (_timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        _timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
