using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Reseptipalojen lukum‰‰r‰
    private int recipeAmmount;

    // Canvaksessa oleva tekstilaatikko, joka n‰ytt‰‰ ker‰tyt reseptipalat
    [SerializeField] private Text recipeCounterText;

    // Oven suojacollider
    public GameObject wallCollider;
    private void Awake()
    {
        // Nollataan reseptipala laskuri pelin alussa 
        recipeAmmount = 0;
    }

    private void Update()
    {
        // Tulostetaan ker‰ttyjen reseptipalojen lukum‰‰r‰ konsoliin
        recipeCounterText.text = recipeAmmount.ToString() + " / 8";

        // Tutkitaan onko reseptinpaloja ker‰tty tarpeeksi
        if(recipeAmmount == 4)
        {
            // Poistetaan oven suojacollider
            wallCollider.SetActive(false);
        }
    }

    // Metodi kasvattaa reseptipala laskuria yhdell‰
    
    public void AddRecipe()
    {
        recipeAmmount++;
    }
}
