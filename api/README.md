# Vehicle Calculation API

## Overview

The Vehicle Calculation API is designed to handle vehicle cost calculations for different types of vehicles. It includes middleware for error handling, a structured result handling approach, and CORS configuration to manage cross-origin requests.

## Features

### Middleware for Error Handling

The API includes a middleware component that catches unhandled exceptions and returns a `500 Internal Server Error` response. This middleware also logs errors for monitoring and debugging purposes. This ensures that any unexpected issues are handled gracefully, providing a consistent response format and allowing for better tracking of issues.

### Result and DataResult

To streamline error handling and response management, the API uses `Result` and `DataResult` types. These types encapsulate the response data and handle errors internally, avoiding the need for extensive exception handling in the API methods. This approach simplifies the API's logic and ensures a consistent response structure.

### CORS Configuration

The API is configured to handle cross-origin requests through its CORS settings. You need to specify the `AllowedOrigins` in the configuration to allow requests from the web UI project. This setup ensures that only specified origins can interact with the API, improving security and control over who can access the API.

### CORS Setup

In the `appsettings.json`, you can configure the allowed origins for CORS by setting the `AllowedOrigins` property. This property should list the origins that are permitted to make requests to the API.

## Project Structure

The project is organized into separate, flexible projects to enhance maintainability and scalability:

- **vehicle_calculation.api.models**: Contains data models used by the API.
- **vehicle_calculation.api**: Implements the API layer, handling HTTP requests and responses.
- **vehicle_calculation.common**: Includes common utilities and shared components used across the application.
- **vehicle_calculation.logic.interfaces**: Defines interfaces for the business logic layer.
- **vehicle_calculation.logic.models**: Contains models specific to the business logic.
- **vehicle_calculation.logic.tests**: Holds unit tests for the business logic layer.
- **vehicle_calculation.logic**: Implements the core business logic.
- **vehicle_calculation.mappings**: Manages mappings between different data models.

This structure supports a clear separation of concerns, making it easier to manage, test, and extend each part of the application independently.
