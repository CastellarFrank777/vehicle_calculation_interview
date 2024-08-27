# Vehicle Calculation Web Application

This web application allows users to calculate the total cost of a vehicle by entering the vehicle's price and selecting its type. The application provides a breakdown of fees, including basic, special, association, and storage fees.

## Features

- **Real-Time Calculation**: Automatically updates the total cost as you input the vehicle price and type, without needing to click a submit button.
- **Debounce Feature**: Configurable debounce delay ensures smooth user experience by waiting for the user to finish typing before making API requests.
- **Fee Breakdown**: Displays detailed fees, including basic, special, association, and storage fees, along with the total cost.

## Configuration

The application uses environment variables to configure key settings:

- **VITE_VEHICLE_API_BASE_URL**: Set this to the correct backend API URL for your environment.
- **VITE_FORM_DEBOUNCE_DELAY**: Configures the debounce delay (in milliseconds) for API calls to ensure a better experience.

Ensure that these variables are properly set in your `.env` file according to your environment.

## Installation

1. Clone the repository.
2. Navigate to the project directory.
3. Run `npm install` to install dependencies.
4. Set up your `.env` file with the appropriate variables.
5. Run `npm run dev` to start the development server.

## Usage

- Enter the vehicle price and select the vehicle type.
- The application will automatically calculate and display the total cost and fee breakdown.
