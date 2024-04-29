using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button SavedGameButton;

    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Turn;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;
    
    public TileManager _tileManager;
    
    public float elapsedTime;
    
    public UnityEvent NoSavedGameEvent;
    private void Start()
    {
        //if no saved game
        NoSavedGameEvent.Invoke();
        SavedGameButton.interactable = false;
        Turn.text = "Turn : White";
        //
        _tileManager = FindObjectOfType<TileManager>();
        
    }
    void Update()
    {
        //only when start & end menu canvas are not active
        if (endMenu.activeInHierarchy || startMenu.activeInHierarchy) return;
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateUITurnText()
    {
        if (_tileManager.gameTurn == false)
            Turn.text = "Turn : White";
        else
            Turn.text = "Turn : Black";
    }
}
