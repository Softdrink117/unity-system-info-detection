using UnityEngine;
using System.Collections;
namespace Softdrink{

	// HardwareInfo is a Singleton used to get information about the system hardware and software
	// This can then be used by other scripts to auto-set quality settings or for other convenience features

	// HardwareInfo will also compare a user's machine to a target "reference" machine (which can be defined
	// in the Inspector) and give it a comparative score based on that
	// Scores are out of 100.0; a score of 100 means the user machine is identical to the reference
	// Then, based on what graphics settings will run on the reference, graphics can be scaled on the user
	// NOTE: This comparison is a rough approximation only, and should not be taken as a truly accurate
	// performance benchmark of any sort
	public class HardwareInfo : MonoBehaviour {

		// Class for holding simple system info used for comparison
		[System.Serializable]
		public class SimpleHardwareInfo{
			// SIMPLE HARDWRE PARAMETERS
			// [HideInInspector]
			public DeviceType deviceType = DeviceType.Unknown;
			// [HideInInspector]
			public int gpuMemory = -1;
			// [HideInInspector]
			public bool gpuMultiThread = false;
			// [HideInInspector]
			public int gpuShaderLevel = -1;
			// [HideInInspector]
			public int maxTextureSize = -1;
			// // [HideInInspector]
			// public UnityEngine.OperatingSystemFamily operatingSystemFamily = UnityEngine.OperatingSystemFamily.Other;
			// [HideInInspector]
			public int processorCount = -1;
			// [HideInInspector]
			public int processorFrequency = -1;
			// [HideInInspector]
			public bool supportsImageEffects = false;
			// [HideInInspector]
			public bool supportsShadows = false;
			// [HideInInspector]
			public int systemMemory = -1;

			public SimpleHardwareInfo(){

			}
			
			// Set the hardwareInfo using the current system config
			public void SetFromCurrentConfig(){
				deviceType = SystemInfo.deviceType;
				gpuMemory = SystemInfo.graphicsMemorySize;
				gpuMultiThread = SystemInfo.graphicsMultiThreaded;
				gpuShaderLevel = SystemInfo.graphicsShaderLevel;
				maxTextureSize = SystemInfo.maxTextureSize;
				processorCount = SystemInfo.processorCount;
				processorFrequency = SystemInfo.processorFrequency;
				supportsImageEffects = SystemInfo.supportsImageEffects;
				supportsShadows = SystemInfo.supportsShadows;
				systemMemory = SystemInfo.systemMemorySize;
			}
		}

		// Class for holding complex system info - all properties including 'useless' stuff
		[System.Serializable]
		public class ComplexHardwareInfo{
			// [HideInInspector]
			public string deviceModel = "";
			// [HideInInspector]
			public string deviceName = "";
			// [HideInInspector]
			public DeviceType deviceType = DeviceType.Unknown;
			// [HideInInspector]
			public string deviceID = "";
			// [HideInInspector]
			public int gpuDeviceID = -1;
			// [HideInInspector]
			public string gpuDeviceName = "";
			// [HideInInspector]
			public UnityEngine.Rendering.GraphicsDeviceType gpuDeviceType = UnityEngine.Rendering.GraphicsDeviceType.Null;
			// [HideInInspector]
			public string gpuDeviceVendor = "";
			// [HideInInspector]
			public int gpuDeviceVendorID = -1;
			// [HideInInspector]
			public string gpuDeviceVersion = "";
			// [HideInInspector]
			public int gpuMemory = -1;
			// [HideInInspector]
			public bool gpuMultiThread = false;
			// [HideInInspector]
			public int gpuShaderLevel = -1;
			// [HideInInspector]
			public int maxTextureSize = -1;
			// [HideInInspector]
			public NPOTSupport npotSupport = NPOTSupport.None;
			// [HideInInspector]
			public string operatingSystem = "";
			// // [HideInInspector]
			// public UnityEngine.OperatingSystemFamily operatingSystemFamily = UnityEngine.OperatingSystemFamily.Other;
			// [HideInInspector]
			public int processorCount = -1;
			// [HideInInspector]
			public int processorFrequency = -1;
			// [HideInInspector]
			public string processorType = "";
			// [HideInInspector]
			public bool supportsImageEffects = false;
			// [HideInInspector]
			public bool supportsShadows = false;
			// [HideInInspector]
			public int systemMemory = -1;

