using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Countdown : MonoBehaviour
{
    [SerializeField] int duration;

    public UnityEvent OnCountFinished = new UnityEvent();
    public UnityEvent<int> OnCount = new UnityEvent<int>();
    bool isCounting;
    Sequence seq;


    public void StartCountdown(){
        
        if (isCounting == true)
            StopAllCoroutines();

        StartCoroutine(CountCoroutine());
    }

    private IEnumerator CountCoroutine(){
        isCounting = true;
        for (int i = duration; i > 0; i--)
        {
            OnCount.Invoke(i);
            yield return new WaitForSecondsRealtime(1);
        }
        isCounting = false;
        OnCountFinished.Invoke();
    }

    // public void StartCount(){
    //     if (isCounting == true) return;

    //     else isCounting = true;

    //     DOTween.Kill(seq);

    //     seq = DOTween.Sequence();

    //     OnCount.Invoke(duration);
    //     for (int i = duration; i >= 0; i--)
    //     {
    //         var count = i;
    //         seq.Append(transform
    //         .DOMove(this.transform.position, 1f)
    //         .SetUpdate(true)
    //         .OnComplete(() => OnCount.Invoke(count)));
    //     }

    //     seq.Append(transform
    //     .DOMove(this.transform.position, 1f))
    //     .SetUpdate(true)
    //     .OnComplete(() => OnCountFinished.Invoke());
    // }
}
