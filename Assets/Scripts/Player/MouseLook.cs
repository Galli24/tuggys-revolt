using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;

        public float JoystickXSensitivity = 2f;
        public float JoystickYSensitivity = 2f;

        public float CamAmp = 1f;
        private float maxCamAmp = 2f;
        public float CamAmpMult = 1.01f;

        public bool clampVerticalRotation = true;

        public float MinimumX = -360F;
        public float MaximumX = 360F;

        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;

        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            if (CrossPlatformInputManager.GetAxis("Joystick X") != 0 || CrossPlatformInputManager.GetAxis("Joystick Y") != 0 || CrossPlatformInputManager.GetAxis("Mouse Y") != 0 || CrossPlatformInputManager.GetAxis("Mouse X") != 0)
            {
                if (CamAmp * CamAmpMult < maxCamAmp)
                    CamAmp *= CamAmpMult;
            }   
            else
                CamAmp = 1f;
            float yOn = CrossPlatformInputManager.GetAxis("Joystick X") * JoystickXSensitivity * CamAmp;
            float xOn = CrossPlatformInputManager.GetAxis("Joystick Y") * JoystickYSensitivity * CamAmp;

            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity * CamAmp;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity * CamAmp;

            if (yOn > 0.05f || xOn > 0.05f || yOn < 0.05f || xOn < 0.05f)
            {
                m_CharacterTargetRot *= Quaternion.Euler(0f, yOn, 0f);
                m_CameraTargetRot *= Quaternion.Euler(-xOn, m_CameraTargetRot.y, m_CameraTargetRot.z);
            }
            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, m_CameraTargetRot.y, m_CameraTargetRot.z);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            /*if(Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else*/ if(Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
