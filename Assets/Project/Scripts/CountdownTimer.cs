using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    // GameOver paneeli
    [SerializeField]
    private GameObject gameOverPanel;
    // K‰ynniss‰ oleva aika
    private float currentTime = 0f;
    // Aloitus aika
    [SerializeField]
    private float startingTime = 0f;
    // Tekstilaatikko, jossa aika n‰ytet‰‰n
    [SerializeField]
    private TextMeshProUGUI countdownTimerText;
    // Tekstin v‰ri
    private Color color = new Color(1, 1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCounting();
    }

    void StartCounting()
    {
        // Aloitetaan v‰hent‰m‰‰n aikaa
        currentTime -= Time.deltaTime;
        // Varmistetaan ett‰ aika ei mene alle 0:00
        currentTime = Mathf.Clamp(currentTime, 0f, Mathf.Infinity);
        // P‰ivitet‰‰n aika
        countdownTimerText.text = DisplayTime(currentTime);
        // Ajastimen v‰ri
        countdownTimerText.color = color;
        // Vaihdetaan v‰ri‰ kun aikaa on en‰‰ 10 sekunttia
        if (currentTime <= 10)
        {
            // V‰ri vaihtuu
            color = new Color(255, 0, 0, 1);
            // T‰h‰n lis‰t‰‰n Beep ‰‰ni
            // Loppuiko aika?
            if (currentTime <= 0)
            {
                // Nollataan laskuri
                currentTime = 0;
                // T‰h‰n tulee GameOver koodi
                StartCoroutine(GameOver());
            }
        }
    }

    // Metodi muuttaa sekunnit minuuteiksi ja sekunneiksi ja palauttaa ajan kutsujalle
    private string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        return "Aikaa " + string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    // N‰yt‰‰ GameOver panelia 2 sekunttia, jonka j‰lkeen peli alkaa alusta
    private IEnumerator GameOver()
    {
        // Pys‰ytet‰‰n musat
        AudioManager.instance.StopAll();
        // N‰ytet‰‰n GameOver paneli
        gameOverPanel.SetActive(true);
        // Odotetaan 2 sekunttia
        yield return new WaitForSeconds(2);
        // Piilotetaan
        gameOverPanel.SetActive(false);
        // Aloitetaan peli alusta
        SceneManager.LoadScene(0);
        
    }
 
}
