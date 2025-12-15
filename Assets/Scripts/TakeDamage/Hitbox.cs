using UnityEngine;




public class Hitbox : MonoBehaviour
{
    public float damage = 1f;
    public GameObject owner;
    
    // Unqiue ID per attack instance
    public int attackID;

 
    private void Awake()
    {
        attackID = Random.Range(int.MinValue, int.MaxValue); //probably change to a class that generates a unique ID
    }
    
}
