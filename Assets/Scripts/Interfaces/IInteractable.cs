using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public abstract void PlayerEnteredTrigger();
    public abstract void PlayerLeftTrigger();
    public abstract void Interact();
}
