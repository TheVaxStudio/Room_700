using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VolFx
{
    [Serializable, VolumeComponentMenu("VolFx/Vhs")]
    public sealed class VhsVol : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("Total blending of full applied effect")]
        public ClampedFloatParameter _weight = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);

        [Tooltip("Tape noises impact")]
        public ClampedFloatParameter _tape = new ClampedFloatParameter(0.0f, 0.0f, 2.0f);
        
        [Tooltip("Tape noises impact")]
        public ClampedFloatParameter _shades = new ClampedFloatParameter(0.0f, 0.0f, 3.0f);

        [Header("Distort")]
        [Tooltip("Frame distortions")]
        public ClampedFloatParameter _rocking = new ClampedFloatParameter(0.0f, 0.0f, 0.1f);
        
        [Tooltip("Tape squeeze distortions")]
        public ClampedFloatParameter _squeeze = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);

        [Header("Noise")]
        [Tooltip("White noise density")]
        public ClampedFloatParameter _density = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);

        [Tooltip("White noise intensity")]
        public ClampedFloatParameter _intensity = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);

        [Tooltip("White noise scale")]
        public ClampedFloatParameter _scale = new ClampedFloatParameter(1.0f, 3.0f, 3.0f);
        
        [Tooltip("Grain flickering")]
        public ClampedFloatParameter _flickering = new ClampedFloatParameter(0.0f, -1.0f, 1.0f);

        [Header("Glow")]
        [Tooltip("Crt glow color")]
        public ColorParameter _color = new ColorParameter(new Color(1.0f, 0.0f, 0.0f, 1.0f));
        
        [Tooltip("Crt glow offset")]
        public ClampedFloatParameter _bleed = new ClampedFloatParameter(7.0f, 0.0f, 3.0f);

        //[HideInInspector]
        [Header("Anim")]
        [Tooltip("Speed of flow animations")]
        public ClampedFloatParameter _flow = new ClampedFloatParameter(1.0f, 0.0f, 24.0f);
        
        [Tooltip("Speed of pulsating animations")]
        public ClampedFloatParameter _pulsation = new ClampedFloatParameter(1.0f, 0.0f, 14.0f);

        [Serializable]
        public class ModeParameter : VolumeParameter<VhsPass.Mode>
        {
            public ModeParameter(VhsPass.Mode value, bool overrideState) : base(value, overrideState)
            {
                
            }
        } 

        public bool IsActive() => active && _weight.value > 0.0f;

        public bool IsTileCompatible() => true;
    }
}