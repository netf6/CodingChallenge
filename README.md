# CodingChallenge

Run as a .Net Console App. 

**Main Menu**
When you start the application, you will be presented with the following options:
1.	Find the common base of two different path strings: This function allows you to input two different path strings and find their common base.
2.	Find the closest word: This function helps you find the word that is closest to a given word based on certain criteria.
3.	Find instantaneous speed of a vehicle: This function calculates the instantaneous speed of a vehicle based on the provided data.
4.	Exit: This option will close the application.
To select a function, simply enter the corresponding number and press Enter.

** 1. Find the common base**

    Achieved by splitting the input paths into arrays and comparing each folder element until there is no longer a match. 

    **Normal Operation: **
    Let's find the common base of two different path strings...
    Enter the first path string (eg. /home/daniel/memes):
    /home/daniel/memes
    Enter the second path string (eg. /home/daniel/work):
    /home/daniel/work
    The common base of the two paths is: /home/daniel

    **Boundary Conditions**
    - Single word which is the same
      Enter the first path string (eg. /home/daniel/memes):
      sdf
      Enter the second path string (eg. /home/daniel/work):
      sdf
      The common base of the two paths is: /sdf

      Note how the slash is prefixed when ledft out at input.

    - Mismatch single word
      Enter the first path string (eg. /home/daniel/memes):
      mis
      Enter the second path string (eg. /home/daniel/work):
      match
      There is no common base.

**2.	**Find the closest word: ****
    
