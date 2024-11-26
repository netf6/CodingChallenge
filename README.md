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

**2.	Find the closest word: **

    Enter a sample word to be compared with a list of words to see which is the most similar. 
    It possible to test agaist and existing doctionary (hard-coded) or input a custom list. 
    The algorithm used, measures the "edit distance," or how many edits(insertions, deletions, or substitution Function to calculate similarity. 
    Known as the Levenshtein distance abd was a recomendation by from ChatGPT. 

     **Normal Operation: **
    Let's find a word that is most like the word you enter...
    Enter a word:
    berry
    Would you like to compare "berry" to:
     (1) use a pre-loaded dictionary, or
     (2) enter your own list of strings?
     Enter 1 or 2 (3 to exit):
    1
    comparing: apple
    comparing: banana
    comparing: blueberry
    comparing: cherry
    comparing: date
    comparing: elderberry
    comparing: fig
    comparing: grape
    comparing: honeydew
    comparing: jackfruit
    comparing: kiwi
    comparing: lemon
    comparing: mango
    comparing: nectarine
    comparing: orange
    comparing: papaya
    comparing: quince
    comparing: raspberry
    comparing: strawberry
    comparing: tangerine
    comparing: vanilla
    comparing: watermelon
    comparing: yellow watermelon
    comparing: zucchini
    The word that is most like "berry" is: cherry 
    
**3.	Find instantaneous speed of a vehicle:**

    Predefined points loaded from a configurable xml document. 
    The time offset is inputed and is added to the time of the first predefeined point.
    We then loop trough the predefined points and check if the time offset falls between the current point and the next.
    The distance between points is calculated as the SQRT((X2 - X1)^2 + (Y2 - Y1)^2)
    The time between two points is calculated by subtracting the next pointInTime from the current. 
    Distance = speed * time which means speed = distance / time, which is returned. 
    
    Reading PointsInTime.xml and loading predefined points...
    X: 1 Y: 1 Time: 1732595439 (2024-11-26 04:30:39:000)
    X: 2 Y: 2 Time: 1732595499 (2024-11-26 04:31:39:000)
    X: 3 Y: 3 Time: 1732595559 (2024-11-26 04:32:39:000)
    X: 4 Y: 4 Time: 1732595619 (2024-11-26 04:33:39:000)
    Predefined points loaded successfully.
    Let's find the instantaneous speed of a vehicle...
    PointsInTime.xml contains a list of predefined points
    Enter a count in seconds from when the vehicle set off: (eg 30)

    **Boundary Conditions**
    - Entering a time before the earlieset PointInTime (a negative number)
        Enter a count in seconds from when the vehicle set off: (eg 30)
        -30
        atTime: 1732595409 (2024-11-26 04:30:09:000)
        Time entered is before the setoff point.

    - Entering a time after the last PointInTime (a large number)
    Enter a count in seconds from when the vehicle set off: (eg 30)
    54654
    atTime: 1732650093 (2024-11-26 19:41:33:000)
    Time entered is after the last arrival point.
    
