using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] public GameObject block;
    void BasicBlock()
    {
        Instantiate(block, Input.mousePosition, Quaternion.identity);
    }
}
