using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button SavedGameButton;

    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] public TextMeshProUGUI Turn;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;

    [SerializeField] private GameObject checkCanvas;
    
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
        if(_tileManager.whiteCheck == false && _tileManager.blackCheck == false)
            checkCanvas.SetActive(false);
        
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
    public void EnableCheckCanvas()
    {
        if (_tileManager.gameTurn == false) _tileManager.whiteCheck = true;
        else _tileManager.blackCheck = true;
        
        checkCanvas.SetActive(true);
        checkCanvas.transform.DOShakeRotation(1,20, 4, 1, true);
    }
}
