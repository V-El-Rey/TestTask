using Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ShowPointsManager : IStartExecute, IScoreManager
    {
        
        
        #region PrivateData
        
        private Canvas _canvas;
        private Text _text;
        
        #endregion
        
        
        #region Fields
        
        public float _pointsToWin;

        #endregion


        #region Properties

        public bool Win { get; set; }

        #endregion
    

        public ShowPointsManager(Canvas canvas, float pointsToWin)
        {
            _canvas = canvas;
            _pointsToWin = pointsToWin;
        }


        #region Methods

        public void StartExecute()
        {
            _text = _canvas.GetComponentInChildren<Text>();
            _text.text = $"Points to win: {(int) _pointsToWin}";
        }

        public void UpdateText()
        {
            _pointsToWin--;
            _text.text = $"Points to win: {(int) _pointsToWin}";
            if (_pointsToWin < 1)
            {
                Win = true;
            }
        }

        #endregion

    }
}
