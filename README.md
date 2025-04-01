# 🦠 Covid19.Microservices

A modern, event-driven **microservices-based COVID-19 test management system**, built with **.NET 8**, **gRPC**, **RabbitMQ**, **Docker**, and **Kubernetes**.

> 🛠️ This project is a **complete redesign** of the original monolithic app:  
> 👉 [Covid19ManagementSystemDotNet](https://github.com/aces32/Covid19ManagementSystemDotNet)

---

## 🚀 Highlights

- ✅ Microservices architecture using **ASP.NET Core**
- ✅ **gRPC** for fast, typed service-to-service communication
- ✅ **RabbitMQ** + **MassTransit** for asynchronous event messaging
- ✅ Clean, maintainable **Domain-Driven Design (DDD)**
- ✅ Fully containerized with **Docker** and orchestrated via **Kubernetes**
- ✅ Uses **EF Core**, **FluentValidation**, **MediatR**, **AutoMapper**

---

## 🏗️ Project Structure

```
Covid19.Microservices/
├── Covid19.AdministratorService       # Booking allocation logic
├── Covid19.IndividualService          # Handles individual test bookings
├── Covid19.IndividualLabsService      # Manages lab test results
├── Covid19.LocationService            # Manages test locations
├── Covid19.Shared                     # Shared contracts, events, DTOs
├── K8S                                # Kubernetes manifests
│   ├── deployments/
│   ├── services/
│   └── volumes/
├── run-*.sh                           # Shell scripts to run services locally
├── README.md
├── docker-compose.yml (optional)
└── Covid19.Microservices.sln
```

---

## ⚙️ Microservice Interactions

- **IndividualService**: Handles booking and emits `CovidTestBookedEvent`, `SpaceAllocatedIncreasedEvent`
- **AdministratorService**: Listens to `SpaceAllocatedIncreasedEvent` and manages capacity
- **IndividualLabsService**: Receives `CovidTestBookedEvent`, updates and sets test outcome
- **gRPC** is used for querying data between services (e.g., availability)

---

## 📦 Tech Stack

| Layer           | Technology                           |
|----------------|---------------------------------------|
| Language        | C# (.NET 9.0)                          |
| APIs            | REST, gRPC                           |
| Messaging       | RabbitMQ + MassTransit               |
| Database        | PostgreSQL, In-Memory (dev)          |
| Architecture    | Microservices, DDD, CQRS             |
| Infrastructure  | Docker, Kubernetes                   |
| Tools           | Visual Studio, VS Code, GitHub, Ingress, Insomnia |

---

## 🐳 Running Locally (Per Service)

Use the provided `.sh` scripts in the root folder:

```bash
./run-adminservice-locally.sh
./run-individualservice-locally.sh
./run-individuallabsservice-locally.sh
./run-locationservice-locally.sh
```

Each script starts its respective service on HTTPS using `dotnet run`.

> 💡 You must have the dev certificate trusted:
```bash
dotnet dev-certs https --trust
```

---

## ☸️ Kubernetes Deployment

From the root of the solution:

```bash
./run-k8s-deployment.sh
```

Or manually apply:

```bash
kubectl apply -f K8S/volumes/
kubectl apply -f K8S/deployments/
kubectl apply -f K8S/services/
```

Then access services via **Ingress**:

```
http://127.0.0.1/api/Location
http://127.0.0.1/api/Individual
http://127.0.0.1/api/Administrator
http://127.0.0.1/api/IndividualLabs
```

> ⚠️ Make sure your ingress controller (`ingress-nginx`) is up and running.

---

## 📚 Origin & Motivation

This project is inspired by and evolved from:

🎓 [Covid19ManagementSystemDotNet](https://github.com/aces32/Covid19ManagementSystemDotNet)

It began as a monolith and has been restructured for:

- ✅ Improved scalability
- ✅ Loose coupling and modular design
- ✅ Real-world DevOps and cloud-native deployment experience

---

## 🤝 Contributing

This project is for **personal development**, but feel free to:

- ⭐ Star or Fork the repo
- 🐞 Open an issue or suggestion
- 🚀 Share ideas for extending it (e.g., authentication, reporting dashboard)

---

## 📄 License

MIT License
