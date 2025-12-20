using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] dogs;
    public GameObject food;
    public GameObject live;

    float bound = 48f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepareGameObjectsForLevel(GameManager.Instance.Level());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareGameObjectsForLevel(int level)
    {
        int[] dogCountArr = GameManager.Instance.DogCountArray();

        if (level == 1)
        {
            InstantiateObject(dogs[0], dogCountArr[0]);
            InstantiateObject(dogs[1], dogCountArr[1]);
            InstantiateObject(dogs[2], dogCountArr[2]);
            InstantiateObject(dogs[3], dogCountArr[3]);

            InstantiateObject(food, GameManager.Instance.FoodCount());
            InstantiateObject(live, GameManager.Instance.LiveCount());
        }else
        {
            Debug.LogError("Only level one is ready at the moment");
        }
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
