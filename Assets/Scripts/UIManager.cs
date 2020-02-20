using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Button startGameButton;
    private Button commandsButton;
    private Button infoButton;
    private Button quitButton;
    private Button commandsPanelCloseButton;
    private Button infoPanelCloseButton;

    private GameObject commandsPanel;
    private GameObject infoPanel;

    private void Awake()
    {
        // Find all references
        startGameButton = GameObject.Find("StartButton").GetComponent<Button>();
        commandsButton = GameObject.Find("CommandsButton").GetComponent<Button>();
        infoButton = GameObject.Find("InfoButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        commandsPanelCloseButton = GameObject.Find("CommandsPanelCloseButton").GetComponent<Button>();
        infoPanelCloseButton = GameObject.Find("InfoPanelCloseButton").GetComponent<Button>();

        commandsPanel = GameObject.Find("CommandsPanel");
        infoPanel = GameObject.Find("InfoPanel");
        // -------------------

        // Add listener to buttons
        startGameButton.onClick.AddListener(StartGame);
        commandsButton.onClick.AddListener(() => { GenericButton(commandsPanel, true); });
        infoButton.onClick.AddListener(() => { GenericButton(infoPanel, true); });
        quitButton.onClick.AddListener(QuitGame);
        commandsPanelCloseButton.onClick.AddListener(() => { GenericButton(commandsPanel, false); });
        infoPanelCloseButton.onClick.AddListener(() => { GenericButton(infoPanel, false); });
        // ----------------------

        commandsPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
    }
    private void QuitGame()
    {
        Application.Quit();
    }

    // Generic method that takes an panel and if it's active or not
    private void GenericButton(GameObject panelObj, bool isActive)
    {
        panelObj.SetActive(isActive);
    }
}
