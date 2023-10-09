using UnityEngine;

public class BedRoaomDoorAction : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _objectDistance = 3f;
    [SerializeField] private Sprite _doorSprite;
    [SerializeField] private AudioSource _audioSource;
    private SpriteRenderer _doorRenderer;

    void Start()
    {
        _doorRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = Vector3.Distance(_playerPosition.position, transform.position);
        if (distance <= _objectDistance)
        {
            _doorRenderer.sprite = _doorSprite;
            _audioSource.Play();
            gameObject.SetActive(false);
        }
    }
}
