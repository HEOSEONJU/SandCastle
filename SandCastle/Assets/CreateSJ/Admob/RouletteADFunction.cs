using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roulette;

public class RouletteADFunction : ADFunction
{
    [SerializeField]
    RouletteFunction Roulette;
    public override void Reward()
    {
        Roulette.TempActive();
    }
}
