## Blendshape2Animation

This program makes the animations (AimationClip) based on the blendshape.

### Usage
1. Delete all files in the target directory, `Assets/AZW/BS2A/GeneratedAnimations/`
1. Select a mesh on the Hierarchy
1. Click [Tools] > [AZW] > [Blendshape2Animation]
1. The animation will be in `Assets/AZW/BS2A/GeneratedAnimations/`
1. Move the generated files to your directory

Note: this folder is only for temporary use. This script won't overwrite the file if there is a file with the same name in target directory.

## AddToAnimatorController

This program adds layers having an animation clip with motion time configuration.

### Usage
First, you need to create the animation clips to be added.
If not yet, prepare them with Blendshape2Animation or your own way.

1. Click [Tools] > [AZW] > [Add animations to an animation layer]
1. Put your animation controller
    - Note that the animation controller will be overwritten. Please make a backup before running to be safety.
1. Put the animation clips
1. Click *Run* button

## Licence
MIT
