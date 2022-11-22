using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]GameObject startmenu;
    [SerializeField]GameObject gamemenu;
    
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
        gamemenu.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 

    }

    public void PassLevel(){


    }
}
