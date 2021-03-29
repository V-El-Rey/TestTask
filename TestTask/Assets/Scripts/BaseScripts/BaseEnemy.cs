using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class BaseEnemy
{
    private IModel _model;
    private IView _view;

    public Vector3 _distanceToPlayer;
    private Vector3 _normalizedDirection;
    private Vector3 _playerPosition = new Vector3(0.0f, 0.0f, 0.0f);
    
    public BaseEnemy(IModel model, IView view)
    {
        _model = model;
        _view = view;
    }

    public virtual void Move(float attackDistance)
    {
        _distanceToPlayer = _playerPosition - _view.objectTransform.position;
        _normalizedDirection = _distanceToPlayer.normalized;
    }
}
