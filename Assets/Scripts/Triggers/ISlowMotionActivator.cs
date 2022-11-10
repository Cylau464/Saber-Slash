using System;

namespace Triggers
{
    public interface ISlowMotionActivator
    {
        Action OnDisabled { get; set; }
    }
}