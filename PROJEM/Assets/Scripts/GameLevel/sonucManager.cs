using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sonucManager : MonoBehaviour
{ 
    public void OyunaYenidenBasla()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("MenuLevel");
    }

}
