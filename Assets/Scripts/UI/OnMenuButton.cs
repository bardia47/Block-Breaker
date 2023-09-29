using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class OnMenuButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public float scaleAmt = 1.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.PlaySound(SoundClip.PADDLEHIT, 0.5f, 1.2f);
        this.transform.DOScale(scaleAmt, 0.2f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.DOScale(Vector3.one, 0.2f).SetUpdate(true);
    }
}
