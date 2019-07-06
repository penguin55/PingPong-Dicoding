using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{    
    [Header("Transition")]
    [SerializeField] private Animator transition;

    public AudioClip buttonClick;
    
    protected bool clicked = false;

    public void UICommand(string command)
    {
        if (clicked) return;
        clicked = true;
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
        Transition("TransitionOut");
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

    public void SoundEffect()
    {
        AudioManager.instance.PlayOneShot(buttonClick);
    }
}
