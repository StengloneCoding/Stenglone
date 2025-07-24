Feature: FizzBuzz Generation
  As a user
  I want to generate a list of numbers in a given range
  But instead of the number 3, I want to see "Fizz" 
  instead of the number 5, I want to see "Buzz" 
  and instead of the number 15, I want to see "FizzBuzz"    

  Scenario Outline: Generate FizzBuzz for single value
    When I generate FizzBuzz from <start> to <end>
    Then the result should contain exactly one item
    And the first value should be "<expected>"

    Examples:
      | start | end | expected   |
      | 1     | 1   | 1          |
      | 3     | 3   | Fizz       |
      | 5     | 5   | Buzz       |
      | 15    | 15  | FizzBuzz   |

  Scenario: Generate FizzBuzz from 1 to 100
    When I generate FizzBuzz from 1 to 100
    Then the result should contain 100 items
