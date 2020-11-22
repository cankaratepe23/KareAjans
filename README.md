# Can Karatepe Devbase Backend Challenge

This project was created using .NET Core 5.0: ASP.NET Core with Razor Pages and EF Core with the Code First Approach. Some choices I made with their explanations:

- I choose to use Razor pages instead of MVC for simplicity and modernity's sake.
- I choose to not use a Repository pattern as an abstraction layer because after some reasearch I figured out it was considered an anti-pattern with the features available in EntityFramework.
- I choose to use the SQL LocalDB server included in Visual Studio because it made testing simpler and easier and since this project is not meant for production.
- I choose not to implement strict and/or localization-dependent validations for some fields such as phone numbers and addresses to make it easier for testing quickly.
