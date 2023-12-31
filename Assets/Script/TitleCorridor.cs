using UnityEngine;

public class TitleCorridor : MonoBehaviour
{
    [SerializeField] Transform _corridorPosition;
    Vector3 pos = new Vector3(-30.6f, 0, 0);
    private bool _corridorWait = false;
    [SerializeField] private float _corridorSpeed;
    
    void Update()
    {
        //背景を右に移動
        transform.position += new Vector3(_corridorSpeed, 0, 0) * Time.deltaTime;

        //新しい廊下の背景をプレハブで生み出す
        if (_corridorPosition.position.x >= 0 && !_corridorWait)
        {
            Instantiate(_corridorPosition, pos, Quaternion.identity);
            _corridorWait = true;
        }

        //これを消す
        if (_corridorPosition.position.x >= 25)
        {
            Destroy(this.gameObject);
        }
    }
}
