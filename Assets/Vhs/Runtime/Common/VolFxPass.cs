#if !VOL_FX

using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

//  VolFx © NullTale - https://x.com/NullTale
namespace VolFx
{
    public static partial class VolFx
    {
        [Serializable]
        public abstract class Pass : ScriptableObject
        {
            [SerializeField]
            [HideInInspector]
            private Shader _shader;
            
            protected Material _material;
            
            bool _isActive;

            public VolumeStack Stack => VolumeManager.instance.stack;
            
            protected virtual bool Invert => false;
            
            protected virtual int MatPass => 0;
            
            internal bool IsActiveCheck
            {
                get => _isActive && _material != null;

                set => _isActive = value;
            }
            
            public abstract string ShaderName { get; }
            
            internal void _init()
            {
#if UNITY_EDITOR
                if (_shader == null || _material == null)
                {
                    var sna = GetType().GetCustomAttributes(typeof(ShaderNameAttribute),
                    true).FirstOrDefault() as ShaderNameAttribute;

                    var shaderName = sna == null ? ShaderName : sna._name;
                    
                    if (string.IsNullOrEmpty(shaderName) == false)
                    {
                        _shader = Shader.Find(shaderName);
                        
                        var assetPath = UnityEditor.AssetDatabase.GetAssetPath(_shader);

                        if (_editorValidate && string.IsNullOrEmpty(assetPath) == false)
                        {
                            _editorSetup(Path.GetDirectoryName(assetPath),
                            Path.GetFileNameWithoutExtension(assetPath));
                        }

                        UnityEditor.EditorUtility.SetDirty(this);
                    }
                }
#endif

                if (_shader != null)
                {
                    _material = new Material(_shader);
                }
                
                Init();
            }

            public virtual void Init(InitApi initApi)
            {
                
            }
            
            public virtual void Invoke(RTHandle source, RTHandle dest, CallApi callApi)
            {
                callApi.Blit(source, dest, _material, MatPass);
            }

            public void Validate()
            {
#if UNITY_EDITOR
                if (_shader == null || _editorValidate)
                {
                    var shaderName = GetType().GetCustomAttributes(typeof(ShaderNameAttribute),
                    true).FirstOrDefault() as ShaderNameAttribute;
                    
                    if (shaderName != null)
                    {
                        _shader = Shader.Find(shaderName._name);
                        
                        var assetPath = UnityEditor.AssetDatabase.GetAssetPath(_shader);

                        if (string.IsNullOrEmpty(assetPath) == false)
                        {
                            _editorSetup(Path.GetDirectoryName(assetPath),
                            Path.GetFileNameWithoutExtension(assetPath));
                        }
                        
                        UnityEditor.EditorUtility.SetDirty(this);
                    }
                }
                
                if ((_material == null || _material.shader != _shader) && _shader != null)
                {
                    _material = new Material(_shader);

                    Init();
                }
#endif
                IsActiveCheck = Validate(_material);
            }

            public virtual void Init()
            {
                
            }

            public abstract bool Validate(Material mat);
            
            public virtual void Cleanup(CommandBuffer cmd)
            {
                
            }
            
            protected virtual bool _editorValidate => false;

            protected virtual void _editorSetup(string folder, string asset)
            {
                
            }
        }
    }
}

#endif