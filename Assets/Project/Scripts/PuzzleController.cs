using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*   T‰m‰n luokan teht‰vi‰ on
     *** n‰ytt‰‰ puzzleen liittyv‰ kysymys
     *** k‰sitell‰ kysymyksen vastaus
     *** Est‰‰ pelihahmon liikkuminen puzzlen aikana
*/

public class PuzzleController : MonoBehaviour
{

    // Tehd‰‰n luokasta staattinen, jotta sit‰ voidaan k‰ytt‰‰ muista koodeista
    public static PuzzleController instance;

    // Pulman k‰ynnist‰v‰t kytkimet tallennetaan taulukkoon 
    [SerializeField] private Puzzle[] puzzles;

    // Canavas - Matemaattinen teht‰v‰
    [SerializeField] private GameObject puzzlePanel;    // Paneli
    [SerializeField] private TextMeshProUGUI questionText;         // Matemaattinen teht‰v‰
    [SerializeField] private Button answer1Button;      // Vastauspainike 1
    [SerializeField] private Button answer2Button;      // Vastauspainike 2
    [SerializeField] private Button answer3Button;      // Vastauspainike 3
    [SerializeField] private TextMeshProUGUI answer1Text;          // Painikkee 1 teksti
    [SerializeField] private TextMeshProUGUI answer2Text;          // Painikkee 2 teksti
    [SerializeField] private TextMeshProUGUI answer3Text;          // Painikkee 3 teksti

    // Start is called before the first frame update
    void Start()
    {
        // Otetaan staattinen luokka k‰yttˆˆn
        instance = this;

    }

    // K‰ynnist‰‰ pulman k‰sittelyn
    public void HandlePuzzle(int _puzzleID)
    {
        // Estet‰‰n pelihahmon liikkuminen 
        PlayerController.instance.MyCanMove = false;

        // Siirryt‰‰n Idle-animaatioon
        PlayerController.instance.MyAnim.SetFloat("Speed", 0);
        PlayerController.instance.MyAnim.SetBool("Grounded", true);

        // N‰yt‰ matemaattinen teht‰v‰
        puzzlePanel.SetActive(true);

        switch (_puzzleID)
        {
            case 1:
                // Pelihahmo on osunut kytkimeen 1, joten k‰ynnistet‰‰n puzzle 1.
                ShowProblem(_puzzleID);
                break;
            case 2:
                // Pelihahmo on osunut kytkimeen 2, joten k‰ynnistet‰‰n puzzle 2.
                ShowProblem(_puzzleID);
                break;
            case 3:
                // Pelihahmo on osunut kytkimeen 3, joten k‰ynnistet‰‰n puzzle 3.
                ShowProblem(_puzzleID);
                break;
            case 4:
                // Pelihahmo on osunut kytkimeen 4, joten k‰ynnistet‰‰n puzzle 4.
                ShowProblem(_puzzleID);
                break;
            default:
                // Ei tehd‰ mit‰‰n
                break;
        }
    }


    public void ShowProblem(int _puzzleID)
    {
        System.Random random = new System.Random();
        int ekanumero = random.Next(0, 10);
        int tokanumero = random.Next(0, 10);
        int summavaierotus = random.Next(0, 2);
        int vastauspaikka = random.Next(1, 4);
        string v‰limerkki;

        if (summavaierotus == 1)
        {
            v‰limerkki = "+";
        } else
        {
            v‰limerkki = "-";
        }

        if (v‰limerkki == "-" && ekanumero < tokanumero)
        {
            int eka = ekanumero;
            int toka = tokanumero;
            tokanumero = eka;
            ekanumero = toka;
            
        }

        string ekanumerostring = System.Convert.ToString(ekanumero);
        string tokanumerostring = System.Convert.ToString(tokanumero);

        var vastaus = new System.Data.DataTable().Compute(ekanumerostring+v‰limerkki+tokanumerostring, null);

        questionText.text = ekanumerostring + " " + v‰limerkki + " " + tokanumerostring+ " = _";        // Matemaattinen teht‰v‰

        string ekav‰‰r‰ = System.Convert.ToString(random.Next(0, 21));
        string tokav‰‰r‰ = System.Convert.ToString(random.Next(0, 21));

        if (ekav‰‰r‰ == System.Convert.ToString(vastaus))
        {
            ekav‰‰r‰ += 1;
        }
        else if (tokav‰‰r‰ == System.Convert.ToString(vastaus))
        {
            tokav‰‰r‰ += 1;
        }

        string nimi = System.Convert.ToString(_puzzleID);

        if (vastauspaikka == 1)
        {

            answer1Button.name = nimi;
            answer1Text.text = System.Convert.ToString(vastaus);

            answer2Button.name = "Wrong";       // V‰‰r‰ painike                      
            answer2Text.text = ekav‰‰r‰;        // V‰‰r‰ vastaus

            answer3Button.name = "Wrong";       // V‰‰r‰ painike
            answer3Text.text = tokav‰‰r‰;       // V‰‰r‰ vastaus
        }
        else if (vastauspaikka == 2)
        {

            answer1Button.name = "Wrong";
            answer1Text.text = ekav‰‰r‰;

            answer2Button.name = nimi;
            answer2Text.text = System.Convert.ToString(vastaus);

            answer3Button.name = "Wrong";
            answer3Text.text = tokav‰‰r‰;
            
        }
        else if (vastauspaikka == 3)
        {

            answer1Button.name = "Wrong";
            answer1Text.text = ekav‰‰r‰;

            answer2Button.name = "Wrong";                     
            answer2Text.text = tokav‰‰r‰;

            answer3Button.name = nimi;
            answer3Text.text = System.Convert.ToString(vastaus);
        }

    }

    /*Pelaaja painaa vastauspainiketta.
     Oikea vastaus painike tunnistetaan painikkeen nimen perusteella.
    Painikkeet on nimetty 1, Wrong*/
    public void HandleCorrectAnswer(Button button)
    {
        switch (button.name)
        {
            case "1":
                //Oikea vastaus teht‰v‰‰n
                StartCoroutine(CheckAnswerCO(1));
                break;
            case "2":
                //Oikea vastaus teht‰v‰‰n
                StartCoroutine(CheckAnswerCO(2));
                break;
            case "3":
                //Oikea vastaus teht‰v‰‰n
                StartCoroutine(CheckAnswerCO(3));
                break;
            case "4":
                //Oikea vastaus teht‰v‰‰n
                StartCoroutine(CheckAnswerCO(4));
                break;
            default:
                //V‰‰r‰ vastaus teht‰v‰‰n
                StartCoroutine(CheckAnswerCO(0));
                break;
        }

    }

    /*  Alairutiini n‰ytt‰‰ 2 sekunnin ajan onko vastaus oikein vai v‰‰rin.
        Vastaus on v‰‰rin jos _puzzleID = 0
     */
    private IEnumerator CheckAnswerCO(int _puzzleID)
    {
        if (_puzzleID == 0)
        {
            questionText.text = "VƒƒRIN!";
        }
        
        else
        {
            questionText.text = "OIKEIN!";
            // K‰‰nnet‰‰n kytkin 1 auki
            puzzles[_puzzleID].HandleAnimations(_puzzleID);
        }
        yield return new WaitForSeconds(2f);
        // Piilotetaan paneli
        puzzlePanel.SetActive(false);
        // Pelihahmo voi liikkua
        PlayerController.instance.MyCanMove = true;
    }

}
