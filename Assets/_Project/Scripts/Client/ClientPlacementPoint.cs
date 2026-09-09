using UnityEngine;

public class ClientPlacementPoint : MonoBehaviour
{
    public static ClientPlacementPoint Instance;
    private void Awake()
    {
        Instance = this;
    }
}
