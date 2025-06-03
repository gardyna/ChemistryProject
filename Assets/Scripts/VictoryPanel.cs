using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload scene
    }

    public void Next()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
