using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Insect : MonoBehaviour
{
    [SerializeField] private bool testKill;
    public bool startMovement;
    [HideInInspector] public bool IsMoving;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private GameObject _itemPrefabReverse;
    public Transform Target;
    public Vector3 TargetOffset;
    public bool ReverseEndPos;
    public float MoveDuration = 3f;
    public float LandDuration = 1f;

    private Vector3 _endPos;

    private Coroutine _movementCoroutine;

    private Animator _animator;
    private Rigidbody _rb;
    private Basket _basket;
    private GameObject _item;
    private int _itemIndex;
    private bool _canDie;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _basket = Target.gameObject.GetComponentInParent<Basket>();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (startMovement)
        {
            startMovement = false;
            _movementCoroutine = StartCoroutine(Movement());
        }

        if (testKill)
        {
            testKill = false;
            StartCoroutine(Die());
        }
    }

    virtual protected IEnumerator Movement()
    {
        IsMoving = true;
        _canDie = true;

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
            this.gameObject.SetActive(false);
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

        print("dff");

        MoveToTarget(_endPos, MoveDuration);

        yield return new WaitForSeconds(MoveDuration);

        IsMoving = false;
        gameObject.SetActive(false);
    }

    //Actions


    protected GameObject GetItem()
    {
        if (_basket.HighestAppleIndex < 0)
            return null;

        _itemIndex = _basket.HighestAppleIndex;
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

        print("dadadad");

        StopCoroutine(_movementCoroutine);
        transform.DOKill();

        yield return new WaitForSeconds(0.01f);

        AnimSetTrigger("death");
        _rb.isKinematic = false;

        yield return new WaitForSeconds(3f);

        _rb.isKinematic = true;
        transform.position = _endPos;
        AnimPlayAnimation("idle");
        IsMoving = false;
        this.gameObject.SetActive(false);
    }

    //Movement

    virtual protected void MoveToTarget(Vector3 target, float duration)
    {
        transform.DOMove(target + TargetOffset, duration);
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
