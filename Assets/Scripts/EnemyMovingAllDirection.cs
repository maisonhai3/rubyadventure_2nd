using UnityEngine;

/// <summary>
/// This class is created with the Template Method pattern in mind.
/// It can do what the Enemy class does, but the direction is not confined to horizontal or vertical.
/// </summary>
public class EnemyMovingAllDirection : Enemy
{
    private new void Start()
    {
        base.Start();

        GetDirection();
    }

    public override void GetDirection()
    {
        direction = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2));
    }
}
