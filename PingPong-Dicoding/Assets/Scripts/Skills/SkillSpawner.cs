using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : MonoBehaviour
{

    [Header("Skills Random Properties")] 
    [SerializeField] private GameObject[] skills;
    [SerializeField] private Vector2 range;
    [SerializeField, Range(3,10)] private float minAppear;
    [SerializeField, Range(3,10)] private float maxAppear;
    
    private float nextAppear;

    private List<GameObject> skillQueue = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        nextAppear = RandomTime();
        skillQueue.AddRange(skills);
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    Vector2 RandomPosition()
    {
        float x = Random.Range(0, range.x);
        float y = Random.Range(0, range.y);
        
        Vector2 pos = new Vector2(x,y);

        if (Physics2D.OverlapCircle(pos, 1f)) return RandomPosition();
        
        return pos;
    }

    void Spawn()
    {
        if (nextAppear > 0) nextAppear -= Time.deltaTime;
        else
        {
            Vector2 pos = RandomPosition();
            Instantiate(RandomObject(), pos, Quaternion.identity);
            nextAppear = RandomTime();
            if (skillQueue.Count == 0) skillQueue.AddRange(skills);
        }
    }

    GameObject RandomObject()
    {
        int index = Random.Range(0, skillQueue.Count - 1);
        GameObject obj = skillQueue[index];
        skillQueue.RemoveAt(index);
        return obj;
    }

    float RandomTime()
    {
        return Random.Range(minAppear, maxAppear);
    }

    private void OnDrawGizmos() // Just for visualize in editor
    {
        Gizmos.DrawWireCube(transform.position, range*2);
    }
}
