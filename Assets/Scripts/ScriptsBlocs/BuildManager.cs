using UnityEngine;

public class BuildManager : MonoBehaviour // Pour l'instant pas utilisé
{
    [SerializeField] public GameObject block;
    public void BasicBlock()
    {
        Instantiate(block, Input.mousePosition, Quaternion.identity);
    }
}
