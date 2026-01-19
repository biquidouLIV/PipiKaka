using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] public GameObject block;
    public void BasicBlock()
    {
        Instantiate(block, Input.mousePosition, Quaternion.identity);
    }
}
