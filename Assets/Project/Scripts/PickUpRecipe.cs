using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRecipe : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    // Jos pelihahmo törmäsi reseptiin, kasvatetaan laskuria ja tuhotaan resepti lopuksi.
    // ScoreManager huolehtii pistiden kasvattamisesta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reseptin palan keräys ääni
            AudioManager.instance.Play("PickupRecipe");
            scoreManager.AddRecipe();
            Destroy(gameObject);
        }
            
    }
   
}
