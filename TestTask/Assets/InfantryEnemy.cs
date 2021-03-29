using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class InfantryEnemy : BaseEnemy
{
    private IModel _model;
    private IView _view;
    public InfantryEnemy(IModel model, IView view) : base(model, view)
    {
        _model = model;
        _view = view;
    }
    
    public override void Move(float attackDistance)
    {
        base.Move(attackDistance);
        if (_distanceToPlayer.sqrMagnitude > attackDistance)
        {
            _view.objectTransform.Translate(_distanceToPlayer * (Time.deltaTime * 2.0f));
        }
    }
}
