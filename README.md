# unity-system-info-detection
Automatic hardware detection and evaluation in Unity

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

TODO:
* Make custom editor
* Actually test this
