# BomberMan
My very own Object Oriented version of the classic game bomberman. For education purposes only.

## PLEASE NOTE for students of COS20007
This project has been created during live demos in tutorials. The purpose of it is to demonstrate what the design process for a program would be like in practice. I'm also using it as an opportunity to show you new and advanced techniques/design patterns one can use once they are comfortable with the fundamentals of OOP. You will notice I have used many techniques and/or design patterns not taught in this unit.

Although it is not directly an issue if you draw inspiration from this project and use some of those techniques/patterns, don't use anything you don't understand! If you want to use an idea from this project, make sure you understand it first.

Also, UNDER NO CIRCUMSTANCE ARE YOU ALLOWED TO DIRECTLY COPY AND USE any code from this project in your own submissions.

## Documentation
All relevant documentation can be found in the Documentation/ directory. Code has been commented using standard C# xml commenting.

## Dependencies
 - SplashKit: see website splashkit.io for more info including setup instructions for dotnet.

## Projects
- BomberManGame: Backend code, the "brains" of the game, theoretically adaptable to any frontend/UI
- NUnitTest: Testing project for the backend, runs unit tests on relevant code.
- SplashKitUI: A frontend/UI for desktop based platforms using the SplashKit API from https://splashkit.io
- Client: Code for managing a client
- Server: Code for running a hosted server

# Developers
Below is information for developers of this plugin.

## Repo Management
 - When coding, NEVER operate out of master.
 - Branch naming conventions are as follows:
    - feature-NameOfFeature (for new features)
    - fix-NameOfFix (for bug fixes and/or updates of existing features)
    - test-NameOfTest (for any testing)
 - If a branch is abandoned, please permanantly delete it.

### Merging to Master
 - Only merge to master when the feature/fix/test is complete.
 - Merges must only be made via pull requests.
 - Assuming there are no conflicts, contributers may freely merge pull requests.
 - If there are conflicts, resolve with descretion, and involve other contributers when necessary.

