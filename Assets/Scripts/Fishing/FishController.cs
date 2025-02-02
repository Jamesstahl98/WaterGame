using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public string Name;
    public IPickupable ScriptableObject;

    private SpriteRenderer sr;
    private float speed;
    private (Vector2, Vector2) patrolPoints;
    private bool movingToPoint1 = true;
    private bool caught = false;

    public void Init(IPickupable fish)
    {
        ScriptableObject = fish;
        sr = GetComponent<SpriteRenderer>();
        Name = fish.GetName();

        sr.sprite = fish.GetSprite();
        speed = fish.GetSpeed();

        float patrolY = Random.Range(-fish.GetMinDepth(), -fish.GetMaxDepth());

        patrolPoints.Item1 = new Vector2(Random.Range(0, 20f), patrolY);
        patrolPoints.Item2 = new Vector2(Random.Range(-20f, 0), patrolY);

        transform.position = new Vector2(Random.Range(patrolPoints.Item1.x, patrolPoints.Item2.x), patrolY);

        Vector2 S = sr.sprite.bounds.size;
        GetComponent<BoxCollider2D>().size = S;
    }

    private void Update()
    {
        if(!caught)
        {
            Move();
        }
    }

    public void HookCollision()
    {
        caught = true;
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, movingToPoint1 ? patrolPoints.Item1 : patrolPoints.Item2, step);

        if (transform.position.x == patrolPoints.Item1.x)
        {
            movingToPoint1 = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x == patrolPoints.Item2.x)
        {
            movingToPoint1 = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
