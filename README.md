# Self-Finance Management API

This project is a **.NET Core Backend** application that provides a REST API for managing personal finances. It allows users to track income and expenses, generate daily and periodic financial reports, and manage financial operations effectively.

## Features

### 1. **Income and Expense Types**
- **CRUD Operations**:
  - Create, read, update, and delete types for income and expenses.

### 2. **Financial Operations**
- **CRUD Operations**:
  - Manage individual financial operations, including specifying the type (income/expense), amount, and date.

### 3. **Daily Report**
- **Endpoint**:
  - Input: **Date**
  - Output:
    - Total income for the specified date.
    - Total expenses for the specified date.
    - List of operations for the specified date.

### 4. **Date Period Report**
- **Endpoint**:
  - Input: **Start Date** and **End Date**
  - Output:
    - Total income for the specified period.
    - Total expenses for the specified period.
    - List of operations for the period.

## Tools and Technologies

- **.NET Core**: Backend framework for API development.
- **SwaggerUI**: Integrated API documentation and testing interface.
- **Postman**: Optional tool for manual testing of API endpoints.
