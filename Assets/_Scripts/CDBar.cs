using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class CDBar : MonoBehaviour
{
    public Image bar;

    private void Awake()
    {
        LeanTween.init(800);
    }


    public IEnumerator AnimateMissileBar(float time)
    {
        LeanTween.scaleX(bar.gameObject, 0, time);

        yield return new WaitForSeconds(time);

        LeanTween.scaleX(bar.gameObject, 1, 0);
    }


    public void AnimateLaserBar(float amount)
    {

        if (amount < 0)
            amount = 0;
        else if(amount > 1) 
            amount = 1;

        bar.fillAmount = amount;

 
    }
}
