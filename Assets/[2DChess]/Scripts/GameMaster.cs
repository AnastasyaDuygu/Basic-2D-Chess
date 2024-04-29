using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
