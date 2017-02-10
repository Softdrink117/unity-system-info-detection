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
			[HideInInspector]
			public bool initialized = false;

			// SIMPLE HARDWRE PARAMETERS
			[TooltipAttribute("Device type. Options are 'Desktop' (for Laptop, Tablet, or Desktop), 'Console', 'Handheld', 'Unknown'.")]
			public DeviceType deviceType = DeviceType.Unknown;
			[TooltipAttribute("GPU Rendering API information. See Rendering.GraphicsDeviceType documentation for details.")]
			public UnityEngine.Rendering.GraphicsDeviceType gpuDeviceType = UnityEngine.Rendering.GraphicsDeviceType.Null;

			[SpaceAttribute(10)]

			[TooltipAttribute("Graphics memory (VRAM), in MB. A value of -1 indicates an error.")]
			public int gpuMemory = -1;
			[TooltipAttribute("Graphics ability to multithread. Default is false.")]
			public bool gpuMultiThread = false;
			[TooltipAttribute("Graphics Shader Level. See SystemInfo.GraphicsShaderLevel documentation for details. A value of -1 indicates an error.")]
			public int gpuShaderLevel = -1;
			[TooltipAttribute("Maximum texture size supported by the GPU, in pixels. A value of -1 indicates an error.")]
			public int maxTextureSize = -1;

			[SpaceAttribute(10)]

			[TooltipAttribute("System memory (RAM), in MB. A value of -1 indicates an error.")]
			public int systemMemory = -1;
			[TooltipAttribute("Logical processor threads. A value of -1 indicates an error.")]
			public int processorCount = -1;
			[TooltipAttribute("Processor speed, in MHz. A value of -1 indicates an error.")]
			public int processorFrequency = -1;
			[TooltipAttribute("Does the system support GPU Compute Shaders? Defaults to false.")]

			[SpaceAttribute(10)]

			public bool supportsComputeShaders = false;
			[TooltipAttribute("Does the system support Image Effects and GPU-accelerated Post Processing? Defaults to false.")]
			public bool supportsImageEffects = false;
			[TooltipAttribute("Does the system support shadows? Defaults to false.")]
			public bool supportsShadows = false;

			[SpaceAttribute(10)]
			[Range(1.0f,3.5f)]
			[TooltipAttribute("SLI cannot be detected by Unity, so this number will multiply the GPU score to 'approximate' effect of SLI. Default is 1.0. \nSLI scaling is non-linear and varies by GPU, but a safe number for a dual-card system would be about 1.6.")]
			public float SLIScalar = 1.0f;

			public SimpleHardwareInfo(){
				initialized = true;
			}
			
			// Set the hardwareInfo using the current system config
			public void SetFromCurrentConfig(){
				deviceType = SystemInfo.deviceType;
				gpuDeviceType = SystemInfo.graphicsDeviceType;
				gpuMemory = SystemInfo.graphicsMemorySize;
				gpuMultiThread = SystemInfo.graphicsMultiThreaded;
				gpuShaderLevel = SystemInfo.graphicsShaderLevel;
				maxTextureSize = SystemInfo.maxTextureSize;
				processorCount = SystemInfo.processorCount;
				processorFrequency = SystemInfo.processorFrequency;
				supportsComputeShaders = SystemInfo.supportsComputeShaders;
				supportsImageEffects = SystemInfo.supportsImageEffects;
				supportsShadows = SystemInfo.supportsShadows;
				systemMemory = SystemInfo.systemMemorySize;
			}

			// Clear the current hardwareInfo
			public void Clear(){
				deviceType = DeviceType.Unknown;
				gpuDeviceType = UnityEngine.Rendering.GraphicsDeviceType.Null;
				gpuMemory = -1;
				gpuMultiThread = false;
				gpuShaderLevel = -1;
				maxTextureSize = -1;
				processorCount = -1;
				processorFrequency = -1;
				supportsComputeShaders = false;
				supportsImageEffects = false;
				supportsShadows = false;
				systemMemory = -1;

				SLIScalar = 1.0f;

				initialized = false;
			}
		}

		// Class for holding complex system info - all properties including 'useless' stuff
		[System.Serializable]
		public class ComplexHardwareInfo{
			[HideInInspector]
			public bool initialized = false;

			public string deviceModel = "";
			public string deviceName = "";
			public DeviceType deviceType = DeviceType.Unknown;
			public string deviceID = "";
			public int gpuDeviceID = -1;
			public string gpuDeviceName = "";
			public UnityEngine.Rendering.GraphicsDeviceType gpuDeviceType = UnityEngine.Rendering.GraphicsDeviceType.Null;
			public string gpuDeviceVendor = "";
			public int gpuDeviceVendorID = -1;
			public string gpuDeviceVersion = "";
			public int gpuMemory = -1;
			public bool gpuMultiThread = false;
			public int gpuShaderLevel = -1;
			public int maxTextureSize = -1;
			public NPOTSupport npotSupport = NPOTSupport.None;
			public string operatingSystem = "";
			// public UnityEngine.OperatingSystemFamily operatingSystemFamily = UnityEngine.OperatingSystemFamily.Other;
			public int processorCount = -1;
			public int processorFrequency = -1;
			public string processorType = "";
			public int supportedRenderTargetCount = -1;
			public bool supports2DArrayTextures = false;
			public bool supports3DTextures = false;
			public bool supportsAudio = false;
			public bool supportsComputeShaders = false;
			// public bool supportsCubemapArrayTextures = false;
			public bool supportsGyroscope = false;
			public bool supportsImageEffects = false;
			public bool supportsInstancing = false;
			public bool supportsLocationService = false;
			public bool supportsMotionVectors = false;
			public bool supportsRawShadowDepthSampling = false;
			public bool supportsRenderToCubemap = false;
			public bool supportsShadows = false;
			public bool supportsSparseTextures = false;
			public bool supportsVibration = false;
			// public bool usesReversedZBuffer = false;
			public int systemMemory = -1;

			public ComplexHardwareInfo(){
				initialized = true;
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
				supportedRenderTargetCount = SystemInfo.supportedRenderTargetCount;
				supports2DArrayTextures = SystemInfo.supports2DArrayTextures;
				supports3DTextures = SystemInfo.supports3DTextures;
				supportsAudio = SystemInfo.supportsAudio;
				supportsComputeShaders = SystemInfo.supportsComputeShaders;
				//supportsCubemapArrayTextures = SystemInfo.supportsCubemapArrayTextures;
				supportsGyroscope = SystemInfo.supportsGyroscope;
				supportsImageEffects = SystemInfo.supportsImageEffects;
				supportsInstancing = SystemInfo.supportsInstancing;
				supportsLocationService = SystemInfo.supportsLocationService;
				supportsMotionVectors = SystemInfo.supportsMotionVectors;
				supportsRawShadowDepthSampling = SystemInfo.supportsRawShadowDepthSampling;
				supportsRenderToCubemap = SystemInfo.supportsRenderToCubemap;
				supportsShadows = SystemInfo.supportsShadows;
				supportsSparseTextures = SystemInfo.supportsSparseTextures;
				supportsVibration = SystemInfo.supportsVibration;
				//usesReversedZBuffer = SystemInfo.usesReversedZBuffer;
				systemMemory = SystemInfo.systemMemorySize;
			}
		}

		// Class for storing the weighting information for different parameters
		[System.Serializable]
		public class HardwareComparisonWeights{
			[HideInInspector]
			public bool initialized = false;

			[HeaderAttribute("Device Type Weights")]
			// Weights for device type
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Weight for the Desktop device type (which includes Laptops and some Tablets). Points are set to 100 * this value.")]
			public float desktopWeight = 1.0f;
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Weight for the Console device type. Points are set to 100 * this value.")]
			public float consoleWeight = 1.0f;
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Weight for the Console device type. Points are set to 100 * this value.")]
			public float handheldWeight = 0.7f;

			//[SpaceAttribute(10)]
			//[HeaderAttribute("Main Property Weights")]
			// Weights for main categories - CPU, GPU
			//[Range(0.0f, 1.0f)]
			//public float CPUWeight = 0.5f;
			//[Range(0.0f, 1.0f)]
			//public float GPUWeight = 0.5f;
			// Weight for RAM
			//[Range(0.0f, 1.0f)]
			//public float RAMWeight = 0.25f;
			[SpaceAttribute(10)]
			[Range(1,8)]
			[TooltipAttribute("If the User's Processor has more than this many logical cores, the additional ones do not factor into the score. \nThis is helpful since Unity primarily stresses only one thread.")]
			public int ignoreCoresAbove = 2;

			[SpaceAttribute(10)]
			[HeaderAttribute("Penalties")]
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Penalty for failing to support GPU multithreading. GPU score -= (GPU score * this value).")]
			public float gpuMultithreadPenalty = 0.5f;
			// Weight for Compute Shaders
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Penalty for failing to support Compute Shaders. Score -= (score * this value).")]
			public float computePenalty = 0.5f;
			// Weight for Image Effects
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Penalty for failing to support Image Effects. Score -= (score * this value).")]
			public float imageEffectPenalty = 0.25f;
			// Weight for Shadow support
			[Range(0.0f, 1.0f)]
			[TooltipAttribute("Penalty for failing to support Shadows. Score -= (score * this value).")]
			public float shadowPenalty = 0.9f;

			public HardwareComparisonWeights(){
				initialized = true;
			}

		}

		// Singleton instance
		public static HardwareInfo Instance = null;

		[HeaderAttribute("Final User Score")]
		[ContextMenuItem ("Calculate Score", "CalculateHardwareScore")]
		[TooltipAttribute("The final calculated User Hardware Score, calibrated to a scale of 100. \nA score of 100 means that the User hardware matches the Reference hardware exactly. \nLess than 100 means the User is a lesser system, and more than 100 means the User is a greater system.")]
		public float userHardwareScore = 0.0f;

		[HeaderAttribute("Breakdown")]

		[ContextMenuItem ("Calculate Score", "CalculateHardwareScore")]
		[TooltipAttribute("The GPU score for the User System, calibrated to a scale of 100 where 100 = Reference.")]
		public float userGPUScore = 0.0f;
		[ContextMenuItem ("Calculate Score", "CalculateHardwareScore")]
		[TooltipAttribute("The CPU score for the User System, calibrated to a scale of 100 where 100 = Reference.")]
		public float userCPUScore = 0.0f;

		[HeaderAttribute("Warnings")]

		[HideInInspector]
		//[TextArea(6,6)]
		//[ContextMenuItem ("Calculate Score", "CalculateHardwareScore")]
		public string compatibilityWarnings = "";

		[SpaceAttribute(20)]

		[SerializeField]
		private HardwareComparisonWeights comparisonWeights;

		// Toggle for SIMPLE vs COMPLEX data
		[SerializeField]
		[SpaceAttribute(10)]
		[ContextMenuItem ("Set Reference", "SetReference")]
		[ContextMenuItem ("Clear Reference", "ClearReference")]
		private bool useComplexHardwareData = false;
		// SIMPLE DATA is defined as what is necessary for the comparison operation:
		// Device Type
		// GPU memory
		// GPU multithreading
		// GPU shader level
		// GPU max texture size
		// CPU frequency
		// System Memory
		// Support for Compute Shaders
		// Support for Image Effects
		// Support for Shadows

		// COMPLEX DATA is defined as all other data

		// Add a ContextMenu item to set the reference configuration
		[SpaceAttribute(10)]
		[ContextMenuItem ("Set Reference", "SetReference")]
		[ContextMenuItem ("Clear Reference", "ClearReference")]
		public SimpleHardwareInfo referenceConfiguration;
		[ContextMenuItem ("Set Reference", "SetReference")]
		[ContextMenuItem ("Clear Reference", "ClearReference")]
		public SimpleHardwareInfo userConfiguration;

		// [HideInInspector]
		[ContextMenuItem ("Set Reference", "SetReference")]
		[ContextMenuItem ("Clear Reference", "ClearReference")]
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

			// Create the Weights if they do not already exist
			if(comparisonWeights == null || comparisonWeights.initialized == false) comparisonWeights = new HardwareComparisonWeights();

			// Create the Reference info from the current config if it does not already exist
			if(referenceConfiguration == null || referenceConfiguration.initialized == false) referenceConfiguration = new SimpleHardwareInfo();
			// Create the User info from the current config
			userConfiguration = new SimpleHardwareInfo();
			userConfiguration.SetFromCurrentConfig();

			// If we are using complex info, create it from the current info
			if(useComplexHardwareData){
				complexUserConfiguration = new ComplexHardwareInfo();
				complexUserConfiguration.SetFromCurrentConfig();
			}else complexUserConfiguration = null;					// Otherwise set it to null for GC to deal with it later

			// Calculate user score
			CalculateHardwareScore();

		}

		// Calculate user hardware score
		public void CalculateHardwareScore(){

			// Clear compatibility warnings
			compatibilityWarnings = "";

			float basePoints = 100.0f;
			if(userConfiguration.deviceType == referenceConfiguration.deviceType) basePoints *= 1.0f;
			else{
				if(userConfiguration.deviceType == DeviceType.Desktop) basePoints *= comparisonWeights.desktopWeight;
				if(userConfiguration.deviceType == DeviceType.Console) basePoints *= comparisonWeights.consoleWeight;
				if(userConfiguration.deviceType == DeviceType.Handheld) basePoints *= comparisonWeights.handheldWeight;
			}

			// Check that they use the same graphics APIs and flag a warning if not
			if(userConfiguration.gpuDeviceType != referenceConfiguration.gpuDeviceType) compatibilityWarnings += "WARNING: The reference configration and user configuration are using different graphics APIs. This may cause incompatibility or rendering issues.\n";

			float GPUScore = 1.0f;
			GPUScore *= (float)(userConfiguration.gpuMemory)/(float)(referenceConfiguration.gpuMemory);
			GPUScore *= (float)(userConfiguration.gpuShaderLevel)/(float)(referenceConfiguration.gpuShaderLevel);
			// If there is a difference of more than 5 in reference and user GPU shader levels, create a warning
			if((referenceConfiguration.gpuShaderLevel - userConfiguration.gpuShaderLevel) > 5){
				compatibilityWarnings += "WARNING: The reference configuration and user configuration support different Shader Models. This may cause incompatibility or rendering issues.\n";
			}
			GPUScore *= (float)(userConfiguration.maxTextureSize)/(float)(referenceConfiguration.maxTextureSize);
			if(userConfiguration.gpuMultiThread == false && referenceConfiguration.gpuMultiThread == true){
				GPUScore -= (GPUScore * comparisonWeights.gpuMultithreadPenalty);
				compatibilityWarnings += "WARNING: The reference configuration supports GPU multithreading, but the user configuration does not!\n";
			}
			GPUScore *= userConfiguration.SLIScalar/referenceConfiguration.SLIScalar;
			//GPUScore *= comparisonWeights.GPUWeight;
			userGPUScore = GPUScore * 100.0f;

			float CPUScore = 1.0f;
			if(userConfiguration.processorCount > comparisonWeights.ignoreCoresAbove) CPUScore *= 1.0f;
			else CPUScore *= (float)userConfiguration.processorCount/(float)referenceConfiguration.processorCount;
			CPUScore *= (float)userConfiguration.processorFrequency/(float)referenceConfiguration.processorFrequency;
			//CPUScore *= comparisonWeights.CPUWeight;
			userCPUScore = CPUScore * 100.0f;

			float RAMScore = 1.0f;
			RAMScore *= (float)userConfiguration.systemMemory/(float)referenceConfiguration.systemMemory;
			//RAMScore *= comparisonWeights.RAMWeight;

			float avgScore = CPUScore + GPUScore + RAMScore;
			avgScore = avgScore / 3.0f;

			//float penalties = 0.0f;
			Vector3 penaltyVect = new Vector3(comparisonWeights.shadowPenalty, comparisonWeights.computePenalty, comparisonWeights.imageEffectPenalty);
			//float penaltySize = penaltyVect.x + penaltyVect.y + penaltyVect.z;
			//penaltyVect *= 1.0f/penaltySize;
			// penaltyVect.Normalize();
			if(userConfiguration.supportsShadows == referenceConfiguration.supportsShadows) avgScore *= 1.0f;
			else{
				if(userConfiguration.supportsShadows == false && referenceConfiguration.supportsShadows == true){
					avgScore *= (1.0f - penaltyVect.x);
					compatibilityWarnings += "WARNING: The reference configuration supports shadows, but the user configuration does not!\n";
				}
			}
			if(userConfiguration.supportsComputeShaders == referenceConfiguration.supportsComputeShaders) avgScore *= 1.0f;
			else{
				if(userConfiguration.supportsComputeShaders == false && referenceConfiguration.supportsComputeShaders == true){
					avgScore *= (1.0f - penaltyVect.y);
					compatibilityWarnings += "WARNING: The reference configuration supports Compute Shaders, but the user configuration does not!\n";
				}
			}
			if(userConfiguration.supportsImageEffects == referenceConfiguration.supportsImageEffects) avgScore *= 1.0f;
			else{
				if(userConfiguration.supportsImageEffects == false && referenceConfiguration.supportsImageEffects == true){
					avgScore *= (1.0f - penaltyVect.z);
					compatibilityWarnings += "WARNING: The reference configuration supports Image Effects, but the user configuration does not!\n";
				}
			}

			//avgScore *= (1.0f - penalties);
			

			userHardwareScore = avgScore*basePoints;
			//userHardwareScore = penalties;
		}

		// Set and clear reference values
		public void SetReference(){
			referenceConfiguration.SetFromCurrentConfig();
		}

		public void ClearReference(){
			referenceConfiguration.Clear();
			referenceConfiguration = null;
		}

	}

}
