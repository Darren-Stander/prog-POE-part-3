# Darren Stander St10209886.Prog6221POEPart1

# https://github.com/Darren-Stander/prog-POE-part-3

# commits for Part 3

![Screenshot 2024-06-27 112038](https://github.com/Darren-Stander/prog-POE-part-3/assets/163761702/e7c321ab-f926-44ce-ab5b-2290d1326e60)

# Recipe WPF Application

# This is a WPF (Windows Presentation Foundation) application designed to manage recipes. You can create, view, reset, and delete recipes using this app.

# Features
Enter Ingredients: Allows you to enter the ingredients for your recipe.

Enter Steps: Lets you input the steps to make your recipe.

View Recipe: Displays the current recipe details.
Reset Recipe: Resets any changes made to the recipe.

Delete Recipe: Deletes the current recipe.

Close App: Exits the application.

How to Run

# Prerequisites
Install .NET SDK:

Ensure that the .NET SDK is installed on your machine. You can download it from the official .NET website.

Install Microsoft Visual Studio:

## Steps to Run
Download the Zip File:

# Download the zip file containing the source code and extract it to your desired location.
Navigate to the Project Directory:

# Open a terminal or command prompt and navigate to the directory where you extracted the zip file.
Compile and Run the App:

Run the following commands to compile and execute the application:

# On Windows:
Locate the Zip File:

# Navigate to the folder where the zip file is located.

# Extract the Zip File:

# Right-click on the zip file.
Select "Extract All..." from the context menu.
Choose a destination folder to extract the contents.
Click "Extract".

# On macOS:
Locate the Zip File:
Navigate to the folder where the zip file is located.
Extract the Zip File:
Double-click on the zip file.
The contents will be automatically extracted to a folder with the same name as the zip file.

# Install Microsoft Visual Studio

# On Windows:
Download Visual Studio Installer:
Visit the Visual Studio Downloads page.
Click on the "Download Visual Studio" button to download the installer.
Run the Installer:

# Locate the downloaded installer (usually in the Downloads folder).
Double-click on the installer to run it.
Select Workloads:

# In the installer, you can select the workloads you want to install (e.g., .NET desktop development, ASP.NET development).
Choose the workloads that fit your needs and click "Install".
Follow the Installation Wizard:

# Follow the on-screen instructions to complete the installation.
Visual Studio will be installed with the selected workloads.

# Detailed Steps to Run
# 1. Download the Source Code
Visit the GitHub repository.

Click on the green "Code" button and select "Download ZIP".

Save the ZIP file to your computer.

# 3. Open Visual Studio
Open Visual Studio from your Start menu or desktop shortcut.

# 4. Open the Project in Visual Studio
In Visual Studio, select "Open a project or solution".

Navigate to the extracted folder from step 2.

Open the .sln file within the extracted folder. This is the solution file for the project.

# 5. Prepare the Project
If Visual Studio prompts you to install missing components, click "Yes" or "OK" to install them.

Wait for Visual Studio to restore any necessary NuGet packages and dependencies.

# 6. Build the Solution
Go to the "Build" menu at the top of Visual Studio.

Select "Build Solution" or press Ctrl+Shift+B.

Visual Studio will compile the project, ensuring that all dependencies are correctly resolved and that the code is ready to run.

# 7. Run the Application
Go to the "Debug" menu at the top of Visual Studio.

Select "Start Debugging" or press F5.

The WPF application will start, and you will see the GUI for managing recipes.

# Using the Application
Enter Ingredients: Use the provided interface to enter the ingredients for your recipe.

Enter Steps: Use the provided interface to input the steps to make your recipe.

View Recipe: View the current recipe details on the main interface.

Reset Recipe: Reset any changes made to the recipe using the reset option.

Delete Recipe: Delete the current recipe from the list using the delete option.

Close App: Exit the application by selecting the appropriate menu option or closing the window.

# Troubleshooting
Missing SDK or Components: Ensure that the .NET SDK and Visual Studio are correctly installed.

Project Dependencies: If the project fails to build, ensure all NuGet packages are restored. Go to "Tools" -> "NuGet Package Manager" -> "Manage NuGet Packages for Solution" and restore any missing packages.

Code Issues: If there are errors in the code, review the error messages in the "Error List" window in Visual Studio for guidance on how to resolve them.

By following these steps, you should be able to successfully run the Recipe Manager WPF Application on your machine.

# Changes I have made to the code since part 2 and the changes I have made based on the lectures feedback

I significantly upgraded the Recipe Manager WPF Application to improve both its functionality and code organisation in response to the lecturer's recommendations. Reorganising the codebase was one of the main modifications. I separated the programme into discrete components, each of which handled particular capabilities like ingredient management, recipe phases, and user interface controls, by taking a more modular approach. This made the code easier to read and maintain, made debugging and future improvements easier, and enhanced overall.

I also added a function that lets users choose which food group each item belongs to. This update responds to user input by giving the programme more precise nutritional data and classification. It is now possible for users to group ingredients into categories like proteins, carbs, fats, and others, which facilitates recipe management and analysis. To implement this functionality, a dropdown menu was added to the ingredient entry form so that users may choose the proper food group for each item.

The application's fundamental functions don't alter in spite of these enhancements. It is still simple for users to create, view, reset, and remove recipes. The application integrates the additional organising and classification functions without sacrificing its strong performance or user-friendliness, making for a smooth user experience.
