using System;
using UnityEngine;
using DG.Tweening;

namespace Coins
{
    public class CoinView : MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(transform.up, 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
        }
    }
}