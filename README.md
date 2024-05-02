# CRFrameworkProject
 
The provided framework offers a foundation for developing cross-reality applications within Unity. The framework is designed to be extensible, leveraging interfaces and abstract classes, developers can extend the functionality of the framework to suit their specific requirements. These abstract structures provide a foundation for implementing custom logic and behaviors.

## The 5 Basic Classes

This repository contains several C# Unity classes aimed at facilitating the development of Unity projects:

- **AbstractController.cs**: This class provides an abstract base for controllers in Unity projects. Controllers in Unity are responsible for handling user input, updating the model, and communicating changes to the views. The `AbstractController` class encapsulates common functionality needed by controllers, such as managing views, subscribing to model updates, and propagating changes.

- **AbstractDataModel.cs**: This class defines an abstract base for data models in Unity projects. Data models represent the state of the application and are typically used to store and manipulate data. The `AbstractDataModel` class provides an event-driven architecture for notifying observers (such as controllers and views) when the model data changes.

- **AbstractTransitionConfig.cs**: Transition configurations are essential for managing transitions between different states or environments in Unity projects, such as transitioning between desktop and augmented reality (AR) modes. The `AbstractTransitionConfig` class defines an abstract base for transition configurations, allowing developers to implement custom logic for transitioning between different states or environments.

- **AbstractView.cs**: Views are responsible for presenting the data from the model to the user and responding to user interactions. The `AbstractView` class provides an abstract base for views in Unity projects, encapsulating common functionality such as subscribing to model updates and updating the UI in response to changes in the model.

- **SystemConfig.cs**: This class manages system configuration and environment settings within Unity projects. It provides a centralized way to access configuration settings such as cameras, scene containers, and supported realities (e.g., desktop, AR). The `SystemConfig` class follows the singleton pattern to ensure there is only one instance of the configuration manager in the project.

These classes serve as foundational structures for managing data, controllers, views, transitions, and system configuration within Unity projects. Developers can extend and customize these classes according to the specific requirements of their projects, helping to streamline development and improve code organization.