using UnityEngine;

public class RotationMasseBlock : MonoBehaviour
{
    [SerializeField] private float vitesseRotation = 5f;
    void Update()
    {
        transform.Rotate(new Vector3(0,0,vitesseRotation) * Time.deltaTime); // ça fait la rotation autour de l'axe z
    }
}
