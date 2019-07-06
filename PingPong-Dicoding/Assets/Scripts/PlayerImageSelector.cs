using UnityEngine;
using UnityEngine.UI;

// This class is for handling the image of the player
public class PlayerImageSelector : MonoBehaviour
{

    [Header("Player Sprite")] 
    [SerializeField] private Sprite spritePlayer1;
    [SerializeField] private Sprite spritePlayer2;
    [SerializeField] private Sprite spriteAI;
    [SerializeField] private Sprite spriteAIExpert;
    
    [Header("Material Player")]
    [SerializeField] private Material materialPlayer1;
    [SerializeField] private Material materialPlayer2;
    [SerializeField] private Material materialAI;
    [SerializeField] private Material materialAIExpert;
    
    [Header("Player Image")]
    [SerializeField] private Image paddle1;
    [SerializeField] private Image paddle2;

    void Start()
    {
        ChangeSprite(paddle1, spritePlayer1);
        ChangeMaterial(paddle1, materialPlayer1);
        
        if (GameVariable._mode.Equals("PvP"))
        {
            ChangeSprite(paddle2, spritePlayer2);
            ChangeMaterial(paddle2, materialPlayer2);
        } else if (GameVariable._mode.Equals("AI"))
        {
            ChangeSprite(paddle2, spriteAI);
            ChangeMaterial(paddle2, materialAI);
        }
        else
        {
            ChangeSprite(paddle2, spriteAIExpert);
            ChangeMaterial(paddle2, materialAIExpert);
        }
    }

    void ChangeSprite(Image _image, Sprite sprite)
    {
        _image.sprite = sprite;
    }

    void ChangeMaterial(Image _image, Material _material)
    {
        _image.material = _material;
    }
}
