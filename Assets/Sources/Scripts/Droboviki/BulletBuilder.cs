using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(CapsuleCollider2D),typeof(Animator))]
public class BulletBuilder : MonoBehaviour, IBuilder
{
   [SerializeField] private Weapon _weapon;

   public bool MagazineInWeapon{get ; set ; }
   
    public bool MagazineInTrigger{get ; set ; }
    public bool IsCollider{get ; set ; }
    public bool Enabled { get; set ; }
   
    

    public bool OnMouse{get; set; }
    private Vector3 _startPosition;
    private Vector3 _cursorPosition;
   

   private Action OnTrigger;
    
    

   private void OnEnable() 
   {
    
    OnTrigger += CheckMagazine;
   }

   private void OnDisable() 
   {
    
    OnTrigger -= CheckMagazine;
   }

    private void Start()
    {
       OnMouse = false;
      _startPosition = transform.position;
    }

    private void OnMouseDown()
    { 
      OnMouse = true;
    }
    private void OnMouseUp()
    {
       OnMouse = false;
    }

    private void Update() 
    {
       SetEnable();  
    }

    private void LateUpdate()
    {
      
        _cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cursorPosition.z = 0;

        
          if (OnMouse == true)
           {

            if(IsCollider == false)
            {
              transform.position = _cursorPosition;

            }
              else if(IsCollider == true)
             {
                        
               OnMouse = false;
             
               MagazineInTrigger = true;
               OnTrigger?.Invoke();           
 
           }

           }
        
    }  

    private void OnTriggerEnter2D( Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Spawn"))
        {
            IsCollider = true;
        }
    }
   

    private void CheckMagazine()
    {
       _weapon.CheckOnMagazineAnimation();
    }

    public void SetEnable()
    {
        if(Enabled == true)
           this.enabled = true;
        else
         this.enabled = false;
    }
}
