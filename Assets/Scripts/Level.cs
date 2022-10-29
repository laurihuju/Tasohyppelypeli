using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int coinAmount;
    [SerializeField] private Transform startPosition;
    [SerializeField] private CameraBoundaries cameraBoundaries;
    [SerializeField] private Sprite background;
    [SerializeField] private float backgroundScale;
    [SerializeField] private float backgroundPositionY;
    [SerializeField] private string musicName;

    public int GetCoinAmount()
    {
        return coinAmount;
    }

    public Transform GetStartPosition()
    {
        return startPosition;
    }

    public CameraBoundaries GetCameraBoundaries()
    {
        return cameraBoundaries;
    }

    public Sprite GetBackground()
    {
        return background;
    }

    public float GetBackgroundScale()
    {
        return backgroundScale;
    }

    public float GetBackgroundPositionY()
    {
        return backgroundPositionY;
    }

    public string GetMusicName()
    {
        return musicName;
    }
}
