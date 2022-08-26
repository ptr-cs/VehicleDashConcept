# VehicleDashConcept
Concept user interface (UI) for a vehicle dashboard. The UI was implemented with C# using Windows Presentation Foundation (WPF).

![Vehicle Dashboard Concept Overview](/media/vehicle_dash_concept_overview.gif "Vehicle Dashboard Concept Overview")

&ensp;&ensp;&ensp;&ensp;This portfolio project is a concept user interface for a vehicle dashboard / infotainment display along with a set of test tools that could be useful during Human-Systems Integration (HSI) tests. The dashboard has five main pages: Home, Driver, Navigation, Media, and Phone. Each page has a touch-friendly layout that also accommodates mouse input. The goals of this project were:

- to create a realistic UI with interactive controls and
- to test the controls via simulated data.
The end result is a neat concept UI that is fun to play with (at least I think it is 👍) and test changes in state. It would also not be that difficult to inject real data and services into the UI, since the mock-up business logic is loose-coupled to the UI.

For a more detailed explanation of the code, [a blog post about the project is available here](http://www.dividebyzeno.com/vehicle-dashboard-concept.html).

# Features

- Rapid prototype UI/UX desktop application for hardware proof-of-concept
- Model-View-ViewModel (M-V-VM / MVVM) design pattern
- Custom WPF UserControls
- Navigation implemented using the System.Windows.Navigation API and Frame control
- XAML-structured UI with data bindings
- Test tools to demonstrate and automate UI functionality

# Setup / Running 
The project includes a Visual Studio solution file (.sln). Opening the solution file within Visual Studio should allow the project to be built and run.

# Screenshots
## Home Page
![Vehicle Dashboard Concept - Home](/media/vehicle_dash_concept_home.jpg "Vehicle Dashboard Concept - Home") 
## Driver Page
![Vehicle Dashboard Concept - Driver](/media/vehicle_dash_concept_driver.JPG "Vehicle Dashboard Concept - Driver")
## Navigation Page
![Vehicle Dashboard Concept - Navigation](/media/vehicle_dash_concept_navigation.JPG "Vehicle Dashboard Concept - Navigation") 
## Weather Page
![Vehicle Dashboard Concept - Weather](/media/vehicle_dash_concept_weather.JPG "Vehicle Dashboard Concept - Weather") 
## Media Page
![Vehicle Dashboard Concept - Media](/media/vehicle_dash_concept_media.JPG "Vehicle Dashboard Concept - Media") 
## Phone Page
![Vehicle Dashboard Concept - Phone](/media/vehicle_dash_concept_phone.JPG "Vehicle Dashboard Concept - Phone") 
## Test Tools Page
![Vehicle Dashboard Concept - Test](/media/vehicle_dash_concept_test.JPG "Vehicle Dashboard Concept - Test") 
