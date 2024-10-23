using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Client
{
    public class StatusEffectCollectionView : MonoBehaviour
    {
        [SerializeField] private StatusEffectView[] _viewPool;

        public void Set(IEnumerable<StatusEffectViewData> viewDatas)
        {
            Clear();

            foreach (var viewData in viewDatas)
                Draw(viewData);
        }

        public void Clear()
        {
            foreach (var view in _viewPool)
                view.Clear();
        }

        private void Start()
        {
            Clear();
        }

        private void Draw(StatusEffectViewData viewData)
        {
            GetView().Set(viewData);
        }

        private StatusEffectView GetView()
        {
            foreach (var view in _viewPool)
            {
                if (!view.IsOccupied())
                {
                    view.transform.SetSiblingIndex(0);

                    return view;
                }
            }

            throw new IndexOutOfRangeException("Not enough views in pool.");
        }
    }
}
