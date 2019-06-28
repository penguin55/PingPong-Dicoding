using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInGameManager : MonoBehaviour
{    
    [Header("Transition")]
    [SerializeField] private Animator transition;

    public void UICommand(string command)
    {
        switch (command)
        {
            case "Home" :
                StartCoroutine(Home());
                break;
            case "Rematch" :
                StartCoroutine(Rematch());
                break;
        }
    }

    private IEnumerator Home()
    {
        Transition("TransitionIn");
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("MainMenu");
    }
    
    private IEnumerator Rematch()
    {
        Transition("TransitionOut");
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("Game");
    }

    public void Transition(string _transition)
    {
        transition.gameObject.SetActive(true);
        transition.Play("Default");
        if (_transition.Equals("TransitionOut")) 
            transition.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0,720);
        else transition.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
        transition.SetTrigger(_transition);
    }
}
