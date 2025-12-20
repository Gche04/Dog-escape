using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public float bounary = 49f;

    // Update is called once per frame
    void Update()
    {
        
        //set boundary
        if (transform.position.x > bounary)
        {
            transform.position = new Vector3(bounary, transform.position.y, transform.position.z);
        }else if (transform.position.x < -bounary)
        {
            transform.position = new Vector3(-bounary, transform.position.y, transform.position.z);
        }else if (transform.position.z > bounary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bounary);
        }else if (transform.position.z < -bounary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -bounary);
        }
    
    }
}
