using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    [SerializeField] private Transform bottomBoundary;
    [SerializeField] private Transform topBoundary;

    public Transform GetLeftBoundary()
    {
        return leftBoundary;
    }
    public Transform GetRightBoundary()
    {
        return rightBoundary;
    }
    public Transform GetBottomBoundary()
    {
        return bottomBoundary;
    }
    public Transform GetTopBoundary()
    {
        return topBoundary;
    }
}
