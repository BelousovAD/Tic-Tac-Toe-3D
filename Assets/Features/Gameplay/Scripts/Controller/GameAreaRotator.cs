namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Вращатель игрового поля
    /// </summary>
    public class GameAreaRotator : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Изменён шаг вращения
        /// </summary>
        public event Action OnStepChanged = delegate { };

        #endregion

        #region Properties

        [SerializeField, Range(0f, 1f)]
        protected float interpolateValue = 0f;
        [SerializeField, Range(float.Epsilon, 360f)]
        protected float epsilonAngle = 0.5f;

        /// <summary>
        /// Текущий шаг вращения
        /// </summary>
        public float CurrentStep
        {
            get => currentStep;
            protected set
            {
                if (value != currentStep)
                {
                    currentStep = value;
                    OnStepChanged();
                }
            }
        }
        protected float currentStep = 0f;

        protected readonly List<float> steps = new() { 90f, 45f };
        protected IEnumerator<float> stepsEnumerator = default;
        protected WaitForFixedUpdate wait = new();
        protected Coroutine rotateRoutine = default;

        #endregion

        #region Methods

        /// <summary>
        /// Изменить шаг вращения на следующий в списке
        /// </summary>
        public virtual void MoveToNextStep()
        {
            if (!stepsEnumerator.MoveNext())
            {
                stepsEnumerator.Reset();
                stepsEnumerator.MoveNext();
            }

            CurrentStep = stepsEnumerator.Current;
        }

        /// <summary>
        /// Повернуть игровое поле
        /// </summary>
        /// <param name="moveDirection">Направление, в которое нужно произвести поворот</param>
        public virtual void Rotate(MoveDirection moveDirection)
        {
            if (rotateRoutine == null)
            {
                switch (moveDirection)
                {
                    case MoveDirection.Left:
                        rotateRoutine = StartCoroutine(RotateSmoothlyRoutine(CurrentStep));
                        break;
                    case MoveDirection.Right:
                        rotateRoutine = StartCoroutine(RotateSmoothlyRoutine(-CurrentStep));
                        break;
                    default:
                        Debug.LogError($"Game area rotate direction is not implemented: {moveDirection}");
                        break;
                }
            }
        }

        protected virtual void Awake()
        {
            stepsEnumerator = steps.GetEnumerator();
            MoveToNextStep();
        }

        protected virtual IEnumerator RotateSmoothlyRoutine(float angle)
        {
            float currentAngleY = gameObject.transform.eulerAngles.y;
            float newAngleY = currentAngleY + angle;

            while (currentAngleY != newAngleY)
            {
                currentAngleY = Mathf.LerpAngle(currentAngleY, newAngleY, interpolateValue);
                if (Mathf.Abs(newAngleY - currentAngleY) <= epsilonAngle)
                {
                    currentAngleY = newAngleY;
                }
                gameObject.transform.eulerAngles = new Vector3(0, currentAngleY, 0);
                yield return wait;
            }

            rotateRoutine = null;
        }

        #endregion
    }
}