			public ComplexHardwareInfo(){

			}

			// Set the hardwareInfo using the current system config
			public void SetFromCurrentConfig(){
				deviceModel = SystemInfo.deviceModel;
				deviceName = SystemInfo.deviceName;
				deviceType = SystemInfo.deviceType;
				deviceID = SystemInfo.deviceUniqueIdentifier;
				gpuDeviceID = SystemInfo.graphicsDeviceID;
				gpuDeviceName = SystemInfo.graphicsDeviceName;
				gpuDeviceType = SystemInfo.graphicsDeviceType;
				gpuDeviceVendor = SystemInfo.graphicsDeviceVendor;
				gpuDeviceVendorID = SystemInfo.graphicsDeviceVendorID;
				gpuDeviceVersion = SystemInfo.graphicsDeviceVersion;
				gpuMemory = SystemInfo.graphicsMemorySize;
				gpuMultiThread = SystemInfo.graphicsMultiThreaded;
				gpuShaderLevel = SystemInfo.graphicsShaderLevel;
				maxTextureSize = SystemInfo.maxTextureSize;
				npotSupport = SystemInfo.npotSupport;
				operatingSystem = SystemInfo.operatingSystem;
				// operatingSystemFamily = SystemInfo.operatingSystemFamily;
				processorCount = SystemInfo.processorCount;
				processorFrequency = SystemInfo.processorFrequency;
				processorType = SystemInfo.processorType;
				supportsImageEffects = SystemInfo.supportsImageEffects;
				supportsShadows = SystemInfo.supportsShadows;
				systemMemory = SystemInfo.systemMemorySize;
			}
		}

		// Singleton instance
		public static HardwareInfo Instance = null;

		// Toggle for SIMPLE vs COMPLEX data
		[SerializeField]
		private bool useComplexHardwareData = false;
		// SIMPLE DATA is defined as what is necessary for the comparison operation:
		// Device Type
		// GPU memory
		// GPU multithreading
		// GPU shader level
		// GPU max texture size
		// CPU frequency
		// System Memory
		// Support for Image Effects
		// Support for Shadows

		// COMPLEX DATA is defined as all other data

		public SimpleHardwareInfo referenceConfiguration;
		public SimpleHardwareInfo userConfiguration;

		// [HideInInspector]
		public ComplexHardwareInfo complexUserConfiguration;

		void Awake (){
			// If the Instance doesn't already exist
			if(Instance == null){
				// If the instance doesn't already exist, set it to this
				Instance = this;
			}else if(Instance != this){
				// If an instance already exists that isn't this, destroy this instance and log what happened
				Destroy(gameObject);
				Debug.LogError("ERROR! The HardwareInfo script encountered another instance of HardwareInfo; it destroyed itself rather than overwrite the existing instance.", this);
			}

			referenceConfiguration = new SimpleHardwareInfo();
			userConfiguration = new SimpleHardwareInfo();
			userConfiguration.SetFromCurrentConfig();

			if(useComplexHardwareData){
				complexUserConfiguration = new ComplexHardwareInfo();
				complexUserConfiguration.SetFromCurrentConfig();
			}

			// Get hardware info 
			// userConfiguration.deviceType = SystemInfo.deviceType;
			// userConfiguration.gpuMemory = SystemInfo.graphicsMemorySize;
			// userConfiguration.gpuMultiThread = SystemInfo.graphicsMultiThreaded;
			// userConfiguration.gpuShaderLevel = SystemInfo.graphicsShaderLevel;
			// userConfiguration.maxTextureSize = SystemInfo.maxTextureSize;
			// userConfiguration.processorCount = SystemInfo.processorCount;
			// userConfiguration.processorFrequency = SystemInfo.processorFrequency;
			// userConfiguration.supportsImageEffects = SystemInfo.supportsImageEffects;
			// userConfiguration.supportsShadows = SystemInfo.supportsShadows;
			// userConfiguration.systemMemory = SystemInfo.systemMemorySize;
		}

	}

}
