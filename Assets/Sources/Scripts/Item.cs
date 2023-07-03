using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{ 
   public  bool  IsBuyed = false;
   public WeaponInfo WeaponInfoo;
   public GameObject WeaponGameobject;
   
   public Animator Animator;
   public GameObject UIweaponPanel;

   public GameObject IBuilderGameObject;
   public IBuilder Builder;
   
   public bool IsLoaded = false;
   
   public Animator Magazine;
   public int CurrentBulletsCount;
   public int CountOfAddedBullets;

   public Transform BulletSpawn;

   public Init _init;
   public UiController _uiController;
   public GunTrigger GunTriggers;

   [Header("Sound")]
   public SoundController Sound;

    public void Shot()
    {
                         
      if(CurrentBulletsCount > 0)
      {
         if(UiController.InfinityBulletsActive == true)
         {
            CurrentBulletsCount -= 0;
         }
         else
         {
           CurrentBulletsCount -= 1;
         }

      }
     
       _init.playerData.CoinsValue += WeaponInfoo.CoinsPerShot;
           
       _uiController.ApplyUiElements();
      _uiController.AddExperience();
     // StartCoroutine(MuzlleFlash());
                       
    }
   
}
