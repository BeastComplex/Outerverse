using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }
    public void LoadGame()
    {
        FindObjectOfType<GameSession>().ResetScore();
        SceneManager.LoadScene("Game");
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
