# VehicleDashConcept
Concept user interface (UI) for a vehicle dashboard. The UI was implemented with C# using Windows Presentation Foundation (WPF).

This project is a concept user interface for a vehicle dashboard / infotainment display along with a set of test tools that could be useful during Human-Systems Integration (HSI) tests. The dashboard has five main pages: Home, Driver, Navigation, Media, and Phone. Each page has a touch-friendly layout that also accommodates mouse input. The goals of this project were:

- to create a realistic UI with interactive controls and
- to test the controls via simulated data.
The end result is a neat concept UI that is fun to play with (at least I think it is 👍) and test changes in state. It would also not be that difficult to inject real data and services into the UI, since the mock-up business logic is loose-coupled to the UI.

# Features

- Rapid prototype UI/UX desktop application for hardware proof-of-concept
- Model-View-ViewModel (M-V-VM / MVVM) design pattern
- Custom WPF UserControls
- Navigation implemented using the System.Windows.Navigation API and Frame control
- XAML-structured UI with data bindings
- Test tools to demonstrate and automate UI functionality
