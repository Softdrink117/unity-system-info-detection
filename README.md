# unity-system-info-detection
Automatic hardware detection and evaluation in Unity

![HardwareInfo Overview](/Assets/HardwareInfo/Examples/Inspector_Overview.PNG)

A set of scripts to evaluate the user's hardware configuration relative to a "reference" configuration. Thought not an exhaustive test by any means, and not necessarily accurate, it can be used as a baseline to approximate what Quality Settings to use for a first-time user of a Unity game.

###Details

The HardwareInfo script does three basic things:
* **It gets system information** - GPU VRAM, shader model, API, shadow/compute/image effect support, cpu cores and frequency, etc. - from the computer it is running on.
* **It compares this system information to a saved 'reference' configuration,** and gives the current machine a System Score scaled to 100. A score of 100 means that the new machine is identical to the reference; a score of less than 100 means that the new machine is less powerful than the reference; a score of more than 100 means that the new machine is more powerful than the reference.
* **It produces warnings when the current machine and the reference machine have serious differences** that might cause errors, such as significantly different shader models, lack of support for core features like shadows and image effects, and different graphics APIs

It is capable of obtaining much more sophisticated system information as well (manufacturer of motherboard and GPU; system name, system unique ID, detailed support for Unity engine features, etc.)

###Usage

The basic *(as-yet-untested)* usage idea is as follows:

1. Set up the game on a known 'Reference' machine, and save it as the Reference Configuration.
2. Then set up the game on a different (ideally lesser) machine, and observe the User Score given by the HardwareInfo script. 
  * Record information about the framerate on both machines, to compare.
3. Ideally, repeat step 2 on multiple machines to get more points of reference for System Score and performance.

Assuming a deterministic relationship between game performance and System Score *(which may or may not be the case! Testing is needed!)*, calculating a system score on the first run of a game should allow the Unity game to automatically determine it's optimal settings.

---

###TODO:
* Make custom editor
* Actually test this

---

###Features

####Automatic Compatibility Warnings
![Compatibility Warnings](/Assets/HardwareInfo/Examples/Compatibility_Warnings.PNG)

The *HardwareInfo* script can determine if there are obvious compatibility issues between the Reference and User machines, and exposes them as visible Warning messages in the Inspector. Currently, it checks for Shadows, Graphics API, GPU Multithreading, Shader Model, Compute Shader, and Image Effect support. The Compatibility Warnings are also accessible by script (*through an admittedly extremely primitive set of methods right now, but these would be easily expandable to suit a project's needs*).

####Adjustable Score Calculation
![Comparison Weights](/Assets/HardwareInfo/Examples/Comparison_Weights.PNG)

The equation used for determining a Score can be modified to suit a specific project. Some weights are exposed in the Inspector that allow for fine-tuning of the output; the score calculation is also very self-explanatory and (*will be*) documented in code.

####Simple and Complex Data Classes
![Simple Data](/Assets/HardwareInfo/Examples/Inspector_SimpleData.PNG)

The *HardwareInfo* script automatically obtains some Simple Hardware Info from the current machine on Awake(), and compares it to stored Reference Data. The Simple Hardware Info class is used to store the information that is necessary for the comparison. It also contains an 'SLI Scalar' property (only used for the Reference Machine) that accounts for the fact that Unity cannot detect an SLI configuration (*at least, as far as I can tell*), and also accounts for the nonlinear scaling of SLI performance. 

![Complex Data](/Assets/HardwareInfo/Examples/Inspector_ComplexData.PNG)

In addition to the Simple Hardware Info class used to calculate the System Score, the *HardwareInfo* script can also be used to get a copy of the complete system data that is currently exposed to Unity's SystemInfo class (*Current as of Unity 5.4.0f3 for this build version, which was created for a project in a slightly outdated version of Unity. I plan to release a 5.5 and 5.6 version in the near future.*). This could be useful for any number of reasons. The Complex Data can be toggled on and off, to save on memory usage and performance.
