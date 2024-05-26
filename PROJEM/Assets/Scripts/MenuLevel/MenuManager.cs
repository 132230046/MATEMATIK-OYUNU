using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startBtn, exitBtn;
    void Start()
    {
        FadeOut();
    }

    // Update is called once per frame
    void FadeOut()
    {
        startBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(1); 
    }
   public void StartGameLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
