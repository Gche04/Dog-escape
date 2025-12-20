using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] dogs;
    public GameObject food;
    public GameObject live;
    public int foodcount = 20;

    float bound = 48f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiateObject(dogs[0], 1);
        InstantiateObject(dogs[1], 4);
        InstantiateObject(dogs[2], 6);
        InstantiateObject(food, foodcount);
        InstantiateObject(live, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateObject(GameObject thing, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(thing, RandomPos(), thing.transform.rotation);
        }
    }

    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-bound, bound), 0, Random.Range(-bound, bound));
    }
}
