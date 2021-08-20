using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    [SerializeField] 
    private LevelGenerator generator;

    [SerializeField] 
    private Image rebootScreen;

    private Text descriptionLevel;
    
    public void StartEffects()
    {
        Transform _transform;
        List<GameObject> objectsToAnimate = generator.Blocks;

        foreach (GameObject obj in objectsToAnimate)
        {
            _transform = obj.GetComponent<Transform>();
            _transform.DOScale(4f, 0.5f).From(0f);
            _transform.GetComponent<Transform>().DOScale(2f, 0.5f).SetDelay(0.5f);
        }
        
        descriptionLevel = generator.MissionText;
        descriptionLevel.DOFade(1f, 1f).From(0f).SetDelay(1f);
    }

    public void EndEffects()
    {   
        rebootScreen.GetComponent<CanvasGroup>().DOFade(1f, 1f).From(0f);
    }

    public void NewGameEffects()
    {
        rebootScreen.GetComponent<CanvasGroup>().DOFade(0f, 1f).From(1f);
    }
}
