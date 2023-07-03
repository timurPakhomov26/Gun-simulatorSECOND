using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GunTrigger : MonoBehaviour
{
    
    private bool IsDrag = false;
    public Item item;
   [SerializeField] private Init _init;
   [SerializeField] private UiController _uiController;
   [SerializeField] private Weapon _weapon;
   
     private float _time;
    
    private void OnMouseDown() 
    {

      if(item.WeaponInfoo.ShootingType == ShootingType.Automaatic)
      {
        IsDrag = true; 


       if(item.IsLoaded == false || item.CurrentBulletsCount <= 0)
       {
          item.Animator.SetTrigger("ShotWithout");
                
         StartCoroutine(ComeAnimationToDefault());
       }

       else if(item.IsLoaded == true && _time >= item.WeaponInfoo.RateOfFire && item.CurrentBulletsCount > 0)
       {
           item.Sound.ShotSoundPlay();
          item.Animator.SetBool("Shot",true);
       }
      }
      else if(item.WeaponInfoo.ShootingType == ShootingType.Single)
      {
         if(item.IsLoaded == true )
              {                                  

                 if(item.CurrentBulletsCount <= 0)
                       {

                         _weapon.ReloadMagazine();
                
                          StartCoroutine(ComeAnimationToDefault());
                         
                       }
                 if(item.CurrentBulletsCount > 0)
                       {

                        if(UiController.InfinityBulletsActive == true)
                        {
                            item.Animator.SetBool("Shot",true);
                            // Shot(0);
                             _time = 0;
                             StartCoroutine(ComeAnimationToDefault());
                        }
                 else if(UiController.InfinityBulletsActive == false)
                         {
                           item.Animator.SetBool("Shot",true);
                          // Shot(1);
                           _time = 0;
                           StartCoroutine(ComeAnimationToDefault());
                         }   
                     
                      }  
              }
              else
              {
                  item.Animator.SetTrigger("ShotWithout");
                
                StartCoroutine(ComeAnimationToDefault());
              }
      }
               
    }

    public void Shot(int bullet)
    {
                         
       
        item.CurrentBulletsCount -= bullet;
     
       _init.playerData.CoinsValue += item.WeaponInfoo.CoinsPerShot;
           
       _uiController.ApplyUiElements();
      _uiController.AddExperience();
     // StartCoroutine(MuzlleFlash());
                       
    }

    private void OnMouseUp() 
    {
      if(item.WeaponInfoo.ShootingType == ShootingType.Automaatic)
      {
           StartCoroutine(ComeAnimationToDefaultAutomatic());
      }
      
       IsDrag = false;   

    }

    private void Update() 
    {
      _time += Time.deltaTime;    
    }

     private void OnMouseDrag() 
     {
          
              if(item.IsLoaded == true && IsDrag == true)
              {
                 if(_time >= item.WeaponInfoo.RateOfFire)
                   { 

                       if(item.CurrentBulletsCount <= 0)
                       {
                         _weapon.ReloadMagazine();
                          StartCoroutine(ComeAnimationToDefault());
                       }

                       if(item.CurrentBulletsCount > 0)
                       {    
                         if(UiController.InfinityBulletsActive == false)
                         {
                            //Shot(1);
                            _time = 0;
                         }  
                         else if(UiController.InfinityBulletsActive == true)
                         {
                             //Shot(0);
                             _time = 0;
                         } 
                         
                       }                      
                     
                      }  
              }

         }

 
    private IEnumerator ComeAnimationToDefaultAutomatic()
    {
      
       yield return new WaitForSeconds(0.3f);
       item.Animator.SetBool("Shot",false); 
        item.Animator.SetTrigger("Idle");
       item.Sound.ShotSoundStop();
    }
     private IEnumerator ComeAnimationToDefault()
    {
      
       yield return new WaitForSeconds(0.3f);
       item.Animator.SetBool("Shot",false); 
        item.Animator.SetTrigger("Idle");
       
    }

    
}
