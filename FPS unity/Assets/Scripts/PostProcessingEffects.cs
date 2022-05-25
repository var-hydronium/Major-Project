using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessingEffects : MonoBehaviour
{
    public static PostProcessingEffects _instance;

    [SerializeField] Volume _volume;
    [SerializeField] float _impactTime;

   

    //effects
    Bloom _bloom;
    ChromaticAberration _chrom;
    ColorAdjustments _colAdjust;

    //Adjusting Settings
    [SerializeField] float _bloomIntensity;
    [SerializeField] float _chromIntensity;
    [SerializeField] float _postExpoIntensity;

    void Start()
    {
        _instance = this;
    }


    void Update()
    {
        if (_volume == null)
        {
            _volume = gameObject.GetComponent<Volume>();

            if (_volume.profile.TryGet<Bloom>(out _bloom))
            {
            }
            if (_volume.profile.TryGet<ChromaticAberration>(out _chrom)) { }
        }

    }
    public void postExpo()
    {
        StartCoroutine(flash());
    }
    public IEnumerator flash()
    {
        GetPostProcess();
        _colAdjust.postExposure.value = _postExpoIntensity;
        yield return new WaitForSeconds(_impactTime);
        GetPostProcess();
        _colAdjust.postExposure.value = 0;
        
    }

    public void EffectsOnEnemyDead()
    {
        if (_volume.profile.TryGet<Bloom>(out _bloom)) { }
        if (_volume.profile.TryGet<ChromaticAberration>(out _chrom)) { }
        StartCoroutine(EnemyDead(_impactTime));
    }

    public void EffectsOnEnemyHit(GameObject _enemy)
    {
        float _waitingTime = _impactTime;
        Color enemyCol = _enemy.GetComponent<SpriteRenderer>().color;
        StartCoroutine(EnemyHit(_waitingTime, _enemy, enemyCol));
    }

    IEnumerator EnemyHit(float _waitingTime, GameObject _enemy, Color _originalCol)
    {
        float scaleX = _enemy.transform.localScale.x;
        float scaleY = _enemy.transform.localScale.y;
        _enemy.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        _enemy.transform.localScale = new Vector2(scaleX, scaleY-.2f);
       // _enemy.transform.localScale = new Vector2(Random.Range(.8f, 1f), Random.Range(.8f, 1f));
        yield return new WaitForSeconds(_waitingTime);
        if (_enemy != null)
        {
            _enemy.transform.localScale = new Vector2(scaleX,scaleY);
            _enemy.GetComponent<SpriteRenderer>().color = _originalCol;
            
        }
            
    }

    public void Fire()
    {
        StartCoroutine(EnemyDead(_impactTime));
    }

    IEnumerator EnemyDead(float _impactTime)
    {
        GetPostProcess();
        _bloom.intensity.value = _bloomIntensity;
        _chrom.intensity.value = .5f;
        yield return new WaitForSeconds(_impactTime);
        GetPostProcess();
        _bloom.intensity.value = 1f;
        _chrom.intensity.value = 0f;
    }

    public void GetPostProcess()
    {
        _volume.profile.TryGet<Bloom>(out _bloom);
        _volume.profile.TryGet<ChromaticAberration>(out _chrom);
        _volume.profile.TryGet<ColorAdjustments>(out _colAdjust);
    }

}
