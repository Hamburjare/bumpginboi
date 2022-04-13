using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHitBox : MonoBehaviour
{
    // GameOver paneeli
    [SerializeField]
    private GameObject gameOverPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // Pys‰ytet‰‰n musat
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("GameOver");
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
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
