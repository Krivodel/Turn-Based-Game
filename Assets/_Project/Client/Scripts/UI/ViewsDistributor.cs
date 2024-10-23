using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Client
{
    public abstract class ViewsDistributor<TTarget, TViewData> : ScriptableObject, IViewsDistributor<TTarget, TViewData>
        where TTarget : class
        where TViewData : class
    {
        [SerializeField] private ViewDataDescriptor[] _descriptors;

        public TViewData this[TTarget target] => GetViewDataByConcreteTarget(target);

        private TViewData GetViewDataByConcreteTarget(TTarget target)
        {
            string key = target.GetType().Name;

            foreach (var descriptor in _descriptors)
            {
                if (descriptor.Key == key)
                    return descriptor.ViewData;
            }

            throw new ArgumentException($"View for '{key}' not found.");
        }

        #region Editor
#if UNITY_EDITOR
        protected virtual void EditorInitDescriptors()
        {
            List<ViewDataDescriptor> descriptors = new();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var targetType = typeof(TTarget);

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsSubclassOf(targetType))
                    {
                        descriptors.Add(new ViewDataDescriptor(type.Name));
                    }
                }
            }

            _descriptors = descriptors.ToArray();
        }
#endif
        #endregion

        [Serializable]
        public struct ViewDataDescriptor
        {
            [field: SerializeField] public string Key { get; private set; }
            [field: SerializeField] public TViewData ViewData { get; private set; }

            public ViewDataDescriptor(string key)
            {
                Key = key;
                ViewData = null;
            }
        }
    }
}
