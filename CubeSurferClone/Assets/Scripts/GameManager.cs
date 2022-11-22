using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject startmenu;
    public static int Points;

    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update() { }

    public void PressStart()
    {
        Time.timeScale = 1f;
        startmenu.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0); 

    }

    public void PassLevel(){


    }
}
