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


    // public void StartCountdown() {
    //     StartCoroutine(CountdownCoroutine());
    // }

    public void StartCount(){
        if (isCounting == true) return;

        else isCounting = true;

        DOTween.Kill(seq);

        seq = DOTween.Sequence();

        OnCount.Invoke(duration);
        for (int i = duration; i >= 0; i--)
        {
            var count = i;
            seq.Append(transform
            .DOMove(this.transform.position, 1f)
            .SetUpdate(true)
            .OnComplete(() => OnCount.Invoke(count)));
        }

        seq.Append(transform
        .DOMove(this.transform.position, 1f))
        .SetUpdate(true)
        .OnComplete(() => OnCountFinished.Invoke());
    }
}
