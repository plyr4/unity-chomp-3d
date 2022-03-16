# unity-chomp-3d
starter project for chomp proof of concepts in 3D

## summary

the arm is built using articulation bodies, colliders, and joint rotations using joint motors

## controls

use a gamepad. joint controls are:
- left stick horizontal: shoulder
- left stick vertical: elbow
- right stick horizontal: wrist
- right stick vertical: hand
- right shoulder button: jaw

## camera

orthographic, aka 3D as 2D

## ArticulationBody

the arm uses [ArticulationBody](https://docs.unity3d.com/ScriptReference/ArticulationBody.html) to act as arm joints controlled by the physics engine.

references:
- https://blog.unity.com/manufacturing/use-articulation-bodies-to-easily-prototype-industrial-designs-with-realistic-motion
- https://docs.unity3d.com/Manual/class-ArticulationBody.html
- https://docs.unity3d.com/ScriptReference/ArticulationBody.html