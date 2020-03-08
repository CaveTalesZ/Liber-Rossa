# Liber-Rossa
The Tower Defense set on Liber IV

## Unity Version ##

Make sure you are using Unity version 2019.3.0f6

## Merging and version control ##

Make sure you are using sourcetree to push and pull to and from the repository (unless you are confident in your use of another program)

On your computer, navigate to the Unity install folder (Probably C:\Program Files\Unity)

Go to Unity\2019.3.0f6\Editor\Data\Tools\UnityYAMLMerge.exe
Copy the path to that .exe file

Go to Sourcetree -> Options/Tools -> Diff
Under "External Diff/Merge" change Merge Tool to Custom
Under Merge Command, paste the path to UnityYAMLMerge.exe
Under Arguments, paste "merge -p $BASE $REMOTE $LOCAL $MERGED"