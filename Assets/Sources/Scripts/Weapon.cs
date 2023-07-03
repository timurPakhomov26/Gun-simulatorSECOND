using System.Collections;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public static Action OnShot;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Item[] _items;
    [SerializeField] private UiController _uiController;
    public int ItemsLenght => _items.Length;

    private int _index;
    private int _previusItemIndex = -1;
    private float _time;
    private bool _isMobileButtonDown = false;
    
    private bool _isShotButtonDown = false;


   private void OnEnable() 
   {
      for(int i=0; i < _items.Length;i++)
      {
         _items[i].Builder = _items[i].IBuilderGameObject.GetComponent<IBuilder>();
        
      } 
   }

   private void Start() 
   {

      foreach(var item in _items)
      {
        item.gameObject.SetActive(false);
        item.UIweaponPanel.SetActive(false);
        //item.IsBuyed = false;
        item.GunTriggers.enabled = false;
      }

      _items[0].IsBuyed = true;
      _items[0].GunTriggers.enabled = true;
       
       EquipItem(0);   
   }


    public void ZatvorInput()
    {
       if(_items[UiController.WeaponIndex].Builder.MagazineInWeapon == true)
       {
         _items[UiController.WeaponIndex].IsLoaded = true;
       }

       _items[UiController.WeaponIndex].Animator.SetTrigger("Load");
       StartCoroutine(ComeAnimationToDefault());

    }


    private IEnumerator ComeAnimationToDefault()
    {
       yield return new WaitForSeconds(0.15f);
       _items[UiController.WeaponIndex].Animator.SetTrigger("Idle");
    }


    private void Update() 
    {
       
      _time += Time.deltaTime;
      EquipItem(UiController.WeaponIndex);
      
      if(_items[UiController.WeaponIndex].Builder.MagazineInWeapon == true)
      {
         _items[UiController.WeaponIndex].Builder.OnMouse = false;
      }

    }

   public void CheckOnMagazineAnimation()
    {
       if(_items[UiController.WeaponIndex].Builder.MagazineInTrigger == true)
       {
          _items[UiController.WeaponIndex].Magazine.SetTrigger("Magazine");
         _items[UiController.WeaponIndex].CurrentBulletsCount += _items[UiController.WeaponIndex].CountOfAddedBullets;
         if(_items[UiController.WeaponIndex].CurrentBulletsCount >= _items[UiController.WeaponIndex].WeaponInfoo.BulletsCount)
         {
           _items[UiController.WeaponIndex].Builder.MagazineInWeapon = true;

         }
          _uiController.ApplyUiElements();
       }
    }

    public void ReloadMagazine()
    {
       _items[UiController.WeaponIndex].IsLoaded = false;
       _items[UiController.WeaponIndex].Builder.IsCollider = false;
       _items[UiController.WeaponIndex].Builder.MagazineInTrigger = false;
       _items[UiController.WeaponIndex].Builder.MagazineInWeapon = false;
       _items[UiController.WeaponIndex].Magazine.SetTrigger("NewMagazine");
       
    }


     private void EquipItem(int index)
     {
        _index = index;

        if(_index == _previusItemIndex)
           return;

        if(_index >= _items.Length)
           return;
       
        _items[_index].WeaponGameobject.SetActive(true);
        _items[_index].UIweaponPanel.SetActive(true); 

        if(_previusItemIndex != -1)
        {
           _items[_previusItemIndex].WeaponGameobject.SetActive(false);
           _items[_previusItemIndex].UIweaponPanel.SetActive(false);
        
        }

        _previusItemIndex = _index;
     }

     
}


public enum ShootingType
{
   Single,
   Automaatic
}

