using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chenille_Tank : MonoBehaviour
{
    public float Force = 0;

    public float Acceleration = 0;

    [SerializeField]
    ChenilleType Direction = ChenilleType.Droite;

    float VitesseMax = 0f;
    float Freinage = 1f;
    float coefvitesse = 0.5f;
    float coefrotation = 1 / 60f;

    Vector3 old;

    public void SetInfo(float freinage_, float vitesseMax_)
    {
        Freinage = freinage_;
        VitesseMax = vitesseMax_;
    }


    // Start is called before the first frame update
    void Start()
    {
        old = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void recaculateVitesse(float vitesse)
    {
        float move_x = Input.GetAxis("Horizontal");
        float move_y = Input.GetAxis("Vertical");

        bool opposite = (vitesse > 0 && move_y < 0) || (vitesse < 0 && move_y > 0);

        if (opposite)
            Acceleration = -move_y;
        else
            Acceleration = move_y;
        if (move_y == 0 || opposite)
        {
            Acceleration /= (1 + Freinage);
            Acceleration -= vitesse * coefvitesse;
        }
        Acceleration = Mathf.Clamp(Acceleration, -VitesseMax, VitesseMax);
        float coef = 1.0f;

        Force = GetExp(Acceleration * (1 + Mathf.Abs(vitesse)));

        coef = Mathf.Min(Mathf.Max(VitesseMax - Mathf.Abs(vitesse), 0), 1);

        Force *= coef;


        if (ChenilleType.Droite == Direction)
        {
            coef = (move_x * Mathf.Exp(-vitesse / VitesseMax) + 1);
        }
        else if (ChenilleType.Gauche == Direction)
        {
            coef = (- move_x * Mathf.Exp(-vitesse / VitesseMax) + 1);
        }

        Force *= coef;
        Force = Mathf.Clamp(Force, -10, 10);
    }



    private float GetExp(float x)
    {
        return (1 - Mathf.Exp(-x));
    }
}

enum ChenilleType
{
    Droite,
    Gauche
}

