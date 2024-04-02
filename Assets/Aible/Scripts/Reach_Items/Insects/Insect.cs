using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Insect : MonoBehaviour
{
    [HideInInspector] public bool startMovement;
    [HideInInspector] public bool IsMoving;
    [HideInInspector] public Basket _basket;
    public MobType MobType;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private GameObject _itemPrefabReverse;
    public Vector3 TargetOffset;
    public bool ReverseEndPos;
    public float MoveDuration = 3f;
    public float LandDuration = 1f;

    private Vector3 _startPos;
    private Vector3 _endPos;

    private Coroutine _movementCoroutine;

    private Animator _animator;
    private Rigidbody _rb;
    private GameObject _item;
    private int _itemIndex;
    private bool _canDie;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (startMovement)
        {
            startMovement = false;
            _movementCoroutine = StartCoroutine(Movement());
        }
    }

    virtual protected IEnumerator Movement()
    {
        IsMoving = true;
        _canDie = true;

        _startPos = transform.position;

        if (ReverseEndPos)
        {
            _endPos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            _endPos = transform.position;
        }
        

        _item = GetItem();

        if (!_item)
        {
            IsMoving = false;
            Reach_Item_Actions.ReleaseMob(this);
            yield break;
        }

        _itemPrefab.SetActive(false);
        _itemPrefabReverse.SetActive(false);

        MoveToTarget(_item.transform.position, MoveDuration);

        yield return new WaitForSeconds(MoveDuration);

        Land();

        yield return new WaitForSeconds(LandDuration);

        TakeItem();
        _canDie = false;

        MoveToTarget(_endPos, MoveDuration);

        yield return new WaitForSeconds(MoveDuration);

        IsMoving = false;
        Reach_Item_Actions.ReleaseMob(this);
    }

    //Actions

    protected GameObject GetItem()
    {
        if (_basket.HighestAppleIndex < 0)
            return null;

        if (_basket.HighestAppleIndex < _basket._apples.Length)
            _itemIndex = _basket.HighestAppleIndex;
        else
            _itemIndex = _basket._apples.Length - 1;

        return _basket._apples[_itemIndex];
    }

    public void TakeItem()
    {
        ItemManager.AddItem(-1);
        _basket.RemoveApple(_itemIndex);
        if (_endPos.x < 0)
        {         
            _itemPrefab.SetActive(true);
        }
        else
        {
            _itemPrefabReverse.SetActive(true);
        }
    }

    public IEnumerator Die()
    {
        if (!_canDie)
            yield break;

        _canDie = false;

        StopCoroutine(_movementCoroutine);
        transform.DOKill();
        
        yield return new WaitForSeconds(0.01f);
        
        AnimSetTrigger("death");
        
        _rb.isKinematic = false;
        
        yield return new WaitForSeconds(3f);
      
        _rb.isKinematic = true;
        AnimPlayAnimation("idle");
        IsMoving = false;
        Reach_Item_Actions.ReleaseMob(this);
    }

    //Movement

    virtual protected void MoveToTarget(Vector3 target, float duration)
    {
        if (_startPos.x > 0)
        {
            transform.DOMove(target + TargetOffset, duration);
        }
        else
        {
            transform.DOMove(target + new Vector3(-TargetOffset.x, TargetOffset.y, TargetOffset.z), duration);
        }    
        transform.LookAt(target);
    }

    virtual protected void MoveToTarget(Vector3 target, float duration, Ease ease)
    {
        transform.DOMove(target, duration).SetEase(ease);
    }

    virtual protected void Land()
    {
        AnimSetBool("fly", false);
        AnimSetFloat("speed", 0);
    }

    //Animation
    protected void AnimPlayAnimation(string anim)
    {
        _animator.Play(anim);
    }

    protected void AnimSetBool(string anim, bool value)
    {
        _animator.SetBool(anim, value);
    }

    protected void AnimSetTrigger(string anim)
    {
        _animator.SetTrigger(anim);
    }

    protected void AnimSetFloat(string anim, float value)
    {
        _animator.SetFloat(anim, value);
    }
}
