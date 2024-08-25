Feature: User login
  In order to access the application
  As a registered user
  I want to be able to login using valid credentials

  @chrome @SuccessfulLogin
  Scenario: Successful login with valid credentials
    Given I am on the login page
    When I enter valid username and password
    And I click the login button
    Then I should be redirected to the dashboard

  @chrome @EmptyName
  Scenario: Unsuccessful login with empty username
    Given I am on the login page
    When I clear the username field
    And I enter a valid password
    And I click the login button
    Then I should see the error message "Epic sadface: Username is required"

  @chrome @EmptyPassword
  Scenario: Unsuccessful login with empty password
    Given I am on the login page
    When I enter a valid username
    And I clear the password field
    And I click the login button
    Then I should see the error message "Epic sadface: Password is required"

  @edge @SuccessfulLogin
  Scenario: Successful login with valid credentials (Edge)
    Given I am on the login page
    When I enter valid username and password
    And I click the login button
    Then I should be redirected to the dashboard

  @edge @EmptyName
  Scenario: Unsuccessful login with empty username (Edge)
    Given I am on the login page
    When I clear the username field
    And I enter a valid password
    And I click the login button
    Then I should see the error message "Epic sadface: Username is required"

  @edge @EmptyPassword
  Scenario: Unsuccessful login with empty password (Edge)
    Given I am on the login page
    When I enter a valid username
    And I clear the password field
    And I click the login button
    Then I should see the error message "Epic sadface: Password is required"