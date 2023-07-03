using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 3;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private Bullet _bulletPrefab;

    private PoolMono<Bullet> _pool;
    

    private void Start() 
    {
      _pool = new PoolMono<Bullet>(_bulletPrefab,_poolCount,transform);
      _pool.AutoExpand = _autoExpand;    
    }

    public void Create(Vector3 position,Vector3 direction,Sprite sprite)
    {
       var  bullet = _pool.GetFreeElement();
       bullet.GetComponent<SpriteRenderer>().sprite = sprite;
       bullet.transform.position = position;
       bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bullet.Speed,ForceMode2D.Impulse);
    } 
}
