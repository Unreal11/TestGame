using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public LevelGenerator generator { get; set; }
    public bool isCorrect { get; set; }
    public Vector2 MainPosition;
    public string Id
    {
        get
        {
            return id;
        }
    }

    [SerializeField] 
    private UnityEvent onClick; 

    [SerializeField] 
    private ParticleSystem particleWin; 

    [SerializeField] 
    private Transform blockTransform;

    [SerializeField] 
    private string id;
   
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    [SerializeField] 
    private AnimationCurve curveX;

    [SerializeField]
    private AnimationCurve curveY;
    
    private Vector2 scaleSprite = new Vector2(0.75f, 0.75f);
    private bool canTap = true;

    public void Initialize(BlockData cardData)
    {
        id = cardData.Id;
        spriteRenderer.sprite = cardData.Sprite;
        spriteRenderer.size = scaleSprite;
        isCorrect = false;
        canTap = true;

        blockTransform.position = MainPosition;
    }

    public void OnClick()
    {
        blockTransform.position = MainPosition;

        if (isCorrect)
            CorrectAnimation();
        else 
            IncorrectAnimation();
    }

    private void IncorrectAnimation()
    {
        blockTransform.DOKill();
        blockTransform.DOLocalMoveX(1f, 0.5f).SetEase(curveX);
        blockTransform.DOLocalMoveY(1f, 0.5f).SetEase(curveY);
    }

    private void CorrectAnimation()
    {
        if (canTap)
        {
            canTap = false;

            blockTransform.DOComplete();
            StopAllCoroutines();
            StartCoroutine(WaitCorrectAnimation());
        }
        
    }

    private void OnMouseDown()
    {
        onClick.Invoke();
    }

    private IEnumerator WaitCorrectAnimation()
    {
        particleWin.Play(true);
        blockTransform.DOScale(1.25f, 0.25f).SetLoops(8, LoopType.Yoyo);

        yield return new WaitForSeconds(2f);

        DOTween.KillAll();
        generator.OnEndLevel.Invoke();
    }
}
