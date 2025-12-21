using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] dogs;
    public GameObject food;
    public GameObject live;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepareGameObjectsForLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PrepareGameObjectsForLevel()
    {
        int[] dogCountArr = GameManager.Instance.GetDogCounts();

        if (GameManager.Instance.GetIsANewGame())
        {
            InstantiateObject(dogs[0], dogCountArr[0]);
            InstantiateObject(dogs[1], dogCountArr[1]);
            InstantiateObject(dogs[2], dogCountArr[2]);
            InstantiateObject(dogs[3], dogCountArr[3]);

            InstantiateObject(food, GameManager.Instance.GetFood());
            InstantiateObject(live, GameManager.Instance.GetLive());
        }
        else
        {
            InstantiateObject(dogs[0], GameManager.Instance.GetSavedDog1Pos());
            InstantiateObject(dogs[1], GameManager.Instance.GetSavedDog2Pos());
            InstantiateObject(dogs[2], GameManager.Instance.GetSavedDog3Pos());
            InstantiateObject(dogs[3], GameManager.Instance.GetSavedDog4Pos());

            InstantiateObject(food, GameManager.Instance.GetSavedFoodPos());
            InstantiateObject(live, GameManager.Instance.GetSavedLivePos());
        }
    }

    void InstantiateObject(GameObject thing, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(thing, RandomPos(), thing.transform.rotation);
        }
    }

    void InstantiateObject(GameObject thing, List<Vector3> positions)
    {
        foreach (Vector3 position in positions)
        {
            Instantiate(thing, position, thing.transform.rotation);
        }
    }

    Vector3 RandomPos()
    {
        float bound = GameManager.Instance.GetBounary();
        return new Vector3(Random.Range(-bound, bound), 0, Random.Range(-bound, bound));
    }
}
