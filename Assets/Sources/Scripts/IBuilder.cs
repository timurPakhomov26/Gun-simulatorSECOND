
public interface IBuilder

{
     public bool MagazineInTrigger {get; set; }
     public bool MagazineInWeapon {get; set; }
     public bool IsCollider {get; set; }
     public bool OnMouse{get; set; }
     public bool Enabled {get; set; }

     void SetEnable();

}
