using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private float _coolDown = 1.0f;
    private float _timer;
    [SerializeField]
    private GameObject _bloodPrefab;

    private void Start()
    {
        _timer = Time.time + _coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        RangeAttack();
    }

    private void RangeAttack()
    {
        if (Input.GetMouseButton(0) && _timer < Time.time)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, 1 << 9 | 1 << 0))
            {
                Health damagable = hit.transform.GetComponent<Health>();
                if (damagable != null)
                {
                    var hitPos = hit.point;
                    var hitNorm = hit.normal;
                    if (_bloodPrefab != null)
                    {
                        Instantiate(_bloodPrefab, hitPos, Quaternion.LookRotation(hitNorm));
                        damagable.Damage(50);
                    }
                }
            }
            _timer = Time.time + _coolDown;
        }
    }
}
