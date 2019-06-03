using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Control_Simple : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    float Coef_Acceleration = 0.02f;
    [SerializeField]
    float Coef_Freinage = 0.1f;

    [SerializeField]
    float Max_Acceleration = 0.5f;
    [SerializeField]
    float Max_Vitesse = 2f;

    [SerializeField]
    float Coef_Acceleration_Angulaire = 1f;
    [SerializeField]
    float Coef_Freinage_Angulaire = 3f;

    [SerializeField]
    float Max_Acceleration_Angulaire = 15f;
    [SerializeField]
    float Max_Vitesse_Angulaire = 60f;


    float acceleration = 0;
    float vitesse = 0;
    float acceleration_Angulaire = 0;
    float vitesse_Angulaire = 0;

    public GameObject Balle;

    Vector2 Direction = Vector2.up;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Calcul_Vitesse();
        Calcul_Rotation();

        float angle = rb.rotation; // Récupération de l'angle actuel du véhicule

        rb.angularVelocity = vitesse_Angulaire; // Assignation de la vitesse angulaire
        rb.velocity = Rotate(Direction, angle ) * vitesse; // Assignation de la vitesse linéaire

        if (Input.GetAxis("Fire1") > 0)
            
    }

    private void Calcul_Vitesse()
    {
        // Récupération des Input
        float move_y = Input.GetAxis("Vertical");

        // Assignation de l'acceleration rectiligne
        if (move_y != 0) // Si l'Input est présent
            acceleration = move_y * Coef_Acceleration;
        else // Si aucune touche n'est appuyé
        {
            if (vitesse > 0) // Si la vitesse est positive
                acceleration = -Coef_Freinage; // Le freinage est négatif
            else if (vitesse < 0) // Si la vitesse est négative
                acceleration = +Coef_Freinage; // Le freinage est positif
            else // Si la vitesse est nul l'acceleration est nul
                acceleration = 0;
        }

        // Correction de la vitesse
        if ((vitesse > 0 && (vitesse + acceleration) < 0) || (vitesse < 0 && (vitesse + acceleration) > 0)) // Si l'acceleration inverse la vitesse
            vitesse = 0; // On stop le véhicule
        else // Sinon
            vitesse += acceleration; // On ajoute l'acceleration

        vitesse = Mathf.Clamp(vitesse, -Max_Vitesse, Max_Vitesse); // On Encadre la vitesse
    }

    private void Calcul_Rotation()
    {
        // Récupération des Input
        float move_x = Input.GetAxis("Horizontal");

        // Assignation de l'acceleration rotative
        if (move_x != 0)// Si l'Input est présent
            acceleration_Angulaire = -move_x * Coef_Acceleration_Angulaire;
        else // Si aucune touche n'est appuyé
        {
            if (vitesse_Angulaire > 0) // Si la vitesse est positive
                acceleration_Angulaire = -Coef_Freinage_Angulaire; // Le freinage est négatif
            else if (vitesse_Angulaire < 0) // Si la vitesse est négative
                acceleration_Angulaire = +Coef_Freinage_Angulaire; // Le freinage est positif
            else // Si la vitesse est nul l'acceleration est nul
                acceleration_Angulaire = 0;
        }

        // Correction de la vitesse
        if ((vitesse_Angulaire > 0 && (vitesse_Angulaire + acceleration_Angulaire) < 0) || (vitesse_Angulaire < 0 && (vitesse_Angulaire + acceleration_Angulaire) > 0)) // Si l'acceleration inverse la vitesse
            vitesse_Angulaire = 0; // On stop le véhicule
        else // Sinon
            vitesse_Angulaire += acceleration_Angulaire; // On ajoute l'acceleration

        vitesse_Angulaire = Mathf.Clamp(vitesse_Angulaire, -Max_Vitesse_Angulaire, Max_Vitesse_Angulaire); // On Encadre la vitesse
    }


    // Cette Fonction permet de faire tourner un vecteur d'un nombre de degré donné
    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
