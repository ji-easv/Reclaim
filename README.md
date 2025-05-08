# Compulsory Assignment 2: Databases for Developers

## Requirements
Your assignment must satisfy the following requirements:

1. The implementation must clearly demonstrate contributions from every group member.
2. All design decisions and assumptions must be thoroughly documented in a README.md file.
3. The implementation must be done in a public GitHub repository.

## Scenario
Your team has been hired to design and implement the backend of an e-commerce platform specializing in second-hand items. The platform allows users to

1. List items for sale
2. Browse listings
3. Place orders
4. Review sellers

Key design considerations include:

- **NoSQL Database**: Efficient handling of user-generated listings, flexible schema requirements for varying item types, and rapid scalability.
- **Cloud Storage**: Reliable and scalable storage for item images and multimedia content.
- **Caching**: Improve performance by caching frequently accessed data such as item listings, user profiles, and search results.
- **CQRS** (Command Query Responsibility Segregation): Separate read and write operations to enhance scalability, performance, and maintainability.
- **Transactions**: Ensure consistency and reliability when users perform critical operations like placing an order or updating listings.

## Design & Implementation
You must clearly outline and justify your design choices addressing the following questions:

1. **Database Selection**:
   - Identify and justify the selection of databases (relational and NoSQL) for various parts of your application. 

2. **Data Schema and Storage Strategy**:
   - Define the data models and storage strategies you will use. 
   - Clearly document how you will store and manage different kinds of data (listings, user profiles, orders, reviews).

3. **Integration of Cloud Storage**:
   - Describe how you will integrate cloud storage for images and other media. 
   - Include a clear explanation of interactions between cloud storage and your databases.

4. **Caching Strategy**:
   - Define your caching approach, including technologies used, cache invalidation strategy, and which data will be cached.

5. **CQRS Implementation**:
   - Explain your approach to separating read and write operations, if applicable. 
   - Clarify how this impacts the application's scalability and complexity.
   
6. **Transaction Management**:
   - Detail your approach to ensuring transactional integrity, particularly in scenarios such as orders and listing updates.