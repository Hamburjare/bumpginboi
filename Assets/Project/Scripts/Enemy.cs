using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    

    public LayerMask whatIsGround;      // Kerros, jossa vihollinen 1iikkuu
    public float speed = 1;             // Vihollisen nopeus

    private Rigidbody2D myBody;         // Vihollisen fysiikkamoottori
    private Transform myTrans;          // Vihollisen sijainti
    private float myWidth;              // Vihollisen leveys

    private bool isGrounded;

    private void Start()
    {
        // Luodaan yhteys vihollisen Transformiin
        myTrans = transform;
        // Luodaan yhteys vihollisen fysiikkamoottoriin
        myBody = GetComponent<Rigidbody2D>();
        // Otetaan talteen vihollisen piirtokomponentin leveys
        myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    

    // Vihollinen liikkuu tasolla edestakas.
    void FixedUpdate()
    {
        // Vihollilnen tarkistaa onko edess‰ naata (isGrounded = true) ennen kuin liikkuu eteenp‰in.
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, whatIsGround);


        // Jos edess‰ ei ole maata(isGrounded = false), vihollinen k‰‰ntyy ymp‰ri
        if (!isGrounded) {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        // N‰m‰ koodin p‰tk‰t pit‰v‰t huolen siit‰ ett‰ vihollinen menee aina eteenp‰in.
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }
       

}
