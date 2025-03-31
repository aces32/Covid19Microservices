# 🦠 Covid19.Microservices

A modern, event-driven **microservices-based COVID-19 test management system**, built with **.NET 8**, **gRPC**, **RabbitMQ**, **Docker**, and **Kubernetes**.

> 🛠️ This project is a **complete redesign** of the original monolithic app:  
> 👉 [Covid19ManagementSystemDotNet](https://github.com/aces32/Covid19ManagementSystemDotNet)

---

## 🚀 Highlights

- ✅ Microservices architecture using **ASP.NET Core**
- ✅ **gRPC** for fast, typed service-to-service communication
- ✅ **RabbitMQ** + **MassTransit** for asynchronous event messaging
- ✅ Clean, maintainable **domain-driven** structure
- ✅ Fully containerized with **Docker** and orchestrated via **Kubernetes**
- ✅ Uses **EF Core**, **FluentValidation**, **MediatR**, **AutoMapper**

---

## 🏗️ Project Structure
Covid19.Microservices/ │ ├── Covid19.AdministratorService # Manages booking allocations ├── Covid19.IndividualService # Handles individual test bookings ├── Covid19.LocationService # Provides test location data ├── Covid19.Shared # Shared DTOs, events, and proto files ├── Covid19.Microservices.sln # Visual Studio solution file



---

## ⚙️ Microservice Interactions

- **IndividualService** books tests, manages `SpaceAllocated`, and publishes `SpaceAllocatedIncreasedEvent`.
- **AdministratorService** listens to the event and updates central records.
- Services use **gRPC** to sync availability projections and lookup data.

---

## 📦 Technologies Used

| Category            | Tech Stack                                 |
|---------------------|--------------------------------------------|
| Language & Runtime  | C#, .NET 8                                 |
| Communication       | gRPC, REST                                 |
| Messaging           | RabbitMQ, MassTransit                      |
| Data Storage        | PostgreSQL, In-Memory (dev/test)           |
| Architecture        | Microservices, Clean Architecture, DDD     |
| Infrastructure      | Docker, Kubernetes (K8s)                   |
| Dev Tools           | Visual Studio, VS Code, Docker Desktop     |

---

## 🐳 Running Locally with Docker

Ensure Docker is installed and running.

```bash
docker-compose up --build


📚 Origin & Motivation
This project is inspired by and evolved from:

🎓 Covid19ManagementSystemDotNet

It began as a monolith and has been restructured for:

✅ Improved scalability

✅ Loose coupling and maintainability

✅ Real-world DevOps practices

🤝 Contributing
This project is currently for personal development and experimentation, but feel free to:

Fork it

Open an issue

Suggest improvements


