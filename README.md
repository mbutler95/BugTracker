# BugTracker
This web application is built with ASP.Net using razor with an MVC model.

The data for this application is hosted on MongoDb which uses NoSQL. One of the main reasons being that the data requirements are simple and the UI for MongoDb made it easy to setup the cloud data hosting that is permenantly free.

The application is setup with a Model-View-Controller framework, with Business Logic and Data Access seperated into discreet classes for readabilities sake.

There is an exposed API which should allow you to add a bug to the system with the url, accessible at "Bug/Create/{Title}/{Description}".

One design choice I made was to not delete bugs/users instead to archive them, this allows the application to keep track of unique IDs for the users/bugs alongside the MongoDB required ObjectId.

To Run: Should hopefully be as simple as opening the solution in Visual Studio and starting it.

To Test: For the Next week from 16-01-23, the database should allow network from anywhere. If there is a problem with this or access needs allowing/extending please email me at matthew.butler97@gmail.com.

There are a few things I would have liked to have added/changed namely:
Error Handling - Haven't ran into many errors while testing but there are probably erroring scenarios that I've not thought of.
Unit Testing - I couldn't quite figure out how to correctly set up the MongoDB to be fully mockable using the Moq package to test with expected input and output.
Overall Look - Some of the UI is very simple and not overly appealing in places. 
