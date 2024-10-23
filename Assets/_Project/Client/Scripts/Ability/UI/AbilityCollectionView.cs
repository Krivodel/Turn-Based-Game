using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Client
{
    // TODO: Дубляж.
    public class AbilityCollectionView : MonoBehaviour
    {
        public event Action<string> AbilitySelected;

        [SerializeField] private AbilityView[] _viewPool;

        public void Set(IEnumerable<AbilityViewDataProxy> viewDatas)
        {
            Clear();

            foreach (var viewData in viewDatas)
                Draw(viewData);

            foreach (var view in _viewPool)
                view.Clicked += OnViewClicked;
        }

        public void Clear()
        {
            foreach (var view in _viewPool)
            {
                view.Clicked -= OnViewClicked;

                view.Clear();
            }
        }

        private void Draw(AbilityViewDataProxy viewData)
        {
            GetView().Set(viewData);
        }

        private AbilityView GetView()
        {
            foreach (var view in _viewPool)
            {
                if (!view.IsOccupied())
                    return view;
            }

            throw new IndexOutOfRangeException("Not enough views in pool.");
        }

        private void OnViewClicked(AbilityView view)
        {
            AbilitySelected?.Invoke(view.Id);
        }
    }
}
