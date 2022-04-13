using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Tehd‰‰n pelihahmosta Singelton
    public static PlayerController instance;

    [SerializeField] private float moveSpeed; // pelihahmon nopeus x-akselin suunnassa
    [SerializeField] private float jumpForce; // pelihahmon hyppynopeus
    
    // N‰pp‰inmuttujat
    public KeyCode left;    // n‰pp‰in, joka liikuttaa pelihahmoa vasemmalle
    public KeyCode right;   // n‰pp‰in, joka liikuttaa pelihahmoa oikealle
    public KeyCode jump;    // n‰pp‰in, joka hypyytt‰‰ pelihahmoa
           
    // Referenssi fysiikkamoottoriin
    private Rigidbody2D rb2d;

    // Referenssi piirtokomponentiin
    private SpriteRenderer spriteRenderer;

    // Referenssi animaattoriin
    //private Animator anim;
    public Animator MyAnim{ get; set; }

    // Voiko pelihahmo liikkua 
    public bool MyCanMove { get; set; }

    // Parempi hyppy
    private float fallMultiplier = 4f; // Mit‰ suurempi arvo sen nopeammin tullaan alas (1=normitila)
    private float lowJumpMultiplier = 2f; // Mit‰ suurempi arvo sit‰ pienempi hyppy (1=normitila)

    // Hyppyyn liittyv‰t muuttujat
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGround;  // Jos IsGround=true, niin ett‰ ollaan maassa

    // Tomupilvi
    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmission;

    // Start is called before the first frame update
    void Start()
    {
        // Otetaan Singelton k‰yttˆˆn
        instance = this;

        // Luodaan yhteys pelihahmon fysiikkamoottoriin
        rb2d = GetComponent<Rigidbody2D>();

        // Luodaan yhteys pelihahmon piirtokomponenttiin
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Luodaan yhteys animaattoriin
        //anim = GetComponent<Animator>();
        MyAnim = GetComponent<Animator>();

        // Pelihahmo voi liikkua oletuksena
        MyCanMove = true;

        // Luodaan yhteys Dust-effektin partikkelisysteemiin (emissio)
        footEmission = footsteps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        // Voiko pelihahmo liikkua? false = ei voi, t‰m‰ tieto saadaan PuzzleControllerista
        if (!MyCanMove)
        {
            // Ei voi, joten hyp‰t‰‰n Update-metodista pois
            rb2d.velocity = Vector3.zero;
            return;
        }

        // Tutkitaan onko kurpitsapoia maassa vai ilmassa
        // Imassa silloi kun isGround = false ja maassa kun isGround = true
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        // Liikutellaan pelihahmoa
        MovePlayer();

        //K‰sittele animaatiot
        HandleAnimation();
    }
    
    // K‰sittelee kurpitsapojan animaatiot
    private void HandleAnimation()
    {
        // K‰velyanimaation kutsu
        MyAnim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)); // Mathf.Abs(); Varmistaa ett‰ x eli nopeus on aina positiivinen

        // Hyppy animaation kutsu
        MyAnim.SetBool("Grounded", isGround);
    }

    private void MovePlayer()
    {
        // Liikkuuko pelihahmo vasemmalle?
        if (Input.GetKey(left))
        {
            // Kyll‰, joten suoritetaan liike
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            // Varmistetaan ett‰ pelihahmo katsoo menosuuntaan
            spriteRenderer.flipX = true;

            // Dust-effect p‰‰lle
            footEmission.rateMultiplier = 35f;
        }
        // Liikutaanko oikealle?
        else if (Input.GetKey(right))
        {
            // Kyll‰, joten suoritetaan liike
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            // Varmistetaan ett‰ pelihahmo katsoo menosuuntaan
            spriteRenderer.flipX = false;

            // Dust-effect p‰‰lle
            footEmission.rateMultiplier = 35f;  
        }
        else
        {
            // Dust-effect pois p‰‰lt‰
            footEmission.rateMultiplier = 0f;

            // Onko viel‰ ilmassa?
            if (rb2d.velocity.y != 0)
            {
                // Kyll‰, joten liike jatkuu
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            }
                
            else
            {
                // Ei, joten liike p‰‰ttyy
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
        // Painettiinko hyppypainiketta?
        if (Input.GetKeyDown(jump) && isGround)
        {
            // Hyppy‰‰ni
            AudioManager.instance.Play("Jump");

            // Kyll‰, joten pelihahmo hypp‰‰
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        BetterJump();
                
    }

    /// Kun hyppy painiketta painetaan nopeasti pelihahmo hypp‰‰ lyhyemm‰n matkan.
    /// Kun hyppy painikettaa pidet‰‰n alhaalla, niin pelihahmo hypp‰‰ korkeammalle ja
    /// hyppy kest‰‰ pidemp‰‰n.
    private void BetterJump()
    {
        // Ollaanko tulossa alasp‰in?
        if (rb2d.velocity.y < 0)
        {
            // N‰pp‰int‰ painetaan --> korkeampi hyppy
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            // Dust-effect pois p‰‰lt‰
            footEmission.rateMultiplier = 0f;
        }

        // 0llaanko ilmassa ja hyppypainiketta ei paineta?
        else if (rb2d.velocity.y > 0 && !Input.GetKey(jump))
        {
            // Kyll‰ joten --> matalampi hyppy
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

            // Dust-effect pois p‰‰lt‰
            footEmission.rateMultiplier = 0f;
        }
    }

}
