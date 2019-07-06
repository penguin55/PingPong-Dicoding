using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")] 
    [SerializeField] private AudioSource sourceSFX;
    [SerializeField] private AudioSource sourceBGM;

    [Header("Audio Clip")] 
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip ballCollideWithWallorPaddle;
    [SerializeField] private AudioClip ballCollideWithLeftorRightWall;
    [SerializeField] private AudioClip winPanelOpen;
    [SerializeField] private AudioClip itemPickUp;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
            sourceBGM.clip = bgm;
            sourceBGM.loop = true;
            sourceBGM.Play();
            
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    public void PlayOneShot(AudioClip clip)
    {
        sourceSFX.PlayOneShot(clip);
    }

    public void PlayBallCollide(string _object)
    {
        switch (_object)
        {
            case "Wall" :
            case "Paddle" :
                sourceSFX.PlayOneShot(ballCollideWithWallorPaddle);
                break;
            case "Dead" :
                sourceSFX.PlayOneShot(ballCollideWithLeftorRightWall);
                break;
        }
    }

    public void PlayWinPanelOpen()
    {
        sourceSFX.PlayOneShot(winPanelOpen);
    }

    public void GetAnItem()
    {
        sourceSFX.PlayOneShot(itemPickUp);
    }
}
