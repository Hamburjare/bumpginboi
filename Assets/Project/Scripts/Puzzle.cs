using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unityn tapahtumien hallinta
using UnityEngine.Events;

/*  Puzzle luokan teht‰vi‰ on 
    *** deaktivoida kytkin ja k‰ynnist‰‰ pulma
    *** suorittaa kytkin animaatio sek‰ portin aukaisu animaatio
*/

public class Puzzle : MonoBehaviour
{
    // Tapahtuma, joka k‰ynnistyy kun tˆrm‰t‰‰n porttiin
    public UnityEvent OnPuzzle;

    // Animaattorit 
    private Animator switchAnim; // Kytkimen animaatio
    [SerializeField]
    private Animator gateAnim; // Portin animaatio

    // Start is called before the first frame update
    void Start()
    {
        // Kytkimen animaattori otetaan k‰yttˆˆn
        switchAnim = GetComponent<Animator>();

    }

    // Pulmaan liitetty triggeri, joka k‰ynnistyy pelaajan toimesta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Deaktivoi kytkimen collaiderin
            GetComponent<BoxCollider2D>().enabled = false; 
            // Tapahtuma (pulman kysymys) on k‰ynnistetty. Pulmakotrolleri voi nyt k‰sitell‰ pulman
            OnPuzzle?.Invoke();
        }
    }

    // Suorittaa tiettyyn pulmaan liittyv‰t animaatio. Pulmat numeroitu 1, 2, 3, ...
    public void HandleAnimations(int _puzzleID)
    {
        
        // K‰ynnist‰‰ pulmaan liittyv‰t animaatiot
        switch (_puzzleID)
        {
            // Pulma 1
            case 1:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ‰‰ni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            // Pulma 2
            case 2:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ‰‰ni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            case 3:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ‰‰ni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            case 4:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ‰‰ni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            default:
                // Ei tehd‰ mit‰‰n
                break;
        }
    }

}
