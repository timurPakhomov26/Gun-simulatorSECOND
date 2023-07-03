using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(CapsuleCollider2D))]
public class Bullet : MonoBehaviour
{ 
   [SerializeField] private float speed = 10;
    public float Speed => speed;


    private void OnTriggerEnter2D(Collider2D other) 
    {
       if(other.gameObject.CompareTag("Destroy"))
       {
          gameObject.SetActive(false);
       }    
    }
}
