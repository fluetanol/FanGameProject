using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer _sRenderer;

    private Material _mat;
    private BoxCollider2D _collider;

    void Start()
    {
        _mat = _sRenderer.material;
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,_collider.size * 0.6f ,0, -transform.up, 1.5f);

        if(hits.Length>1) {
            float distance = Vector2.Distance(transform.position,hits[1].point);
            float value = (1.7f - distance) > 0 ? 1.7f - distance : 0;
            _mat.SetFloat("_Down", value);
        }
        else{
            float value = _mat.GetFloat("_Down") > 0 ? _mat.GetFloat("_Down")-0.01f : 0;
            _mat.SetFloat("_Down", value);
        }


    }
}
