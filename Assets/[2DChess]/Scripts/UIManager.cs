using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button SavedGameButton;

    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] public TextMeshProUGUI Turn;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;
    
    public TileManager _tileManager;
    public JsonSaveLoadScript _Json;
    
    public float elapsedTime;
    public UnityEvent NoSavedGameEvent;
    private void Start()
    {
        _Json = FindObjectOfType<JsonSaveLoadScript>();
        //if no saved game
        if (!_Json.CheckIfFileExists())
        {
            NoSavedGameEvent.Invoke();
            SavedGameButton.interactable = false;
        }

        _tileManager = FindObjectOfType<TileManager>();
        Turn.text = "Turn : White";
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
