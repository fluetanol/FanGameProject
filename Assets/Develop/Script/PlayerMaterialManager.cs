using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer _sRenderer;

    private Material _mat;
    private CapsuleCollider2D _collider;

    void Start()
    {
        _mat = _sRenderer.material;
        _collider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,_collider.size * 0.6f ,0, -transform.up, 1.5f, LayerMask.GetMask("Default"));

        if(hits.Length>0) {
            float distance = Vector2.Distance(transform.position,hits[0].point);
            float value = (1.7f - distance) > 0 ? 1.7f - distance : 0;
            _mat.SetFloat("_Down", value);
        }
        else{
            float value = _mat.GetFloat("_Down") > 0 ? _mat.GetFloat("_Down")-0.01f : 0;
            _mat.SetFloat("_Down", value);
        }
    }
}
