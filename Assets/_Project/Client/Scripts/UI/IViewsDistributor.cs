using System;

namespace TurnBasedGame.Client
{
    public interface IViewsDistributor<TTarget, TViewData> where TTarget : class where TViewData : class
    {
        TViewData this[TTarget target] { get; }
    }
}
