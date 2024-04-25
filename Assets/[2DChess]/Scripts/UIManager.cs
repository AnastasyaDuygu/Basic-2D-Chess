using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button SavedGameButton;

    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Turn;
    
    public float elapsedTime;
    
    public UnityEvent NoSavedGameEvent;
    private void Start()
    {
        //if no saved game
        NoSavedGameEvent.Invoke();
        SavedGameButton.interactable = false;
        //
    }
    void Update()
    {
        //only when start & end menu canvas are not active
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
