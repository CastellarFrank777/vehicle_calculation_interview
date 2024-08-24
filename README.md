# Vehicle Calculation

## Overview

The Vehicle Calculation Tool is a web application designed to help vehicle buyers calculate the total cost of a vehicle purchase at auction. The tool dynamically computes various fees and charges based on the vehicle's type (Common or Luxury) and base price, ensuring users have a clear understanding of the total cost before placing a bid.

## Project Components

The project is divided into two main components:

1. **API Backend**: A .NET Core 8 Web API responsible for calculating the total cost of a vehicle, considering all relevant fees.
2. **Web Frontend**: A Vue.js application that provides an interactive user interface for entering vehicle details and displaying the calculated costs.

## Features

- **Dynamic Fee Calculation**: The backend API calculates buyer fees, seller fees, association costs, and storage fees based on vehicle type and base price.
- **Real-Time Updates**: The frontend application updates the total cost dynamically as the user inputs or modifies vehicle details.
- **Detailed Cost Breakdown**: Both the API and frontend work together to provide a comprehensive breakdown of all fees involved in the vehicle auction process.

## Sample Calculations

Below is a table of sample calculations illustrating how the tool computes the total cost for different vehicle types and base prices:

| Vehicle Price ($) | Vehicle Type | Basic Fee ($) | Special Fee ($) | Association Fee ($) | Storage Fee ($) | Total Cost ($) |
|-------------------|--------------|---------------|-----------------|---------------------|-----------------|----------------|
| 398.00            | Common       | 39.80         | 7.96            | 5.00                | 100.00          | 550.76         |
| 501.00            | Common       | 50.00         | 10.02           | 10.00               | 100.00          | 671.02         |
| 57.00             | Common       | 10.00         | 1.14            | 5.00                | 100.00          | 173.14         |
| 1800.00           | Luxury       | 180.00        | 72.00           | 15.00               | 100.00          | 2167.00        |
| 1100.00           | Common       | 50.00         | 22.00           | 15.00               | 100.00          | 1287.00        |
| 1000000.00        | Luxury       | 200.00        | 40000.00        | 20.00               | 100.00          | 1040320.00     |

## Getting Started

To set up and run the Vehicle Calculation, please refer to the individual README files for the API and Web frontend components. These files contain detailed instructions on how to install dependencies, configure the environment, and start the application.
