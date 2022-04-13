using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    [SerializeField] private GameObject virtualCamera1;
    [SerializeField] private GameObject virtualCamera2;
    [SerializeField] private GameObject DoorCollider;
    [SerializeField] private GameObject DoorCollider2;

    public static SwapCamera instance;
    private void Start()
    {
        instance = this;
        // virtuaalikamera 1 aktiivinen
        virtualCamera1.SetActive(true);
        // virtuaalikamera 2 pois k�yt�st�
        virtualCamera2.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // T�rm�sik� kurpitsapoika oveen 1
        if (collision.CompareTag("Player"))
        {
            // Kyll�, joten virtuaalikamera 2 aktivoituu
            virtualCamera1.SetActive(false);
            virtualCamera2.SetActive(true);

            // Taustamusa vaihtuu
            AudioManager.instance.StopPlay("Background");
            AudioManager.instance.Play("Background2");
        }
    }

    public void BlockLevel()
    {
        DoorCollider.SetActive(false);
        DoorCollider2.SetActive(true);
    }
   
}
