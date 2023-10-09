using UnityEngine;

public class TitleCorridor : MonoBehaviour
{
    [SerializeField] Transform _corridorPosition;
    Vector3 pos = new Vector3(-30.6f, 0, 0);
    private bool _corridorWait = false;
    [SerializeField] private float _corridorSpeed;
    
    void Update()
    {
        //�w�i���E�Ɉړ�
        transform.position += new Vector3(_corridorSpeed, 0, 0) * Time.deltaTime;

        //�V�����L���̔w�i���v���n�u�Ő��ݏo��
        if (_corridorPosition.position.x >= 0 && !_corridorWait)
        {
            Instantiate(_corridorPosition, pos, Quaternion.identity);
            _corridorWait = true;
        }

        //���������
        if (_corridorPosition.position.x >= 25)
        {
            Destroy(this.gameObject);
        }
    }
}
