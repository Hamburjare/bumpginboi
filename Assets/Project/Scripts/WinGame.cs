using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{   // Voittopaneli
    public GameObject winPanel;
    // Pause
    public static bool gameIsPaused;
    private void Update()
    {
        // ESCillă pääsee päävalikkoon
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pysäytetään kaikki musa
            AudioManager.instance.StopAll();
            // Peli käynnistyy
            Time.timeScale = 1;
            // Aloitetaan uusi peli
            SceneManager.LoadScene("MainMenu");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pysäytetään musat
        AudioManager.instance.StopAll();
        // Soitetaan loppufanfaari
        AudioManager.instance.Play("GameWin");
        // Näytetään pelihahmon 2 voittopaneli
        winPanel.SetActive(true);
        // Pysäytetään peli
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }

    void PauseGame()
    {
        if (gameIsPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1;
    }
}
