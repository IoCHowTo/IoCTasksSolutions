#Task 14: Unit Testing Without IoC

## How to start?

1. Get familiar with project 140_UnitTestingWithIoC.
2. Identify classes which can be unit tested.
3. Implement unit tests.
  * Consider usage of NUnit and Moq as needed.
  * Make sure you covered the ```Calculator``` class.
4. Implement support for additional operators (plus, divide).
5. Extend the unit test coverage for newly added operators.
6. Compare the differences against 090_UnitTestingWithoutIoC.

## Sample solution

* Sample solution adds support for three new operators.
* Unit tests are added for:
  * Operators.
  * Factory.
  * Calculator class itself covers the internal logic via Moq library which
    allows you to focus on algorithm. You do not have to deal with every 
	single operator which is making the difference when compared to 
	090_UnitTestingWithoutIoC.
* The sample also demonstrates usage of the Moq unit test framework.

## Solution

* For more info about the sample solution please see IoCTaskSolutions, project 
  140_UnitTestingWithIoC.
* Read the comments throughout the code carefully, they contain important 
  information in regards inversion of control and dependency injection.
