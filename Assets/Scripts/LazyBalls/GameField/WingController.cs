using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField
{
    public class WingController : MonoBehaviour
    {
        [SerializeField] private string buttonKey;
        [SerializeField] private HingeJoint flapHingeJoint;
        [SerializeField] private int flapMotorVelocity;
        [SerializeField] private int flapMotorForce;

        private void Update()
        {
            if (Input.GetButtonDown(buttonKey))
            {
                flapHingeJoint.motor = new JointMotor
                {
                    force = flapMotorForce,
                    targetVelocity = flapMotorVelocity
                };
                SoundController.Instance().PlaySound(SoundController.SoundType.WingFlapUp);
            }
            else if (Input.GetButtonUp(buttonKey))
            {
                flapHingeJoint.motor = new JointMotor
                {
                    force = flapMotorForce,
                    targetVelocity = -flapMotorVelocity
                };
                SoundController.Instance().PlaySound(SoundController.SoundType.WingFlapDown);
            }
        }
    }
}
