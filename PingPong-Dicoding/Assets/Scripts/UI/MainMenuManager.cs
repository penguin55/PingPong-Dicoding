using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : UIManager
{
    [Header("Panel In Main Menu")] 
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject buttonTutorial;

    private void Start()
    {
        StartCoroutine(PrepareMainMenu());
    }

    IEnumerator PrepareMainMenu()
    {
        clicked = true;
        Transition("TransitionIn");
        yield return new WaitForSeconds(2f);
        clicked = false;
    }

    public void MainMenuCommand(string command)
    {
        if (clicked) return;
        clicked = true;
        switch (command)
        {
            case "PvP" :
                GameVariable._mode = command;
                StartCoroutine(OpenTutorial());
                break;
            case "AI" :
                GameVariable._mode = command;
                StartCoroutine(OpenTutorial());
                break;
            case "AIExpert" :
                GameVariable._mode = command;
                StartCoroutine(OpenTutorial());
                break;
            case "Exit" :
                StartCoroutine(ExitGame());
                break;
        }
    }
    
    private IEnumerator OpenTutorial()
    {
        Transition("TransitionOut");
        yield return new WaitForSeconds(2.2f);
        tutorialPanel.SetActive(true);
        Transition("TransitionIn");
        yield return new WaitForSeconds(2f);
        clicked = false;
        buttonTutorial.SetActive(true);
    }

    public void Play()
    {
        if (clicked) return;
        clicked = true;
        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        Transition("TransitionOut");
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("Game");
    }
    
    private IEnumerator ExitGame()
    {
        Transition("TransitionOut");
        yield return new WaitForSeconds(2.2f);
        Application.Quit();
    }
}
