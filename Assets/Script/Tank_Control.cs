using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Control : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private Chenille_Tank droite;
    [SerializeField]
    private Chenille_Tank gauche;

    Vector2 Direction = new Vector2(0,1);
    float Vitesse = 0;
    float VitesseRotation = 0;


    [SerializeField]
    float freinage = 1/180f;
    [SerializeField]
    float coefvitesse = 20/60f;
    [SerializeField]
    float coefrotation = 1 / 60f;
    [SerializeField]
    float Vitesse_Max = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gauche.SetInfo(freinage, Vitesse_Max);
        droite.SetInfo(freinage, Vitesse_Max);
    }

    // Update is called once per frame
    void Update()
    {
        recaculateVitesse();
    }

    void recaculateVitesse()
    {
        gauche.recaculateVitesse(Vitesse);
        droite.recaculateVitesse(Vitesse);

        float f1 = gauche.Force;
        float f2 = droite.Force;

        VitesseRotation = (f2 - f1);
        if (f1 > 0 && f2 > 0)
            Vitesse += Mathf.Min(f1, f2);
        else if (f1 < 0 && f2 < 0)
            Vitesse += Mathf.Max(f1, f2);
        else
        {
            if (Mathf.Abs(f1) < Mathf.Abs(f2))
                Vitesse += f1;
            else
                Vitesse += f2;
        }


        rb.velocity = Direction * Vitesse * coefvitesse;
        rb.angularVelocity = VitesseRotation * coefrotation;
    }
}
