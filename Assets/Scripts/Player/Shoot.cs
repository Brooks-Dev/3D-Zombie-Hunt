using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private float _coolDown = 1.0f;
    private float _timer;

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
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Health damagable = hit.transform.GetComponent<Health>();
                if (damagable != null)
                {
                    damagable.Damage(10);
                }
            }
            _timer = Time.time + _coolDown;
        }
    }
}
