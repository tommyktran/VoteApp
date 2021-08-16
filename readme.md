This console app is a representation of an accessible voting system that displays a ballot with multiple contests, and allows the user to navigate and select candidates using numbers on a keypad. The program draws data from local JSON files that contain data on the ballot and contests. 

# Features: 

- Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program

-- The program use a master loop and takes commands to go to next / previous candidate or contest, select a candidate, or display the ballot. When the user is on the last contest, they can exit the ballot.

- Read data from an external file, such as text, JSON, CSV, etc and use that data in your application

-- The program reads data from local JSON files such as "BALLOT_0001" and "CONTEST_C01" to use for the Ballot, Contest, and Candidate classes.

- Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program

-- The Ballot class has a contests variable which contains the list of all the contests in that ballot, and the Contest class has a candidates variable which contains all the candidates in that contest.