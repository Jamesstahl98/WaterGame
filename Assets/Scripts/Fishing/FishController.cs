using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishController : MonoBehaviour
{
    private SpriteRenderer sr;

    [HideInInspector]public string Name;

    private float speed;
    private (Vector2, Vector2) patrolPoints;
    private bool movingToPoint1 = true;

    public void Init(FishScriptableObject fish)
    {
        sr = GetComponent<SpriteRenderer>();
        Name = fish.name;

        sr.sprite = fish.sprite;
        speed = fish.speed;

        transform.position = new Vector2(0, Random.Range(-fish.minDepth, -fish.maxDepth));

        patrolPoints.Item1 = new Vector2(Random.Range(-20f, 20f), transform.position.y);
        patrolPoints.Item2 = new Vector2(Random.Range(-20f, 20f), transform.position.y);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, movingToPoint1 ? patrolPoints.Item1 : patrolPoints.Item2, step);
        
        if(transform.position.x == patrolPoints.Item1.x)
        {
            Debug.Log("A");
            movingToPoint1 = false;
        }
        else if(transform.position.x == patrolPoints.Item2.x)
        {
            Debug.Log("B");
            movingToPoint1 = true;
        }
    }
}
