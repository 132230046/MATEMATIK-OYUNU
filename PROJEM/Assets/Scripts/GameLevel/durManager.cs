using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class durManager : MonoBehaviour
{
    [SerializeField]
    private GameObject durPaneli;
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void devamEt()
    {
        durPaneli.SetActive(false);
    }
    public void anaMenuyeDön()
    {
        SceneManager.LoadScene("MenuLevel");
    }
    public void oyundanCýk()
    {
        Application.Quit();
    }
}
