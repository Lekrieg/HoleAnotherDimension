using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditQuitButton : MonoBehaviour
{
    private GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton = GameObject.Find("QuitButton");
        quitButton.GetComponent<Button>().onClick.AddListener(Quit);
    }

    private void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
