using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    float degat;
    float vitesse;
    Type_Balle Type = Type_Balle.ami;

    Vector2 depart = new Vector2(0,0);

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, vitesse);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.IsTouching(new Collider2D()))
            Destroy(this);
        if (Distance(depart, transform.position) > 50)
            Destroy(this);
    }


    float Distance(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
    }

}

enum Type_Balle
{
    ami,
    enemy,
    quelconque
}
