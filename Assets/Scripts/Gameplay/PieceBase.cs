using UnityEngine;

public class PieceBase : MonoBehaviour
{
    public void SetPosition(Vector3 tilePos)
    {
        transform.position = tilePos;
    }
}
