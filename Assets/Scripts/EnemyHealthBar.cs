using UnityEngine;

public class EnemyHealthBar : HealthBar
{
    [SerializeField] private Transform enemyTransform;

    private Transform myTransform;

    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (IsTurningHealthBarNeeded())
        {
            TurnHealthBar();
        }
    }

    private bool IsTurningHealthBarNeeded()
    {
        if (enemyTransform.localScale.x < 0 && myTransform.localScale.x > 0)
        {
            return true;
        }
        else if (enemyTransform.localScale.x > 0 && myTransform.localScale.x < 0)
        {
            return true;
        }

        return false;
    }

    private void TurnHealthBar()
    {
        myTransform.localScale = new Vector3(myTransform.localScale.x * -1, myTransform.localScale.y, myTransform.localScale.z);
    }
}
